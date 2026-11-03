const net = require('node:net');
const config = require('config');

jest.mock('config', () => {
    const values = {};
    return { get: (key) => values[key], __set: (key, value) => { values[key] = value; } };
});
jest.mock('../../utils/logger', () => ({ warn: jest.fn() }));

const logger = require('../../utils/logger');
const stag = require('../../models/stag');

const serve = (onData) => new Promise((resolve) => {
    const server = net.createServer((socket) => {
        socket.on('data', (data) => onData(socket, data.toString()));
        socket.on('error', () => {});
    });
    server.listen(0, '127.0.0.1', () => {
        config.__set('stag.host', '127.0.0.1');
        config.__set('stag.port', server.address().port);
        resolve(server);
    });
});

test('Stag.Status sends the command and parses the answer', async () => {
    const received = [];
    const server = await serve((socket, command) => {
        received.push(command);
        socket.end('{"speed":"120"}');
    });
    await expect(stag.stag_status()).resolves.toEqual({ speed: '120' });
    expect(received).toEqual(['Stag.Status']);
    server.close();
});

test('Stag.Status is null for a bad answer and for no program at all', async () => {
    const server = await serve((socket) => socket.end('error'));
    await expect(stag.stag_status()).resolves.toBeNull();
    await new Promise((resolve) => server.close(resolve));
    await expect(stag.stag_status()).resolves.toBeNull();
});

test('Stag.Status gives up on a program that never answers', async () => {
    const server = await serve(() => {});
    const started = Date.now();
    await expect(stag.stag_status()).resolves.toBeNull();
    expect(Date.now() - started).toBeLessThan(3000);
    server.close();
});

test('Stag.Status logs nothing when the program is off or silent', async () => {
    logger.warn.mockClear();
    const silent = await serve(() => {});
    await expect(stag.stag_status()).resolves.toBeNull();
    await new Promise((resolve) => silent.close(resolve));
    await expect(stag.stag_status()).resolves.toBeNull();
    expect(logger.warn).not.toHaveBeenCalled();
});
