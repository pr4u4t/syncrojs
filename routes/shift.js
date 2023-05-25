const Router = require('express-promise-router')
const db = require('../db')
const config = require('config')
const template = require('../templates')
const router = new Router()
const multer  = require('multer')
const upload = multer({ dest: './uploads/' })
const shift = require('../models/shift.js')
const log = require('../utils/log.js')

module.exports = router

function isNumeric(str) {
    if (typeof str != "string"){
        return false
    }
    
    return !isNaN(str) && !isNaN(parseFloat(str))
}

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

router.get('/start', async (req,res) => {
    template.render('shift_new.twig', {
        'url': '/shift/'
    }).then((output) => {
        res.end(output);
    })
})

router.post('/start', upload.none(), async (req,res) => {
    if(typeof req.body !== undefined){
        if(!isNumeric(req.body.ID)){
            log.debug("shift add invalid ID value")
            res.redirect('/shift/')
            return
        }
        
        if(req.body.manager.length == 0){
            log.debug("shift add invalid manager value")
            res.redirect('/shift/')
            return
        }
        
        if(!isNumeric(req.body.people)){
            log.debug("shift add invalid people count")
            res.redirect('/shift/')
            return
        }
        
        const id = req.body.ID
        const manager = req.body.manager
        const people = req.body.people
        
        let result = shift.shift_new(id,manager,people)
        log.debug("shift add:",result)
        
    }else{
        log.debug("requestd shift start but form data empty")
        res.redirect('/shift/')
    }
})

router.get('/stop', async (req,res) => {
    let result = await shift.shift_end()
    log.debug("shift stop:",result)
    res.redirect('/shift/')
})
