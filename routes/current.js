const Router = require('express-promise-router');
const config = require('config');
const port = config.get('syncro.port');
const host = config.get('syncro.host');
const shift_skip = config.get('orders.shift');
const logger = require('../utils/logger');
const sendAppShell = require('../utils/app-shell');
const extendedOrdersParser = require('../models/orders/extended_orders');
const { detectOrderFormat } = require('../models/orders/detect');
const router = new Router();
const multer  = require('multer');
const upload = multer({ dest: './uploads/' });
const ordmac = require('../models/order.js');
const papers = require('../models/papers.js');
const quality = require('../models/quality.js');
const flute = require('../models/flute.js');
const shift = require('../models/shift.js');
const fs = require('fs');

const zeroQuantityWarning = (row) => {
    const orderFields = Array.isArray(row)
        ? [
            { number: row[10], quantity: row[19] },
            { number: row[25], quantity: row[34] },
            { number: row[40], quantity: row[49] }
        ]
        : row && Array.isArray(row.orders)
            ? row.orders.map((order) => ({ number: order.orderNumber, quantity: order.quantitySheets }))
            : [];
    const zeroOrders = orderFields
        .filter((order) => String(order.number || '').trim() && String(order.quantity ?? '').trim() && Number(order.quantity) === 0)
        .map((order) => String(order.number).trim());

    if(zeroOrders.length === 0) return null;
    return `${zeroOrders.length > 1 ? 'Zamówienia' : 'Zamówienie'} ${zeroOrders.join(', ')} ${zeroOrders.length > 1 ? 'mają' : 'ma'} 0 sztuk. Ten zestaw zostanie pominięty.`;
};

module.exports = router;

// fast-xml-parser turns numeric values into numbers, so leading zeros must not decide equality.
const normalizeId = (value) => String(value ?? '').trim().replace(/^0+(?=\d)/, '');
const orderNumbersKey = (numbers) => numbers.map(normalizeId).filter(Boolean).sort().join(', ');
const sourceOrderNumbers = (row) => Array.isArray(row)
    ? [row[10], row[25], row.length === 55 ? row[40] : '']
    : (row && Array.isArray(row.orders) ? row.orders.map((order) => order.orderNumber) : []);

// true: same setup already queued, false: code taken by other orders, null: queue unavailable.
const queuedSetupMatches = async (setupCode, row) => {
    const queued = await ordmac.orders_all();
    if(!Array.isArray(queued)) return null;
    const match = queued.find((order) => normalizeId(order.SetupCode) === normalizeId(setupCode));
    if(!match) return null;
    return orderNumbersKey([match.OrderNumber0, match.OrderNumber1, match.OrderNumber2]) === orderNumbersKey(sourceOrderNumbers(row));
};

router.get('/', async (req, res) => {
    logger.debug("get /current");
    sendAppShell(req,res);
});

router.get('/orders', async(req, res) => {
    logger.debug("get /current/orders");
    const orders = await ordmac.orders_all();
    res.set('Cache-Control', 'no-store').json({ rows: orders, unavailable: orders === null });
});

router.get('/import', async (req, res) => {
    logger.debug("get /current/import");
    sendAppShell(req,res);
});

router.get('/manual/options', async (req, res) => {
    const [qualities, flutes] = await Promise.all([
        quality.quality_all(),
        flute.flute_all()
    ]);
    if (!qualities || !flutes) {
        res.status(503).json({ error: 'manual_options_unavailable' });
        return;
    }

    const qualityIds = [...new Set(qualities.rows.map((row) => String(row.qualityid || '').trim()).filter(Boolean))].sort((first, second) => first.localeCompare(second));
    const fluteIds = [...new Set(flutes.rows.map((row) => String(row.flute || '').trim()).filter(Boolean))].sort((first, second) => first.localeCompare(second));
    const configuredMaxTrim = Number(config.get('orders.max_trim'));
    const maxTrimTenthsMm = Number.isFinite(configuredMaxTrim) ? configuredMaxTrim : 2300;
    res.set('Cache-Control', 'no-store').json({ qualityIds, fluteIds, maxTrimTenthsMm });
});

router.post('/import', upload.single('orderFile'), async (req, res) => {
    let rows;
    let backend;
    const invalid = {};
    const warnings = {};

    logger.debug("post /current/import");
    if(req.session.pendingOrderAck){
        res.status(409).json({ error: 'order_ack_unknown', setupCode: req.session.pendingOrderAck.setupCode });
        return;
    }
    try {
        if(!req.file){
            res.status(400).json({ error: 'order_file_required' });
            return;
        }
            const data = fs.readFileSync(req.file.path, 'utf8');
            // Unrecognised files fall back to the parser configured in Settings.
            const format = detectOrderFormat(data, req.file.originalname) || ordmac.current_backend();
            logger.info(`order import ${req.file.originalname}: ${format} parser`);
            backend = format;
            rows = await ordmac.parse(data, backend);
            if(!Array.isArray(rows) || rows.length === 0){
                res.status(400).json({ error: 'order_file_invalid' });
                return;
            }
        const queuedOrders = await ordmac.orders_all();
        const queuedByCode = new Map((queuedOrders || [])
            .map((order) => [normalizeId(order.SetupCode), orderNumbersKey([order.OrderNumber0, order.OrderNumber1, order.OrderNumber2])])
            .filter(([code]) => code));
        const firstRowByCode = new Map();
        for(let i = 0;  i < rows.length; ++i){
                const warning = zeroQuantityWarning(rows[i]);
                if(warning){
                    warnings[i] = { message: warning };
                    continue;
                }
                const tmp = await ordmac.validate(rows[i], papers, quality, flute, backend);
            if(tmp.result === null || tmp.result === undefined){
                invalid[i] = tmp;
                continue;
            }
            const setupCode = String(tmp.result[0] || '').trim();
            const code = normalizeId(setupCode);
            if(!code) continue;
            const orders = orderNumbersKey(sourceOrderNumbers(rows[i]));
            if(queuedByCode.has(code)){
                const queued = queuedByCode.get(code);
                if(queued === orders){
                    warnings[i] = { message: `Ustawienie ${setupCode} (${orders}) jest już w kolejce Syncro. Zostanie pominięte.`, duplicate: true };
                }else{
                    invalid[i] = { error: `Kod ustawienia ${setupCode} jest już zajęty w Syncro przez inne zamówienia (${queued || 'brak numerów'}). Plik zawiera: ${orders || 'brak numerów'}.` };
                }
            }else if(firstRowByCode.has(code)){
                const first = firstRowByCode.get(code);
                if(first.orders === orders){
                    warnings[i] = { message: `Ustawienie ${setupCode} powtarza wiersz ${first.index + 1}. Zostanie pominięte.`, duplicate: true };
                }else{
                    invalid[i] = { error: `Kod ustawienia ${setupCode} występuje też w wierszu ${first.index + 1} z innymi zamówieniami.` };
                }
            }else{
                firstRowByCode.set(code, { index: i, orders });
            }
        }
        req.session.current = rows;
        req.session.currentBackend = backend;
    } catch (err) {
        logger.error(`order add post ${err}`);
        res.status(400).json({ error: 'order_file_invalid' });
        return;
    } finally {
        if(req.file){
            fs.promises.unlink(req.file.path).catch((error) => logger.warn({error}, 'Unable to remove uploaded order file'));
        }
    }

    res.json({
        rows,
        fields: rows.some((row) => row && row.format === 'extended_orders_csv') ? extendedOrdersParser.preview_fields : ordmac.fields,
        invalid,
        warnings,
        knifeLevelCount: Number(config.get('orders.validation.knife_level_count')) || 3
    });
});

router.post('/manual', upload.none(), async (req,res) => {
    if(req.session.pendingOrderAck){
        res.status(409).json({ accepted: false, error: 'order_ack_unknown', setupCode: req.session.pendingOrderAck.setupCode });
        return;
    }
    if(Array.isArray(req.session.current) && req.session.current.length > 0){
        res.status(409).json({ accepted: false, error: 'import_already_pending' });
        return;
    }

    const converted = await ordmac.manual_setup_to_syncro(req.body);
    if(!converted || !Array.isArray(converted.result)){
        res.status(400).json({ accepted: false, error: converted && converted.error ? converted.error : 'invalid_manual_setup' });
        return;
    }
    const validation = ordmac.validate_syncro_payload(converted.result, req.body);
    if(validation){
        res.status(400).json({ accepted: false, error: validation.error, fieldIndexes: validation.fieldIndexes });
        return;
    }

    const setupCode = converted.setupCode;
    const exists = await ordmac.order_exists(setupCode);
    if(exists === null){
        res.status(503).json({ accepted: false, error: 'syncro_unavailable' });
        return;
    }
    if(exists){
        res.status(409).json({ accepted: false, error: 'setup_code_already_exists', setupCode });
        return;
    }

    req.session.current = [{manual: true}];
    req.session.step = 0;
    const result = await ordmac.orders_add(converted.result);
    if(result === null){
        const reconciled = await ordmac.order_exists(setupCode);
        if(reconciled === true){
            req.session.current = null;
            req.session.step = null;
            res.json({ accepted: true, setupCode, trim: converted.trim });
            return;
        }
        req.session.pendingOrderAck = {setupCode, step: 1, manual: true};
        res.status(202).json({ accepted: false, pending: true, setupCode, error: 'order_ack_unknown' });
        return;
    }
    if(result === false){
        req.session.current = null;
        req.session.step = null;
        res.status(502).json({ accepted: false, setupCode, error: 'order_rejected' });
        return;
    }

    req.session.current = null;
    req.session.step = null;
    res.json({ accepted: true, setupCode, trim: converted.trim });
});

router.post('/import/confirm', upload.none(), async (req, res) => {
    logger.debug("post /current/import/confirm");
    if(req.session.pendingOrderAck){
        res.status(409).json({ accepted: false, error: 'order_ack_unknown', setupCode: req.session.pendingOrderAck.setupCode });
        return;
    }
    if(req.body.accept === 'true' || req.body.accept === '1'){
        if(!Array.isArray(req.session.current) || req.session.current.length === 0){
            res.status(400).json({ accepted: false, error: 'no_import_pending' });
            return;
        }
        const invalid = [];
        for(let index = 0; index < req.session.current.length; ++index){
            if(zeroQuantityWarning(req.session.current[index])) continue;
            const validation = await ordmac.validate(req.session.current[index], papers, quality, flute, req.session.currentBackend);
            if(!validation || validation.result === null || validation.result === undefined){
                invalid.push({ index, error: validation && validation.error ? validation.error : 'Nie udało się zwalidować zamówienia.' });
            }
        }
        if(invalid.length > 0){
            res.status(400).json({ accepted: false, error: 'invalid_import_rows', invalid });
            return;
        }
        req.session.step = 0;
        if(shift_skip.check_skip === true){
            const shift_state = await shift.shift_state();
            if(shift_state == 'idle'){
                req.session.step = Number(shift_skip.skip_idle) || 0;
            } else {
                req.session.step = Number(shift_skip.skip_working) || 0;
            }
        }
        req.session.step = Math.max(0, Math.min(req.session.step, req.session.current.length));
        res.json({ accepted: true, current: req.session.step, steps: req.session.current.length });
    }else{
        req.session.current = null;
        req.session.step = null;
        res.json({ accepted: false });
    }
});

router.post('/import/remove', upload.none(), (req, res) => {
    if(req.session.pendingOrderAck){
        res.status(409).json({ removed: false, error: 'order_ack_unknown', setupCode: req.session.pendingOrderAck.setupCode });
        return;
    }
    if(!Array.isArray(req.session.current)){
        res.status(400).json({ removed: false, error: 'no_import_pending' });
        return;
    }

    const index = Number(req.body.index);
    if(!Number.isSafeInteger(index) || index < 0 || index >= req.session.current.length){
        res.status(400).json({ removed: false, error: 'invalid_import_index' });
        return;
    }

    req.session.current.splice(index, 1);
    if(Number(req.session.step) > index){
        req.session.step = Number(req.session.step) - 1;
    }
    res.json({ removed: true, remaining: req.session.current.length });
});

router.post('/import/update-scores', upload.none(), async (req, res) => {
    if(req.session.pendingOrderAck){
        res.status(409).json({ saved: false, error: 'order_ack_unknown', setupCode: req.session.pendingOrderAck.setupCode });
        return;
    }
    if(req.session.step !== undefined && req.session.step !== null){
        res.status(409).json({ saved: false, error: 'import_already_started' });
        return;
    }
    if(!Array.isArray(req.session.current)){
        res.status(400).json({ saved: false, error: 'no_import_pending' });
        return;
    }

    const rowIndex = Number(req.body.index);
    if(!Number.isSafeInteger(rowIndex) || rowIndex < 0 || rowIndex >= req.session.current.length){
        res.status(400).json({ saved: false, error: 'invalid_import_index' });
        return;
    }
    const sourceRow = req.session.current[rowIndex];
    if(!Array.isArray(sourceRow) || (sourceRow.length !== 42 && sourceRow.length !== 55)){
        res.status(400).json({ saved: false, error: 'score_edit_supported_only_for_classic_dat' });
        return;
    }

    let scoresByOrder;
    try{
        scoresByOrder = JSON.parse(req.body.scoresByOrder || '{}');
    }catch{
        res.status(400).json({ saved: false, error: 'invalid_score_data' });
        return;
    }
    if(!scoresByOrder || typeof scoresByOrder !== 'object' || Array.isArray(scoresByOrder)){
        res.status(400).json({ saved: false, error: 'invalid_score_data' });
        return;
    }

    const scoreColumns = [
        { start: 8, width: 11, fields: [12, 13, 14, 15, 16] },
        { start: 23, width: 26, fields: [27, 28, 29, 30, 31] },
        { start: 38, width: 41, fields: [42, 43, 44, 45, 46] }
    ];
    const updatedRow = sourceRow.slice();
    let updatedOrders = 0;
    for(let orderIndex = 0; orderIndex < scoreColumns.length; orderIndex += 1){
        if(!Object.prototype.hasOwnProperty.call(scoresByOrder, orderIndex)) continue;
        const order = scoreColumns[orderIndex];
        if(orderIndex === 2 && sourceRow.length !== 55){
            res.status(400).json({ saved: false, error: 'invalid_order_index' });
            return;
        }
        if(!String(sourceRow[order.start + 2] || '').trim()){
            res.status(400).json({ saved: false, error: 'order_not_found' });
            return;
        }
        const values = scoresByOrder[orderIndex];
        if(!Array.isArray(values) && typeof values !== 'string'){
            res.status(400).json({ saved: false, error: 'invalid_score_data' });
            return;
        }

        const parsedScores = [];
        const scoreValues = Array.isArray(values) ? values : values.split(/[\s,;]+/).filter(Boolean);
        for(const value of scoreValues){
            const text = String(value ?? '').trim();
            if(!text) continue;
            const millimeters = Number(text);
            const tenths = Math.round(millimeters * 10);
            if(!Number.isFinite(millimeters) || millimeters <= 0 || Math.abs(millimeters * 10 - tenths) > 0.000001){
                res.status(400).json({ saved: false, error: 'Bigowanie musi być dodatnią wartością z dokładnością do 0,1 mm.' });
                return;
            }
            if(tenths > Number(sourceRow[order.width])){
                res.status(400).json({ saved: false, error: 'Bigowanie nie może przekraczać szerokości arkusza.' });
                return;
            }
            if(tenths > 0) parsedScores.push(String(tenths));
        }

        updatedRow[order.fields[0]] = parsedScores.length > 0
            ? parsedScores.join(' ')
            : String(sourceRow[order.width]);
        order.fields.slice(1).forEach((field) => { updatedRow[field] = ''; });
        updatedOrders += 1;
    }
    if(updatedOrders === 0){
        res.status(400).json({ saved: false, error: 'no_scores_to_update' });
        return;
    }

    const totalCreaseMeasures = scoreColumns.reduce((total, order) => {
        if(!String(updatedRow[order.start + 2] || '').trim()) return total;
        const values = order.fields.flatMap((field) => String(updatedRow[field] || '').trim().split(/[\s,;]+/).filter(Boolean));
        if(values.length === 1 && Number(values[0]) === Number(updatedRow[order.width])) return total;
        return total + values.filter((value) => Number(value) > 0).length;
    }, 0);
    if(totalCreaseMeasures > 22){
        res.status(400).json({ saved: false, error: `Setup zawiera ${totalCreaseMeasures} bigów; limit to 22.` });
        return;
    }

    let validation = null;
    if(!zeroQuantityWarning(updatedRow)){
        try{
            validation = await ordmac.validate(updatedRow, papers, quality, flute, req.session.currentBackend);
        }catch(error){
            logger.warn({error, rowIndex}, 'Could not validate edited DAT scores');
            res.status(400).json({ saved: false, error: error.message || 'Nie udało się zwalidować bigów.' });
            return;
        }
    }

    req.session.current[rowIndex] = updatedRow;
    const invalid = !validation || validation.result === null || validation.result === undefined
        ? { error: validation && validation.error ? validation.error : 'Nie udało się zwalidować zamówienia.', fieldIndexes: validation && validation.fieldIndexes }
        : null;
    res.json({ saved: true, row: updatedRow, invalid });
});

router.post('/import/cancel', upload.none(), (req,res) => {
    if(req.session.pendingOrderAck){
        res.status(409).json({ cancelled: false, error: 'order_ack_unknown', setupCode: req.session.pendingOrderAck.setupCode });
        return;
    }
    req.session.current = null;
    req.session.step = null;
    res.json({ cancelled: true });
});

router.post('/import/status', upload.none(), async (req,res) => {
    const pending = req.session.pendingOrderAck;
    if(!pending){
        res.status(400).json({ resolved: false, error: 'no_order_ack_pending' });
        return;
    }

    const exists = await ordmac.order_exists(pending.setupCode);
    if(exists === null){
        res.status(503).json({ resolved: false, error: 'syncro_unavailable', setupCode: pending.setupCode });
        return;
    }

    if(!exists){
        res.json({ resolved: false, error: 'order_ack_unknown', setupCode: pending.setupCode });
        return;
    }

    const steps = req.session.current ? req.session.current.length : pending.step;
    req.session.step = pending.step;
    delete req.session.pendingOrderAck;
    const complete = pending.step >= steps;
    if(complete){
        req.session.current = null;
        req.session.step = null;
    }
    res.json({ resolved: true, current: pending.step, steps, complete });
});

router.get('/import/batch/:step', async (req, res) => {
    let current = Number(req.session.step) || 0;
    let steps = Array.isArray(req.session.current) ? req.session.current.length : 0;
    try{
        if(!Array.isArray(req.session.current)){
            res.status(400).json({ status: false, error: 'no_import_pending' });
            return;
        }
        if(req.session.pendingOrderAck){
            res.status(409).json({
                status: false,
                current,
                steps,
                error: 'order_ack_unknown',
                setupCode: req.session.pendingOrderAck.setupCode
            });
            return;
        }
        const next = current + 1;

        logger.debug("get /current/batch/"+req.params.step);
        if(Number(req.params.step) !== next){
            res.status(409).json({ status: false, current, steps, error: 'unexpected_import_step' });
            return;
        }
        if(next > steps){
            res.json({ status: false, current: steps, steps, error: 'import_already_complete' });
            return;
        }
        const sourceRow = req.session.current[next - 1];
        const warning = zeroQuantityWarning(sourceRow);
        if(warning){
            req.session.step = next;
            if(steps === next){
                req.session.current = null;
                req.session.step = null;
            }
            res.json({ status: true, current: next, steps, skipped: true, warning });
            return;
        }
        const transformed = await ordmac.mac2syncro(sourceRow,papers,quality,flute);
    
        if(!transformed || !Array.isArray(transformed.result)){
            const err = transformed && transformed.error ? transformed.error : "Failed to parse order";
            logger.error(err);
            res.status(400).json({ status: false, current, steps, error: err });
            return;
        }

        const setupCode = String(transformed.result[0] || '').trim();
        const alreadyQueued = await ordmac.order_exists(setupCode);
        if(alreadyQueued === null){
            res.status(503).json({ status: false, current, steps, error: 'syncro_unavailable' });
            return;
        }
        if(alreadyQueued){
            const sameSetup = await queuedSetupMatches(setupCode, sourceRow);
            if(sameSetup !== true){
                res.status(409).json({ status: false, current, steps, setupCode, error: 'order_rejected' });
                return;
            }
            req.session.step = next;
            if(steps === next){
                req.session.current = null;
                req.session.step = null;
            }
            res.json({ status: true, current: next, steps, setupCode, skipped: true, warning: 'already_queued' });
            return;
        }
    
        logger.debug(transformed.result);
    
        let result = await ordmac.orders_add(transformed.result);
        if(result === null){
            const setupExists = await ordmac.order_exists(setupCode);
            if(setupExists === true){
                result = true;
            }
        }
    
        logger.info("Order added",result);

        if(result === null){
            req.session.pendingOrderAck = { setupCode, step: next };
            res.status(202).json({ status: false, current, steps, setupCode, error: 'order_ack_unknown' });
            return;
        }
        if(result === false){
            res.status(502).json({ status: false, current, steps, setupCode, error: 'order_rejected' });
            return;
        }

        req.session.step = next;
        current = next;
        if(steps === current){
            req.session.current = null;
            req.session.step = null;
        }
        res.json({ status: true, current, steps, error: null });
    }catch(error){
        logger.error({error}, 'Order batch failed');
        res.status(502).json({ status: false, current, steps, error: 'order_transfer_failed' });
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
    if(!Array.isArray(req.session.current)){
        res.status(400).json({ error: 'no_import_pending' });
        return;
    }
    if(req.session.pendingOrderAck){
        res.status(409).json({ status: false, error: 'order_ack_unknown', setupCode: req.session.pendingOrderAck.setupCode });
        return;
    }
    const steps = req.session.current.length;
    const current = (Number(req.session.step) || 0) + 1;
    if(current > steps){
        res.status(400).json({ status: false, error: 'import_already_complete' });
        return;
    }
    logger.debug("post /current/add");
    const sourceRow = req.session.current[current - 1];
    const warning = zeroQuantityWarning(sourceRow);
    if(warning){
        req.session.step = current;
        if(steps === current){
            req.session.current = null;
            req.session.step = null;
        }
        res.json({ status: true, current, steps, skipped: true, warning });
        return;
    }
    const transformed = await ordmac.mac2syncro(sourceRow,papers,quality,flute);
    
    if(!transformed || !Array.isArray(transformed.result)){
        res.status(400).json({ status: false, error: transformed && transformed.error ? transformed.error : 'order_conversion_failed' });
        return;
    }
    
    const setupCode = String(transformed.result[0] || '').trim();
    const alreadyQueued = await ordmac.order_exists(setupCode);
    if(alreadyQueued === null){
        res.status(503).json({ status: false, error: 'syncro_unavailable' });
        return;
    }
    if(alreadyQueued){
        const sameSetup = await queuedSetupMatches(setupCode, sourceRow);
        if(sameSetup !== true){
            res.status(409).json({ status: false, setupCode, error: 'order_rejected' });
            return;
        }
        req.session.step = current;
        if(steps === current){
            req.session.current = null;
            req.session.step = null;
        }
        res.json({ status: true, current, steps, setupCode, skipped: true, warning: 'already_queued' });
        return;
    }

    logger.debug(transformed.result);
    
    let result = await ordmac.orders_add(transformed.result);
    if(result === null){
        const exists = await ordmac.order_exists(setupCode);
        if(exists === true){
            result = true;
        }else{
            req.session.pendingOrderAck = { setupCode, step: current };
            res.status(202).json({ status: false, current: current-1, steps, setupCode, error: 'order_ack_unknown' });
            return;
        }
    }
    
    logger.info("Order added",result);

    if(result === false){
        res.status(502).json({ status: false, current: current-1, steps, setupCode, error: 'order_rejected' });
        return;
    }

    req.session.step = current;
    if(steps == current){
        req.session.current = null;
        req.session.step = null;
    }
    
    res.json({ status: true, current, steps });
});
