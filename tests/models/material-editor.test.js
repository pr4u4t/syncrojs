jest.mock('../../db', () => ({ query: jest.fn() }));
jest.mock('../../utils/cache', () => ({
    keys: jest.fn(),
    del: jest.fn()
}));

const db = require('../../db');
const cache = require('../../utils/cache');
const { getEditorFields, getDefinition, updateMaterial, attachEditorRoute } = require('../../utils/material-editor');

const validFluteValues = () => Object.fromEntries(
    getEditorFields('flute')
        .filter((field) => !field.primaryKey)
        .map((field) => [field.name, field.type === 'integer' ? '17' : field.type === 'boolean' ? 'false' : field.type === 'datetime-local' ? '2026-10-10T10:00:00' : 'A'])
);

beforeEach(() => {
    jest.clearAllMocks();
    db.query.mockResolvedValue({ rowCount: 1, rows: [{ flute: 'A' }] });
    cache.keys.mockReturnValue(['flute.all', 'flute.by_id.A', 'papers.all']);
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
