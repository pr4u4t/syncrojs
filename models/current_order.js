const { XMLParser } = require("fast-xml-parser")
const parser = new XMLParser()
const fs = require('fs')
const net = require('net')
const db = require('../db')
const {PromiseSocket, TimeoutError} = require("promise-socket")
const log = require('../utils/log.js')
const config = require('config')
const port = config.get('syncro.port')
const host = config.get('syncro.host')

var mac_parse = function(data){
    var rows = []
    const re = /(.{587})/g
    const rec = /(.{11})(.{44})(.{12})(.{36})(.{23})(.{5})(.{10})(.{1})(.{15})(.{12})(.{30})(.{5})(.{5})(.{5})(.{5})(.{5})(.{5})(.{4})(.{5})(.{6})(.{1})(.{8})(.{2})(.{15})(.{12})(.{30})(.{5})(.{5})(.{5})(.{5})(.{5})(.{5})(.{4})(.{5})(.{6})(.{1})(.{8})(.{2})(.{57})(.{56})(.{8})(.{8})/
    
    const matches = data.matchAll(re);
    
    for (const match of matches) {
        var str = match[0]
        if(str.length>0 && str.replace(/\s/g, '').length==0){  
            continue;
        }
        
        var row = str.match(rec)
        rows.push(row.slice(1,43))
        for(let i = 0; i < rows[rows.length-1].length;++i){
            rows[rows.length-1][i] = rows[rows.length-1][i].replace(/  +/g, ' ').trim()
        }
    }
    
    return rows
} 

var m2s_serialize = function(sync){
    const sep = '\u241E'
    var ret = "Order.Add( "+sep
    
    for(let i = 0; i < sync.length; ++i){
        ret += sync[i]+sep
    }
    
    ret += ")"
    
    return ret
}

var scorer = function(sc1,sc2,sc3,sc4){
    var ret = ''
    ret += sc1
    ret +=" "+sc2
    
    if(sc3 !== undefined && sc3 != '0'){
        ret += " "+sc3
    }
    
    if(sc4 !== undefined && sc4 != '0'){
        ret += " "+sc4
    }
    
    return ret
}

var syncro_header = function(setupid,webwidth,trimtype,scoregap,qualityid,fluteid,qweight,qthickness,trim){
    var ret = []
    
    ret.push(setupid+'.2')                                  //SetupID glueing SEQ to setup ID
    ret.push(webwidth)                                      //WebWidth
    ret.push(trimtype)                                      //TrimType
    ret.push(scoregap)                                      //ScoreGap - od rodzaju tektury
    ret.push(qualityid)                                     //QualityID
    ret.push(fluteid)                                       //FluteID
    ret.push(qweight)                                       //QualityWeight - sprawdzic skad sie to bierze  //
    ret.push(qthickness)                                    //QualityThickness                              //
    ret.push(trim)                                          //Trim - trzeba zweryfikowac
    
    return ret
}

var syncro_order = function(onumber,delivery,cname,sheetWidth,outs,measures,scoreType,posType,twinOffset,scoreGap,level,sheetLength,sheetCount,sheetsStack,stacks,empty,sending){
    var ret = []
    
    ret.push(onumber)                                       //OrderNumber0
    ret.push(delivery)                                      //Delivery0 - jakas liczba niewiadomego pochodzenia
    ret.push(cname)                                         //CustomerName0 
    ret.push(sheetWidth)                                    //SheetWidth0
    ret.push(outs)                                          //Outs0
    ret.push(measures)                                      //ScoreMeasures0 roznica 4 i 5
    ret.push(scoreType)                                     //ScoreType0
    ret.push(posType)                                       //PositType0
    ret.push(twinOffset)                                    //TeleTwinOffset0 - puste
    ret.push(scoreGap)                                      //ScoreGap0
    ret.push(level)                                         //LevelName0 - dol 2, gora 3
    ret.push(sheetLength)                                   //sheetLenght0
    ret.push(sheetCount)                                    //segmentSheetCount0
    ret.push(sheetsStack)                                   //sheetsPerStack0
    ret.push(stacks)                                        //stacksPerPallet0
    ret.push(sending)                                       //Sending0
    ret.push("False")
    ret.push(" ") 
    ret.push(" ") 
    ret.push(" ")
    ret.push(" ")
    ret.push(" ")
    ret.push("False")
    ret.push("False")
    ret.push(" ") 
    ret.push(" ") 
    ret.push(" ")                                           //Address
    ret.push(" ")                                           //Next Machine
    ret.push(empty)
    ret.push(sending)
    ret.push("False")
    
    return ret
}

var syncro_default_order = function(){
    var ret = []
    const delivery = "1 01 01 0001 08 00 00 0"
    
    var header = syncro_header("7734","22200","0","30","FALA A",'B',"285","27",2000)
    var order0 = syncro_order("Z2702300041",delivery,"ERKAR","5000","2",scorer("2000","3000"),"A","2"," "," ","3","16960","1000","200","4","84")
    var order1 = syncro_order("1",delivery,"1","5100","2",scorer("1000","1000","1000","2100"),"A","2"," "," ","1","12000","1412","340","2"," ")
    var order2 = syncro_order(" ",delivery," "," "," "," "," "," "," "," "," "," "," "," "," "," ")
    
    ret = header.concat(order0)
    ret = ret.concat(order1)
    ret = ret.concat(order2)
    
    return ret
}

var calculate_trim = function(webWidth,width0,outs0,width1,outs1){
    log.debug("trim calculation",webWidth,width0,outs0,width1,outs1)
    
    var ret = parseInt(webWidth)

    log.debug("trim",ret)
    
    if(width0 !== 'undefined' && outs0 !== 'undefined'){
        ret -= parseInt(width0)*parseInt(outs0)
    }

    log.debug("trim",ret)
    
    if(width1 !== 'undefined' && outs1 !== 'undefined'){
        ret -= parseInt(width1)*parseInt(outs1)
    }
    
    log.debug("trim",ret)
    
    return ret
}

var mac2syncro = async function(mac,papers,quality,flute){
    var ret
    const delivery = "1 01 01 0001 08 00 00 0"
    
    const quality_row = await quality.quality_params_by_id(mac[3])
    const flute_row = await flute.flute_params_by_id('B')
    
    const qualityWeight = quality_row.rows[0].weightfactoruwg_m      //635
    const qualityThickness = quality_row.rows[0].thickness_uddmm     //45 
    const qualityGap = quality_row.rows[0].scoregap_uddmm            //30
    
    console.log(mac)
    const trim = calculate_trim(mac[5],mac[11],mac[20],mac[26],mac[35])
    /**	
     * HEADER
    */
    var header = syncro_header(mac[2],mac[5],0,qualityGap,mac[3],'B',qualityWeight,qualityThickness,trim)
    
    /**
     * ORDER 0  
     */
    var order0 = syncro_order(mac[10],delivery,mac[8],mac[11],mac[20],scorer(mac[12],mac[13],mac[14],mac[15]),"A","2"," "," ","3",mac[18],mac[19],parseInt(mac[17])/2,1,0,84)
   
    /**
     * ORDER 1
     */
    var order1 = (mac[25].trim() == "") ? syncro_order(" ",delivery," "," "," "," "," "," "," "," "," "," "," "," "," "," ") : syncro_order(mac[25],delivery,mac[23],mac[26],mac[35],scorer(mac[27],mac[28],mac[29],mac[30]),"A","2"," "," ","1",mac[33],mac[34],parseInt(mac[32])/2,1,0,84)
    
    /**
     * ORDER 2
     */
    var order2 = syncro_order(" ",delivery," "," "," ","0"," "," "," "," "," "," "," "," "," "," "," ")
    
    ret = header.concat(order0)
    ret = ret.concat(order1)
    ret = ret.concat(order2)
    
    //ret = syncro_default_order()
    
    return ret  
}

var fields = ["program number", "scheduled papers", "fosber ID", "flute", "flute type", "rollsize", "linear meters", "G", "customer name 1", "customer code 1", "order number 1", "sheet width 1", "1 score", "2 score", "3 score", "4 score", "5 score", "sheets per stack 1", "sheet length 1", "quantity of sheets 1", "outs 1", "linear meters 1", "20", "customer name 2", "customer code 2", "order number 2", "sheet width 2", "1 score", "2 score", "3 score", "4 score", "5 score", "sheets per stack 2", "sheet length 2", "quantity of sheets 2", "outs 2", "linear meters 2", "10"]

var orders_all = async function(){
    const cmd = 'Orders.Get'
    
    try{
        var xmlbuff
        const psocket = new PromiseSocket()
        await psocket.connect(port,host)
        result = await psocket.write(cmd)
        if(result != cmd.length){
            log.error('Command send failed',new Error())
            return null
        }
        xmlbuff = await psocket.readAll()
        xmlbuff = xmlbuff.toString()
        let jml = parser.parse(xmlbuff)
            
        return (Array.isArray(jml.NewDataSet.DryEndList)) ? jml.NewDataSet.DryEndList : [ jml.NewDataSet.DryEndList ]
    }catch(error){
        log.error('Failed to obtain orders',error)
        return null
    }
}

var orders_delete_all = async function(){
    const cmd = 'Orders.Delete'
    try{
        const psocket = new PromiseSocket()
        await psocket.connect(port,host)
        var result = await psocket.write(cmd)
        if(result != cmd.length){
            log.error('Command send failed',new Error())
            return null
        }
        result = await psocket.readAll()
        result = result.toString()
        return result.toLowerCase() == 'ok'
    }catch(error){
        log.error('Failed to delete orders',error)
        return null
    }
    
}

var orders_add = async function(data){
    try{
        let cmd = m2s_serialize(data)
        const psocket = new PromiseSocket()
        await psocket.connect(port,host)
        result = await psocket.write(cmd)
        if(result != cmd.length){
            log.error('Failed to send command to syncro', new Error())
            return null
        }
        result = await psocket.readAll()
        result = result.toString()
        return result.toLowerCase() == "ok"
    }catch(error){
        log.error('Failed to add order',error)
        return null
    }
}

module.exports = {
    fields,
    mac_parse,
    m2s_serialize,
    mac2syncro,
    orders_all,
    orders_delete_all,
    orders_add
}
