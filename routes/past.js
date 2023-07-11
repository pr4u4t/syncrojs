const Router = require('express-promise-router')
const template = require('../templates')
const router = new Router()
const past = require('../models/past_orders.js')

module.exports = router

router.get('/', async (req,res) => {
    const {rows,fields} = await past.past_all()
    const result = await past.past_count()
    
    template.render('past.twig', {
        'title': "Past Order List",
        'header_title': "Past Order List",
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
    const {rows,fields} = await past.past_paginated_all(offset)
    const result = await past.past_count()
    
    template.render('past.twig', {
        'title': "Past Order List",
        'header_title': "Past Order List",
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
    const {rows,fields} = await past.past_export()
    
    let ts = Date.now();
    
    let date_ob = new Date(ts);
    let date = date_ob.getDate() < 10 ? '0'+date_ob.getDate() : date_ob.getDate();
    let month = date_ob.getMonth() + 1
    month = month < 10 ? '0'+month : month
    let year = date_ob.getFullYear();
    let today = year + "-" + month + "-" + date
    
    template.render('past_export.twig', {
        'rows': rows, 
        'fields': fields, 
        'url': '/past/export',
        'today': today
    }).then((output) => {
        res.end(output)
    })
})

router.post('/export.csv', async (req,res) => {
    res.set('Content-Type', 'application/octet-stream');
    res.send(await past.past_export_csv())
})

router.post('/export.xlsx', async (req,res) => {
    res.set('Content-Type', 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet');
    res.send(await past.past_export_xlsx())
})
