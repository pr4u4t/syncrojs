const Router = require('express-promise-router')
const config = require('config')
const template = require('../templates')
const multer  = require('multer')
const upload = multer({ dest: './uploads/' })
const fs = require('fs')
const router = new Router()

module.exports = router

const traverse_config = function(conf,ret,path){
    switch(typeof conf){ 
        case "undefined":
            return
    
        case "function":
            return
    
        case "object":
            for(const [key,value] of Object.entries(conf)){
                path.push(key)
                traverse_config(value,ret,path)
                path.pop()
            }
            return
            
        default:
            ret.push({ path: path.join('.'), conf})
    }
}

const config_to_fields = function(conf){
    var ret = []
    var path = []
    traverse_config(conf,ret,path)
    return ret
}

const config_set = function(obj, path, value){
    var schema = obj  // a moving reference to internal objects within obj
    var pList = path.split('.')
    var len = pList.length
        
    for(var i = 0; i < len-1; i++) {
        var elem = pList[i]
        if( !schema[elem] ){
            schema[elem] = {}
        }
        
        schema = schema[elem];
    }
        
    schema[pList[len-1]] = value;
}

router.get('/', async (req,res) => {
    template.render('settings.twig', {
        'title': "Settings",
        'header_title': "Settings",
        'fields': config_to_fields(config),
        'url': '/settings/'        
    }).then((output) => {
        res.end(output)
    })
})

router.post('/save', upload.none(), async (req,res) => {
    var conf = JSON.parse(JSON.stringify(config));
    
    for(const [key,value] of Object.entries(req.body)){
       config_set(conf,key,value)
    }
    
    let data = JSON.stringify(conf);
    fs.writeFileSync('../config/default.json', data)
    
    res.redirect('/settings')
})
