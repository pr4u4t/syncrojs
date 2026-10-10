const { EventEmitter } = require('node:events');

jest.mock('net', () => ({ createConnection: jest.fn() }));

const net = require('net');
const { generateDatFromCsv } = require('../../utils/classic-generator');
const expectedLength = 59040;

const createSocket = () => {
    const socket = new EventEmitter();
    socket.setTimeout = jest.fn((timeout, callback) => { socket.timeoutCallback = callback; });
    socket.end = jest.fn();
    socket.destroy = jest.fn();
    return socket;
};

const createFrame = (payload, status = 0, declaredLength = payload.length) => {
    const header = Buffer.alloc(5);
    header[0] = status;
    header.writeUInt32BE(declaredLength, 1);
    return Buffer.concat([header, payload]);
};

beforeEach(() => {
    jest.clearAllMocks();
});

test('sends the configured trim and CSV frame and accepts a complete DAT response', async () => {
    const socket = createSocket();
    net.createConnection.mockReturnValue(socket);
    const result = generateDatFromCsv('order,quantity\nA1,2', { socketPath: '/tmp/generator.sock', maxTrimTenthsMm: 1750 });

    expect(net.createConnection).toHaveBeenCalledWith('/tmp/generator.sock');
    expect(socket.setTimeout).toHaveBeenCalledWith(30000, expect.any(Function));
    socket.emit('connect');
    expect(socket.end.mock.calls[0][0].toString()).toBe('1750\norder,quantity\nA1,2');

    const dat = 'D'.repeat(expectedLength);
    const frame = createFrame(Buffer.from(dat));
    socket.emit('data', frame.subarray(0, 3));
    socket.emit('data', frame.subarray(3));
    socket.emit('end');

    await expect(result).resolves.toBe(dat);
});

test('rejects invalid options and requests exceeding the CSV size limit before connecting', async () => {
    await expect(generateDatFromCsv('csv', { maxTrimTenthsMm: -1 })).rejects.toMatchObject({ code: 'CLASSIC_GENERATOR_INVALID_OPTION' });
    await expect(generateDatFromCsv('csv', { maxTrimTenthsMm: 1.5 })).rejects.toMatchObject({ code: 'CLASSIC_GENERATOR_INVALID_OPTION' });
    await expect(generateDatFromCsv('x'.repeat(8 * 1024 * 1024 + 1))).rejects.toMatchObject({ code: 'CLASSIC_GENERATOR_REQUEST_TOO_LARGE' });
    expect(net.createConnection).not.toHaveBeenCalled();
});

test('maps missing socket errors and timeouts to stable error codes', async () => {
    const unavailableSocket = createSocket();
    net.createConnection.mockReturnValueOnce(unavailableSocket);
    const unavailable = generateDatFromCsv('csv');
    const missing = Object.assign(new Error('missing'), { code: 'ENOENT' });
    unavailableSocket.emit('error', missing);
    await expect(unavailable).rejects.toMatchObject({ code: 'CLASSIC_GENERATOR_UNAVAILABLE' });
    expect(unavailableSocket.destroy).toHaveBeenCalled();

    const timedOutSocket = createSocket();
    net.createConnection.mockReturnValueOnce(timedOutSocket);
    const timedOut = generateDatFromCsv('csv', { timeoutMs: 250 });
    timedOutSocket.timeoutCallback();
    await expect(timedOut).rejects.toMatchObject({ code: 'CLASSIC_GENERATOR_TIMEOUT' });
    expect(timedOutSocket.setTimeout).toHaveBeenCalledWith(250, expect.any(Function));
    expect(timedOutSocket.destroy).toHaveBeenCalled();
});

test.each([
    ['short header', Buffer.from([0, 0, 0, 0])],
    ['invalid payload length', createFrame(Buffer.from('abc'), 0, 4)],
    ['generator rejection', createFrame(Buffer.from('bad CSV'), 1)],
    ['short DAT', createFrame(Buffer.from('short'))],
    ['invalid UTF-8 DAT', createFrame(Buffer.concat([Buffer.from([0xff]), Buffer.alloc(expectedLength - 1, 0x44)]))]
])('rejects malformed response: %s', async (name, frame) => {
    const socket = createSocket();
    net.createConnection.mockReturnValue(socket);
    const result = generateDatFromCsv('csv');
    socket.emit('data', frame);
    socket.emit('end');

    await expect(result).rejects.toMatchObject({ code: name === 'generator rejection' ? 'CLASSIC_GENERATOR_REJECTED' : 'CLASSIC_GENERATOR_PROTOCOL' });
});

test('rejects responses that exceed the protocol limit and ignores duplicate errors after settlement', async () => {
    const socket = createSocket();
    net.createConnection.mockReturnValue(socket);
    const result = generateDatFromCsv('csv');
    socket.emit('data', Buffer.alloc(5 + expectedLength * 4 + 4097));
    await expect(result).rejects.toMatchObject({ code: 'CLASSIC_GENERATOR_PROTOCOL' });
    expect(socket.destroy).toHaveBeenCalledTimes(1);
    socket.emit('error', new Error('late error'));
});
