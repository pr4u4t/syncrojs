process.env.ALLOW_CONFIG_MUTATIONS = '1';

jest.mock('../../models/app-settings', () => ({
    isDatabaseSetting: (path) => /(^|\.)(database|db)(\.|$)|^PG(?:HOST|PORT|DATABASE|USER|PASSWORD)$/i.test(path),
    save: jest.fn(),
    toNestedObject: jest.fn(() => ({}))
}));
jest.mock('../../models/order', () => ({
    available_backends: jest.fn().mockResolvedValue(['classic.js', 'extended_orders.js']),
    current_backend: jest.fn(() => 'classic'),
    switch_backend: jest.fn().mockResolvedValue(true)
}));
jest.mock('../../utils/cache', () => ({ flushAll: jest.fn() }));
jest.mock('../../utils/app-shell', () => jest.fn());

const express = require('express');
const config = require('config');
const appSettings = require('../../models/app-settings');
const orderModel = require('../../models/order');
const cache = require('../../utils/cache');
const settingsRoute = require('../../routes/settings');

const app = express();
app.use(express.urlencoded({ extended: false }));
app.use(express.json());
app.use('/settings', settingsRoute);
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
    orderModel.available_backends.mockResolvedValue(['classic.js', 'extended_orders.js']);
    orderModel.current_backend.mockReturnValue('classic');
});

test('settings API groups visible values and exposes available parser choices', async () => {
    const response = await fetch(`${baseUrl}/settings/data`, { headers: { Connection: 'close' } });
    const body = await response.json();
    const fields = body.groups.flatMap((group) => group.fields);
    const parser = fields.find((field) => field.path === 'orders.parser.type');

    expect(response.status).toBe(200);
    expect(response.headers.get('cache-control')).toBe('no-store');
    expect(body.groups.length).toBeGreaterThan(0);
    expect(parser.options).toEqual(['classic', 'extended_orders']);
    expect(fields.some((field) => field.path === 'server.secret')).toBe(false);
    expect(fields.some((field) => field.path === 'PGPASSWORD')).toBe(false);
});

test('settings API displays the canonical name for a legacy extended_outs setting', async () => {
    config.orders.parser.type = 'extended_outs';
    try {
        const response = await fetch(`${baseUrl}/settings/data`, { headers: { Connection: 'close' } });
        const body = await response.json();
        const parser = body.groups.flatMap((group) => group.fields).find((field) => field.path === 'orders.parser.type');
        expect(parser.conf).toBe('extended_orders');
        expect(parser.options).toContain('extended_orders');
        expect(parser.options).not.toContain('extended_outs');
    } finally {
        config.orders.parser.type = 'classic';
    }
});

test('settings save accepts valid values, rejects protected and invalid fields, and switches parser', async () => {
    const form = new URLSearchParams({
        'app.debug': 'false',
        'orders.max_trim': '2400',
        'orders.parser.type': 'extended_orders',
        'app.pagination': 'not-a-number',
        'server.secret': 'do-not-save',
        PGPASSWORD: 'do-not-save',
        unknown: 'ignored'
    });
    const response = await fetch(`${baseUrl}/settings/save`, {
        method: 'POST',
        redirect: 'manual',
        headers: { 'Content-Type': 'application/x-www-form-urlencoded', Connection: 'close' },
        body: form
    });

    expect(response.status).toBe(302);
    expect(response.headers.get('location')).toBe('/settings?saved=1');
    expect(appSettings.save).toHaveBeenCalledWith({
        'app.debug': false,
        'orders.max_trim': 2400,
        'orders.parser.type': 'extended_orders'
    });
    expect(orderModel.switch_backend).toHaveBeenCalledWith('extended_orders');
});

test('settings save ignores unsupported parser choices', async () => {
    const response = await fetch(`${baseUrl}/settings/save`, {
        method: 'POST',
        redirect: 'manual',
        headers: { 'Content-Type': 'application/x-www-form-urlencoded', Connection: 'close' },
        body: new URLSearchParams({ 'orders.parser.type': 'unknown' })
    });

    expect(response.status).toBe(302);
    expect(appSettings.save).toHaveBeenCalledWith({});
    expect(orderModel.switch_backend).not.toHaveBeenCalled();
});

test('cache flush endpoint clears entries and redirects', async () => {
    const response = await fetch(`${baseUrl}/settings/flush-cache`, {
        method: 'POST',
        redirect: 'manual',
        headers: { Connection: 'close' },
        body: new FormData()
    });

    expect(response.status).toBe(302);
    expect(response.headers.get('location')).toBe('/settings?cache=cleared');
    expect(cache.flushAll).toHaveBeenCalledTimes(1);
});
