const home = require('../models/home');
const operatorStatus = require('../models/operator-status');
const logger = require('./logger');

const clients = new Set();
const pollIntervalMs = 1000;
let interval = null;
let polling = false;
let current = {
    loading: true,
    data: null,
    unavailable: false,
    operatorStatus: null,
    operatorStatusUnavailable: false,
    updatedAt: null
};

const publish = function(){
    const message = `data: ${JSON.stringify(current)}\n\n`;
    for(const client of clients){
        if(client.destroyed || client.writableEnded){
            clients.delete(client);
            continue;
        }
        try{
            client.write(message);
        }catch(error){
            logger.warn({error}, 'Unable to publish dashboard update');
            clients.delete(client);
        }
    }
};

const poll = async function(){
    if(polling || clients.size === 0){
        return;
    }
    polling = true;
    try{
        const [data, status] = await Promise.all([
            home.home_info(),
            operatorStatus.operator_status()
        ]);
        const previous = current;
        const next = {
            loading: false,
            data: data === null ? current.data : data,
            unavailable: data === null,
            operatorStatus: status === null ? current.operatorStatus : status,
            operatorStatusUnavailable: status === null,
            updatedAt: data === null ? current.updatedAt : new Date().toISOString()
        };
        current = next;
        const statusChanged = previous.operatorStatusUnavailable !== next.operatorStatusUnavailable
            || JSON.stringify(previous.operatorStatus) !== JSON.stringify(next.operatorStatus);
        if(data !== null || previous.unavailable !== next.unavailable || statusChanged){
            publish();
        }
    }catch(error){
        logger.warn({error}, 'Dashboard update poll failed');
    }finally{
        polling = false;
    }
};

const start = function(){
    if(interval !== null){
        return;
    }
    void poll();
    interval = setInterval(() => void poll(), pollIntervalMs);
};

const stop = function(){
    if(interval !== null){
        clearInterval(interval);
        interval = null;
    }
};

const attach = function(req, res){
    res.status(200).set({
        'Content-Type': 'text/event-stream',
        'Cache-Control': 'no-cache, no-transform',
        'Connection': 'keep-alive',
        'X-Accel-Buffering': 'no'
    });
    res.flushHeaders();
    res.write('retry: 3000\n\n');
    clients.add(res);
    res.write(`data: ${JSON.stringify(current)}\n\n`);
    start();

    const heartbeat = setInterval(() => {
        if(!res.destroyed && !res.writableEnded){
            res.write(': keep-alive\n\n');
        }
    }, 15000);

    req.on('close', () => {
        clearInterval(heartbeat);
        clients.delete(res);
        if(clients.size === 0){
            stop();
        }
    });
};

module.exports = {
    attach
};
