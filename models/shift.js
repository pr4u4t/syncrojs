const { XMLParser } = require("fast-xml-parser");
const parser = new XMLParser();
const fs = require('fs');
var net = require('net');
const {PromiseSocket, TimeoutError} = require("promise-socket");
const config = require('config');
const loglevel = config.get("log.level") || "info";
const logger = require('pino')({"level": loglevel});

const dry = config.get('app.dry');
const port = config.get('syncro.port');
const host = config.get('syncro.host');
 

const shift_all = async function(){
    const cmd = 'Shift.List';
    try{
        var ret;
        var xmlbuff;
        const psocket = new PromiseSocket();
        await psocket.connect(port,host);
        var result = await psocket.write(cmd);
        if(result != cmd.length){
            logger.error('Error sending shift query');
            return null;
        }
        xmlbuff = await psocket.readAll();
        xmlbuff = xmlbuff.toString();
        let jml = parser.parse(xmlbuff);
        ret = ((Array.isArray(jml.NewDataSet.ShiftList)) ? jml.NewDataSet.ShiftList : [ jml.NewDataSet.ShiftList ]).reverse();
        return ret;
    }catch(error){
        logger.error('Error getting shift data',error)
        return null;
    }
};

const shift_state = async function(){
    try{
        const psocket = new PromiseSocket();
        await psocket.connect(port,host);
        var result = await psocket.write('Shift.State');
        result = await psocket.readAll();
        result = result.toString();
        return result;
    }catch(error){
        logger.error('Error getting shift state');
        return null;
    }
}

const shift_count = async function(){
    let count = await shift_all();
    return (count == null) ? null : count.length;
}

const shift_end = async function(){
    const cmd = 'Shift.Stop()';
    
    if(dry === true){
        logger.debug("shift_end",cmd);
        return true;
    }
    
    try{
        const psocket = new PromiseSocket();
        await psocket.connect(port,host);
        var result = await psocket.write(cmd);
        result = await psocket.readAll();
        result = result.toString();
        return result.toLowerCase() == 'ok';
    }catch(error){
        logger.error('Error getting shift state',error);
        return null;
    }
}

const shift_new = async function(id,manager,people){
    const sep = '\u241E';
    const cmd = "Shift.Shift("+ id + sep + manager + sep + people + ")";
    
    if(dry === true){
        logger.debug("shift_new",cmd);
        return true;
    }
    
    try{
        const psocket = new PromiseSocket();
        await psocket.connect(port,host);
        var result = await psocket.write(cmd);
        result = await psocket.readAll();
        result = result.toString();
        return result.toLowerCase() == 'ok';
    }catch(error){
        logger.error('Error getting shift state',error);
        return null;
    }
}

module.exports = {
    shift_all,
    shift_state,
    shift_count,
    shift_end,
    shift_new
}
