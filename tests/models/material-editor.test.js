jest.mock('../../db', () => ({ query: jest.fn() }));
jest.mock('../../utils/cache', () => ({
    keys: jest.fn(),
    del: jest.fn()
}));
jest.mock('../../models/order', () => ({ orders_all: jest.fn() }));

const db = require('../../db');
const cache = require('../../utils/cache');
const orders = require('../../models/order');
const { getEditorFields, getDefinition, createMaterial, updateMaterial, deleteMaterial, attachEditorRoute } = require('../../utils/material-editor');

const validFluteValues = () => Object.fromEntries(
    getEditorFields('flute')
        .filter((field) => !field.primaryKey)
        .map((field) => [field.name, field.type === 'integer' ? '17' : field.type === 'boolean' ? 'false' : field.type === 'datetime-local' ? '2026-10-10T10:00:00' : 'A'])
);

beforeEach(() => {
    jest.clearAllMocks();
    db.query.mockResolvedValue({ rowCount: 1, rows: [{ flute: 'A' }] });
    cache.keys.mockReturnValue(['flute.all', 'flute.by_id.A', 'papers.all']);
    orders.orders_all.mockResolvedValue([]);
});

test('material editor exposes every column and protects record identifiers', () => {
    expect(getEditorFields('papers')).toHaveLength(15);
    expect(getEditorFields('quality')).toHaveLength(38);
    expect(getEditorFields('flute')).toHaveLength(15);
    expect(getEditorFields('papers').find((field) => field.name === 'paperid').primaryKey).toBe(true);
    expect(getEditorFields('papers').find((field) => field.name === 'lastused').readOnly).toBe(true);
    expect(getEditorFields('quality').find((field) => field.name === 'qualityid').primaryKey).toBe(true);
    expect(getEditorFields('flute').find((field) => field.name === 'flute').primaryKey).toBe(true);
});

test('updates only allowed columns by the original key and clears matching caches', async () => {
    await updateMaterial('flute', { flute: 'A' }, validFluteValues());

    const [query, parameters] = db.query.mock.calls[0];
    expect(query).toMatch(/^UPDATE syncro_db_3892\.flute SET /);
    expect(query).toContain('WHERE flute = $15 RETURNING *');
    expect(query.split(' WHERE ')[0]).not.toContain('flute =');
    expect(parameters.at(-1)).toBe('A');
    expect(cache.del).toHaveBeenCalledWith(['flute.all', 'flute.by_id.A']);
});

test('rejects empty numeric values before querying the database', async () => {
    const values = { ...validFluteValues(), scoregap_uddmm: '' };

    await expect(updateMaterial('flute', { flute: 'A' }, values)).rejects.toThrow('Invalid value for scoregap_uddmm');
    expect(db.query).not.toHaveBeenCalled();
});

test('rejects unknown material types, missing keys, invalid booleans, text and integer ranges', async () => {
    expect(() => getDefinition('unknown')).toThrow('Unknown material type: unknown');
    await expect(updateMaterial('flute', null, validFluteValues())).rejects.toThrow('Invalid material update');
    await expect(updateMaterial('flute', {}, validFluteValues())).rejects.toThrow('Missing key field: flute');
    await expect(updateMaterial('flute', { flute: 'A' }, { ...validFluteValues(), sfsposition: 'TOOLONG' })).rejects.toThrow('Invalid value for sfsposition');
    await expect(updateMaterial('quality', { qualityid: 'Q1' }, {
        ...Object.fromEntries(getEditorFields('quality').filter((field) => !field.primaryKey && !field.readOnly).map((field) => [field.name, field.type === 'integer' ? '1' : field.type === 'boolean' ? 'false' : 'A'])),
        preprint: 'yes'
    })).rejects.toThrow('Invalid value for preprint');
    await expect(updateMaterial('flute', { flute: 'A' }, { ...validFluteValues(), scoregap_uddmm: '2147483648' })).rejects.toThrow('Invalid value for scoregap_uddmm');
    expect(db.query).not.toHaveBeenCalled();
});

test('returns null when the update key no longer matches a row', async () => {
    db.query.mockResolvedValue({ rowCount: 0, rows: [] });

    await expect(updateMaterial('flute', { flute: 'A' }, validFluteValues())).resolves.toBeNull();
    expect(cache.del).not.toHaveBeenCalled();
});

test.each([
    { status: 404, error: 'material_not_found', rowCount: 0 },
    { status: 409, error: 'material_conflict', code: '23505', rowCount: 0 },
    { status: 400, error: 'invalid_material_update', invalid: true }
])('maps material update outcomes to HTTP $status', async ({ status, error, code, rowCount, invalid }) => {
    const router = { post: jest.fn((path, handler) => { router.handler = handler; }) };
    attachEditorRoute(router, 'flute');
    if (invalid) {
        db.query.mockRejectedValue(new Error('bad value'));
    } else if (code) {
        db.query.mockRejectedValue(Object.assign(new Error('duplicate key'), { code }));
    } else {
        db.query.mockResolvedValue({ rowCount, rows: [] });
    }
    const response = {
        status: jest.fn(function(value){ this.statusCode = value; return this; }),
        json: jest.fn(function(value){ this.body = value; return this; })
    };

    await router.handler({ body: invalid ? {} : { key: { flute: 'A' }, values: validFluteValues() } }, response);

    expect(router.post).toHaveBeenCalledWith('/update', expect.any(Function));
    expect(response.status).toHaveBeenCalledWith(status);
    expect(response.body).toEqual({ error });
});

test('creates a material with a generated lastused, a blank-text default and clears matching caches', async () => {
    const values = { ...Object.fromEntries(getEditorFields('quality').map((field) => [field.name, field.type === 'integer' ? '1' : field.type === 'boolean' ? 'false' : ''])), qualityid: 'Q9' };
    await createMaterial('quality', values);

    const [query, parameters] = db.query.mock.calls[0];
    expect(query).toMatch(/^INSERT INTO syncro_db_3892\.quality \(qualityid, /);
    expect(query).toContain('lastused');
    expect(query).toContain('NOW()');
    expect(parameters[0]).toBe('Q9');
    expect(parameters[1]).toBe(' ');
    expect(parameters).toHaveLength(37);
    expect(cache.keys).toHaveBeenCalled();
});

test('rejects a new material with a blank key or a missing number', async () => {
    const values = validFluteValues();
    await expect(createMaterial('flute', { ...values, flute: '  ' })).rejects.toThrow('Invalid value for flute');
    await expect(createMaterial('flute', { ...values, flute: 'X', scoregap_uddmm: '' })).rejects.toThrow('Invalid value for scoregap_uddmm');
    expect(db.query).not.toHaveBeenCalled();
});

const noUsage = () => db.query.mockImplementation(async (query) => (query.startsWith('SELECT') ? { rows: [] } : { rowCount: 1, rows: [] }));
const qualityValues = (overrides) => ({
    ...Object.fromEntries(getEditorFields('quality').map((field) => [field.name, field.type === 'integer' ? '0' : field.type === 'boolean' ? 'false' : '---'])),
    qualityid: 'Q9',
    ...overrides
});

test('deletes an unused material by its full key and clears matching caches', async () => {
    noUsage();

    await expect(deleteMaterial('papers', { paperid: 'P1', weightfactor_uwg_m: 120 })).resolves.toBe(true);

    expect(db.query).toHaveBeenLastCalledWith('DELETE FROM syncro_db_3892.papers WHERE paperid = $1 AND weightfactor_uwg_m = $2', ['P1', 120]);
    expect(cache.keys).toHaveBeenCalled();
});

test('does not delete without a complete key and reports a missing row', async () => {
    await expect(deleteMaterial('flute', null)).rejects.toThrow('Invalid material delete');
    await expect(deleteMaterial('papers', { paperid: 'P1' })).rejects.toThrow('Missing key field: weightfactor_uwg_m');
    expect(db.query).not.toHaveBeenCalled();

    db.query.mockImplementation(async (query) => (query.startsWith('SELECT') ? { rows: [] } : { rowCount: 0 }));
    await expect(deleteMaterial('flute', { flute: 'A' })).resolves.toBe(false);
    expect(cache.del).not.toHaveBeenCalled();
});

test.each([
    ['papers', { paperid: 'T110', weightfactor_uwg_m: 100 }],
    ['flute', { flute: 'BC' }]
])('refuses to delete a %s used by a quality', async (name, key) => {
    db.query.mockResolvedValue({ rows: [{ qualityid: 'C 06' }] });

    await expect(deleteMaterial(name, key)).rejects.toMatchObject({ code: 'MATERIAL_IN_USE', usedBy: { type: 'quality', ids: ['C 06'] } });
    expect(db.query.mock.calls.some(([query]) => query.startsWith('DELETE'))).toBe(false);
});

test.each([
    ['quality', { qualityid: 'c 06' }, { SetupCode: 9001, QualityID: 'C 06', Flute: 'B' }],
    ['flute', { flute: 'b' }, { SetupCode: 9001, QualityID: 'C 06', Flute: 'B' }]
])('refuses to delete a %s used by a setup in the Syncro queue', async (name, key, row) => {
    noUsage();
    orders.orders_all.mockResolvedValue([row]);

    await expect(deleteMaterial(name, key)).rejects.toMatchObject({ code: 'MATERIAL_IN_USE', usedBy: { type: 'queue', ids: ['9001'] } });
    expect(db.query.mock.calls.some(([query]) => query.startsWith('DELETE'))).toBe(false);
});

test.each([
    ['the queue cannot be read', null],
    ['queue rows lack the field', [{ SetupCode: '9001' }]]
])('refuses to delete a quality when %s', async (_, queue) => {
    orders.orders_all.mockResolvedValue(queue);

    await expect(deleteMaterial('quality', { qualityid: 'Q1' })).rejects.toMatchObject({ code: 'MATERIAL_USAGE_UNKNOWN' });
    expect(db.query).not.toHaveBeenCalled();
});

test('allows deleting a quality that is not in the queue', async () => {
    noUsage();
    orders.orders_all.mockResolvedValue([{ SetupCode: '9001', QualityID: 'OTHER', Flute: 'B' }]);

    await expect(deleteMaterial('quality', { qualityid: 'Q1' })).resolves.toBe(true);
});

test('blocks changing the weight of a paper used by a quality, but not other paper fields', async () => {
    const values = Object.fromEntries(getEditorFields('papers').filter((field) => !field.primaryKey).map((field) => [field.name, field.type === 'integer' ? '100' : field.type === 'boolean' ? 'false' : field.type === 'datetime-local' ? '2026-10-10T10:00:00' : 'A']));
    db.query.mockImplementation(async (query) => (query.startsWith('SELECT') ? { rows: [{ qualityid: 'C 06' }] } : { rowCount: 1, rows: [{}] }));

    await expect(updateMaterial('papers', { paperid: 'T110', weightfactor_uwg_m: 120 }, values)).rejects.toMatchObject({ code: 'MATERIAL_IN_USE' });
    await expect(updateMaterial('papers', { paperid: 'T110', weightfactor_uwg_m: 100 }, values)).resolves.toBeTruthy();
});

test('rejects a quality pointing at a missing paper or flute and allows unused slots', async () => {
    noUsage();
    await expect(createMaterial('quality', qualityValues({ paper1id: 'NOPE', paper1weightfactor_uwg_m: '90', flute: 'X' }))).rejects.toMatchObject({
        code: 'MATERIAL_REFERENCE_MISSING',
        missing: ['paper NOPE / 90', 'flute X']
    });
    expect(db.query.mock.calls.some(([query]) => query.startsWith('INSERT'))).toBe(false);

    await expect(createMaterial('quality', qualityValues())).resolves.toBeUndefined();
    expect(db.query.mock.calls.some(([query]) => query.startsWith('INSERT'))).toBe(true);
});

test.each([
    ['MATERIAL_IN_USE', 409, { error: 'material_in_use', usedBy: { type: 'quality', ids: ['C 06'] } }],
    ['MATERIAL_USAGE_UNKNOWN', 503, { error: 'material_usage_unknown' }]
])('maps %s on delete to HTTP %i', async (code, status, body) => {
    const handlers = {};
    attachEditorRoute({ post: (path, handler) => { handlers[path] = handler; } }, 'flute');
    if (code === 'MATERIAL_IN_USE') {
        db.query.mockResolvedValue({ rows: [{ qualityid: 'C 06' }] });
    } else {
        noUsage();
        orders.orders_all.mockResolvedValue(null);
    }
    const response = {
        status: jest.fn(function(value){ return this; }),
        json: jest.fn(function(value){ this.body = value; return this; })
    };

    await handlers['/delete']({ body: { key: { flute: 'BC' } } }, response);

    expect(response.status).toHaveBeenCalledWith(status);
    expect(response.body).toEqual(body);
});
