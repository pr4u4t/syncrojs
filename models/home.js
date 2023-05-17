const { XMLParser } = require("fast-xml-parser")
const parser = new XMLParser()
const fs = require('fs')
var net = require('net')
const {PromiseSocket, TimeoutError} = require("promise-socket")
const config = require('config')
const port = config.get('syncro.port')
const host = config.get('syncro.host')

const home_info = async function(){
    var xmlbuff = ''
    const psocket = new PromiseSocket()
    var result = await psocket.connect(port,host)
    result = await psocket.write('Home.Info')
    xmlbuff = await psocket.readAll()
    xmlbuff = xmlbuff.toString()
    let jml = parser.parse(xmlbuff)
    
    return (Array.isArray(jml.NewDataSet.Home)) ? jml.NewDataSet.Home : [ jml.NewDataSet.Home ]
}

module.exports = {
    home_info,
}
