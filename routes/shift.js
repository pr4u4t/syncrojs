const Router = require('express-promise-router')
const db = require('../db')
const config = require('config');
const port = config.get('syncro.port');
const host = config.get('syncro.host');
const template = require('../templates')
const router = new Router()
const multer  = require('multer')
const upload = multer({ dest: './uploads/' })
const { XMLParser } = require("fast-xml-parser")
const parser = new XMLParser()
const fs = require('fs')
var net = require('net')

module.exports = router

router.get('/', async (req,res) => {
    var client = new net.Socket()
    var xmlbuff = ''
    client.connect(port,host,function(){
        client.write('Shift.List')
    })  
    client.on('data', function(data){
        xmlbuff += data.toString();
    })  
    client.on('close', function(){
        let jml = parser.parse(xmlbuff)
        template.render('shift.twig', {
            'rows': (Array.isArray(jml.NewDataSet.ShiftList)) ? jml.NewDataSet.ShiftList : [ jml.NewDataSet.ShiftList ], 
            'fields': [], 
            'total': 1,  
            'current': 1,
            'url': '/shift/'
        }).then((output) => {
            res.end(output);
        })
    })
})
