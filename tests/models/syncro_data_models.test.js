const { EventEmitter } = require('node:events');

jest.mock('../../db', () => ({ query: jest.fn() }));
jest.mock('node:net', () => ({ createConnection: jest.fn() }));
jest.mock('../../utils/syncro', () => ({ connect: jest.fn() }));

const db = require('../../db');
const net = require('node:net');
const syncro = require('../../utils/syncro');
const home = require('../../models/home');
const operatorStatus = require('../../models/operator-status');
const shift = require('../../models/shift');
const status = require('../../models/status');

const createSocket = ({ written, response, error } = {}) => ({
    write: jest.fn(async (command) => {
        if (error) throw error;
        return written === undefined ? Buffer.byteLength(command) : written;
    }),
    readAll: jest.fn(async () => {
        if (error) throw error;
        return Buffer.from(response === undefined ? 'ok' : response);
    }),
    destroy: jest.fn()
});

beforeEach(() => {
    jest.clearAllMocks();
});

test('database status reports query success and failure', async () => {
    db.query.mockResolvedValue({ rows: [{ '?column?': 1 }] });
    await expect(status.database()).resolves.toBe(true);
    expect(db.query).toHaveBeenCalledWith('SELECT 1');

    db.query.mockRejectedValueOnce(new Error('database unavailable'));
    await expect(status.database()).resolves.toBe(false);
});

test('Syncro TCP status resolves connect, error and timeout outcomes', async () => {
    const socket = new EventEmitter();
    socket.destroy = jest.fn();
    net.createConnection.mockReturnValue(socket);

    const connected = status.syncro();
    socket.emit('connect');
    await expect(connected).resolves.toBe(true);
    expect(socket.destroy).toHaveBeenCalledTimes(1);

    const failedSocket = new EventEmitter();
    failedSocket.destroy = jest.fn();
    net.createConnection.mockReturnValue(failedSocket);
    const failed = status.syncro();
    failedSocket.emit('error', new Error('connection refused'));
    await expect(failed).resolves.toBe(false);
    expect(failedSocket.destroy).toHaveBeenCalledTimes(1);

    jest.useFakeTimers();
    const timeoutSocket = new EventEmitter();
    timeoutSocket.destroy = jest.fn();
    net.createConnection.mockReturnValue(timeoutSocket);
    const timedOut = status.syncro();
    jest.advanceTimersByTime(1500);
    await expect(timedOut).resolves.toBe(false);
    expect(timeoutSocket.destroy).toHaveBeenCalledTimes(1);
    jest.useRealTimers();
});

test('Home.Info parses valid XML and rejects incomplete responses', async () => {
    const socket = createSocket({ response: '<NewDataSet><Home><Speed>1200</Speed></Home></NewDataSet>' });
    syncro.connect.mockResolvedValueOnce(socket);
    await expect(home.home_info()).resolves.toEqual({ Speed: 1200 });
    expect(socket.write).toHaveBeenCalledWith('Home.Info');
    expect(socket.destroy).toHaveBeenCalled();

    syncro.connect.mockResolvedValueOnce(createSocket({ written: 2 }));
    await expect(home.home_info()).resolves.toBeNull();
    syncro.connect.mockResolvedValueOnce(createSocket({ response: '' }));
    await expect(home.home_info()).resolves.toBeNull();
    syncro.connect.mockResolvedValueOnce(createSocket({ response: '<Other />' }));
    await expect(home.home_info()).resolves.toBeNull();
    syncro.connect.mockRejectedValueOnce(new Error('offline'));
    await expect(home.home_info()).resolves.toBeNull();
});

test('Syncro version requests return trimmed versions and tolerate failures', async () => {
    const sockets = [
        createSocket({ response: ' 1.2.3\n' }),
        createSocket({ response: '' }),
        createSocket({ error: new Error('offline') })
    ];
    for (const socket of sockets) syncro.connect.mockResolvedValueOnce(socket);

    await expect(home.syncro_versions()).resolves.toEqual({ syncro: '1.2.3', data: null, ite: null });
    for (const socket of sockets) expect(socket.destroy).toHaveBeenCalled();

    syncro.connect.mockRejectedValue(new Error('offline'));
    await expect(home.syncro_versions()).resolves.toBeNull();
});

test('operator status maps scalar fields, active alert details and machine records', async () => {
    const xml = '<OperatorStatus><LineSpeed>1450</LineSpeed><DryProgressPercent>42</DryProgressPercent><Simulation>FALSE</Simulation><ReadyToRun>true</ReadyToRun><Machines><Machine><Name>Knife</Name><Configured>true</Configured><ReadyToRun>true</ReadyToRun><ReadyToAOC>false</ReadyToAOC><Alarmed>false</Alarmed><Warned>true</Warned><InEmergency>false</InEmergency></Machine><Machine><Name>Stacker</Name><Alarmed>true</Alarmed></Machine></Machines><ActiveAlerts><Alert><Machine>KN</Machine><Code>KN_42</Code><Severity>Warning</Severity><Zone>Knife</Zone><Description>Knife guard open</Description><StartedAt>2026-10-10T10:00:00</StartedAt></Alert><Alert><Machine>STK1</Machine><Code>STK1_7</Code><Severity>Alarm</Severity><Zone>Stacker 1</Zone><Description>Stacker stop</Description><StartedAt>2026-10-10T10:01:00</StartedAt></Alert></ActiveAlerts></OperatorStatus>';
    const socket = createSocket({ response: xml });
    syncro.connect.mockResolvedValueOnce(socket);

    await expect(operatorStatus.operator_status()).resolves.toMatchObject({
        lineSpeed: 1450,
        dryProgressPercent: 42,
        simulation: false,
        readyToRun: true,
        readyToAoc: false,
        activeAlerts: [
            { machine: 'KN', code: 'KN_42', severity: 'Warning', zone: 'Knife', description: 'Knife guard open', startedAt: '2026-10-10T10:00:00' },
            { machine: 'STK1', code: 'STK1_7', severity: 'Alarm', zone: 'Stacker 1', description: 'Stacker stop', startedAt: '2026-10-10T10:01:00' }
        ],
        machines: [
            { name: 'Knife', configured: true, readyToRun: true, alarmed: false, warned: true },
            { name: 'Stacker', alarmed: true }
        ]
    });
    expect(socket.destroy).toHaveBeenCalled();

    syncro.connect.mockResolvedValueOnce(createSocket({ response: '<OperatorStatus><Machines><Machine><Name>Knife</Name></Machine></Machines></OperatorStatus>' }));
    await expect(operatorStatus.operator_status()).resolves.toMatchObject({ machines: [{ name: 'Knife' }] });
});

test.each([
    createSocket({ written: 1 }),
    createSocket({ response: '' }),
    createSocket({ response: 'error' }),
    createSocket({ response: '<Other />' })
])('operator status returns null for invalid responses', async (socket) => {
    syncro.connect.mockResolvedValueOnce(socket);
    await expect(operatorStatus.operator_status()).resolves.toBeNull();
    expect(socket.destroy).toHaveBeenCalled();
});

test('Shift.List parses and reverses shift rows, and Shift.State returns raw state', async () => {
    const listSocket = createSocket({ response: '<NewDataSet><ShiftList><ID>1</ID></ShiftList><ShiftList><ID>2</ID></ShiftList></NewDataSet>' });
    syncro.connect.mockResolvedValueOnce(listSocket);
    await expect(shift.shift_all()).resolves.toEqual([{ ID: 2 }, { ID: 1 }]);

    const stateSocket = createSocket({ response: 'working' });
    syncro.connect.mockResolvedValueOnce(stateSocket);
    await expect(shift.shift_state()).resolves.toBe('working');
});

test('Shift commands acknowledge stop and new shift requests', async () => {
    const stopSocket = createSocket({ response: 'OK' });
    syncro.connect.mockResolvedValueOnce(stopSocket);
    await expect(shift.shift_end()).resolves.toBe(true);
    expect(stopSocket.write).toHaveBeenCalledWith('Shift.Stop()');

    const newSocket = createSocket({ response: 'ok' });
    syncro.connect.mockResolvedValueOnce(newSocket);
    await expect(shift.shift_new('12', 'Manager', '4')).resolves.toBe(true);
    expect(newSocket.write).toHaveBeenCalledWith('Shift.Shift(12\u241eManager\u241e4)');

    const rejectedSocket = createSocket({ response: 'rejected' });
    syncro.connect.mockResolvedValueOnce(rejectedSocket);
    await expect(shift.shift_new('12', 'Manager', '4')).resolves.toBe(false);
});

test('Shift query errors return null and shift count reflects loaded rows', async () => {
    syncro.connect.mockResolvedValueOnce(createSocket({ written: 1 }));
    await expect(shift.shift_all()).resolves.toBeNull();

    syncro.connect.mockRejectedValueOnce(new Error('offline'));
    await expect(shift.shift_state()).resolves.toBeNull();

    syncro.connect.mockRejectedValueOnce(new Error('offline'));
    await expect(shift.shift_end()).resolves.toBeNull();

    syncro.connect.mockRejectedValueOnce(new Error('offline'));
    await expect(shift.shift_new('12', 'Manager', '4')).resolves.toBeNull();

    syncro.connect.mockResolvedValueOnce(createSocket({ response: '<NewDataSet><ShiftList><ID>1</ID></ShiftList></NewDataSet>' }));
    await expect(shift.shift_count()).resolves.toBe(1);
});
