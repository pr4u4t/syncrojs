const net = require('node:net');
const config = require('config');
const db = require('../db');


const database = async function(){
    try{
        await db.query('SELECT 1');
        return true;
    }catch{
        return false;
    }
};

const reachable = function(host, port){
    return new Promise((resolve) => {
        const socket = net.createConnection({ host, port });
        let settled = false;
        const finish = (connected) => {
            if(settled){
                return;
            }
            settled = true;
            clearTimeout(timer);
            socket.destroy();
            resolve(connected);
        };
        const timer = setTimeout(() => finish(false), 1500);

        socket.once('connect', () => finish(true));
        socket.once('error', () => finish(false));
    });
};

const syncro = () => reachable(config.get('syncro.host'), config.get('syncro.port'));
const stag = () => reachable(config.get('stag.host'), config.get('stag.port'));

module.exports = {
    database,
    syncro,
    stag
};
