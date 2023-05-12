const db = require('../db')

const quality_all = async function(){
    const {rows,fields} = await db.query('SELECT * FROM syncro_db_3892.quality')
    return {rows,fields}
}

const quality_params_by_id = async function(id){
    const {rows,fields} = await db.query('SELECT qualitytext, weightfactoruwg_m, thickness_uddmm, scoregap_uddmm FROM syncro_db_3892.quality WHERE qualityid = \''+id+'\'')
    return {rows,fields}
}

const quality_paginated_all = async function(offset){
    const {rows,fields} = await db.query('SELECT * FROM syncro_db_3892.quality OFFSET '+offset+' LIMIT 25')
    
    return {rows,fields}
}

const quality_count = async function(){
    let count = await db.query('SELECT count(*) FROM syncro_db_3892.quality')
    
    return count
}

module.exports = {
    quality_all,
    quality_params_by_id,
    quality_count,
    quality_paginated_all
}
