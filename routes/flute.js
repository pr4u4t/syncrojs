const Router = require('express-promise-router');
const template = require('../templates');
const router = new Router();
const flute = require('../models/flute.js');
const config = require('config');
const loglevel = config.get("log.level") || "info";
const logger = require('pino')({"level": loglevel});

module.exports = router;

router.get('/', async (req,res) => {
    const {rows,fields} = await flute.flute_all();
    const result = await flute.flute_count();
    
    template.render('flute.twig', {
        'title': "Flute List",
        'header_title': "Flute List",
        'rows': rows, 
        'fields': fields, 
        'total': Math.ceil(result.rows[0].count/25),
        'current': 1,
        'url': '/flute/'        
    }).then((output) => {
        res.end(output);
    });
});

router.get('/:page([0-9]+)', async (req,res) => {
    const current = req.params.page;
    const offset = (req.params.page-1)*25;
    const {rows,fields} = await flute.flute_paginated_all(offset);
    const result = await flute.flute_count();
    
    template.render('flute.twig', {
        'title': "Flute List",
        'header_title': "Flute List",
        'rows': rows, 
        'fields': fields, 
        'total': Math.ceil(result.rows[0].count/25), 
        'current': current,
        'url': '/flute/'
    }).then((output) => {
        res.end(output);
    });
});
