const config = require('config');
const logger = require('../utils/logger');
const backtype = config.get("orders.parser.type") || "classic";
const timeout = config.get("syncro.timeout") || 30000;
const dry = config.get('app.dry') || false;
const fs = require('fs').promises;
const path = require('path');
const net = require("node:net");
const papers = require('./papers.js');
const quality = require('./quality.js');
const flute = require('./flute.js');
const {PromiseSocket, TimeoutError} = require("promise-socket");
const port = config.get('syncro.port');
const host = config.get('syncro.host');
var current = null;
var backend = null;

const fields = [
    //header fields
    "program number",           //0
    "scheduled papers",         //1
    "fosber ID",                //2
    "flute",                    //3
    "flute type",               //4
    "rollsize",                 //5
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
    "10"                        //37
];

function isInteger(value) {
    return /^\d+$/.test(value);
}

async function syncroConnect(){
    const socket = new net.Socket();
    socket.setTimeout(timeout);
    const psocket = new PromiseSocket(socket);
    await psocket.connect(port,host);

    return psocket;
}

const chooser = function(back = null){
    logger.debug("Attempting to choose orders backend");

    if(back == null){
        logger.debug('backend parameter is null using config value');
        back = backtype;
    }

    try{
        switch(back){
            case 'classic':
                logger.debug('selecting classic parser');
                backend = require('./orders/classic');
                current = 'classic';
                break;

            case 'extended_outs':
                logger.debug('selecting extended outs parser');
                backend = require('./orders/extended_outs');
                current = 'extended_outs';
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

    if(isInteger(sc1) == false){
        throw new Error("Required scorer 1 paramter in not a number string");
    }

    var ret = '';
    ret += parseInt(sc1);
    
    if(sc2 !== undefined && sc2 != '0'){
        if(isInteger(sc2) == false){
            throw new Error("scorer 2 parameter is not a number string");
        }
        ret += " "+parseInt(sc2);
    }
    
    if(sc3 !== undefined && sc3 != '0'){
        if(isInteger(sc3) == false){
            throw new Error("scorer 3 parameter is not a number string");
        }
        ret += " "+parseInt(sc3);
    }
    
    if(sc4 !== undefined && sc4 != '0'){
        if(isInteger(sc4) == false){
            throw new Error("scorer 4 parameter is not a number string");
        }
        ret += " "+parseInt(sc4);
    }

    if(sc5 !== undefined && sc5 != '0'){
        if(isInteger(sc5) == false){
            throw new Error("scorer 5 parameter is not a number");
        }
        ret += " "+parseInt(sc5);
    }
    
    logger.debug(`orders: Scorer: ${ret}`);
    
    return ret;
};

const syncro_header = function(setupid, webwidth, trimtype, scoregap, qualityid, fluteid, qweight, qthickness, trim){
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

    var ret = [];
    
    ret.push(setupid+'.2');                                  //SetupID glueing SEQ to setup ID
    ret.push(webwidth);                                      //WebWidth
    ret.push(trimtype);                                      //TrimType
    ret.push(scoregap);                                      //ScoreGap - od rodzaju tektury
    ret.push(qualityid);                                     //QualityID
    ret.push(fluteid);                                       //FluteID
    ret.push(qweight);                                       //QualityWeight - sprawdzic skad sie to bierze  //
    ret.push(qthickness);                                    //QualityThickness                              //
    ret.push(trim);                                          //Trim - trzeba zweryfikowac
    
    return ret
};

const syncro_order = function(onumber,delivery,cname,sheetWidth,outs,measures,scoreType,posType,twinOffset,scoreGap,level,sheetLength,sheetCount,sheetsStack,stacks,empty,sending){
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

    ret.push(onumber);                                       //OrderNumber0
    ret.push(delivery);                                      //Delivery0 - jakas liczba niewiadomego pochodzenia
    ret.push(cname);                                         //CustomerName0 
    ret.push(sheetWidth);                                    //SheetWidth0
    ret.push(outs);                                          //Outs0
    ret.push(measures);                                      //ScoreMeasures0 roznica 4 i 5
    ret.push(scoreType);                                     //ScoreType0
    ret.push(posType);                                       //PositType0
    ret.push(twinOffset);                                    //TeleTwinOffset0 - puste
    ret.push(scoreGap);                                      //ScoreGap0
    ret.push(level);                                         //LevelName0 - dol 2, gora 3
    ret.push(sheetLength);                                   //sheetLenght0
    ret.push(sheetCount);                                    //segmentSheetCount0
    ret.push(sheetsStack);                                   //sheetsPerStack0
    ret.push(stacks);                                        //stacksPerPallet0
    ret.push(sending);                                       //Sending0
    ret.push("False");
    ret.push(" "); 
    ret.push(" "); 
    ret.push(" ");
    ret.push(" ");
    ret.push(" ");
    ret.push("False");
    ret.push("False");
    ret.push(" "); 
    ret.push(" "); 
    ret.push(" ");                                           //Address
    ret.push(" ");                                           //Next Machine
    ret.push(empty);
    ret.push(sending);
    ret.push("False");
    
    return ret;
};

const calculate_trim = function(webWidth, width0, outs0, width1, outs1){
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
    
    return ret;
};

const mac2syncro = async function(mac){
    var ret;
    var order;

    if(Array.isArray(mac) !== true || mac.length !== 42){
        const err = 'mac2syncro: Invalid mac data mac.length != 42';
        logger.error(err);
        return {result: null, error: err};
    }

    const delivery = "1 01 01 0001 08 00 00 0";
    const quality_row = await quality.quality_params_by_id(mac[3]);
    const flute_row = await flute.flute_params_by_id('B');
    
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
    const setupid = mac[2];
    const qualityId = mac[3];
    const clientNum0 = mac[10];
    const clientNum1 = mac[25];
    const clientName0 = mac[8];
    const clientName1 = mac[23];
    const sheetLength0 = mac[33];
    const sheetCount0 = mac[34];
    const sheetLength1 = mac[33];
    const sheetCount1 = mac[34];

    logger.debug("mac2syncro: WebWidth:");
    logger.debug(webWidth);
    logger.debug("mac2syncro: Sheet Width:");
    logger.debug(sheetWidth0);
    logger.debug(sheetWidth1);
    
    const trim = calculate_trim(webWidth, sheetWidth0, outs0, sheetWidth1, outs1);
    if(isNaN(trim) || trim > 2300 || trim < 0){
        const err = `mac2syncro: Invalid trim value ${trim}`;
        logger.error(err);
        return {result: null, error: err};
    }
    
    /**	
     * HEADER
    */
    const header = syncro_header(setupid, webWidth, 0, qualityGap, qualityId, 'B', qualityWeight, qualityThickness, trim);
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
        "3",
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
            "1", 
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
    order = syncro_order(" ", delivery, " "," "," ","0"," "," "," "," "," "," "," "," "," "," "," ");
    ret = ret.concat(order);

    return {result: ret, error: null};
};

const orders_add = async function(data){
    if(data === null || data === undefined){
        logger.error('invalid data');
        return false;
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
        var result = await psocket.write(cmd);
        if(result != cmd.length){
            logger.error('order: Failed to send command to syncro');
            return null;
        }
        result = await psocket.readAll();
        result = result.toString();
        return result.toLowerCase() == "ok";
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
    
    try{
        var xmlbuff;
        const psocket = await syncroConnect();
        result = await psocket.write(cmd);
        if(result != cmd.length){
            logger.error('order: command send failed');
            return null;
        }
        xmlbuff = await psocket.readAll();
        xmlbuff = xmlbuff.toString();
        let jml = parser.parse(xmlbuff);
            
        return (Array.isArray(jml.NewDataSet.DryEndList)) ? jml.NewDataSet.DryEndList : [ jml.NewDataSet.DryEndList ];
    }catch(error){
        logger.error('order: failed to obtain orders');
        logger.error(error);
    }

    return null;
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
    const modules = files.filter(file => path.extname(file) === '.js');
    var ret = [];

    for (const module of modules) {
        ret.push(module);
    }

    return ret;
};

const switch_backend = async function(back){
    if(back === undefined || back === null){
        logger.error('Missing backend parameter');
        return false;
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

const validate = function(data){
    if(data === undefined || data === null){
        logger.error('invalid data');
        return false;
    }

    if(backend === undefined || backend === null){
        logger.error('no valid backup selected');
        return false;
    }

    return backend.validate(data, papers, quality, flute, module.exports);
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
    default_order,
    current_backend,
    available_backends,
    switch_backend,
    syncro_header,
    syncro_order,
    m2s_serialize,
    mac2syncro,
    default_backend,
    parse,
    validate,
    dump
};