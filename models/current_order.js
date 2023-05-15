const { XMLParser } = require("fast-xml-parser")
const parser = new XMLParser()
const fs = require('fs')
const net = require('net')
const db = require('../db')
const {PromiseSocket, TimeoutError} = require("promise-socket")
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
    
    if(sc3 != '0'){
        ret += " "+sc3
    }
    
    if(sc4 != '0'){
        ret += " "+sc4
    }
    
    return ret
}

var mac2syncro = async function(mac,papers,quality,flute){
    var ret = []
    const delivery = "1 01 01 0001 08 00 00 0"
    
    console.log(mac)
    
    const quality_row = await quality.quality_params_by_id(mac[3])
    const flute_row = await flute.flute_params_by_id('B')
    
    const qualityWeight = quality_row.rows[0].weightfactoruwg_m      //635
    const qualityThickness = quality_row.rows[0].thickness_uddmm     //45 
    const qualityGap = quality_row.rows[0].scoregap_uddmm            //30
    
    /**	
     * HEADER
    */
    
    ret.push(mac[2]+'.2')                                   //SetupID glueing SEQ to setup ID
    ret.push(mac[5])                                        //WebWidth
    ret.push(0)                                             //TrimType
    ret.push(qualityGap)                                    //ScoreGap - od rodzaju tektury
    ret.push(mac[3])                                        //QualityID
    ret.push(/*mac[4]*/'B')                                  //FluteID
    ret.push(qualityWeight)                                 //QualityWeight - sprawdzic skad sie to bierze  //
    ret.push(qualityThickness)                              //QualityThickness                              //
    ret.push(2000)                                          //Trim - trzeba zweryfikowac
    
    /**
     * ORDER 0  
     */
    
    ret.push(mac[10])                                       //OrderNumber0
    ret.push(delivery)                                      //Delivery0 - jakas liczba niewiadomego pochodzenia
    ret.push(mac[8])                                        //CustomerName0 
    ret.push(mac[11])                                       //SheetWidth0
    ret.push(mac[20])                                       //Outs0
    ret.push(scorer(mac[12],mac[13],mac[14],mac[15]))       //ScoreMeasures0 roznica 4 i 5
    ret.push("A")                                           //ScoreType0
    ret.push(2)                                             //PositType0
    ret.push(" ")                                           //TeleTwinOffset0 - puste
    ret.push(" ")                                           //ScoreGap0
    ret.push("2")                                           //LevelName0 - dol 2, gora 3
    ret.push(mac[18])                                       //sheetLenght0
    ret.push(mac[19] == '0' ? 1 : mac[19])                  //segmentSheetCount0
    ret.push(mac[17])                                       //sheetsPerStack0
    ret.push(1)                                             //stacksPerPallet0
    ret.push(84)                                            //Sending0
    ret.push("False")
    ret.push(" ") 
    ret.push(" ") 
    ret.push(" ")
    ret.push(" ")
    ret.push(" ")
    ret.push("False")
    ret.push("False")
    ret.push(" ") 
    ret.push(" ")                                           //Address
    ret.push(" ")                                           //Next Machine
    ret.push("0")
    ret.push(84)
    ret.push("False")
    
    /**
     * ORDER 1
     */
    
    ret.push(mac[25])                                       //OrderNumber1
    ret.push(delivery)                                      //Delivery1 - jakas liczba niewiadomego pochodzenia
    ret.push(mac[22])                                       //CustomerName1 
    ret.push(mac[26])                                       //SheetWidth1
    ret.push(mac[35])                                       //Outs1
    ret.push(scorer(mac[27],mac[28],mac[29],mac[30]))        //ScoreMeasures1
    ret.push("A")                                           //ScoreType1
    ret.push(2)                                             //PositType1
    ret.push(" ")                                           //TeleTwinOffset1 - puste
    ret.push(" ")                                           //ScoreGap1
    ret.push(3)                                             //LevelName1 - dol 2, gora 3
    ret.push(mac[33])                                       //sheetLenght1
    ret.push(mac[34])                                       //segmentSheetCount1
    ret.push(mac[32] == '0' ? '1' : mac[32])                //sheetsPerStack1
    ret.push(1)                                             //stacksPerPallet0
    ret.push(84)                                            //Sending0
    ret.push("False")
    ret.push(" ") 
    ret.push(" ") 
    ret.push(" ")
    ret.push(" ")
    ret.push(" ")
    ret.push("False")
    ret.push("False")
    ret.push(" ") 
    ret.push(" ")                                           //Address
    ret.push(" ")                                           //Next Machine
    ret.push("0")
    ret.push(84)
    ret.push("False")
    
    /**
     * ORDER 2
     */
    
    ret.push(" ") 
    ret.push("1 01 01 0001 08 00 00 0")
    ret.push(" ") 
    ret.push(" ") 
    ret.push(" ") 
    ret.push(0)
    ret.push(" ") 
    ret.push(" ") 
    ret.push(" ") 
    ret.push(" ") 
    ret.push(" ") 
    ret.push(" ") 
    ret.push(" ") 
    ret.push(" ") 
    ret.push(" ")                                             //stacksPerPallet0
    ret.push(" ")                                            //Sending0
    ret.push("False")
    ret.push(" ") 
    ret.push(" ") 
    ret.push(" ")
    ret.push(" ")
    ret.push(" ")
    ret.push("False")
    ret.push("False")
    ret.push(" ") 
    ret.push(" ")                                           //Address
    ret.push(" ")                                           //Next Machine
    ret.push(" ")
    ret.push(" ")
    ret.push("False")
    
    return ret  
}

var fields = ["program number", "scheduled papers", "fosber ID", "flute", "flute type", "rollsize", "linear meters", "G", "customer name 1", "customer code 1", "order number 1", "sheet width 1", "1 score", "2 score", "3 score", "4 score", "5 score", "sheets per stack 1", "sheet length 1", "quantity of sheets 1", "outs 1", "linear meters 1", "20", "customer name 2", "customer code 2", "order number 2", "sheet width 2", "1 score", "2 score", "3 score", "4 score", "5 score", "sheets per stack 2", "sheet length 2", "quantity of sheets 2", "outs 2", "linear meters 2", "10"]

var orders_all = async function(){
    var xmlbuff = ''
    const psocket = new PromiseSocket()
    var result = await psocket.connect(port,host)
    result = await psocket.write('Orders.Get')
    xmlbuff = await psocket.readAll()
    xmlbuff = xmlbuff.toString()
    let jml = parser.parse(xmlbuff)
        
    return (Array.isArray(jml.NewDataSet.DryEndList)) ? jml.NewDataSet.DryEndList : [ jml.NewDataSet.DryEndList ]
}

var orders_delete_all = async function(){
    const psocket = new PromiseSocket()
    var result = await psocket.connect(port,host)
    result = await psocket.write('Orders.Delete')
    result = await psocket.readAll()
    result = result.toString()
    
    return result
}

var orders_add = async function(data){
    let cmd = m2s_serialize(data)
    const psocket = new PromiseSocket()
    var result = await psocket.connect(port,host)
    result = await psocket.write(cmd)
    console.log(cmd)
    result = await psocket.readAll()
    result = result.toString()
    return result
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
