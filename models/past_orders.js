const db = require('../db')
const log = require('../utils/log.js')
const NodeCache = require( "node-cache" )

const cache = new NodeCache({ stdTTL: 100, checkperiod: 120 })

const past_all = async function(){
    const query = 'SELECT begintime, finishtime, cutsproduced0, cutsproduced1, cutsproduced2, customerorderid0, sheetlength0, sheetwidth0, outs0, scoremeasures0, customerorderid1, sheetlength1, sheetwidth1, outs1, customername2, outs2, scoremeasures2, flute  FROM syncro_db_3892.pastorders ORDER BY begintime DESC LIMIT 25'

    const key = 'past.all'
    
    value = cache.get(key)
    if ( value !== undefined ){
        return value
    }
    
    try{
        const {rows,fields} = await db.query(query)
        cache.set(key, {rows,fields})
        return {rows,fields}
    }catch(error){
        log.error('Failed to obtain past orders',error)
        return null
    }
}

const past_count = async function() {
    const query = 'SELECT count(*) FROM syncro_db_3892.pastorders'
    
    const key = 'past.count'
    
    value = cache.get(key)
    if ( value !== undefined ){
        return value
    }
    
    try{
        let count = await db.query(query)
        cache.set(key, count)
        return count
    }catch(error){
        log.error('Failed to obtain past orders',error)
        return null
    }
}

const past_paginated_all = async function(offset){
    const query = 'SELECT begintime, finishtime, cutsproduced0, cutsproduced1, cutsproduced2, customerorderid0, sheetlength0, sheetwidth0, outs0, scoremeasures0, customerorderid1, sheetlength1, sheetwidth1, outs1, customername2, outs2, scoremeasures2, flute FROM syncro_db_3892.pastorders ORDER BY begintime DESC OFFSET '+offset+' LIMIT 25'
    
    const key = 'past.offset.'+offset
    
    value = cache.get(key)
    if ( value !== undefined ){
        return value
    }
    
    try{
        const {rows,fields} = await db.query(query)
        cache.set(key, {rows,fields})
        return {rows,fields}
    }catch(error){
        log.error('Failed to obtain past orders paginated',error)
        return null
    }
}

const past_export_csv = async function(){
    const query = 'SELECT begintime, finishtime, ordernumber0,cutsproduced0, ordernumber1, cutsproduced1, cutsproduced2, customerorderid0, sheetlength0, sheetwidth0, outs0, scoremeasures0, customerorderid1, sheetlength1, sheetwidth1, outs1, ordernumber2, customername2, outs2, scoremeasures2, flute FROM syncro_db_3892.pastorders WHERE begintime >= now()::date ORDER BY begintime DESC'
    
    try{
        const {rows,fields} = await db.query(query)
        return {rows,fields}
    }catch(error){
        log.error('Failed to obtain past orders in csv layout',error)
        return null
    }
}

module.exports = {
    past_all,
    past_paginated_all,
    past_count,
    past_export_csv
}
