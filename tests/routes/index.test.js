jest.mock('../../routes/current', () => (req, res, next) => next());
jest.mock('../../routes/past', () => (req, res, next) => next());
jest.mock('../../routes/papers', () => (req, res, next) => next());
jest.mock('../../routes/quality', () => (req, res, next) => next());
jest.mock('../../routes/flute', () => (req, res, next) => next());
jest.mock('../../routes/shift', () => (req, res, next) => next());
jest.mock('../../routes/settings', () => (req, res, next) => next());
jest.mock('../../utils/app-shell', () => jest.fn((req, res) => res.send('shell')));
jest.mock('../../models/home', () => ({
    home_info: jest.fn(),
    syncro_versions: jest.fn()
}));
jest.mock('../../models/operator-status', () => ({ operator_status: jest.fn() }));
jest.mock('../../utils/status-stream', () => ({
    attach: jest.fn((req, res) => res.status(200).json({ stream: 'status' })),
    snapshot: jest.fn(() => ({ database: true, syncro: false }))
}));
jest.mock('../../utils/dashboard-stream', () => ({
    attach: jest.fn((req, res) => res.status(200).json({ stream: 'dashboard' }))
}));

const express = require('express');
const home = require('../../models/home');
const operatorStatus = require('../../models/operator-status');
const statusStream = require('../../utils/status-stream');
const dashboardStream = require('../../utils/dashboard-stream');
const mountRoutes = require('../../routes');

const app = express();
mountRoutes(app);
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

test('home endpoints return current data and unavailable status', async () => {
    home.home_info.mockResolvedValue({ Speed: 1200 });
    const homeInfo = await fetch(`${baseUrl}/home_info`, { headers: { Connection: 'close' } });
    await expect(homeInfo.json()).resolves.toEqual({ Speed: 1200 });

    const available = await fetch(`${baseUrl}/api/home`, { headers: { Connection: 'close' } });
    expect(available.headers.get('cache-control')).toBe('no-store');
    await expect(available.json()).resolves.toEqual({ Speed: 1200 });

    home.home_info.mockResolvedValue(null);
    const unavailable = await fetch(`${baseUrl}/api/home`, { headers: { Connection: 'close' } });
    expect(unavailable.status).toBe(503);
    await expect(unavailable.json()).resolves.toEqual({ error: 'syncro_unavailable' });
});

test('Syncro version and operator endpoints expose results and service errors', async () => {
    home.syncro_versions.mockResolvedValue({ syncro: '1.2.3', data: '4.5.6', ite: null });
    const versions = await fetch(`${baseUrl}/api/syncro/versions`, { headers: { Connection: 'close' } });
    expect(versions.headers.get('cache-control')).toBe('no-store');
    await expect(versions.json()).resolves.toEqual({ syncro: '1.2.3', data: '4.5.6', ite: null });

    home.syncro_versions.mockResolvedValue(null);
    const noVersions = await fetch(`${baseUrl}/api/syncro/versions`, { headers: { Connection: 'close' } });
    expect(noVersions.status).toBe(503);

    operatorStatus.operator_status.mockResolvedValue({ readyToRun: true });
    const operator = await fetch(`${baseUrl}/api/syncro/operator-status`, { headers: { Connection: 'close' } });
    await expect(operator.json()).resolves.toEqual({ readyToRun: true });

    operatorStatus.operator_status.mockResolvedValue(null);
    const noOperator = await fetch(`${baseUrl}/api/syncro/operator-status`, { headers: { Connection: 'close' } });
    expect(noOperator.status).toBe(503);
});

test('health endpoints expose snapshots and stream handlers', async () => {
    const health = await fetch(`${baseUrl}/health/status`, { headers: { Connection: 'close' } });
    expect(health.headers.get('cache-control')).toBe('no-store');
    await expect(health.json()).resolves.toEqual({ database: true, syncro: false });

    const events = await fetch(`${baseUrl}/health/events`, { headers: { Connection: 'close' } });
    await expect(events.json()).resolves.toEqual({ stream: 'status' });
    expect(statusStream.attach).toHaveBeenCalled();

    const dashboard = await fetch(`${baseUrl}/api/dashboard/events`, { headers: { Connection: 'close' } });
    await expect(dashboard.json()).resolves.toEqual({ stream: 'dashboard' });
    expect(dashboardStream.attach).toHaveBeenCalled();
});
