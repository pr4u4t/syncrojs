const db = require('../db')
const log = require('../utils/log.js')
const NodeCache = require( "node-cache" );
const cache = new NodeCache({ stdTTL: 100, checkperiod: 120 })

const quality_all = async function(){
    const query = 'SELECT * FROM syncro_db_3892.quality'
    const key = 'quality.all'
    
    value = cache.get(key)
    if ( value !== undefined ){
    	return value
    }

    try{
        const {rows,fields} = await db.query(query)
        cache.set(key, {rows,fields})
        return {rows,fields}
    }catch(error){
        log.error('Failed to obtain quality',error)
        return null
    }
}

const quality_params_by_id = async function(id){
    const query = 'SELECT qualitytext, weightfactoruwg_m, thickness_uddmm, scoregap_uddmm FROM syncro_db_3892.quality WHERE qualityid = \''+id+'\''
    const key = 'quality.by_id.'+id
    
    value = cache.get(key)
    if ( value !== undefined ){
        return value
    }

    try{
        const {rows,fields} = await db.query(query)
        cache.set(key, {rows,fields})
        return {rows,fields}
    }catch(error){
        log.error('Failed to find quality by id',error)
        return null
    }
}

const quality_paginated_all = async function(offset){
    const query = 'SELECT * FROM syncro_db_3892.quality OFFSET '+offset+' LIMIT 25'
    const key = 'quality.offset.'+offset
    
    value = cache.get(key)
    if ( value !== undefined ){
        return value
    }
    
    try{
        const {rows,fields} = await db.query(query)
        cache.set(key, {rows,fields})
        return {rows,fields}
    }catch(error){
        log.error('Failed to obtain quality paginated',error)
        return null
    }
}

const quality_count = async function(){
    const query = 'SELECT count(*) FROM syncro_db_3892.quality'
    const key = 'quality.count'
    
    value = cache.get(key)
    if ( value !== undefined ){
        return value
    }
    
    try{
        let count = await db.query(query)
        cache.set(key, count)
        return count
    }catch(error){
        log.error('Failed to obtain quality count')
        return null
    }
}

module.exports = {
    quality_all,
    quality_params_by_id,
    quality_count,
    quality_paginated_all
}
