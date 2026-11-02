const stag = require('../models/stag');
const logger = require('./logger');

const clients = new Set();
const pollIntervalMs = 1000;
let interval = null;
let polling = false;
let current = { loading: true, data: null, unavailable: false, updatedAt: null };

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
            logger.warn({error}, 'Unable to publish STAG update');
            clients.delete(client);
        }
    }
};

// Apart from the dashboard stream: a STAG that does not answer must not delay the data from Syncro.
const poll = async function(){
    if(polling || clients.size === 0){
        return;
    }
    polling = true;
    try{
        const data = await stag.stag_status();
        const wasUnavailable = current.unavailable;
        current = {
            loading: false,
            data: data === null ? current.data : data,
            unavailable: data === null,
            updatedAt: data === null ? current.updatedAt : new Date().toISOString()
        };
        if(data !== null || !wasUnavailable){
            publish();
        }
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
