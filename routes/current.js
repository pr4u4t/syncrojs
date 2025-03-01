const Router = require('express-promise-router');
const config = require('config');
const port = config.get('syncro.port');
const host = config.get('syncro.host');
const shift_skip = config.get('orders.shift');
const template = require('../templates');
const logger = require('pino')();
const router = new Router();
const multer  = require('multer');
const upload = multer({ dest: './uploads/' });
const ordmac = require('../models/current_order.js');
const papers = require('../models/papers.js');
const quality = require('../models/quality.js');
const flute = require('../models/flute.js');
const shift = require('../models/shift.js');
const fs = require('fs');

module.exports = router;

router.get('/', async (req,res) => {
    template.render('current.twig', {
        'rows': await ordmac.orders_all(), 
        'fields': [], 
        'total': 1,  
        'current': 1,
        'url': '/current/'
    }).then((output) => {
        res.end(output);
    });
});

router.get('/import', async (req,res) => {
    template.render('order_import.twig', {
        'url': '/current/'
    }).then((output) => {
        res.end(output);
    });
});

router.post('/import', upload.single('orderFile'), async (req,res) => {
    var rows;
    var val;
    
    try {
        const data = fs.readFileSync(req.file.path, 'utf8')
        rows = ordmac.mac_parse(data)
        val = await ordmac.validate(rows,papers,quality,flute)
        req.session.current = rows
    } catch (err) {
        logger.error("order add post",err);
    }

    template.render('current_confirm.twig', {
        'rows': rows, 
        'fields': ordmac.fields,
        'invalid': val,
        'total': 1,  
        'current': 1,
        'url': '/current/import/confirm'
    }).then((output) => {
        res.end(output);
    });
});

router.post('/import/confirm', upload.none(), async (req,res) => {
    if(typeof req.body.accept !== undefined){
        const shift_state = await shift.shift_state();
        
        if(shift_state == 'idle'){
            req.session.step = shift_skip.skip_idle;
        }else{
            req.session.step = shift_skip.skip_working;
        }
        
        template.render('current_progress.twig', {
            'next': "/current",
            'txt': "Go to Orders"
        }).then((output) => {
            res.end(output);
        });
    }else{
        req.session.current = null;
        
        template.render('current_result.twig', {
            'title': "Adding order list denied",
            'message': "Refused to upload new orders list",
            'next': "/",
            'txt': "Go to Home"
        }).then((output) => {
            res.end(output);
        });
    }
});

router.get('/import/batch/:step', async (req,res) => {
    var steps = req.session.current.length;
    var current = ++req.session.step;
    
    let transformed = await ordmac.mac2syncro(req.session.current[req.session.step-1],papers,quality,flute);
    
    if(transformed == null){
        logger.error("Failed to parse order",new Error());
        res.status(500).send('Internal Server Error');
        return;
    }
    
    logger.debug(transformed);
    
    const result = await ordmac.orders_add(transformed);
    
    logger.info("Order added",result);

    if(steps == current){
        req.session.current = null;
        req.session.step = null;
    }
    
    res.json({ current: current, steps: steps });
})

router.post('/clear', upload.none(), async (req,res) => {
    let result = await ordmac.orders_delete_all();
    logger.info("Orders cleared",result);
    res.redirect('/current/');
})

router.get('/add', upload.none(), async (req,res) => {
     
    /*let transformed = await ordmac.mac2syncro(req.session.current[req.session.step-1],papers,quality,flute)
    
    if(transformed == null){
        log.error("Failed to parse order",new Error())
        res.status(500).send('Internal Server Error')
        return
    }
    
    log.debug(transformed)
    
    const result = await ordmac.orders_add(transformed)
    
    if(steps == current){
        req.session.current = null
        req.session.step = null
    }
    
    res.json({ current: current, steps: steps })
    */
});

router.post('/add', async (req,res) => {
    var steps = req.session.current.length;
    var current = ++req.session.step;
    
    let transformed = await ordmac.mac2syncro(req.session.current[req.session.step-1],papers,quality,flute);
    
    if(transformed == null){
        logger.error("Failed to parse order",new Error());
        res.status(500).send('Internal Server Error')
        return;
    }
    
    logger.debug(transformed);
    
    const result = await ordmac.orders_add(transformed);
    
    logger.info("Order added",result);

    if(steps == current){
        req.session.current = null;
        req.session.step = null;
    }
    
    res.json({ current: current, steps: steps });
});
