const config = require('config');
const logger = require('../utils/logger');
const backend = config.get("parser.type") || "classic";

const choose = function(back = null){
    logger.debug("Attempting to choose orders backend");
    var ret = null;

    if(back == null){
        logger.debug('backend parameter is null using config value');
        back = backend;
    }

    switch(back){
        case 'classic':
            logger.debug('selecting classic parser');
            ret = require('./orders/classic');
            break;

        case 'extended_outs':
            logger.debug('selecting extended outs parser');
            ret = require('./orders/extended_outs');
            break;

        default:
            logger.debug('no valid parser configured, defaulting to classic');
            ret = require('./orders/classic');
    }

    return ret;
};

var modexp = choose();
modexp['chooser'] = choose;

module.exports = modexp;