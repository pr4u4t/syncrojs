const Router = require('express-promise-router');
const config = require('config');
const port = config.get('syncro.port');
const host = config.get('syncro.host');
const shift_skip = config.get('orders.shift');
const template = require('../templates');
const logger = require('../utils/logger');
const router = new Router();
const multer  = require('multer');
const upload = multer({ dest: './uploads/' });
const ordmac = require('../models/order.js');
const papers = require('../models/papers.js');
const quality = require('../models/quality.js');
const flute = require('../models/flute.js');
const shift = require('../models/shift.js');
const fs = require('fs');

module.exports = router;

router.get('/', async (req, res) => {
    logger.debug("get /current");
    template.render('current.twig', {
        'rows': null,
        'fields': [], 
        'total': 1,  
        'current': 1,
        'url': '/current/'
    }).then((output) => {
        res.end(output);
    });
});

router.get('/orders', async(req, res) => {
    logger.debug("get /current/orders");
    const orders = await ordmac.orders_all();
    if(req.headers['content-type'] == 'application/json') {
        res.json(orders);
        return;
    }
    
    template.render('current_orders.twig', {
        'rows': orders,
        'url': '/current',
        'total': 1,
        'current': 1,
        'fields': []
    }).then((output) => {
        res.end(output);
    });
});

router.get('/import', async (req, res) => {
    logger.debug("get /current/import");
    template.render('order_import.twig', {
        'url': '/current/'
    }).then((output) => {
        res.end(output);
    });
});

router.post('/import', upload.single('orderFile'), async (req, res) => {
    var rows;
    var val = {};

    logger.debug("post /current/import");
    try {
        const data = fs.readFileSync(req.file.path, 'utf8');
        rows = await ordmac.parse(data);
        for(let i = 0;  i < rows.length; ++i){
            const tmp = await ordmac.validate(rows[i], papers, quality, flute);
            if(tmp.result === null || tmp.result === undefined){
                val[i] = tmp;
            }
        }
        req.session.current = rows;
    } catch (err) {
        logger.error(`order add post ${err}`);
    }

    template.render('current_confirm.twig', {
        'rows': rows, 
        'fields': ordmac.fields,
        'invalid': val,
        'total': 1,  
        'current': 1,
        'url': '/current/import'
    }).then((output) => {
        res.end(output);
    });
});

router.post('/import/confirm', upload.none(), async (req, res) => {
    logger.debug("post /current/import/confirm");
    if(typeof req.body.accept !== undefined){
        if(shift_skip.check_skip === true){
            const shift_state = await shift.shift_state();
            if(shift_state == 'idle'){
                req.session.step = shift_skip.skip_idle;
            } else {
                req.session.step = shift_skip.skip_working;
            }
        }
        template.render('current_progress.twig', {
            'next': "/current",
            'txt': "Go to Orders",
            'url': "/current"
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

router.get('/import/batch/:step', async (req, res) => {
    try{
        var steps = req.session.current.length;
        var current = ++req.session.step;

        logger.debug("get /current/batch/"+req.params.step);
        let transformed = await ordmac.mac2syncro(req.session.current[req.session.step-1],papers,quality,flute);
    
        if(transformed == null){
            const err = "Failed to parse order";
            logger.error(err);
            res.json({ status: false, current: current, steps: steps, error: err });
            return;
        }
    
        logger.debug(transformed);
    
        const result = await ordmac.orders_add(transformed);
    
        logger.info("Order added",result);

        if(steps == current){
            req.session.current = null;
            req.session.step = null;
        }
    
        if(result === null || result === false){
            res.json({ status: false, current: current, steps: steps, error: 'Failed to add syncro order' });    
        } else {
            res.json({ status: true, current: current, steps: steps, error: null });
        }
    }catch(error){
        res.json({ status: false, current: current, steps: steps, error: error.message });
    }
});

router.post('/clear', upload.none(), async (req, res) => {
    logger.debug("get /current/clear");
    let result = await ordmac.orders_delete_all();
    logger.info("Orders cleared",result);
    res.redirect('/current/');
});

router.get('/add', upload.none(), async (req, res) => {
    logger.debug("get /current/add");
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

router.post('/add', async (req, res) => {
    var steps = req.session.current.length;
    var current = ++req.session.step;
    logger.debug("post /current/add");
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
