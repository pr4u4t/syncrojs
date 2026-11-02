const net = require('node:net');
const config = require('config');
const logger = require('../utils/logger');

const timeoutMs = 1500;

// Stag.Status of the STAG program; null when it does not answer in time, so that it never holds up anything else.
const stag_status = function(){
    return new Promise((resolve) => {
        const socket = net.createConnection({ host: config.get('stag.host'), port: config.get('stag.port') });
        const chunks = [];
        let settled = false;
        const finish = (value) => {
            if(settled){
                return;
            }
            settled = true;
            clearTimeout(timer);
            socket.destroy();
            resolve(value);
        };
        const timer = setTimeout(() => finish(null), timeoutMs);

        socket.once('connect', () => socket.write('Stag.Status'));
        socket.on('data', (chunk) => chunks.push(chunk));
        socket.once('error', () => finish(null));
        socket.once('close', () => {
            try{
                finish(JSON.parse(Buffer.concat(chunks).toString('utf8')));
            }catch(error){
                logger.warn({error}, 'Invalid Stag.Status response');
                finish(null);
            }
        });
    });
};

module.exports = {
    stag_status
};
