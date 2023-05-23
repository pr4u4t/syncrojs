const db = require('../db')
const log = require('../utils/log.js')

const flute_all = async function(){
    const query = 'SELECT * FROM syncro_db_3892.flute'
    try{
        const {rows,fields} = await db.query(query)
        return {rows,fields}
    }catch(error){
        log.error('Failed to obtain flute list',error)
        return null
    }
}

const flute_params_by_id = async function(id){
    const query = 'SELECT * FROM syncro_db_3892.flute WHERE flute= \''+id+'\''
    try{
        const {rows,fields} = await db.query(query)
        return {rows,fields}
    }catch(error){
        log.error('Failed to obtain flute parameters', error)
        return null
    }
}

const flute_paginated_all = async function(offset){
    const query = 'SELECT * FROM syncro_db_3892.flute OFFSET '+offset+' LIMIT 25'
    try{
        const {rows,fields} = await db.query(query)
        return {rows,fields}
    }catch(error){
        log.error('Failed to obtain flute paginated',error)
        return null
    }
}

const flute_count = async function(){
    const query = 'SELECT count(*) FROM syncro_db_3892.flute'
    try{
        let count = await db.query(query)
        return count
    }catch(error){
        log.error('Failed to get flute count')
        return null
    }
}

module.exports = {
    flute_all,
    flute_params_by_id,
    flute_count,
    flute_paginated_all
}
