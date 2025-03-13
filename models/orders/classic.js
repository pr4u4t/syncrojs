const config = require('config');
const dry = config.get('app.dry');
const logger = require('../../utils/logger');

function isFlatArray(arr) {
    return Array.isArray(arr) && arr.every(item => !Array.isArray(item));
  }

const mac_parse = function(data, parent){
    logger.debug("classic: mac_parse");
    var rows = [];
    const re = /(.{587})/g
    const rec = /(.{11})(.{44})(.{12})(.{36})(.{23})(.{5})(.{10})(.{1})(.{15})(.{12})(.{30})(.{5})(.{5})(.{5})(.{5})(.{5})(.{5})(.{4})(.{5})(.{6})(.{1})(.{8})(.{2})(.{15})(.{12})(.{30})(.{5})(.{5})(.{5})(.{5})(.{5})(.{5})(.{4})(.{5})(.{6})(.{1})(.{8})(.{2})(.{57})(.{56})(.{8})(.{8})/
    
    const matches = data.matchAll(re);
    
    for (const match of matches) {
        var str = match[0];
        const r = str.replace(/\s/g, '');
        if(str.length>0 && r.length==0){
            logger.error("classic: mac_parse: order line empty");
            continue;
        }
        
        var row = str.match(rec);
        rows.push(row.slice(1,43));

        for(let i = 0; i < rows[rows.length-1].length;++i){
            rows[rows.length-1][i] = rows[rows.length-1][i].replace(/  +/g, ' ').trim();
        }
    }
    
    return rows;
};

const validate = async function(data, papers, quality, flute, parent){
    logger.debug("classic: Validation started");
    logger.info(`classic: mac rows: ${data.length}`);

    if(isFlatArray(data) !== true){
        logger.error('invalid data parameter should be flat array containing one order in mac format');
        return false;
    }

    const ret = await parent.mac2syncro(data, papers, quality, flute);

    return ret;
};

module.exports = {
    mac_parse,
    validate
};