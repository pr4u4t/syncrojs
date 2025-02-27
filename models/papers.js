const db = require('../db');
const NodeCache = require("node-cache");
const logger = require('pino')();
const cache = new NodeCache({ stdTTL: 100, checkperiod: 120 });

const papers_all = async function(){
    const query = 'SELECT * FROM syncro_db_3892.papers';
    const key = 'papers.all';
    
    value = cache.get(key);
    if ( value !== undefined ){
        logger.info('papers_all: cache hit');
        return value;
    }
    logger.info('papers_all: cache miss');
    try{
        const {rows,fields} = await db.query(query);
        cache.set(key, {rows,fields});
        return {rows,fields};
    }catch(error){
        logger.error('Failed to obtain papers',error);
        return null;
    }
}

const papers_params_by_id = async function(id){
    const query = 'SELECT qualitytext, weightfactoruwg_m, thickness_uddmm, scoregap_uddmm FROM syncro_db_3892.papers WHERE paperid = \''+id+'\'';
    const key = 'papers.by_id.'+id;
    
    value = cache.get(key);
    if ( value !== undefined ){
        logger.info('papers_params_by_id: cache hit');
        return value;
    }
    logger.info('papers_params_by_id: cache miss');
    try{
        const {rows,fields} = await db.query(query);
        cache.set(key, {rows,fields});
        return {rows,fields};
    }catch(error){
        logger.error('Failed to obtain paper by id',error);
        return null;
    }
}

const papers_paginated_all = async function(offset){
    const query = 'SELECT * FROM syncro_db_3892.papers OFFSET '+offset+' LIMIT 25';
    const key = 'papers.offset.'+offset;
    
    value = cache.get(key);
    if ( value !== undefined ){
        logger.info('papers_paginated_all: cache hit');
        return value;
    }
    logger.info('papers_paginated_all: cache miss');
    try{
        const {rows,fields} = await db.query(query);
        cache.set(key, {rows,fields});
        return {rows,fields};
    }catch(error){
        logger.error('failed to paginate papers',error);
        return null;
    }
}

const papers_count = async function(){
    const query = 'SELECT count(*) FROM syncro_db_3892.papers';
    const key = 'papers.count';
    
    value = cache.get(key);
    if ( value !== undefined ){
        logger.info('papers_count: cache hit');
        return value;
    }
    logger.info('papers_count: cache miss');
    try{
        let count = await db.query(query);
        cache.set(key, count);
        return count;
    }catch(error){
        logger.error('Failed to count papers',error);
        return null;
    }
}

module.exports = {
    papers_all,
    papers_params_by_id,
    papers_count,
    papers_paginated_all
}
