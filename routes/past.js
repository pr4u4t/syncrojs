const Router = require('express-promise-router');
const router = new Router();
const past = require('../models/past_orders.js');
const renderDataListView = require('../utils/data-list-view');
const sendAppShell = require('../utils/app-shell');
const multer  = require('multer');
const upload = multer({ dest: './uploads/' });

module.exports = router;

const renderPastPage = (req,res) => renderDataListView(req,res,{
    url: '/past/',
    load: async (page) => {
        const [data, countResult] = await Promise.all([
            past.past_paginated_all((page-1)*25),
            past.past_count()
        ]);
        const count = countResult && countResult.rows && countResult.rows[0] ? countResult.rows[0].count : 0;
        return {
            unavailable: !data || !countResult,
            rows: data && data.rows,
            fields: data && data.fields,
            total: Math.ceil(Number(count)/25)
        };
    }
});

router.get('/', renderPastPage);
router.get('/:page([0-9]+)', renderPastPage);

const current_date = function(req){
    if(req.query.date !== undefined){
        return req.query.date;
    }else{
        let ts = Date.now();
        let date_ob = new Date(ts);
        let date = date_ob.getDate() < 10 ? '0'+date_ob.getDate() : date_ob.getDate();
        let month = date_ob.getMonth() + 1;
        month = month < 10 ? '0'+month : month;
        let year = date_ob.getFullYear();
        return year + "-" + month + "-" + date;
    }
}

router.get('/export', async (req,res) => {
    let today = current_date(req);
    if(req.query.api === '1'){
        const data = await past.past_export(today);
        res.set('Cache-Control', 'no-store').json({
            unavailable: !data,
            rows: data && data.rows || [],
            fields: data && data.fields || []
        });
        return;
    }

    sendAppShell(req,res);
});

router.post('/export.csv', upload.none(), async (req,res) => {
    res.set('Content-Type', 'application/octet-stream');
    res.send(await past.past_export_csv(req.body.date));
});

router.post('/export.xlsx',  upload.none(), async (req,res) => {
    res.set('Content-Type', 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet');
    res.send(await past.past_export_xlsx(req.body.date));
});
