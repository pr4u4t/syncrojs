const Router = require('express-promise-router')
const template = require('../templates')
const router = new Router()
const quality = require('../models/quality.js')

module.exports = router

router.get('/', async (req,res) => {
    const {rows,fields} = await quality.quality_all()
    const result = await quality.quality_count()
    
    template.render('quality.twig', {
        'title': "Quality List",
        'header_title': "Flute List",
        'rows': rows, 
        'fields': fields, 
        'total': Math.ceil(result.rows[0].count/25),
        'current': 1,
        'url': '/quality/'        
    }).then((output) => {
        res.end(output)
    })
})

router.get('/:page([0-9]+)', async (req,res) => {
    const current = req.params.page
    const offset = (req.params.page-1)*25
    const {rows,fields} = await quality.quality_paginated_all(offset)
    const result = await quality.quality_count()
    
    template.render('quality.twig', {
        'title': "Quality List",
        'header_title': "Flute List",
        'rows': rows, 
        'fields': fields, 
        'total': Math.ceil(result.rows[0].count/25), 
        'current': current,
        'url': '/quality/'
    }).then((output) => {
        res.end(output)
    })
})
