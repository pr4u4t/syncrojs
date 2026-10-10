jest.mock('../../db', () => ({ query: jest.fn() }));
jest.mock('../../utils/cache', () => ({
    get: jest.fn(),
    set: jest.fn()
}));
jest.mock('../../utils/convert', () => ({ csv_to_xlsx: jest.fn() }));

const db = require('../../db');
const cache = require('../../utils/cache');
const converter = require('../../utils/convert');
const models = [
    {
        name: 'papers',
        model: require('../../models/papers'),
        all: 'papers_all',
        byId: 'papers_params_by_id',
        paginated: 'papers_paginated_all',
        count: 'papers_count'
    },
    {
        name: 'quality',
        model: require('../../models/quality'),
        all: 'quality_all',
        byId: 'quality_params_by_id',
        paginated: 'quality_paginated_all',
        count: 'quality_count'
    },
    {
        name: 'flute',
        model: require('../../models/flute'),
        all: 'flute_all',
        byId: 'flute_params_by_id',
        paginated: 'flute_paginated_all',
        count: 'flute_count'
    }
];

const pastOrders = {
    name: 'pastorders',
    model: require('../../models/past_orders'),
    all: 'past_all',
    paginated: 'past_paginated_all',
    count: 'past_count',
    export: 'past_export'
};

const rows = [{ id: 'row-1' }];
const fields = [{ name: 'id' }];
const result = { rows, fields };

beforeEach(() => {
    jest.clearAllMocks();
    cache.get.mockReturnValue(undefined);
    db.query.mockResolvedValue(result);
});

test.each(models)('$name list, lookup, pagination and count query the database and populate cache', async ({ name, model, all, byId, paginated, count }) => {
    await expect(model[all]()).resolves.toEqual(result);
    expect(db.query.mock.calls[0][0]).toContain(`FROM syncro_db_3892.${name}`);
    expect(cache.set).toHaveBeenCalledWith(`${name}.all`, result);

    await expect(model[byId]('ITEM-1')).resolves.toEqual(result);
    expect(db.query.mock.calls[1][0]).toContain('ITEM-1');
    expect(cache.set).toHaveBeenCalledWith(`${name}.by_id.ITEM-1`, result);

    await expect(model[paginated](50)).resolves.toEqual(result);
    expect(db.query.mock.calls[2][0]).toContain('OFFSET 50 LIMIT 25');
    expect(cache.set).toHaveBeenCalledWith(`${name}.offset.50`, result);

    await expect(model[count]()).resolves.toEqual(result);
    expect(db.query.mock.calls[3][0]).toContain(`count(*) FROM syncro_db_3892.${name}`);
    expect(cache.set).toHaveBeenCalledWith(`${name}.count`, result);
});

test.each(models)('$name methods return cached values without querying', async ({ model, all, byId, paginated, count, name }) => {
    cache.get.mockReturnValue(result);

    await expect(model[all]()).resolves.toBe(result);
    await expect(model[byId]('ITEM-1')).resolves.toBe(result);
    await expect(model[paginated](25)).resolves.toBe(result);
    await expect(model[count]()).resolves.toBe(result);
    expect(cache.get).toHaveBeenCalledWith(`${name}.all`);
    expect(db.query).not.toHaveBeenCalled();
});

test.each(models)('$name database errors return null', async ({ model, all, byId, paginated, count }) => {
    db.query.mockRejectedValue(new Error('database unavailable'));

    await expect(model[all]()).resolves.toBeNull();
    await expect(model[byId]('ITEM-1')).resolves.toBeNull();
    await expect(model[paginated](0)).resolves.toBeNull();
    await expect(model[count]()).resolves.toBeNull();
});

test('past order list, pagination, count and export use the expected query paths', async () => {
    await expect(pastOrders.model[pastOrders.all]()).resolves.toEqual(result);
    expect(db.query.mock.calls[0][0].toLowerCase()).toContain('order by finishtime desc limit 25');

    await expect(pastOrders.model[pastOrders.paginated](25)).resolves.toEqual(result);
    expect(db.query.mock.calls[1][0]).toContain('OFFSET 25 LIMIT 25');

    await expect(pastOrders.model[pastOrders.count]()).resolves.toEqual(result);
    expect(db.query.mock.calls[2][0]).toContain('count(*) FROM syncro_db_3892.pastorders');

    await expect(pastOrders.model[pastOrders.export]('2026-10-10')).resolves.toEqual(result);
    expect(db.query.mock.calls[3][0]).toContain("to_char(finishtime, 'YYYY-MM-DD') = '2026-10-10'");
});

test('past order exports CSV rows and delegates XLSX conversion', async () => {
    converter.csv_to_xlsx.mockImplementation((csv) => Buffer.from(csv));
    db.query.mockResolvedValue({
        fields: [{ name: 'order' }, { name: 'quantity' }],
        rows: [{ order: 'A-1', quantity: 4 }]
    });

    const csv = await pastOrders.model.past_export_csv('2026-10-10');
    expect(csv).toBe('order,quantity,\nA-1,4,\n');
    await expect(pastOrders.model.past_export_xlsx('2026-10-10')).resolves.toEqual(Buffer.from(csv));
    expect(converter.csv_to_xlsx).toHaveBeenCalledWith(csv);
});

test('past order queries return null on database errors', async () => {
    db.query.mockRejectedValue(new Error('database unavailable'));

    await expect(pastOrders.model.past_all()).resolves.toBeNull();
    await expect(pastOrders.model.past_paginated_all(0)).resolves.toBeNull();
    await expect(pastOrders.model.past_count()).resolves.toBeNull();
    await expect(pastOrders.model.past_export('2026-10-10')).resolves.toBeNull();
    await expect(pastOrders.model.past_export_csv('2026-10-10')).resolves.toBeNull();
    await expect(pastOrders.model.past_export_xlsx('2026-10-10')).resolves.toBeNull();
});
