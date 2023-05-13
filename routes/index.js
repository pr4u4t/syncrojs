const past = require('./past.js')
const current = require('./current.js')
const papers = require('./papers.js')
const quality = require('./quality.js')
const flute = require('./flute.js')
const Router = require('express-promise-router')
const router = new Router()
const template = require('../templates')

router.get('/', async (req,res) => {
    template.render('index.twig', {
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
    app.use('/',router)
}
