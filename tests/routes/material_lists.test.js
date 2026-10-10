jest.mock('../../models/papers', () => ({
    papers_paginated_all: jest.fn(),
    papers_count: jest.fn()
}));
jest.mock('../../models/quality', () => ({
    quality_paginated_all: jest.fn(),
    quality_count: jest.fn()
}));
jest.mock('../../models/flute', () => ({
    flute_paginated_all: jest.fn(),
    flute_count: jest.fn()
}));
jest.mock('../../utils/material-editor', () => ({
    getEditorFields: jest.fn((name) => [{ name: `${name}editable` }]),
    getDefinition: jest.fn((name) => ({ keyFields: [`${name}id`] })),
    attachEditorRoute: jest.fn()
}));

const express = require('express');
const papersModel = require('../../models/papers');
const qualityModel = require('../../models/quality');
const fluteModel = require('../../models/flute');
const papersRoute = require('../../routes/papers');
const qualityRoute = require('../../routes/quality');
const fluteRoute = require('../../routes/flute');

const routes = [
    { url: '/papers', name: 'papers', router: papersRoute, model: papersModel, load: 'papers_paginated_all', count: 'papers_count' },
    { url: '/quality', name: 'quality', router: qualityRoute, model: qualityModel, load: 'quality_paginated_all', count: 'quality_count' },
    { url: '/flute', name: 'flute', router: fluteRoute, model: fluteModel, load: 'flute_paginated_all', count: 'flute_count' }
];

const app = express();
for (const route of routes) app.use(route.url, route.router);
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
    for (const { model, load, count } of routes) {
        model[load].mockResolvedValue({ rows: [{ id: 'item-1' }], fields: [{ name: 'id' }] });
        model[count].mockResolvedValue({ rows: [{ count: '26' }] });
    }
});

test.each(routes)('$name list API includes editor metadata and computes page offset', async ({ url, name, model, load, count }) => {
    const response = await fetch(`${baseUrl}${url}/2?api=1`, { headers: { Connection: 'close' } });

    expect(response.status).toBe(200);
    expect(response.headers.get('cache-control')).toBe('no-store');
    await expect(response.json()).resolves.toEqual({
        unavailable: false,
        rows: [{ id: 'item-1' }],
        fields: [{ name: 'id' }],
        editFields: [{ name: `${name}editable` }],
        keyFields: [`${name}id`],
        total: 2,
        current: 2,
        url: `${url}/`
    });
    expect(model[load]).toHaveBeenCalledWith(25);
    expect(model[count]).toHaveBeenCalledTimes(1);
});

test.each(routes)('$name list API marks failed model reads unavailable', async ({ url, model, load }) => {
    model[load].mockResolvedValue(null);
    const response = await fetch(`${baseUrl}${url}/1?api=1`, { headers: { Connection: 'close' } });

    expect(response.status).toBe(200);
    await expect(response.json()).resolves.toMatchObject({
        unavailable: true,
        rows: [],
        fields: [],
        total: 2,
        current: 1
    });
});
