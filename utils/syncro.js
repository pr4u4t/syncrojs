const { PromiseSocket } = require('promise-socket');
const config = require('config');

const port = config.get('syncro.port');
const host = config.get('syncro.host');
const connectTimeout = config.get('syncro.connect_timeout');
const operationTimeout = config.get('syncro.timeout');

const connect = async function(){
    const socket = new PromiseSocket();
    let timer;

    try{
        await Promise.race([
            socket.connect(port, host),
            new Promise((resolve, reject) => {
                timer = setTimeout(() => reject(new Error('Syncro connection timed out')), connectTimeout);
            })
        ]);
        socket.setTimeout(operationTimeout);
        return socket;
    }catch(error){
        socket.stream.destroy();
        throw error;
    }finally{
        clearTimeout(timer);
    }
};

module.exports = {
    connect
};