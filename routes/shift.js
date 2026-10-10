const Router = require('express-promise-router');
const logger = require('../utils/logger');
const sendAppShell = require('../utils/app-shell');
const router = new Router();
const multer  = require('multer');
const upload = multer({ dest: './uploads/' });
const shift = require('../models/shift.js');


module.exports = router;

function isNumeric(str) {
    if (typeof str != "string"){
        return false;
    }
    
    return !isNaN(str) && !isNaN(parseFloat(str));
}

router.get('/', async (req,res) => {
    sendAppShell(req,res);
});

router.get('/info', async (req,res) => {
    const state = await shift.shift_state();
    const rows = await shift.shift_all();

    res.set('Cache-Control', 'no-store').json({ state, rows, unavailable: state === null || rows === null });
});

router.get('/start', async (req,res) => {
    sendAppShell(req,res);
});

router.post('/start', upload.none(), async (req,res) => {
    const { ID, manager, people } = req.body || {};
    if(!isNumeric(String(ID)) || Number(ID) < 1 || Number(ID) > 9 || !String(manager || '').trim() || !isNumeric(String(people)) || Number(people) < 1){
        res.status(400).json({ success: false, error: 'invalid_shift_details' });
        return;
    }

    const result = await shift.shift_new(ID,manager.trim(),people);
    logger.debug("shift add:",result);
    res.status(result === true ? 200 : 502).json({ success: result === true });
});

router.get('/stop', async (req,res) => {
    const result = await shift.shift_end();
    logger.debug("shift stop:");
    logger.debug(result);
    if(req.query.api === '1'){
        res.status(result === true ? 200 : 502).json({ success: result === true });
        return;
    }
    res.redirect('/shift/');
});
