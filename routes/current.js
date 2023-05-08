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
module.exports = router

router.get('/', async (req,res) => {
    var net = require('net')
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
    console.log(req.file, req.body)
})
