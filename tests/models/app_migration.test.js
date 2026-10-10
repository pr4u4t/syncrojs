const fs = require('node:fs');
const path = require('node:path');
const {
    parseTables,
    parseCopyBlocks,
    parseConstraints
} = require('../../db/migrate');

const schema = fs.readFileSync(path.join(__dirname, '../../data/10/schema.sql'), 'utf8');
const backup = fs.readFileSync(path.join(__dirname, '../../data/10/backup.sql'), 'utf8');
const tables = parseTables(schema);
const tableNames = new Set(tables.map((table) => table.name));
const copyBlocks = parseCopyBlocks(backup, tableNames);
const constraints = parseConstraints(schema, tableNames);

test('database migration covers every table and COPY data block', () => {
    expect(tables.length).toBeGreaterThan(0);
    expect(copyBlocks.map((block) => block.table).sort()).toEqual([...tableNames].sort());
    expect(tables.every((table) => table.sql.startsWith('CREATE TABLE IF NOT EXISTS'))).toBe(true);
});

test('database migration retains large COPY blocks and primary keys', () => {
    const pastOrders = copyBlocks.find((block) => block.table === 'syncro_db_3892.pastorders');

    expect(pastOrders.data.length).toBeGreaterThan(1000);
    expect(constraints.length).toBeGreaterThan(0);
    expect(constraints.every((constraint) => tableNames.has(constraint.table))).toBe(true);
});

test('database migration rejects incomplete COPY blocks', () => {
    const incompleteBackup = 'COPY syncro_db_3892.papers (paperid) FROM stdin;\nPAPER-1';

    expect(() => parseCopyBlocks(incompleteBackup, new Set(['syncro_db_3892.papers'])))
        .toThrow('Unterminated COPY block');
});