const { XMLParser } = require("fast-xml-parser")
const parser = new XMLParser()
const fs = require('fs')
var net = require('net')
const {PromiseSocket, TimeoutError} = require("promise-socket")
const config = require('config')
const log = require('../utils/log.js')

const port = config.get('syncro.port')
const host = config.get('syncro.host')

const status = async function(){
    const cmd = 'Shift.List'
    try{
        const psocket = new PromiseSocket()
        await psocket.connect(port,host)
        var result = await psocket.write(cmd)
        if(result != cmd.length){
            log.error('Command send failed', new Error())
            return null
        }
        var buff = await psocket.readAll()
        ret = [ buff ]
        return ret
    }catch(error){
        log.error("Failed to fetch Status",error)
        return null
    }
}
