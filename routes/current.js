const Router = require('express-promise-router')
const config = require('config');
const port = config.get('syncro.port');
const host = config.get('syncro.host');
const template = require('../templates')
const router = new Router()
const multer  = require('multer')
const upload = multer({ dest: './uploads/' })
const ordmac = require('../models/current_order.js')
const papers = require('../models/papers.js')
const quality = require('../models/quality.js')
const flute = require('../models/flute.js')
const fs = require('fs')

module.exports = router

router.get('/', async (req,res) => {
    
    template.render('current.twig', {
        'rows': await ordmac.orders_all(), 
        'fields': [], 
        'total': 1,  
        'current': 1,
        'url': '/current/'
    }).then((output) => {
        res.end(output);
    })
})

router.get('/add', async (req,res) => {
    template.render('order_add.twig', {
        'url': '/current/'
    }).then((output) => {
        res.end(output);
    })
})

router.post('/add', upload.single('orderFile'), async (req,res) => {
    var rows
    
    try {
        const data = fs.readFileSync(req.file.path, 'utf8')
        rows = ordmac.mac_parse(data)
    } catch (err) {
        console.log(err)
    }

    req.session.current = rows
    
    template.render('current_confirm.twig', {
        'rows': rows, 
        'fields': ordmac.fields, 
        'total': 1,  
        'current': 1,
        'url': '/current/add/confirm'
    }).then((output) => {
        res.end(output);
    })
})

router.post('/add/confirm', upload.none(), async (req,res) => {
    if(typeof req.body.accept !== undefined){
        req.session.step = 0
        template.render('current_progress.twig', {
            'next': "/current",
            'txt': "Go to Orders"
        }).then((output) => {
            res.end(output);
        })
    }else{
        req.session.current = null
        
        template.render('current_result.twig', {
            'title': "Adding order list denied",
            'message': "Refused to upload new orders list",
            'next': "/",
            'txt': "Go to Home"
        }).then((output) => {
            res.end(output);
        })
    }
    
    
})

router.get('/add/batch/:step', async (req,res) => {
    var steps = req.session.current.length
    var current = ++req.session.step
    
    if(steps == current){
        req.session.current = null
        req.session.step = null
    }
    
    let transformed = await ordmac.mac2syncro(req.session.current[req.session.step-1],papers,quality,flute)
    
    //console.log(transformed)
    console.log(await ordmac.orders_add(transformed))
    
    //res.json({ current: current, steps: steps })
})

router.post('/clear', upload.none(), async (req,res) => {
    let result = await ordmac.orders_delete_all()
    res.redirect('/current/')
})
