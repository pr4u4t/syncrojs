const Router = require('express-promise-router')
const template = require('../templates')
const router = new Router()
const past = require('../models/papers.js')

module.exports = router

router.get('/', async (req,res) => {
    const {rows,fields} = await past.past_all()
    const result = await past.past_count()
    
    template.render('papers.twig', {
        'rows': rows, 
        'fields': fields, 
        'total': Math.ceil(result.rows[0].count/25),
        'current': 1,
        'url': '/papers/'        
    }).then((output) => {
        res.end(output)
    })
})

router.get('/:page([0-9]+)', async (req,res) => {
    const current = req.params.page
    const offset = (req.params.page-1)*25
    const {rows,fields} = await past.past_paginated_all(offset)
    const result = await past.past_count()
    
    template.render('papers.twig', {
        'rows': rows, 
        'fields': fields, 
        'total': Math.ceil(result.rows[0].count/25), 
        'current': current,
        'url': '/papers/'
    }).then((output) => {
        res.end(output)
    })
})
