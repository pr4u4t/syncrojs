const db = require('../db');
const logger = require('pino')();
const NodeCache = require( "node-cache" );
const {csv_to_xlsx} = require('../utils/convert.js');

const cache = new NodeCache({ stdTTL: 100, checkperiod: 120 });
const csvquery = "select to_char(finishtime, 'YYYY-MM-DD HH24:MI') as finishtime, cutsproduced0*outs0 as produced0, ordernumber0, customername0, outs0, sheetwidth0/10 as width0, sheetlength0/10 as length0, scoremeasures0, cutsproduced1*outs1 as produced1, ordernumber1, customername1, outs1, sheetwidth1/10 as width1, sheetlength1/10 as length1, scoremeasures1, cutsproduced2*outs2 as produced2, ordernumber2, customername2, outs2, sheetwidth2/10 as width2, sheetlength2/10 as length2, scoremeasures2 from syncro_db_3892.pastorders ";

const past_all = async function(){
    const query = csvquery + 'order by finishtime desc LIMIT 25';
    const key = 'past.all';
    
    value = cache.get(key);
    if ( value !== undefined ){
        logger.info('past_all: cache hit');
        return value;
    }
    logger.info('past_all: cache miss');
    try{
        const {rows,fields} = await db.query(query);
        cache.set(key, {rows,fields});
        return {rows,fields};
    }catch(error){
        logger.error('Failed to obtain past orders',error);
        return null;
    }
}

const past_count = async function() {
    const query = 'SELECT count(*) FROM syncro_db_3892.pastorders';
    const key = 'past.count';
    
    value = cache.get(key);
    if ( value !== undefined ){
        logger.info('past_count: cache hit');
        return value;
    }
    logger.info('past_count: cache miss');
    try{
        let count = await db.query(query);
        cache.set(key, count);
        return count;
    }catch(error){
        logger.error('Failed to obtain past orders',error);
        return null;
    }
}

const past_paginated_all = async function(offset){
    const query = csvquery + 'order by finishtime desc OFFSET '+offset+' LIMIT 25';
    const key = 'past.offset.'+offset;
    
    value = cache.get(key);
    if ( value !== undefined ){
        logger.info('past_paginated_all: cache hit');
        return value;
    }
    logger.info('past_paginated_all: cache miss');
    try{
        const {rows,fields} = await db.query(query);
        cache.set(key, {rows,fields});
        return {rows,fields};
    }catch(error){
        logger.error('Failed to obtain past orders paginated',error);
        return null
    }
}

const past_export = async function(today){
    const query = csvquery + "WHERE to_char(finishtime, 'YYYY-MM-DD') = '"+today+"' ORDER BY finishtime DESC";
    
    try{
        const {rows,fields} = await db.query(query);
        return {rows,fields};
    }catch(error){
        logger.error('Failed to obtain past orders in csv layout',error);
        return null;
    }
}

const past_export_csv = async function(today){
    const query = csvquery + "WHERE to_char(finishtime, 'YYYY-MM-DD') = '"+today+"' ORDER BY finishtime DESC";
    
    try{
        const {rows,fields} = await db.query(query);
        var ret = '';
        
        for(let field of fields){
            ret += field.name+',';
        }	
        
        ret += '\n';
        
        for (let row of rows){
            for(const property in row){
                ret += `${row[property]}`+',';
            }
            ret += '\n';
        }
        
        return ret;
    }catch(error){
        logger.error('Failed to obtain past orders in csv layout',error);
        return null;
    }
}

const past_export_xlsx = async function(today){
    return csv_to_xlsx(await past_export_csv(today));
}

module.exports = {
    past_all,
    past_paginated_all,
    past_count,
    past_export_csv,
    past_export_xlsx,
    past_export
}
