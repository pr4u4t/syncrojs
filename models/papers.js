const db = require('../db')
const log = require('../utils/log.js')

const papers_all = async function(){
    try{
        const {rows,fields} = await db.query('SELECT * FROM syncro_db_3892.papers')
        return {rows,fields}
    }catch(error){
        log.error('Failed to obtain papers',error)
        return null
    }
}

const papers_params_by_id = async function(id){
    try{
        const {rows,fields} = await db.query('SELECT qualitytext, weightfactoruwg_m, thickness_uddmm, scoregap_uddmm FROM syncro_db_3892.papers WHERE paperid = \''+id+'\'')
        return {rows,fields}
    }catch(error){
        log.error('Failed to obtain paper by id',error)
        return null
    }
}

const papers_paginated_all = async function(offset){
    try{
        const {rows,fields} = await db.query('SELECT * FROM syncro_db_3892.papers OFFSET '+offset+' LIMIT 25')
        return {rows,fields}
    }catch(error){
        log.error('failed to paginate papers',error)
        return null
    }
}

const papers_count = async function(){
    const query = 'SELECT count(*) FROM syncro_db_3892.papers'
    try{
        let count = await db.query(query)
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
