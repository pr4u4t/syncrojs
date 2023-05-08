const past = require('./past.js')
const current = require('./current.js')
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
    app.use('/',router)
}
