const Router = require('express-promise-router')
const config = require('config')
const template = require('../templates')
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

router.post('/save', async (req,res) => {
    
})
