const { XMLParser } = require("fast-xml-parser");
const parser = new XMLParser();
const fs = require('fs');
var net = require('net');
const {PromiseSocket, TimeoutError} = require("promise-socket");
const config = require('config');
const loglevel = config.get("log.level") || "info";
const logger = require('pino')({"level": loglevel});

const port = config.get('syncro.port');
const host = config.get('syncro.host');

const home_info = async function(){
    const cmd = "Home.Info";
    try {
        var xmlbuff;
        const psocket = new PromiseSocket();
        await psocket.connect(port,host);
        result = await psocket.write(cmd);
        xmlbuff = await psocket.readAll();
        xmlbuff = xmlbuff.toString();
        let jml = parser.parse(xmlbuff);
        return jml.NewDataSet.Home;
    }catch(error){
        logger.error('Failed to fetch Home Info');
        logger.error(error);
        return null;
    }
}

module.exports = {
    home_info,
}
