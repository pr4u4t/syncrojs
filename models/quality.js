const db = require('../db')
const log = require('../utils/log.js')

const quality_all = async function(){
    try{
        const {rows,fields} = await db.query('SELECT * FROM syncro_db_3892.quality')
        return {rows,fields}
    }catch(error){
        log.error('Failed to obtain quality',error)
        return null
    }
}

const quality_params_by_id = async function(id){
    try{
        const {rows,fields} = await db.query('SELECT qualitytext, weightfactoruwg_m, thickness_uddmm, scoregap_uddmm FROM syncro_db_3892.quality WHERE qualityid = \''+id+'\'')
        return {rows,fields}
    }catch(error){
        log.error('Failed to find quality by id',error)
        return null
    }
}

const quality_paginated_all = async function(offset){
    try{
        const {rows,fields} = await db.query('SELECT * FROM syncro_db_3892.quality OFFSET '+offset+' LIMIT 25')
        return {rows,fields}
    }catch(error){
        log.error('Failed to obtain quality paginated',error)
        return null
    }
}

const quality_count = async function(){
    try{
        let count = await db.query('SELECT count(*) FROM syncro_db_3892.quality')
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
