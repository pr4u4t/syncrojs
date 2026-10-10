const fs = require('node:fs/promises');
const path = require('node:path');
const { Readable } = require('node:stream');
const { pipeline } = require('node:stream/promises');
const copyFrom = require('pg-copy-streams').from;
const db = require('./index');
const logger = require('../utils/logger');

const schemaName = 'syncro_db_3892';
const lockId = 3892030;
const schemaFile = path.join(__dirname, '..', 'data', '10', 'schema.sql');
const backupFile = path.join(__dirname, '..', 'data', '10', 'backup.sql');

const parseTables = function(schema){
    const lines = schema.split(/\r?\n/);
    const tables = [];

    for(let index = 0; index < lines.length; index += 1){
        const match = lines[index].match(/^CREATE TABLE (syncro_db_3892\.[a-zA-Z0-9_]+) \($/);
        if(!match){
            continue;
        }

        let end = index + 1;
        while(end < lines.length && lines[end] !== ');'){
            end += 1;
        }
        if(end === lines.length){
            throw new Error(`Unterminated table definition for ${match[1]}`);
        }

        tables.push({
            name: match[1],
            sql: `${lines.slice(index, end + 1).join('\n').replace('CREATE TABLE ', 'CREATE TABLE IF NOT EXISTS ')};`
        });
        index = end;
    }

    return tables;
};

const parseCopyBlocks = function(backup, tableNames){
    const lines = backup.split(/\r?\n/);
    const blocks = [];

    for(let index = 0; index < lines.length; index += 1){
        const header = lines[index];
        const match = header.match(/^COPY (syncro_db_3892\.[a-zA-Z0-9_]+) \(.+\) FROM stdin;$/);
        if(!match){
            continue;
        }
        if(!tableNames.has(match[1])){
            throw new Error(`Backup contains an unknown table: ${match[1]}`);
        }

        const data = [];
        let end = index + 1;
        while(end < lines.length && lines[end] !== '\\.'){
            data.push(lines[end]);
            end += 1;
        }
        if(end === lines.length){
            throw new Error(`Unterminated COPY block for ${match[1]}`);
        }

        blocks.push({
            table: match[1],
            query: header.replace(/ FROM stdin;$/, ' FROM STDIN'),
            data
        });
        index = end;
    }

    return blocks;
};

const parseConstraints = function(schema, tableNames){
    const expression = /ALTER TABLE ONLY (syncro_db_3892\.[a-zA-Z0-9_]+)\s+ADD CONSTRAINT ([a-zA-Z0-9_]+) (PRIMARY KEY \([^)]+\));/g;
    const constraints = [];
    let match;

    while((match = expression.exec(schema)) !== null){
        if(!tableNames.has(match[1])){
            throw new Error(`Constraint references an unknown table: ${match[1]}`);
        }
        constraints.push({
            table: match[1],
            name: match[2],
            sql: `ALTER TABLE ONLY ${match[1]} ADD CONSTRAINT ${match[2]} ${match[3]}`
        });
    }

    return constraints;
};

const restoreEmptyTables = async function(client, blocks){
    for(const block of blocks){
        const { rows } = await client.query(`SELECT EXISTS (SELECT 1 FROM ${block.table} LIMIT 1) AS has_rows`);
        if(rows[0].has_rows || block.data.length === 0){
            continue;
        }

        await client.query('BEGIN');
        try{
            const copyStream = client.query(copyFrom(block.query));
            await pipeline(Readable.from(block.data.map((line) => `${line}\n`)), copyStream);
            await client.query('COMMIT');
            logger.info({table: block.table, rowCount: block.data.length}, 'Restored empty database table');
        }catch(error){
            await client.query('ROLLBACK');
            throw error;
        }
    }
};

const applyConstraints = async function(client, constraints){
    for(const constraint of constraints){
        const { rows } = await client.query(
            'SELECT 1 FROM pg_constraint WHERE conrelid = $1::regclass AND conname = $2',
            [constraint.table, constraint.name]
        );
        if(rows.length > 0){
            continue;
        }

        await client.query(constraint.sql);
    }
};

const migrate = async function(){
    const [schema, backup] = await Promise.all([
        fs.readFile(schemaFile, 'utf8'),
        fs.readFile(backupFile, 'utf8')
    ]);
    const tables = parseTables(schema);
    const tableNames = new Set(tables.map((table) => table.name));
    const blocks = parseCopyBlocks(backup, tableNames);
    const constraints = parseConstraints(schema, tableNames);
    const copyTables = new Set(blocks.map((block) => block.table));
    const missingDataTables = [...tableNames].filter((table) => !copyTables.has(table));
    if(tables.length === 0 || blocks.length === 0 || missingDataTables.length > 0){
        throw new Error(`Database dump is incomplete; missing COPY data for: ${missingDataTables.join(', ')}`);
    }

    const client = await db.connect();
    try{
        await client.query('SELECT pg_advisory_lock($1)', [lockId]);
        await client.query(`CREATE SCHEMA IF NOT EXISTS ${schemaName}`);
        for(const table of tables){
            await client.query(table.sql);
        }
        await restoreEmptyTables(client, blocks);
        await applyConstraints(client, constraints);
    }finally{
        try{
            await client.query('SELECT pg_advisory_unlock($1)', [lockId]);
        }finally{
            client.release();
        }
    }
};

module.exports = {
    migrate,
    parseTables,
    parseCopyBlocks,
    parseConstraints
};