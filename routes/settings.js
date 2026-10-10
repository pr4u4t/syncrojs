const Router = require('express-promise-router');
const config = require('config');
const logger = require('../utils/logger');
const sendAppShell = require('../utils/app-shell');
const multer  = require('multer');
const upload = multer({ dest: './uploads/' });
const cache = require('../utils/cache');
const appSettings = require('../models/app-settings');
const orderModel = require('../models/order');
const router = new Router();

module.exports = router;

const traverse_config = function(conf,ret,path){
    switch(typeof conf){ 
        case "undefined":
            return;
    
        case "function":
            return;
    
        case "object":
            for(const [key,value] of Object.entries(conf)){
                const fieldPath = [...path, key].join('.');
                if (fieldPath === 'server.secret' || appSettings.isDatabaseSetting(fieldPath)) {
                    continue;
                }
                path.push(key);
                traverse_config(value,ret,path);
                path.pop();
            }
            return;
            
        default:
            ret.push({ path: path.join('.'), conf, type: typeof conf });
    }
}

const config_to_fields = function(conf){
    var ret = [];
    var path = [];
    traverse_config(conf,ret,path);
    return ret;
}

const config_to_groups = function(fields){
    const groups = new Map();

    for(const field of fields){
        const section = field.path.split('.')[0];
        if(!groups.has(section)){
            groups.set(section, {
                id: `settings-${section}`,
                title: section.charAt(0).toUpperCase() + section.slice(1),
                fields: []
            });
        }
        groups.get(section).fields.push(field);
    }

    return Array.from(groups.values());
}

const config_set = function(obj, path, value){
    var schema = obj;  // a moving reference to internal objects within obj
    var pList = path.split('.');
    var len = pList.length;
        
    for(var i = 0; i < len-1; i++) {
        var elem = pList[i];
        if( !schema[elem] ){
            schema[elem] = {};
        }
        
        schema = schema[elem];
    }
        
    schema[pList[len-1]] = value;
}

router.get('/', async (req,res) => {
    sendAppShell(req,res);
});

router.get('/data', async (req,res) => {
    const fields = config_to_fields(config);
    const parserField = fields.find((field) => field.path === 'orders.parser.type');
    if(parserField){
        if(parserField.conf === 'extended_outs'){
            parserField.conf = 'extended_orders';
        }
        parserField.options = (await orderModel.available_backends()).map((file) => file.replace(/\.js$/, ''));
    }

    res.set('Cache-Control', 'no-store').json({
        groups: config_to_groups(fields),
        cacheCleared: req.query.cache === 'cleared',
        settingsSaved: req.query.saved === '1'
    });
});

router.post('/flush-cache', upload.none(), async (req,res) => {
    cache.flushAll();
    res.redirect('/settings?cache=cleared');
});

router.post('/save', upload.none(), async (req,res) => {
    const allowedFields = new Map(config_to_fields(config).map((field) => [field.path, field]));
    const values = {};
    const availableParsers = (await orderModel.available_backends()).map((file) => file.replace(/\.js$/, ''));

    for(const [path,value] of Object.entries(req.body)){
        const field = allowedFields.get(path);
        if(!field || appSettings.isDatabaseSetting(path)){
            continue;
        }

        if(field.type === 'boolean'){
            if(value !== 'true' && value !== 'false'){
                continue;
            }
            values[path] = value === 'true';
        }else if(field.type === 'number'){
            const number = Number(value);
            if(!Number.isFinite(number)){
                continue;
            }
            values[path] = number;
        }else if(path === 'orders.parser.type'){
            if(!availableParsers.includes(value)){
                continue;
            }
            values[path] = value;
        }else{
            values[path] = value;
        }
    }

    await appSettings.save(values);
    if(values['orders.parser.type'] && values['orders.parser.type'] !== orderModel.current_backend()){
        await orderModel.switch_backend(values['orders.parser.type']);
    }
    config.util.extendDeep(config, appSettings.toNestedObject(values));
    res.redirect('/settings?saved=1');
});
