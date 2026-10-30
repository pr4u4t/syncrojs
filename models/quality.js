const db = require('../db');
//const config = require('config');
const logger = require('../utils/logger');
const cache = require('../utils/cache');

const quality_all = async function(){
    const query = 'SELECT * FROM syncro_db_3892.quality';
    const key = 'quality.all';
    
    value = cache.get(key);
    if ( value !== undefined ){
        logger.info('quality_all: cache hit');
    	return value;
    }
    logger.info('quality_all: cache miss');
    try{
        const {rows,fields} = await db.query(query);
        cache.set(key, {rows,fields});
        return {rows,fields};
    }catch(error){
        logger.error('Failed to obtain quality',error);
        return null;
    }
}

const quality_params_by_id = async function(id){
    // Case-insensitive; an exact match wins if IDs differ only by case. The ID comes from uploaded files.
    const query = 'SELECT qualityid, qualitytext, weightfactoruwg_m, thickness_uddmm, scoregap_uddmm FROM syncro_db_3892.quality WHERE lower(qualityid) = lower($1) ORDER BY qualityid = $1 DESC, qualityid';
    const key = 'quality.by_id.'+id;
    
    value = cache.get(key);
    if ( value !== undefined ){
        logger.info('quality_params_by_id: cache hit');
        return value;
    }
    logger.info('quality_params_by_id: cache miss');
    try{
        const {rows,fields} = await db.query(query, [id]);
        cache.set(key, {rows,fields});
        return {rows,fields};
    }catch(error){
        logger.error('Failed to find quality by id',error);
        return null;
    }
}

const quality_paginated_all = async function(offset){
    const query = 'SELECT * FROM syncro_db_3892.quality OFFSET '+offset+' LIMIT 25';
    const key = 'quality.offset.'+offset;
    
    value = cache.get(key);
    if ( value !== undefined ){
        logger.info('quality_paginated_all: cache hit');
        return value;
    }
    logger.info('quality_paginated_all: cache miss');
    try{
        const {rows,fields} = await db.query(query);
        cache.set(key, {rows,fields});
        return {rows,fields};
    }catch(error){
        logger.error('Failed to obtain quality paginated',error);
        return null;
    }
}

const quality_count = async function(){
    const query = 'SELECT count(*) FROM syncro_db_3892.quality';
    const key = 'quality.count';
    
    value = cache.get(key);
    if ( value !== undefined ){
        logger.info('quality_count: cache hit');
        return value;
    }
    logger.info('quality_count: cache miss');
    try{
        let count = await db.query(query);
        cache.set(key, count);
        return count;
    }catch(error){
        logger.error('Failed to obtain quality count');
        return null;
    }
}

module.exports = {
    quality_all,
    quality_params_by_id,
    quality_count,
    quality_paginated_all
}
