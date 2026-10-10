const past = require('./past.js');
const current = require('./current.js');
const papers = require('./papers.js');
const quality = require('./quality.js');
const flute = require('./flute.js');
const shift = require('./shift.js');
const settings = require('./settings.js');
const Router = require('express-promise-router');
const router = new Router();
const sendAppShell = require('../utils/app-shell');
const home = require('../models/home.js');
const operatorStatus = require('../models/operator-status');
const statusStream = require('../utils/status-stream');
const dashboardStream = require('../utils/dashboard-stream');

router.get('/', sendAppShell);

router.get('/home_info', async (req,res) => {
    const info = await home.home_info();
    res.set('Cache-Control', 'no-store').json(info);
});

router.get('/api/home', async (req,res) => {
    const info = await home.home_info();
    if(info === null){
        res.status(503).set('Cache-Control', 'no-store').json({ error: 'syncro_unavailable' });
        return;
    }
    res.set('Cache-Control', 'no-store').json(info);
});

router.get('/api/dashboard/events', dashboardStream.attach);

router.get('/api/syncro/versions', async (req,res) => {
    const versions = await home.syncro_versions();
    if(versions === null){
        res.status(503).set('Cache-Control', 'no-store').json({ error: 'syncro_unavailable' });
        return;
    }
    res.set('Cache-Control', 'no-store').json(versions);
});

router.get('/api/syncro/operator-status', async (req,res) => {
    const snapshot = await operatorStatus.operator_status();
    if(snapshot === null){
        res.status(503).set('Cache-Control', 'no-store').json({ error: 'syncro_unavailable' });
        return;
    }
    res.set('Cache-Control', 'no-store').json(snapshot);
});

router.get('/health/status', async (req,res) => {
    res.set('Cache-Control', 'no-store').json(statusStream.snapshot());
});

router.get('/health/events', (req,res) => {
    statusStream.attach(req,res);
});

module.exports = (app) => {
    app.use('/current',current),
    app.use('/past',past),
    app.use('/papers',papers),
    app.use('/quality',quality),
    app.use('/flute',flute),
    app.use('/shift',shift),
    app.use('/settings',settings),
    app.use('/',router)
}
