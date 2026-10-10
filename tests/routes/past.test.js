jest.mock('../../models/past_orders', () => ({
    past_paginated_all: jest.fn(),
    past_count: jest.fn(),
    past_export: jest.fn(),
    past_export_csv: jest.fn(),
    past_export_xlsx: jest.fn()
}));
jest.mock('../../utils/data-list-view', () => jest.fn(async (req, res, options) => {
    if (req.query.api !== '1') return;
    const data = await options.load(Number(req.params.page) || 1);
    res.set('Cache-Control', 'no-store').json({
        unavailable: !data || data.unavailable,
        rows: data && data.rows || [],
        fields: data && data.fields || [],
        total: data && data.total || 0,
        url: options.url
    });
}));
jest.mock('../../utils/app-shell', () => jest.fn());

const express = require('express');
const past = require('../../models/past_orders');
const pastRoute = require('../../routes/past');

const app = express();
app.use(express.urlencoded({ extended: false }));
app.use('/past', pastRoute);
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

test('export API uses the requested date and reports unavailable data', async () => {
    past.past_export.mockResolvedValue({ rows: [{ order: 'A1' }], fields: [{ name: 'order' }] });
    const response = await fetch(`${baseUrl}/past/export?api=1&date=2026-10-10`, { headers: { Connection: 'close' } });

    expect(response.headers.get('cache-control')).toBe('no-store');
    await expect(response.json()).resolves.toEqual({
        unavailable: false,
        rows: [{ order: 'A1' }],
        fields: [{ name: 'order' }]
    });
    expect(past.past_export).toHaveBeenCalledWith('2026-10-10');

    past.past_export.mockResolvedValue(null);
    const failed = await fetch(`${baseUrl}/past/export?api=1&date=2026-10-10`, { headers: { Connection: 'close' } });
    await expect(failed.json()).resolves.toEqual({ unavailable: true, rows: [], fields: [] });
});

test('CSV and XLSX export endpoints return their generated payloads', async () => {
    past.past_export_csv.mockResolvedValue('order,\nA1,\n');
    const csv = await fetch(`${baseUrl}/past/export.csv`, {
        method: 'POST', headers: { 'Content-Type': 'application/x-www-form-urlencoded', Connection: 'close' },
        body: new URLSearchParams({ date: '2026-10-10' })
    });
    expect(csv.headers.get('content-type')).toContain('application/octet-stream');
    expect(await csv.text()).toBe('order,\nA1,\n');
    expect(past.past_export_csv).toHaveBeenCalledWith('2026-10-10');

    past.past_export_xlsx.mockResolvedValue(Buffer.from('xlsx-data'));
    const xlsx = await fetch(`${baseUrl}/past/export.xlsx`, {
        method: 'POST', headers: { 'Content-Type': 'application/x-www-form-urlencoded', Connection: 'close' },
        body: new URLSearchParams({ date: '2026-10-10' })
    });
    expect(xlsx.headers.get('content-type')).toContain('application/vnd.openxmlformats-officedocument.spreadsheetml.sheet');
    expect(Buffer.from(await xlsx.arrayBuffer())).toEqual(Buffer.from('xlsx-data'));
    expect(past.past_export_xlsx).toHaveBeenCalledWith('2026-10-10');
});
