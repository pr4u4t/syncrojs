const Router = require('express-promise-router')
const db = require('../db')
const config = require('config')
const template = require('../templates')
const router = new Router()
const multer  = require('multer')
const upload = multer({ dest: './uploads/' })
const shift = require('../models/shift.js')

module.exports = router

router.get('/', async (req,res) => {
    template.render('shift.twig', {
        'state': await shift.shift_state(),
        'rows': await shift.shift_all(), 
        'fields': [], 
        'total': 1,  
        'current': 1,
        'url': '/shift/'
    }).then((output) => {
        res.end(output);
    })
})
