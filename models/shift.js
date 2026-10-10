const { XMLParser } = require("fast-xml-parser");
const parser = new XMLParser();
const config = require('config');
const logger = require('../utils/logger');
const { connect: syncroConnect } = require('../utils/syncro');

const dry = config.get('app.dry');
 

const shift_all = async function(){
    const cmd = 'Shift.List';
    try{
        var ret;
        var xmlbuff;
        const psocket = await syncroConnect();
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
        const psocket = await syncroConnect();
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
        const psocket = await syncroConnect();
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
        const psocket = await syncroConnect();
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
