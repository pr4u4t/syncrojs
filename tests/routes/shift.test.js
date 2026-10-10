jest.mock('../../models/shift', () => ({
    shift_state: jest.fn(),
    shift_all: jest.fn(),
    shift_new: jest.fn(),
    shift_end: jest.fn()
}));
jest.mock('../../utils/app-shell', () => jest.fn());

const express = require('express');
const shift = require('../../models/shift');
const shiftRoute = require('../../routes/shift');

const app = express();
app.use(express.urlencoded({ extended: false }));
app.use('/shift', shiftRoute);
let server;
let baseUrl;

beforeAll(async () => {
    server = app.listen(0);
    await new Promise((resolve) => server.once('listening', resolve));
    baseUrl = `http://127.0.0.1:${server.address().port}`;
});

afterAll(async () => {
    if (!server) return;
    server.closeAllConnections();
    await new Promise((resolve, reject) => server.close((error) => error ? reject(error) : resolve()));
});

beforeEach(() => {
    jest.clearAllMocks();
});

test('shift info reports status and availability', async () => {
    shift.shift_state.mockResolvedValue('working');
    shift.shift_all.mockResolvedValue([{ ID: 1 }]);

    const response = await fetch(`${baseUrl}/shift/info`, { headers: { Connection: 'close' } });

    expect(response.headers.get('cache-control')).toBe('no-store');
    await expect(response.json()).resolves.toEqual({ state: 'working', rows: [{ ID: 1 }], unavailable: false });
});

test('shift start validates form fields and forwards trimmed manager', async () => {
    const badResponse = await fetch(`${baseUrl}/shift/start`, {
        method: 'POST', redirect: 'manual', headers: { 'Content-Type': 'application/x-www-form-urlencoded', Connection: 'close' },
        body: new URLSearchParams({ ID: '0', manager: ' ', people: '0' })
    });
    expect(badResponse.status).toBe(400);
    expect(await badResponse.json()).toEqual({ success: false, error: 'invalid_shift_details' });
    expect(shift.shift_new).not.toHaveBeenCalled();

    shift.shift_new.mockResolvedValue(true);
    const goodResponse = await fetch(`${baseUrl}/shift/start`, {
        method: 'POST', redirect: 'manual', headers: { 'Content-Type': 'application/x-www-form-urlencoded', Connection: 'close' },
        body: new URLSearchParams({ ID: '3', manager: '  Manager  ', people: '4' })
    });
    expect(goodResponse.status).toBe(200);
    expect(await goodResponse.json()).toEqual({ success: true });
    expect(shift.shift_new).toHaveBeenCalledWith('3', 'Manager', '4');
});

test('shift start reports Syncro failures', async () => {
    shift.shift_new.mockResolvedValue(null);
    const response = await fetch(`${baseUrl}/shift/start`, {
        method: 'POST', redirect: 'manual', headers: { 'Content-Type': 'application/x-www-form-urlencoded', Connection: 'close' },
        body: new URLSearchParams({ ID: '3', manager: 'Manager', people: '4' })
    });
    expect(response.status).toBe(502);
    expect(await response.json()).toEqual({ success: false });
});

test('shift stop returns JSON for API requests and redirects for pages', async () => {
    shift.shift_end.mockResolvedValue(true);
    const apiResponse = await fetch(`${baseUrl}/shift/stop?api=1`, { headers: { Connection: 'close' } });
    expect(apiResponse.status).toBe(200);
    expect(await apiResponse.json()).toEqual({ success: true });

    shift.shift_end.mockResolvedValue(false);
    const pageResponse = await fetch(`${baseUrl}/shift/stop`, { redirect: 'manual', headers: { Connection: 'close' } });
    expect(pageResponse.status).toBe(302);
    expect(pageResponse.headers.get('location')).toBe('/shift/');
});
