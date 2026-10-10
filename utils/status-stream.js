const status = require('../models/status');

const clients = new Set();
const intervalMs = 10000;
let interval;
let polling = false;
let current = { database: false, syncro: false, updatedAt: null };

const snapshot = () => ({ ...current, application: true });

const publish = function(){
    const message = `data: ${JSON.stringify(snapshot())}\n\n`;
    for(const client of clients){
        if(client.destroyed || client.writableEnded){
            clients.delete(client);
            continue;
        }
        try{
            client.write(message);
        }catch{
            clients.delete(client);
        }
    }
};

const poll = async function(){
    if(polling){
        return;
    }
    polling = true;
    try{
        const [database, syncro] = await Promise.all([
            status.database(),
            status.syncro()
        ]);
        current = { database, syncro, updatedAt: new Date().toISOString() };
        publish();
    }finally{
        polling = false;
    }
};

const start = function(){
    if(interval){
        return;
    }
    void poll();
    interval = setInterval(poll, intervalMs);
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
    res.write(`data: ${JSON.stringify(snapshot())}\n\n`);

    const heartbeat = setInterval(() => {
        if(!res.destroyed && !res.writableEnded){
            res.write(': keep-alive\n\n');
        }
    }, 15000);

    req.on('close', () => {
        clearInterval(heartbeat);
        clients.delete(res);
    });
};

module.exports = {
    start,
    attach,
    snapshot
};