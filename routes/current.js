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
        client.write('GetOrders')
    })  
    client.on('data', function(data){
        xmlbuff += data.toString();
    })  
    client.on('close', function(){
        let jml = parser.parse(xmlbuff)
        template.render('current.twig', {
            'rows': jml.NewDataSet.DryEndList, 
            'fields': [], 
            'total': 1,  
            'current': 1,
            'url': '/current/'
        }).then((output) => {
            res.end(output);
        })
    })
})

router.get('/add', async (req,res) => {
    template.render('order_add.twig', {
        'url': '/current/'
    }).then((output) => {
        res.end(output);
    })
})

router.post('/add', upload.single('orderFile'), async (req,res) => {
    var rows = []
    var fields = ["program number", "scheduled papers", "fosber ID", "flute", "flute type", "rollsize", "linear meters", "G", "customer name 1", "customer code 1", "order number 1", "sheet width 1", "1 score", "2 score", "3 score", "4 score", "5 score", "sheets per stack 1", "sheet length 1", "quantity of sheets 1", "outs 1", "linear meters 1", "20", "customer name 2", "customer code 2", "order number 2", "sheet width 2", "1 score", "2 score", "3 score", "4 score", "5 score", "sheets per stack 2", "sheet length 2", "quantity of sheets 2", "outs 2", "linear meters 2", "10"]
    
    try {
        const data = fs.readFileSync(req.file.path, 'utf8')
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
        }
    } catch (err) {
        console.log(err)
    }

    req.session.current = rows
    
    template.render('current_confirm.twig', {
        'rows': rows, 
        'fields': fields, 
        'total': 1,  
        'current': 1,
        'url': '/current/add/confirm'
    }).then((output) => {
        res.end(output);
    })
})

router.post('/add/confirm', upload.none(), async (req,res) => {
    if(typeof req.body.accept !== undefined){
        req.session.step = 0
        template.render('current_progress.twig', {
            'next': "/current",
            'txt': "Go to Orders"
        }).then((output) => {
            res.end(output);
        })
    }else{
        req.session.current = null
        
        template.render('current_result.twig', {
            'title': "Adding order list denied",
            'message': "Refused to upload new orders list",
            'next': "/",
            'txt': "Go to Home"
        }).then((output) => {
            res.end(output);
        })
    }
    
    
})

router.get('/add/batch/:step', async (req,res) => {
    var steps = req.session.current.length
    var current = ++req.session.step
    
    if(steps == current){
        req.session.current = null
        req.session.step = null
    }
    
    var client = new net.Socket()

    client.connect(port,host,function(){
        //client.write('GetOrders')
        
    })  
    client.on('data', function(data){
        if(data.toString() != "ok"){
            current = -1
        }
    })  
    client.on('close', function(){
        res.json({ current: current, steps: steps })
    })
})
