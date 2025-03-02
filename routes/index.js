const past = require('./past.js');
const current = require('./current.js');
const papers = require('./papers.js');
const quality = require('./quality.js');
const flute = require('./flute.js');
const shift = require('./shift.js');
const settings = require('./settings.js');
const Router = require('express-promise-router');
const router = new Router();
const template = require('../templates');
const home = require('../models/home.js');
const NodeCache = require( "node-cache" );

router.get('/', async (req,res) => {
    //const info = await home.home_info();
    template.render('index.twig', {
        'home': null,
        'url': '/'
    }).then((output) => {
        res.end(output);
    });
});

router.get('/home_info', async (req,res) => {
    const info = await home.home_info();
    if(req.headers['content-type'] == 'application/json') {
        res.json(info);
        return;
    }

    template.render('home.twig', {
        'home': info,
        'url': '/home'
    }).then((output) => {
        res.end(output);
    });
});

router.get('/flush_cache', async (req,res) => {
    NodeCache.flushAll();
    res.redirect(req.headers.referer);
});

module.exports = (app) => {
    app.use('/current',current),
    app.use('/past',past),
    app.use('/papers',papers),
    app.use('/quality',quality),
    app.use('/flute',flute),
    app.use('/shift',shift),
    app.use('/settings',settings),
    app.use('/',router)
}
