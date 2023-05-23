const { XMLParser } = require("fast-xml-parser")
const parser = new XMLParser()
const fs = require('fs')
var net = require('net')
const {PromiseSocket, TimeoutError} = require("promise-socket")
const config = require('config')
const log = require('../utils/log.js')

const port = config.get('syncro.port')
const host = config.get('syncro.host')

const home_info = async function(){
    try {
        var xmlbuff
        const psocket = new PromiseSocket()
        await psocket.connect(port,host)
        result = await psocket.write('Home.Info')
        xmlbuff = await psocket.readAll()
        xmlbuff = xmlbuff.toString()
        let jml = parser.parse(xmlbuff)
        return (Array.isArray(jml.NewDataSet.Home)) ? jml.NewDataSet.Home : [ jml.NewDataSet.Home ]
    }catch(error){
        log.error(error)
        return null
    }
    
}

module.exports = {
    home_info,
}
