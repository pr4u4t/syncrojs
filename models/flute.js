const db = require('../db');
const NodeCache = require("node-cache");
const config = require('config');
const logger = require('../utils/logger');
const ttl = config.get("cache.ttl") || 100;
const period = config.get("cache.period") || 120;
const cache = new NodeCache({ stdTTL: ttl, checkperiod: period });

const flute_all = async function(){
    const query = 'SELECT * FROM syncro_db_3892.flute';
    const key = 'flute.all';
    
    value = cache.get(key);
    if ( value !== undefined ){
        logger.info('flute_all: cache hit');
        return value;
    }
    logger.info('flute_all: cache miss');
    try{
        const {rows,fields} = await db.query(query);
        cache.set(key, {rows,fields});
        return {rows,fields};
    }catch(error){
        logger.error('Failed to obtain flute list',error);
        return null;
    }
}

const flute_params_by_id = async function(id){
    const query = 'SELECT * FROM syncro_db_3892.flute WHERE flute= \''+id+'\'';
    const key = 'flute.by_id.'+id;
    
    value = cache.get(key);
    if ( value !== undefined ){
        logger.info('flute_params_by_id: cache hit');
        return value;
    }
    logger.info('flute_params_by_id: cache miss');
    try{
        const {rows,fields} = await db.query(query);
        cache.set(key, {rows,fields});
        return {rows,fields};
    }catch(error){
        logger.error('Failed to obtain flute parameters', error);
        return null;
    }
}

const flute_paginated_all = async function(offset){
    const query = 'SELECT * FROM syncro_db_3892.flute OFFSET '+offset+' LIMIT 25';
    const key = 'flute.offset.'+offset;
    
    value = cache.get(key);
    if ( value !== undefined ){
        logger.info('flute_paginated_all: cache hit');
        return value;
    }
    logger.info('flute_paginated_all: cache miss');
    try{
        const {rows,fields} = await db.query(query);
        cache.set(key, {rows,fields});
        return {rows,fields};
    }catch(error){
        logger.error('Failed to obtain flute paginated',error);
        return null;
    }
}

const flute_count = async function(){
    const query = 'SELECT count(*) FROM syncro_db_3892.flute';
    const key = 'flute.count';
    
    value = cache.get(key);
    if ( value !== undefined ){
        logger.info('flute_count: cache hit');
        return value;
    }
    logger.info('flute_count: cache miss');
    try{
        let count = await db.query(query);
        cache.set(key, count);
        return count;
    }catch(error){
        logger.error('Failed to get flute count');
        return null;
    }
}

module.exports = {
    flute_all,
    flute_params_by_id,
    flute_count,
    flute_paginated_all
}
