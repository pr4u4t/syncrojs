const past_all = async function(){
    const {rows,fields} = await db.query('SELECT begintime, finishtime, cutsproduced0, cutsproduced1, cutsproduced2, customerorderid0, sheetlength0, sheetwidth0, outs0, scoremeasures0, customerorderid1, sheetlength1, sheetwidth1, outs1, customername2, outs2, scoremeasures2, flute  FROM syncro_db_3892.pastorders ORDER BY begintime DESC LIMIT 25')

    return {rows,fields}
}

const past_count = async function() {
    let count = await db.query('SELECT count(*) FROM syncro_db_3892.pastorders')
    
    return count
}

const past_paginated_all = async function(offset){
    const {rows,fields} = db.query('SELECT begintime, finishtime, cutsproduced0, cutsproduced1, cutsproduced2, customerorderid0, sheetlength0, sheetwidth0, outs0, scoremeasures0, customerorderid1, sheetlength1, sheetwidth1, outs1, customername2, outs2, scoremeasures2, flute FROM syncro_db_3892.pastorders ORDER BY begintime DESC OFFSET '+offset+' LIMIT 25')
    
    return {rows,fields}
}

const past_export_csv = async function(){
    const {rows,fields} = await db.query('SELECT begintime, finishtime, cutsproduced0, cutsproduced1, cutsproduced2, customerorderid0, sheetlength0, sheetwidth0, outs0, scoremeasures0, customerorderid1, sheetlength1, sheetwidth1, outs1, customername2, outs2, scoremeasures2, flute FROM syncro_db_3892.pastorders WHERE begintime >= now()::date ORDER BY begintime DESC')
    
    return {rows,fields}
}

module.exports = {
    past_all,
    past_paginated_all,
    past_count,
    past_export_csv
}
