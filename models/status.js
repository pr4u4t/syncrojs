const { XMLParser } = require("fast-xml-parser");
const parser = new XMLParser();
const fs = require('fs');
var net = require('net');
const {PromiseSocket, TimeoutError} = require("promise-socket");
const config = require('config');
const logger = require('pino')();

const port = config.get('syncro.port');
const host = config.get('syncro.host');

const status = async function(){
    const cmd = 'Shift.List';
    try{
        const psocket = new PromiseSocket();
        await psocket.connect(port,host);
        var result = await psocket.write(cmd);
        if(result != cmd.length){
            logger.error('Command send failed', new Error());
            return null;
        }
        var buff = await psocket.readAll();
        ret = [ buff ];
        return ret;
    }catch(error){
        logger.error("Failed to fetch Status",error);
        return null;
    }
}
