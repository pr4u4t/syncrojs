const past = require('./past.js')
const current = require('./current.js')
const papers = require('./papers.js')
const quality = require('./quality.js')
const flute = require('./flute.js')
const shift = require('./shift.js')
const Router = require('express-promise-router')
const router = new Router()
const template = require('../templates')
const home = require('../models/home.js')

router.get('/', async (req,res) => {
    const info = await home.home_info()
    template.render('index.twig', {
        'home': info,
        'url': '/'
    }).then((output) => {
        res.end(output);
    })
})

module.exports = (app) => {
    app.use('/current',current),
    app.use('/past',past),
    app.use('/papers',papers),
    app.use('/quality',quality),
    app.use('/flute',flute),
    app.use('/shift',shift),
    app.use('/',router)
}
