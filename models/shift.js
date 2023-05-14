const { XMLParser } = require("fast-xml-parser")
const parser = new XMLParser()
const fs = require('fs')
var net = require('net')
const {PromiseSocket, TimeoutError} = require("promise-socket")
const config = require('config')
const port = config.get('syncro.port')
const host = config.get('syncro.host')

const shift_all = async function(){
    var xmlbuff = ''
    const psocket = new PromiseSocket()
    var result = await psocket.connect(port,host)
    result = await psocket.write('Shift.List')
    xmlbuff = await psocket.readAll()
    xmlbuff = xmlbuff.toString()
    let jml = parser.parse(xmlbuff)
    
    return (Array.isArray(jml.NewDataSet.ShiftList)) ? jml.NewDataSet.ShiftList : [ jml.NewDataSet.ShiftList ]
}

const shift_state = async function(){
    const psocket = new PromiseSocket()
    var result = await psocket.connect(port,host)
    result = await psocket.write('Shift.State')
    result = await psocket.readAll()
    result = result.toString()
    return result
}

const shift_count = async function(){
    let count = await shift_all()
    return count.length
}

module.exports = {
    shift_all,
    shift_state,
    shift_count
}
