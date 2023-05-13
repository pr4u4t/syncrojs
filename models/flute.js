const db = require('../db')

const flute_all = async function(){
    const {rows,fields} = await db.query('SELECT * FROM syncro_db_3892.flute')
    return {rows,fields}
}

const flute_params_by_id = async function(id){
    const {rows,fields} = await db.query('SELECT * FROM syncro_db_3892.flute WHERE flute= \''+id+'\'')
    return {rows,fields}
}

const flute_paginated_all = async function(offset){
    const {rows,fields} = await db.query('SELECT * FROM syncro_db_3892.flute OFFSET '+offset+' LIMIT 25')
    
    return {rows,fields}
}

const flute_count = async function(){
    let count = await db.query('SELECT count(*) FROM syncro_db_3892.flute')
    
    return count
}

module.exports = {
    flute_all,
    flute_params_by_id,
    flute_count,
    flute_paginated_all
}
