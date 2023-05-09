const Router = require('express-promise-router')
const db = require('../db')
const template = require('../templates')
const router = new Router()

module.exports = router

router.get('/', async (req,res) => {
    const {rows,fields} = await db.query('SELECT begintime, finishtime, cutsproduced0, cutsproduced1, cutsproduced2, customerorderid0, sheetlength0, sheetwidth0, outs0, scoremeasures0, customerorderid1, sheetlength1, sheetwidth1, outs1, customername2, outs2, scoremeasures2, flute  FROM syncro_db_3892.pastorders LIMIT 25 ORDER BY begintime DESC')
    const result = await db.query('SELECT count(*) FROM syncro_db_3892.pastorders')
    template.render('past.twig', {
        'rows': rows, 
        'fields': fields, 
        'total': Math.ceil(result.rows[0].count/25),
        'current': 1,
        'url': '/past/'        
    }).then((output) => {
        res.end(output)
    })
})

router.get('/:page([0-9]+)', async (req,res) => {
    const current = req.params.page
    const offset = (req.params.page-1)*25
    const {rows,fields} = await db.query('SELECT begintime, finishtime, cutsproduced0, cutsproduced1, cutsproduced2, customerorderid0, sheetlength0, sheetwidth0, outs0, scoremeasures0, customerorderid1, sheetlength1, sheetwidth1, outs1, customername2, outs2, scoremeasures2, flute FROM syncro_db_3892.pastorders OFFSET '+offset+' LIMIT 25 ORDER BY begintime DESC')
    const result = await db.query('SELECT count(*) FROM syncro_db_3892.pastorders')
    template.render('past.twig', {
        'rows': rows, 
        'fields': fields, 
        'total': Math.ceil(result.rows[0].count/25), 
        'current': current,
        'url': '/past/'
    }).then((output) => {
        res.end(output)
    })
})

router.get('/export', async (req,res) => {
    const {rows,fields} = await db.query('SELECT begintime, finishtime, cutsproduced0, cutsproduced1, cutsproduced2, customerorderid0, sheetlength0, sheetwidth0, outs0, scoremeasures0, customerorderid1, sheetlength1, sheetwidth1, outs1, customername2, outs2, scoremeasures2, flute FROM syncro_db_3892.pastorders WHERE begintime >= now()::date ORDER BY begintime DESC')
    template.render('past_export.twig', {
        'rows': rows, 
        'fields': fields, 
        'url': '/past/export'
    }).then((output) => {
        res.end(output)
    })
})

router.post('/export.csv', async (req,res) => {
    const {rows,fields} = await db.query('SELECT begintime, finishtime, cutsproduced0, cutsproduced1, cutsproduced2, customerorderid0, sheetlength0, sheetwidth0, outs0, scoremeasures0, customerorderid1, sheetlength1, sheetwidth1, outs1, customername2, outs2, scoremeasures2, flute FROM syncro_db_3892.pastorders WHERE begintime >= now()::date ORDER BY begintime DESC')
    res.set('Content-Type', 'application/octet-stream');
    var ret = '';
    for (row in rows){
        for(field in row){
            ret += field+','
        }
        ret += '\n'
    }
    res.send(ret)
})
