const config = require('config');
const { XMLParser } = require('fast-xml-parser');
const logger = require('../utils/logger');
const backtype = config.get("orders.parser.type") || "classic";
const dry = config.get('app.dry') || false;
const fs = require('fs').promises;
const path = require('path');
const papers = require('./papers.js');
const quality = require('./quality.js');
const flute = require('./flute.js');
const { connect: syncroConnect } = require('../utils/syncro');
const { classicLevels } = require('../utils/classic-levels');
const { validateSyncroPayload, validateConversion } = require('./order-validation');
const parser = new XMLParser();
var current = null;
var backend = null;

const fields = [
    //header fields
    "program number",           //0
    "scheduled papers",         //1
    "fosber ID",                //2
    "quality ID",               //3
    "flute ID",                 //4
    "web width",                //5
    "linear meters",            //6
    "G",                        //7
    //order 1 fields
    "customer name 1",          //8
    "customer code 1",          //9
    "order number 1",           //10
    "sheet width 1",            //11
    "1 score 1",                //12
    "2 score 1",                //13
    "3 score 1",                //14
    "4 score 1",                //15
    "5 score 1",                //16
    "sheets per stack 1",       //17
    "sheet length 1",           //18
    "quantity of sheets 1",     //19
    "outs 1",                   //20
    "linear meters 1",          //21
    "20",                       //22
    //order 2 fields
    "customer name 2",          //23
    "customer code 2",          //24
    "order number 2",           //25
    "sheet width 2",            //26
    "1 score 2",                //27
    "2 score 2",                //28
    "3 score 2",                //29
    "4 score 2",                //30
    "5 score 2",                //31
    "sheets per stack 2",       //32
    "sheet length 2",           //33
    "quantity of sheets 2",     //34
    "outs 2",                   //35
    "linear meters 2",          //36
    "10",                       //37
    //order 3 fields
    "customer name 3",          //38
    "customer code 3",          //39
    "order number 3",           //40
    "sheet width 3",            //41
    "1 score 3",                //42
    "2 score 3",                //43
    "3 score 3",                //44
    "4 score 3",                //45
    "5 score 3",                //46
    "sheets per stack 3",       //47
    "sheet length 3",           //48
    "quantity of sheets 3",     //49
    "outs 3",                   //50
    "linear meters 3",          //51
    "fixed field 3",            //52
    "reserved field 1",         //53
    "reserved field 2"          //54
];

function isInteger(value) {
    return /^\d+$/.test(value);
}

const chooser = function(back = null){
    logger.debug("Attempting to choose orders backend");

    if(back == null){
        logger.debug('backend parameter is null using config value');
        back = backtype;
    }

    if(back === 'extended_outs'){
        back = 'extended_orders';
    }

    try{
        switch(back){
            case 'classic':
                logger.debug('selecting classic parser');
                backend = require('./orders/classic');
                current = 'classic';
                break;

            case 'extended_orders':
                logger.debug('selecting extended orders parser');
                backend = require('./orders/extended_orders');
                current = 'extended_orders';
                break;

            default:
                logger.debug('no valid parser configured, defaulting to classic');
                backend = require('./orders/classic');
                current = 'classic';
        }

        return true;
    }catch(error){
        logger.error(error);
    }

    return false;
};

const m2s_serialize = function(sync){
    logger.debug("m2s_serialize");
    // The space after '(' is SetupID token 0; SyncroS converts it to ID 0.
    // The header and three 31-field order blocks provide the other 102 values.
    const sep = '\u241E';
    var ret = "Order.Add( "+sep;
    
    for(let i = 0; i < sync.length; ++i){
        ret += sync[i]+sep;
    }
    
    ret += ")";
    
    return ret
};

const scorer = function(sc1, sc2, sc3, sc4, sc5){
    logger.debug("orders: Scorer:");
    logger.debug(sc1);
    logger.debug(sc2);
    logger.debug(sc3);
    logger.debug(sc4); 
    logger.debug(sc5);

    if(sc1 === undefined){
        throw new Error("Required scorer 1 parameter is not defined");
    }

    const scores = [sc1, sc2, sc3, sc4, sc5]
        .filter((value) => value !== undefined && value !== null)
        .flatMap((value, index) => {
            const text = String(value).trim();
            if(!text || text === '0') return [];
            const measures = text.split(/[\s,;]+/).filter(Boolean);
            for(const measure of measures){
                if(!isInteger(measure)){
                    throw new Error(`scorer ${index + 1} parameter is not a number string`);
                }
            }
            return measures.map((measure) => String(parseInt(measure, 10)));
        });
    const ret = scores.length > 0 ? scores.join(' ') : '0';
    
    logger.debug(`orders: Scorer: ${ret}`);
    
    return ret;
};

const syncro_header = function(setupid, webwidth, trimtype, scoregap, qualityid, fluteid, qweight, qthickness, trim, setupSuffix = '2'){
    logger.debug("orders: syncro_header")
    logger.debug(setupid);
    logger.debug(webwidth);
    logger.debug(trimtype)
    logger.debug(scoregap);
    logger.debug(qualityid);
    logger.debug(fluteid);
    logger.debug(qweight);
    logger.debug(qthickness);
    logger.debug(trim);

    // These are SyncroS tokens 1-9; token 0 is the blank SetupID from m2s_serialize().
    var ret = [];
    
    ret.push(setupid+'.'+setupSuffix);                       //1 SetupCode
    ret.push(webwidth);                                      //2 SetupData.webWidth_UDdmm
    ret.push(trimtype);                                      //3 SetupData.trimType
    ret.push(scoregap);                                      //4 SetupData.scoreGap_UDdmm
    ret.push(qualityid);                                     //5 SetupData.quality.qualityID
    ret.push(fluteid);                                       //6 SetupData.quality.qualityFluteData.fluteId
    ret.push(qweight);                                       //7 SetupData.quality.weightFactor
    ret.push(qthickness);                                    //8 SetupData.quality.qualityFluteData.thickness_UDdmm
    ret.push(trim);                                          //9 SetupData.validation.trim_UDdmm
    
    return ret
};

const syncro_order = function(onumber,delivery,cname,sheetWidth,outs,measures,scoreType,posType,twinOffset,scoreGap,level,sheetLength,sheetCount,sheetsStack,stacks,empty,sending){
    // 31 fields per order, in the order consumed by SyncroS::ParseOrderCommand.
    // The global token index is 10 + (orderIndex * 31) + this local index.
    var ret = [];
    logger.debug("classic: syncro_order");
    logger.debug(onumber);
    logger.debug(delivery);
    logger.debug(cname);
    logger.debug(sheetWidth);
    logger.debug(outs);
    logger.debug(measures);
    logger.debug(scoreType);
    logger.debug(posType);
    logger.debug(twinOffset);
    logger.debug(scoreGap);
    logger.debug(level);
    logger.debug(sheetLength);
    logger.debug(sheetCount);
    logger.debug(sheetsStack);
    logger.debug(stacks);
    logger.debug(empty);
    logger.debug(sending);

    ret.push(onumber);                                       //+0 order[n].orderNumber
    ret.push(delivery);                                      //+1 order[n].delivery
    ret.push(cname);                                         //+2 order[n].customer.name
    ret.push(sheetWidth);                                    //+3 order[n].twin.sheetWidth_UDdmm
    ret.push(outs);                                          //+4 order[n].twin.outs
    ret.push(measures);                                      //+5 order[n].twin.scoreMeasures_UDdmm
    ret.push(scoreType);                                     //+6 order[n].twin.scoreType
    ret.push(posType);                                       //+7 order[n].twin.positType
    ret.push(twinOffset);                                    //+8 order[n].twin.teletwinOffset_UDdmm
    ret.push(scoreGap);                                      //+9 order[n].twin.scoreGap_UDdmm
    ret.push(level);                                         //+10 order[n].knife.levelName
    ret.push(sheetLength);                                   //+11 order[n].knife.sheetLength_UDdmm
    ret.push(sheetCount);                                    //+12 order[n].knife.segmentSheetCount
    ret.push(sheetsStack);                                   //+13 order[n].stacker.sheetsPerStack
    ret.push(stacks);                                        //+14 order[n].materialHandling.stacksPerPallet
    ret.push(sending);                                       //+15 order[n].materialHandling.sending (PalletSending enum)
    ret.push("False");                                       //+16 order[n].knife.cutToMark
    ret.push(" ");                                           //+17 order[n].knife.cutToPattern ID (empty)
    ret.push(" ");                                           //+18 order[n].toPrint.dataToPrint[1]
    ret.push(" ");                                           //+19 order[n].toPrint.dataToPrint[2]
    ret.push(" ");                                           //+20 order[n].toPrint.dataToPrint[3]
    ret.push(" ");                                           //+21 order[n].toPrint.dataToPrint[4]
    ret.push("False");                                       //+22 order[n].stacker.continuousStack
    ret.push("False");                                       //+23 order[n].materialHandling.pallet.onPlatform
    ret.push(" ");                                           //+24 order[n].materialHandling.pallet.note
    ret.push(" ");                                           //+25 order[n].customer.destinationAddress
    ret.push(" ");                                           //+26 order[n].materialHandling.boxMachineCode
    ret.push(" ");                                           //+27 order[n].materialHandling.boxMachineName
    ret.push(empty);                                          //+28 order[n].stacker.side (DischargeSide; legacy arg name)
    ret.push(sending);                                        //+29 order[n].total.balance (Balance enum; shares legacy sending arg)
    ret.push("False");                                       //+30 order[n].stacker.bundle
    
    return ret;
};

const calculate_trim = function(webWidth, width0, outs0, width1, outs1, width2, outs2){
    logger.debug("classic: calculate_trim");
    logger.debug(webWidth);
    logger.debug(width0);
    logger.debug(outs0);
    logger.debug(width1);
    logger.debug(outs1);
    
    var ret = parseInt(webWidth);
    
    if(width0 !== undefined && outs0 !== undefined && isInteger(width0) && isInteger(outs0)){
        const w0 = parseInt(width0);
        const o0 = parseInt(outs0);
        logger.debug(`trim calc 0 ${w0} ${o0} ${w0*o0}`);
        ret -= w0*o0;    
    } else {
        logger.debug('width0 or outs0 undefined');
    }

    
    if(width1 !== undefined && outs1 !== undefined && isInteger(width1) && isInteger(outs1)){
        const w1 = parseInt(width1);
        const o1 = parseInt(outs1);
        logger.debug(`trim calc 1 ${w1} ${o1} ${w1*o1}`);
        ret -= w1*o1;
    } else {
        logger.debug('width0 or outs0 undefined');
    }

    if(width2 !== undefined && outs2 !== undefined && isInteger(width2) && isInteger(outs2)){
        const w2 = parseInt(width2);
        const o2 = parseInt(outs2);
        logger.debug(`trim calc 2 ${w2} ${o2} ${w2*o2}`);
        ret -= w2*o2;
    }
    
    return ret;
};

const mac2syncro = async function(mac){
    var ret;
    var order;

    if(mac && !Array.isArray(mac) && mac.format === 'extended_orders_csv'){
        return manual_setup_to_syncro(mac);
    }

    if(Array.isArray(mac) !== true || (mac.length !== 42 && mac.length !== 55)){
        const err = 'mac2syncro: Invalid mac data mac.length must be 42 or 55';
        logger.error(err);
        return {result: null, error: err};
    }

    const delivery = "1 01 01 0001 08 00 00 0";
    const quality_row = await quality.quality_params_by_id(mac[3]);
    const fluteId = mac[4];
    const flute_row = await flute.flute_params_by_id(fluteId);
    
    if(quality_row == null || flute_row == null){
        const err = "mac2syncro: failed to fetch parameters";
        logger.error(err);
        return {result: null, error: err};
    }
   
    logger.info(quality_row);

    const qualityWeight = quality_row.rows[0].weightfactoruwg_m;      //635
    const qualityThickness = quality_row.rows[0].thickness_uddmm;     //45 
    const qualityGap = quality_row.rows[0].scoregap_uddmm;            //30
    const webWidth = parseInt(mac[5]);
    const sheetWidth0 = parseInt(mac[11]);
    const sheetWidth1 = parseInt(mac[26]);
    const outs0 = parseInt(mac[20]);
    const outs1 = parseInt(mac[35]);
    const hasOrder2 = mac.length === 55 && mac[40] !== '';
        const levels = classicLevels(
            [mac[18], mac[33], hasOrder2 ? mac[48] : null],
            Number(config.get('orders.validation.knife_level_count')) || 3
        );
    const sheetWidth2 = hasOrder2 ? parseInt(mac[41]) : undefined;
    const outs2 = hasOrder2 ? parseInt(mac[50]) : undefined;
    const setupid = mac[2];
    const qualityId = mac[3];
    const clientNum0 = mac[10];
    const clientNum1 = mac[25];
    const clientName0 = mac[8];
    const clientName1 = mac[23];
    const sheetLength0 = mac[18];
    const sheetCount0 = mac[19];
    const sheetLength1 = mac[33];
    const sheetCount1 = mac[34];

    logger.debug("mac2syncro: WebWidth:");
    logger.debug(webWidth);
    logger.debug("mac2syncro: Sheet Width:");
    logger.debug(sheetWidth0);
    logger.debug(sheetWidth1);
    
    const trim = calculate_trim(webWidth, sheetWidth0, outs0, sheetWidth1, outs1, sheetWidth2, outs2);
    if(isNaN(trim) || trim > 2300 || trim < 0){
        const err = `mac2syncro: Invalid trim value ${trim}`;
        logger.error(err);
        return {result: null, error: err};
    }
    
    /**	
     * HEADER
    */
    const header = syncro_header(setupid, webWidth, 0, qualityGap, qualityId, fluteId, qualityWeight, qualityThickness, trim);
    ret = header;

    /**
     * ORDER 0  
     */
    const sheetsStack0 = parseInt(mac[17]);
    const scorer0 = scorer(mac[12], mac[13], mac[14], mac[15], mac[16]);
    order = syncro_order(
        clientNum0, 
        delivery, 
        clientName0, 
        sheetWidth0, 
        outs0, 
        scorer0,
        "A",
        "2",
        " ",
        " ",
        levels[0],
        sheetLength0,
        sheetCount0,
        sheetsStack0/2,
        1,
        0,
        84
    );
    ret = ret.concat(order);

    /**
     * ORDER 1
     */
    if(clientNum1 !== ""){
        const sheetsStack1 = parseInt(mac[32]);
        const scorer1 = scorer(mac[27], mac[28], mac[29], mac[30], mac[31]);
        order =  syncro_order(
            clientNum1, 
            delivery, 
            clientName1, 
            sheetWidth1, 
            outs1, 
            scorer1, 
            "A", 
            "2", 
            " ", 
            " ", 
            levels[1], 
            sheetLength1, 
            sheetCount1, 
            sheetsStack1/2, 
            1, 
            0, 
            84
        );
    } else {
        order = syncro_order(" ", delivery, " ", " ", " ", " ", " ", " "," ", " ", " ", " ", " ", " ", " ", " ", " ");
    }
    ret = ret.concat(order);
    /**
     * ORDER 2
     */
    if(hasOrder2){
        const sheetsStack2 = parseInt(mac[47]);
        const scorer2 = scorer(mac[42] || '0', mac[43] || '0', mac[44] || '0', mac[45] || '0', mac[46] || '0');
        order = syncro_order(
            mac[40],
            delivery,
            mac[38],
            sheetWidth2,
            outs2,
            scorer2,
            "A",
            "2",
            " ",
            " ",
            levels[2],
            mac[48],
            mac[49],
            sheetsStack2/2,
            1,
            0,
            84
        );
    } else {
        order = syncro_order(" ", delivery, " "," "," ","0"," "," "," "," "," "," "," "," "," "," "," ");
    }
    ret = ret.concat(order);

    return {result: ret, error: null};
};

const manual_setup_to_syncro = async function(input){
    try{
        if(!input || typeof input !== 'object'){
            throw new Error('Manual setup fields are required');
        }

        const toTenthsMm = (value, name, allowZero = false) => {
            const millimeters = Number(value);
            if(!Number.isFinite(millimeters) || millimeters < 0 || (!allowZero && millimeters === 0)){
                throw new Error(`${name} must be ${allowZero ? 'a non-negative' : 'a positive'} millimeter value`);
            }
            const tenths = Math.round(millimeters * 10);
            if(Math.abs(millimeters * 10 - tenths) > 0.000001){
                throw new Error(`${name} supports at most one decimal place`);
            }
            return tenths;
        };

        const positiveInteger = (value, name, max) => {
            const number = Number(value);
            if(!Number.isSafeInteger(number) || number < 1 || number > max){
                throw new Error(`${name} must be a positive integer no greater than ${max}`);
            }
            return number;
        };

        const setupInput = String(input.setupCode || '').trim();
        const setupMatch = setupInput.match(/^([A-Za-z0-9_-]{1,27})(?:\.(\d{1,2}))?$/);
        if(!setupMatch){
            throw new Error('Setup code must contain 1-27 letters, digits, underscores, or hyphens');
        }
        const setupSuffix = setupMatch[2] || '2';
        const setupCode = `${setupMatch[1]}.${setupSuffix}`;
        const webWidth = toTenthsMm(input.webWidthMm, 'Web width');
        const qualityId = String(input.qualityId || '').trim();
        const fluteId = String(input.fluteId || '').trim();
        const ordersInput = input.orders;
        if(!qualityId || !fluteId || !Array.isArray(ordersInput) || ordersInput.length < 1 || ordersInput.length > 3){
            throw new Error('Enter quality, flute, and between one and three orders');
        }

        const qualityRow = await quality.quality_params_by_id(qualityId);
        const fluteRow = await flute.flute_params_by_id(fluteId);
        if(!qualityRow || !qualityRow.rows || !qualityRow.rows[0] || !fluteRow || !fluteRow.rows || fluteRow.rows.length === 0){
            throw new Error('Quality or flute was not found in the database');
        }

        const parsedOrders = ordersInput.map((order, index) => {
            const label = `Order ${index + 1}`;
            if(!order || typeof order !== 'object'){
                throw new Error(`${label} fields are required`);
            }
            const orderNumber = String(order.orderNumber || '').trim();
            const customerName = String(order.customerName || '').trim();
            if(!orderNumber || orderNumber.length > 30 || !customerName || customerName.length > 15){
                throw new Error(`${label} needs an order number (max 30) and customer name (max 15)`);
            }
            const scoresInput = Array.isArray(order.scores)
                ? order.scores
                : String(order.scores || '').split(/[\s,;]+/).filter(Boolean);
            const scores = scoresInput
                .map((score, scoreIndex) => score === '' || score === null || score === undefined
                    ? null
                    : toTenthsMm(score, `${label} score ${scoreIndex + 1}`, true))
                .filter((score) => score !== null && score > 0)
                .map(String);
            const sheetWidth = toTenthsMm(order.sheetWidthMm, `${label} sheet width`);
            if(scores.some((score) => Number(score) > webWidth)){
                throw new Error(`${label} crease measurements cannot exceed the web width`);
            }
            const level = order.level === undefined ? 3 - index : Number(order.level);
            if(!Number.isInteger(level) || level < 1 || level > 3){
                throw new Error(`${label} level must be 1, 2, or 3`);
            }
            return {
                orderNumber,
                customerName,
                sheetWidth,
                outs: positiveInteger(order.outs, `${label} outs`, 5),
                sheetLength: toTenthsMm(order.sheetLengthMm, `${label} sheet length`),
                sheetCount: positiveInteger(order.quantitySheets, `${label} quantity`, 999999),
                sheetsPerStack: positiveInteger(order.sheetsPerStack, `${label} sheets per stack`, 9999),
                scores,
                level
            };
        });

        const creaseMeasureCount = parsedOrders.reduce((count, order) => count + order.scores.length, 0);
        if(creaseMeasureCount > 22){
            throw new Error('A setup may contain no more than 22 crease measures');
        }

        const usedWidth = parsedOrders.reduce((sum, order) => sum + order.sheetWidth * order.outs, 0);
        const trimWidth = webWidth - usedWidth;
        const maxTrim = Number(config.get('orders.max_trim'));
        if(trimWidth < 0 || trimWidth > (Number.isFinite(maxTrim) ? maxTrim : 2300)){
            throw new Error(`Cut trim ${trimWidth / 10} mm is outside the configured limit`);
        }

        const delivery = '1 01 01 0001 08 00 00 0';
        const result = syncro_header(
            setupMatch[1],
            webWidth,
            0,
            qualityRow.rows[0].scoregap_uddmm,
            qualityId,
            fluteId,
            qualityRow.rows[0].weightfactoruwg_m,
            qualityRow.rows[0].thickness_uddmm,
            trimWidth,
            setupSuffix
        );

        for(let index = 0; index < 3; ++index){
            const order = parsedOrders[index];
            if(!order){
                result.push(...syncro_order(' ', delivery, ' ', ' ', ' ', '0', ' ', ' ', ' ', ' ', '0', ' ', ' ', ' ', ' ', ' ', ' '));
                continue;
            }
            const measures = order.scores.length > 0
                ? order.scores.join(' ')
                : String(order.sheetWidth);
            result.push(...syncro_order(
                order.orderNumber,
                delivery,
                order.customerName,
                order.sheetWidth,
                order.outs,
                measures,
                'A',
                '2',
                ' ',
                ' ',
                String(order.level),
                order.sheetLength,
                order.sheetCount,
                order.sheetsPerStack,
                1,
                0,
                84
            ));
        }

        return {result, error: null, setupCode, trim: trimWidth};
    }catch(error){
        return {result: null, error: error.message};
    }
};

const orders_add = async function(data){
    if(data === null || data === undefined){
        logger.error('invalid data');
        return false;
    }
    if(Array.isArray(data) && data.length === 102){
        const validation = validateSyncroPayload(data);
        if(validation){
            logger.error(`order: pre-send validation failed: ${validation.error}`);
            return false;
        }
    }
    let cmd = m2s_serialize(data);
    
    if(cmd === null){
        logger.error("order: failed to parse "+data);
        return false;
    }
    
    if(dry === true){
        logger.debug("order: orders_add");
        return true;
    }
    
    try{
        const psocket = await syncroConnect();
        const framedCommand = `Order.Add64:${Buffer.from(cmd, 'utf8').toString('base64')}\n`;
        var result = await psocket.write(framedCommand);
        if(result != framedCommand.length){
            logger.error('order: Failed to send command to syncro');
            return null;
        }
        await psocket.end();
        result = await psocket.readAll();
        const acknowledgement = result.toString().trim().toLowerCase();
        if(acknowledgement === 'ok'){
            return true;
        }
        if(acknowledgement === 'pending'){
            return null;
        }
        if(acknowledgement === 'rejected' || acknowledgement === 'duplicate' || acknowledgement === 'invalid'){
            return false;
        }
        logger.error(`order: missing acceptance acknowledgement (${acknowledgement || 'empty response'})`);
        return null;
    }catch(error){
        logger.error('order: Failed to add order');
        logger.error(error);
        return null;
    }
};

const orders_delete_all = async function(){
    const cmd = 'Orders.Delete';
    
    if(dry === true){
        logger.debug(`order: orders_delete_all ${cmd}`);
        return true;
    }
    
    try{
        const psocket = await syncroConnect();
        var result = await psocket.write(cmd);
        if(result != cmd.length){
            logger.error('order: command send failed');
            return null;
        }
        result = await psocket.readAll();
        result = result.toString();
        return result.toLowerCase() == 'ok';
    }catch(error){
        logger.error(`order: failed to delete orders ${error}`);
    }

    return null; 
};

const orders_all = async function(){
    const cmd = 'Orders.Get';
    let psocket;
    try{
        psocket = await syncroConnect();
        const written = await psocket.write(cmd);
        if(written !== Buffer.byteLength(cmd)){
            logger.error('order: command send failed');
            return null;
        }
        const response = await psocket.readAll();
        const xml = response ? response.toString('utf8').trim() : '';
        if(!xml){
            logger.warn('Empty Orders.Get response');
            return null;
        }

        const parsed = parser.parse(xml);
        if(!parsed || !Object.prototype.hasOwnProperty.call(parsed, 'NewDataSet')){
            logger.warn('Orders.Get response did not contain NewDataSet');
            return null;
        }
        const rows = parsed.NewDataSet && parsed.NewDataSet.DryEndList;
        if(rows === null || rows === undefined) return [];
        return Array.isArray(rows) ? rows : [rows];
    }catch(error){
        logger.error('order: failed to obtain orders');
        logger.error(error);
    }finally{
        if(psocket) psocket.destroy();
    }

    return null;
};

const order_exists = async function(setupCode){
    const code = String(setupCode || '').trim();
    if(!/^[A-Za-z0-9._-]{1,30}$/.test(code)){
        return false;
    }

    const cmd = `Orders.Has:${code}`;
    let psocket;
    try{
        psocket = await syncroConnect();
        const written = await psocket.write(cmd);
        if(written !== cmd.length){
            return null;
        }
        const response = (await psocket.readAll()).toString().trim().toLowerCase();
        if(response === 'true') return true;
        if(response === 'false') return false;
        return null;
    }catch(error){
        logger.warn({error, setupCode: code}, 'Failed to check Syncro setup code');
        return null;
    }finally{
        if(psocket){
            psocket.destroy();
        }
    }
};

const default_order = function(){
    var ret = []
    logger.debug("order: syncro_default_order");
    const delivery = "1 01 01 0001 08 00 00 0";
    
    var header = syncro_header("7734","22200","0","30","FALA A",'B',"285","27",2000);
    var order0 = syncro_order("Z2702300041",delivery,"ERKAR","5000","2",scorer("2000","3000"),"A","2"," "," ","3","16960","1000","200","4","84");
    var order1 = syncro_order("1",delivery,"1","5100","2",scorer("1000","1000","1000","2100"),"A","2"," "," ","1","12000","1412","340","2"," ");
    var order2 = syncro_order(" ",delivery," "," "," "," "," "," "," "," "," "," "," "," "," "," ");
    
    ret = header.concat(order0);
    ret = ret.concat(order1);
    ret = ret.concat(order2);
    
    return ret;
};

const current_backend = function(){
    if(backend === undefined || backend === null){
        return null;
    }
    return current;
};

const available_backends = async function(){
    const dir = path.join(__dirname, 'orders');
    const files = await fs.readdir(dir);
    return files.filter((file) => ['classic.js', 'extended_orders.js'].includes(file));
};

const switch_backend = async function(back){
    if(back === undefined || back === null){
        logger.error('Missing backend parameter');
        return false;
    }

    if(back === 'extended_outs'){
        back = 'extended_orders';
    }

    if(current === back){
        logger.info('backend already selected');
        return true;
    }

    let available = await available_backends();
    if(available.find((name) => name === `${back}.js`) === undefined){
        return false;
    }

    return chooser(back);
};

const default_backend = function(){
    return backtype;
};

const parse = function(data){
    if(data === undefined || data === null){
        logger.error('invalid data');
        return false;
    }

    if(backend === undefined || backend === null){
        logger.error('no valid backup selected');
        return false;
    }

    return backend.mac_parse(data, module.exports);
};

const validate = async function(data){
    if(data === undefined || data === null){
        logger.error('invalid data');
        return false;
    }

    if(backend === undefined || backend === null){
        logger.error('no valid backup selected');
        return false;
    }

    const converted = await backend.validate(data, papers, quality, flute, module.exports);
    return validateConversion(converted, data);
};

const dump = function(data){
    var ret = '';

    if(data === undefined || data === null){
        logger.error('invalid data');
        return false;
    }

    ret += '{';
    for(let i = 0; i < fields.length; ++i){
        ret += `${fields[i]}: ${data[i]}\n`;
    }
    ret += '}';

    return ret;
};
//------------------------------------------------------------------------------

chooser();

module.exports = {
    fields,
    orders_add,
    orders_delete_all,
    orders_all,
    order_exists,
    default_order,
    current_backend,
    available_backends,
    switch_backend,
    manual_setup_to_syncro,
    syncro_header,
    syncro_order,
    m2s_serialize,
    mac2syncro,
    validate_syncro_payload: validateSyncroPayload,
    default_backend,
    parse,
    validate,
    dump
};