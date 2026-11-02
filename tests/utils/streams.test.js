const { EventEmitter } = require('node:events');

jest.mock('../../models/status', () => ({
    database: jest.fn(),
    syncro: jest.fn(),
    stag: jest.fn()
}));
jest.mock('../../models/home', () => ({ home_info: jest.fn() }));
jest.mock('../../models/operator-status', () => ({ operator_status: jest.fn() }));

const statusModel = require('../../models/status');
const home = require('../../models/home');
const operatorStatus = require('../../models/operator-status');
const statusStream = require('../../utils/status-stream');
const dashboardStream = require('../../utils/dashboard-stream');

const makeRequest = () => new EventEmitter();
const makeResponse = () => {
    const response = {
        writes: [],
        status: jest.fn(function(){ return this; }),
        set: jest.fn(function(){ return this; }),
        flushHeaders: jest.fn(),
        write: jest.fn(function(message){
            this.writes.push(message);
            return true;
        }),
        destroyed: false,
        writableEnded: false
    };
    return response;
};

const flushPromises = async () => {
    await Promise.resolve();
    await Promise.resolve();
    await Promise.resolve();
};

afterEach(() => {
    jest.clearAllTimers();
    jest.useRealTimers();
    jest.clearAllMocks();
});

test('status stream sends initial state, heartbeats, and removes closed clients', () => {
    jest.useFakeTimers();
    const req = makeRequest();
    const res = makeResponse();

    statusStream.attach(req, res);

    expect(res.status).toHaveBeenCalledWith(200);
    expect(res.set).toHaveBeenCalledWith(expect.objectContaining({ 'Content-Type': 'text/event-stream' }));
    expect(res.flushHeaders).toHaveBeenCalled();
    expect(res.writes[0]).toBe('retry: 3000\n\n');
    expect(res.writes[1]).toContain('"application":true');

    jest.advanceTimersByTime(15000);
    expect(res.writes).toContain(': keep-alive\n\n');

    req.emit('close');
    jest.advanceTimersByTime(15000);
    expect(res.writes.filter((message) => message === ': keep-alive\n\n')).toHaveLength(1);
});

test('status stream polls services and publishes an updated snapshot', async () => {
    jest.useFakeTimers();
    statusModel.database.mockResolvedValue(true);
    statusModel.syncro.mockResolvedValue(false);
    statusModel.stag.mockResolvedValue(true);
    const req = makeRequest();
    const res = makeResponse();
    statusStream.attach(req, res);

    statusStream.start();
    await flushPromises();

    expect(statusModel.database).toHaveBeenCalledTimes(1);
    expect(statusModel.syncro).toHaveBeenCalledTimes(1);
    expect(statusStream.snapshot()).toMatchObject({ database: true, syncro: false, stag: true, application: true });
    expect(res.writes.at(-1)).toContain('"database":true');
    req.emit('close');
});

test('dashboard stream publishes live snapshots and retains last good data on failures', async () => {
    jest.useFakeTimers();
    home.home_info.mockResolvedValueOnce({ Speed: 1200 }).mockResolvedValueOnce(null);
    operatorStatus.operator_status.mockResolvedValueOnce({ readyToRun: true }).mockResolvedValueOnce(null);
    const req = makeRequest();
    const res = makeResponse();

    dashboardStream.attach(req, res);
    await flushPromises();

    expect(home.home_info).toHaveBeenCalledTimes(1);
    expect(operatorStatus.operator_status).toHaveBeenCalledTimes(1);
    expect(res.writes.at(-1)).toContain('"Speed":1200');
    expect(res.writes.at(-1)).toContain('"readyToRun":true');

    jest.advanceTimersByTime(1000);
    await flushPromises();
    expect(res.writes.at(-1)).toContain('"unavailable":true');
    expect(res.writes.at(-1)).toContain('"Speed":1200');
    expect(res.writes.at(-1)).toContain('"operatorStatusUnavailable":true');

    req.emit('close');
    expect(res.writes.filter((message) => message === ': keep-alive\n\n')).toHaveLength(0);
});

describe('STAG stream', () => {
    const stagModel = require('../../models/stag');
    const stagStream = require('../../utils/stag-stream');

    test('tells the page once when STAG does not answer, and then the data when it does', async () => {
        jest.useFakeTimers();
        const spy = jest.spyOn(stagModel, 'stag_status').mockResolvedValue(null);
        const req = makeRequest();
        const res = makeResponse();
        stagStream.attach(req, res);
        await flushPromises();

        const messages = () => res.writes.filter((message) => message.startsWith('data: ')).map((message) => JSON.parse(message.slice(6)));
        expect(messages()[0]).toMatchObject({ loading: true, data: null, unavailable: false });
        expect(messages().at(-1)).toMatchObject({ loading: false, data: null, unavailable: true });

        const count = messages().length;
        jest.advanceTimersByTime(1000);
        await flushPromises();
        expect(messages()).toHaveLength(count);

        spy.mockResolvedValue({ speed: '120' });
        jest.advanceTimersByTime(1000);
        await flushPromises();
        expect(messages().at(-1)).toMatchObject({ loading: false, data: { speed: '120' }, unavailable: false });

        req.emit('close');
        spy.mockRestore();
        jest.useRealTimers();
    });
});
