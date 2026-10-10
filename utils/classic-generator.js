'use strict';

const net = require('net');

const RESPONSE_HEADER_SIZE = 5;
const EXPECTED_DAT_CHARS = 59040;
const MAX_CSV_BYTES = 8 * 1024 * 1024;
const DEFAULT_SOCKET_PATH = '/run/syncrojs/classic-generator.sock';

function createError(message, code){
    const error = new Error(message);
    error.code = code;
    return error;
}

const generateDatFromCsv = function(csv, options = {}){
    const csvBytes = Buffer.isBuffer(csv) ? csv : Buffer.from(csv, 'utf8');
    if(csvBytes.length > MAX_CSV_BYTES){
        return Promise.reject(createError('CSV request exceeds 8 MiB', 'CLASSIC_GENERATOR_REQUEST_TOO_LARGE'));
    }
    const maxTrimTenthsMm = options.maxTrimTenthsMm ?? 2300;
    if(!Number.isSafeInteger(maxTrimTenthsMm) || maxTrimTenthsMm < 0 || maxTrimTenthsMm > 0xffffffff){
        return Promise.reject(createError('Invalid max-trim setting', 'CLASSIC_GENERATOR_INVALID_OPTION'));
    }
    const requestFrame = Buffer.concat([Buffer.from(`${maxTrimTenthsMm}\n`, 'ascii'), csvBytes]);

    return new Promise((resolve,reject) => {
        const socket = net.createConnection(options.socketPath || process.env.CLASSIC_GENERATOR_SOCKET || DEFAULT_SOCKET_PATH);
        const chunks = [];
        let responseBytes = 0;
        let settled = false;

        const fail = (error) => {
            if(settled){
                return;
            }
            settled = true;
            socket.destroy();
            reject(error);
        };

        socket.setTimeout(options.timeoutMs || 30000, () => {
            fail(createError('Classic generator socket timed out', 'CLASSIC_GENERATOR_TIMEOUT'));
        });

        socket.once('connect', () => socket.end(requestFrame));
        socket.on('data', (chunk) => {
            responseBytes += chunk.length;
            if(responseBytes > RESPONSE_HEADER_SIZE + EXPECTED_DAT_CHARS * 4 + 4096){
                fail(createError('Classic generator response is too large', 'CLASSIC_GENERATOR_PROTOCOL'));
                return;
            }
            chunks.push(chunk);
        });
        socket.once('error', (error) => {
            if(error.code === 'ECONNREFUSED' || error.code === 'ENOENT'){
                fail(createError('Classic generator service is unavailable', 'CLASSIC_GENERATOR_UNAVAILABLE'));
                return;
            }
            fail(error);
        });
        socket.once('end', () => {
            if(settled){
                return;
            }
            settled = true;
            const frame = Buffer.concat(chunks, responseBytes);
            if(frame.length < RESPONSE_HEADER_SIZE){
                reject(createError('Incomplete response from classic generator', 'CLASSIC_GENERATOR_PROTOCOL'));
                return;
            }

            const status = frame[0];
            const payloadLength = frame.readUInt32BE(1);
            const payload = frame.subarray(RESPONSE_HEADER_SIZE);
            if(payload.length !== payloadLength){
                reject(createError('Invalid response length from classic generator', 'CLASSIC_GENERATOR_PROTOCOL'));
                return;
            }
            if(status !== 0){
                reject(createError(payload.toString('utf8'), 'CLASSIC_GENERATOR_REJECTED'));
                return;
            }
            const dat = payload.toString('utf8');
            if(!Buffer.from(dat, 'utf8').equals(payload) || dat.length !== EXPECTED_DAT_CHARS){
                reject(createError(`Expected a valid ${EXPECTED_DAT_CHARS}-character DAT, received ${dat.length} characters`, 'CLASSIC_GENERATOR_PROTOCOL'));
                return;
            }
            resolve(dat);
        });
    });
};

module.exports = {
    generateDatFromCsv
};
