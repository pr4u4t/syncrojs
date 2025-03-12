const config = require('config');
const logger = require('../../utils/logger');

var mac_parse = function(data){
    logger.debug("extended_outs: mac_parse");
    var rows = [];
    const re = /(.{589})/g
                                                                                                                                                  //outs                                                                                       //outs 
    const rec = /(.{11})(.{44})(.{12})(.{36})(.{23})(.{5})(.{10})(.{1})(.{15})(.{12})(.{30})(.{5})(.{5})(.{5})(.{5})(.{5})(.{5})(.{4})(.{5})(.{6})(.{2})(.{8})(.{2})(.{15})(.{12})(.{30})(.{5})(.{5})(.{5})(.{5})(.{5})(.{5})(.{4})(.{5})(.{6})(.{2})(.{8})(.{2})(.{57})(.{56})(.{8})(.{8})/
    
    const matches = data.matchAll(re);
    
    for (const match of matches) {
        var str = match[0];
        const r = str.replace(/\s/g, '');
        if(str.length>0 && r.length==0){
            logger.error("mac_parse: order line empty");
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

var validate = async function(rows, papers, quality, flute){
    var ret = [];
    
    logger.debug("extended_outs: validation started");
    logger.info(rows.length);

    for(let i = 0; i < rows.length; ++i){
        logger.info("extended_outs: validation step");
        let result = await mac2syncro(rows[i], papers, quality, flute);
        if(result === null){
            ret.push(i);
        }
    }

    logger.debug("extended_outs: validation ended");
    
    return ret;
};

module.exports = {
    mac_parse,
    validate
};