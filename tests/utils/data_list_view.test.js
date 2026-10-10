jest.mock('../../utils/app-shell', () => jest.fn());

const sendAppShell = require('../../utils/app-shell');
const renderDataListView = require('../../utils/data-list-view');

const createResponse = () => ({
    headers: {},
    set(name, value) {
        this.headers[name] = value;
        return this;
    },
    json(value) {
        this.body = value;
        return this;
    }
});

test('returns paginated JSON and clamps out-of-range page requests', async () => {
    const pages = [];
    const req = { params: { page: '9' }, query: { api: '1' } };
    const res = createResponse();
    const load = jest.fn(async (page) => {
        pages.push(page);
        return { rows: [{ id: page }], fields: [{ name: 'id' }], editFields: [], keyFields: ['id'], total: 2 };
    });

    await renderDataListView(req, res, { url: '/materials/', load });

    expect(pages).toEqual([9, 2]);
    expect(res.headers['Cache-Control']).toBe('no-store');
    expect(res.body).toEqual({
        unavailable: false,
        rows: [{ id: 2 }],
        fields: [{ name: 'id' }],
        editFields: [],
        keyFields: ['id'],
        total: 2,
        current: 2,
        url: '/materials/'
    });
});

test('normalizes invalid page and unavailable data', async () => {
    const req = { params: { page: '-4' }, query: { api: '1' } };
    const res = createResponse();
    const load = jest.fn().mockResolvedValue(null);

    await renderDataListView(req, res, { url: '/papers/', load });

    expect(load).toHaveBeenCalledWith(1);
    expect(res.body).toEqual({
        unavailable: true,
        rows: [],
        fields: [],
        editFields: [],
        keyFields: [],
        total: 0,
        current: 1,
        url: '/papers/'
    });
});

test('serves the application shell for non-API requests', async () => {
    const req = { params: {}, query: {} };
    const res = createResponse();
    const load = jest.fn();

    await renderDataListView(req, res, { url: '/papers/', load });

    expect(sendAppShell).toHaveBeenCalledWith(req, res);
    expect(load).not.toHaveBeenCalled();
});
