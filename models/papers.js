const db = require('../db')
const log = require('../utils/log.js')
const cache = new NodeCache({ stdTTL: 100, checkperiod: 120 })

const papers_all = async function(){
    const query = 'SELECT * FROM syncro_db_3892.papers'
    const key = 'papers.all'
    
    value = cache.get(key)
    if ( value !== undefined ){
        return value
    }
    
    try{
        const {rows,fields} = await db.query(query)
        cache.set(key, {rows,fields})
        return {rows,fields}
    }catch(error){
        log.error('Failed to obtain papers',error)
        return null
    }
}

const papers_params_by_id = async function(id){
    const query = 'SELECT qualitytext, weightfactoruwg_m, thickness_uddmm, scoregap_uddmm FROM syncro_db_3892.papers WHERE paperid = \''+id+'\''
    
    const key = 'papers.by_id.'+id
    
    value = cache.get(key)
    if ( value !== undefined ){
        return value
    }
    
    try{
        const {rows,fields} = await db.query(query)
        cache.set(key, {rows,fields})
        return {rows,fields}
    }catch(error){
        log.error('Failed to obtain paper by id',error)
        return null
    }
}

const papers_paginated_all = async function(offset){
    const query = 'SELECT * FROM syncro_db_3892.papers OFFSET '+offset+' LIMIT 25'
    
    const key = 'papers.offset.'+offset
    
    value = cache.get(key)
    if ( value !== undefined ){
        return value
    }
    
    try{
        const {rows,fields} = await db.query(query)
        cache.set(key, {rows,fields})
        return {rows,fields}
    }catch(error){
        log.error('failed to paginate papers',error)
        return null
    }
}

const papers_count = async function(){
    const query = 'SELECT count(*) FROM syncro_db_3892.papers'
    
    const key = 'papers.count'
    
    value = cache.get(key)
    if ( value !== undefined ){
        return value
    }
    
    try{
        let count = await db.query(query)
        cache.set(key, count)
        return count
    }catch(error){
        log.error('Failed to count papers',error)
        return null
    }
}

module.exports = {
    papers_all,
    papers_params_by_id,
    papers_count,
    papers_paginated_all
}
