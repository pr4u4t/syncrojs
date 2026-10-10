const config = require('config');
const dry = config.get('app.dry');
const logger = require('../../utils/logger');
const { orderFieldsFromMatch } = require('./fixed_width_record');

function isFlatArray(arr) {
    return Array.isArray(arr) && arr.every(item => !Array.isArray(item));
  }

const mac_parse = function(data, parent){
    logger.debug("classic: mac_parse");
    var rows = [];
    const re = /(.{587})/g
    // Capture indexes match the returned row and models/order.js fields list.
    // Header: 0 program number (11), 1 scheduled papers (44), 2 Fosber ID (12),
    // 3 quality ID (36), 4 flute ID (23), 5 web width (5), 6 linear meters (10), 7 G (1).
    // Order 1: 8 customer name (15), 9 customer code (12), 10 order number (30),
    // 11 sheet width (5), 12-16 scores 1-5 (5 each), 17 sheets per stack (4),
    // 18 sheet length (5), 19 quantity of sheets (6), 20 outs (1),
    // 21 linear meters (8), 22 fixed field "20" (2).
    // Order 2: 23 customer name (15), 24 customer code (12), 25 order number (30),
    // 26 sheet width (5), 27-31 scores 1-5 (5 each), 32 sheets per stack (4),
    // 33 sheet length (5), 34 quantity of sheets (6), 35 outs (1),
    // 36 linear meters (8), 37 fixed field "10" (2).
    // The 57- and 56-character fields contain order 3 using the same fixed-width layout.
    // The last two 8-character fields are reserved and retained unchanged.
    const rec = /(.{11})(.{44})(.{12})(.{36})(.{23})(.{5})(.{10})(.{1})(.{15})(.{12})(.{30})(.{5})(.{5})(.{5})(.{5})(.{5})(.{5})(.{4})(.{5})(.{6})(.{1})(.{8})(.{2})(.{15})(.{12})(.{30})(.{5})(.{5})(.{5})(.{5})(.{5})(.{5})(.{4})(.{5})(.{6})(.{1})(.{8})(.{2})(.{57})(.{56})(.{8})(.{8})/
    
    const matches = data.matchAll(re);
    
    for (const match of matches) {
        var str = match[0];
        const r = str.replace(/\s/g, '');
        if(str.length>0 && r.length==0){
            continue;
        }
        
        const row = str.match(rec);
        rows.push(orderFieldsFromMatch(row));
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

    if(ret && Array.isArray(ret.result) && data.length === 55 && data[10] && data[25] && data[40]){
        const orderFields = [0, 1, 2].map((index) => {
            const tokenStart = 9 + index * 31;
            return {
                level: Number(ret.result[tokenStart + 10]),
                sheetLength: Number(ret.result[tokenStart + 11])
            };
        });
        const hasMatchingPair = orderFields.some((order, index) =>
            orderFields.slice(index + 1).some((other) =>
                Number.isFinite(order.level) && Number.isFinite(order.sheetLength) &&
                order.level === other.level && order.sheetLength === other.sheetLength
            )
        );

        if(!hasMatchingPair){
            return {
                ...ret,
                result: null,
                error: 'Przy trzech zamówieniach co najmniej dwa muszą mieć tę samą długość arkusza i poziom noża. Maszyna nie może ciąć trzech niezależnych długości.',
                fieldIndexes: [18, 33, 48]
            };
        }
    }

    return ret;
};

module.exports = {
    mac_parse,
    validate
};