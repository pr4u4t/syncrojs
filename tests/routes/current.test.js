jest.mock('../../models/order', () => ({
    orders_all: jest.fn(),
    current_backend: jest.fn(),
    parse: jest.fn(),
    validate: jest.fn(),
    manual_setup_to_syncro: jest.fn(),
    validate_syncro_payload: jest.fn(() => null),
    order_exists: jest.fn(),
    orders_add: jest.fn(),
    mac2syncro: jest.fn(),
    orders_delete_all: jest.fn()
}));
jest.mock('../../models/quality', () => ({ quality_all: jest.fn(), quality_params_by_id: jest.fn() }));
jest.mock('../../models/flute', () => ({ flute_all: jest.fn(), flute_params_by_id: jest.fn() }));
jest.mock('../../models/papers', () => ({}));
jest.mock('../../models/shift', () => ({ shift_state: jest.fn() }));
jest.mock('../../models/orders/classic', () => ({ mac_parse: jest.fn(), validate: jest.fn() }));
jest.mock('../../utils/classic-generator', () => ({ generateDatFromCsv: jest.fn() }));
jest.mock('../../utils/app-shell', () => jest.fn());

const express = require('express');
const session = require('express-session');
const orders = require('../../models/order');
const classicGenerator = require('../../utils/classic-generator');
const classicParser = require('../../models/orders/classic');
const quality = require('../../models/quality');
const flute = require('../../models/flute');
const currentRoute = require('../../routes/current');

const app = express();
app.use(session({ secret: 'jest-only-secret', resave: false, saveUninitialized: true }));
app.use(express.urlencoded({ extended: false }));
app.use(express.json());
app.use((req, res, next) => {
    if (req.headers['x-test-session']) Object.assign(req.session, JSON.parse(req.headers['x-test-session']));
    next();
});
app.use('/current', currentRoute);
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

const postForm = (url, values = {}, sessionData) => fetch(`${baseUrl}${url}`, {
    method: 'POST',
    redirect: 'manual',
    headers: {
        'Content-Type': 'application/x-www-form-urlencoded',
        Connection: 'close',
        ...(sessionData ? { 'X-Test-Session': JSON.stringify(sessionData) } : {})
    },
    body: new URLSearchParams(values)
});

test('current orders endpoint reports available and unavailable queue data', async () => {
    orders.orders_all.mockResolvedValue([{ id: 1 }]);
    const available = await fetch(`${baseUrl}/current/orders`, { headers: { Connection: 'close' } });
    await expect(available.json()).resolves.toEqual({ rows: [{ id: 1 }], unavailable: false });
    expect(available.headers.get('cache-control')).toBe('no-store');

    orders.orders_all.mockResolvedValue(null);
    const unavailable = await fetch(`${baseUrl}/current/orders`, { headers: { Connection: 'close' } });
    await expect(unavailable.json()).resolves.toEqual({ rows: null, unavailable: true });
});

test('manual options sorts and deduplicates ids and returns configured trim', async () => {
    quality.quality_all.mockResolvedValue({ rows: [{ qualityid: ' Q2 ' }, { qualityid: 'Q1' }, { qualityid: 'Q1' }, { qualityid: '' }] });
    flute.flute_all.mockResolvedValue({ rows: [{ flute: 'B' }, { flute: 'A' }, { flute: 'A' }] });
    const response = await fetch(`${baseUrl}/current/manual/options`, { headers: { Connection: 'close' } });

    expect(response.headers.get('cache-control')).toBe('no-store');
    const body = await response.json();
    expect(body.qualityIds).toEqual(['Q1', 'Q2']);
    expect(body.fluteIds).toEqual(['A', 'B']);
    expect(body.maxTrimTenthsMm).toBe(2300);
});

test('manual options reports unavailable material data', async () => {
    quality.quality_all.mockResolvedValue(null);
    flute.flute_all.mockResolvedValue({ rows: [] });
    const response = await fetch(`${baseUrl}/current/manual/options`, { headers: { Connection: 'close' } });

    expect(response.status).toBe(503);
    await expect(response.json()).resolves.toEqual({ error: 'manual_options_unavailable' });
});

test('extended_orders CSV upload is detected from its header even with the classic parser configured', async () => {
    const source = 'setup_code,web_width_mm,quality_id,flute,order_number,customer_name,sheet_width_mm,outs,sheet_length_mm,quantity_sheets,sheets_per_stack,score_measures_mm\nSETUP-1,2500,Q1,A,A-1,Customer,800,1,1000,10,100,100;200';
    const setup = {
        format: 'extended_orders_csv',
        setupCode: 'SETUP-1',
        orders: [{ orderNumber: 'A-1', scores: ['100', '200'] }],
        preview: ['SETUP-1', '2500', 'Q1', 'A', 'A-1', 2]
    };
    orders.current_backend.mockReturnValue('classic');
    orders.parse.mockReturnValue([setup]);
    orders.validate.mockResolvedValue({ result: ['converted'], error: null });
    const form = new FormData();
    form.append('orderFile', new Blob([source], { type: 'text/csv' }), 'orders.csv');

    const response = await fetch(`${baseUrl}/current/import`, {
        method: 'POST',
        headers: { Connection: 'close' },
        body: form
    });

    expect(response.status).toBe(200);
    const body = await response.json();
    expect(body.rows).toEqual([setup]);
    expect(body.fields).toEqual(['Setup code', 'Web width (mm)', 'Quality ID', 'Flute ID', 'Order count', 'Crease measures (total)']);
    expect(body.invalid).toEqual({});
    expect(orders.parse).toHaveBeenCalledWith(source, 'extended_orders');
    expect(orders.validate).toHaveBeenCalledWith(setup, expect.anything(), expect.anything(), expect.anything(), 'extended_orders');
    expect(classicGenerator.generateDatFromCsv).not.toHaveBeenCalled();
    expect(classicParser.mac_parse).not.toHaveBeenCalled();
});

test('every CSV upload, Ozdan included, goes to extended_orders whatever parser is configured', async () => {
    const upload = async (source, name) => {
        const form = new FormData();
        form.append('orderFile', new Blob([source], { type: 'text/csv' }), name);
        return fetch(`${baseUrl}/current/import`, { method: 'POST', headers: { Connection: 'close' }, body: form });
    };
    const fixture = (file) => require('node:fs').readFileSync(require('node:path').join(__dirname, '..', file), 'utf8');
    orders.validate.mockResolvedValue({ result: ['converted'], error: null });

    orders.current_backend.mockReturnValue('classic');
    orders.parse.mockReturnValue([{ format: 'extended_orders_csv', orders: [] }]);
    const ozdan = fixture('models/ozdan_data/OrdList.csv');
    expect((await upload(ozdan, 'OrdList.csv')).status).toBe(200);
    expect(orders.parse).toHaveBeenCalledWith(ozdan, 'extended_orders');
    expect(classicGenerator.generateDatFromCsv).not.toHaveBeenCalled();

    const plain = fixture('../tools/example_orders.csv');
    orders.parse.mockImplementationOnce(() => { throw new Error('Unrecognised orders file'); });
    const rejected = await upload(plain, 'orders.csv');
    expect(rejected.status).toBe(400);
    await expect(rejected.json()).resolves.toEqual({ error: 'order_file_invalid' });
    expect(orders.parse).toHaveBeenLastCalledWith(plain, 'extended_orders');
    expect(classicGenerator.generateDatFromCsv).not.toHaveBeenCalled();
});

test('DAT upload reports zero-quantity setups as warnings and skips blocking validation', async () => {
    const row = Array(55).fill('');
    row[10] = 'ORDER-ZERO';
    row[19] = '0';
    orders.parse.mockResolvedValue([row]);
    const form = new FormData();
    form.append('orderFile', new Blob(['DAT'], { type: 'text/plain' }), 'zero.dat');

    const response = await fetch(`${baseUrl}/current/import`, {
        method: 'POST',
        headers: { Connection: 'close' },
        body: form
    });

    expect(response.status).toBe(200);
    const body = await response.json();
    expect(body.warnings[0].message).toMatch(/ORDER-ZERO ma 0 sztuk/);
    expect(body.invalid).toEqual({});
    expect(orders.validate).not.toHaveBeenCalled();
});

test('manual setup rejects pending imports and invalid conversion before Syncro calls', async () => {
    const pending = await postForm('/current/manual', {}, { pendingOrderAck: { setupCode: '9001.2' } });
    expect(pending.status).toBe(409);
    await expect(pending.json()).resolves.toEqual({ accepted: false, error: 'order_ack_unknown', setupCode: '9001.2' });

    const conversionFailure = await postForm('/current/manual', {}, {});
    expect(conversionFailure.status).toBe(400);
    expect(orders.order_exists).not.toHaveBeenCalled();
});

test('manual setup rejects Syncro preflight errors before checking or sending', async () => {
    orders.manual_setup_to_syncro.mockResolvedValue({ result: Array(102).fill('0'), setupCode: '9011.2' });
    orders.validate_syncro_payload.mockReturnValueOnce({ error: 'Łączny Setup Length jest poniżej minimum.' });

    const response = await postForm('/current/manual', {});

    expect(response.status).toBe(400);
    await expect(response.json()).resolves.toMatchObject({ error: 'Łączny Setup Length jest poniżej minimum.' });
    expect(orders.order_exists).not.toHaveBeenCalled();
    expect(orders.orders_add).not.toHaveBeenCalled();
});

test('manual setup reconciles success, duplicate, unavailable, pending and rejected outcomes', async () => {
    orders.manual_setup_to_syncro.mockResolvedValue({ result: ['setup-data'], setupCode: '9002.2', trim: 1000 });
    orders.order_exists.mockResolvedValue(false);
    orders.orders_add.mockResolvedValue(true);
    const accepted = await postForm('/current/manual', { setupCode: '9002' });
    expect(accepted.status).toBe(200);
    await expect(accepted.json()).resolves.toEqual({ accepted: true, setupCode: '9002.2', trim: 1000 });

    orders.order_exists.mockResolvedValue(true);
    const duplicate = await postForm('/current/manual', {});
    expect(duplicate.status).toBe(409);
    await expect(duplicate.json()).resolves.toMatchObject({ error: 'setup_code_already_exists' });

    orders.order_exists.mockResolvedValue(null);
    const unavailable = await postForm('/current/manual', {});
    expect(unavailable.status).toBe(503);

    orders.order_exists.mockResolvedValue(false);
    orders.orders_add.mockResolvedValue(false);
    const rejected = await postForm('/current/manual', {});
    expect(rejected.status).toBe(502);
    await expect(rejected.json()).resolves.toMatchObject({ error: 'order_rejected' });

    orders.orders_add.mockResolvedValue(null);
    orders.order_exists.mockResolvedValueOnce(false).mockResolvedValueOnce(false);
    const pending = await postForm('/current/manual', {});
    expect(pending.status).toBe(202);
    await expect(pending.json()).resolves.toMatchObject({ pending: true, error: 'order_ack_unknown' });
});

test('import confirm and cancel enforce pending-ack guards', async () => {
    const guarded = await postForm('/current/import/confirm', { accept: 'true' }, { pendingOrderAck: { setupCode: '9003' } });
    expect(guarded.status).toBe(409);
    await expect(guarded.json()).resolves.toMatchObject({ error: 'order_ack_unknown', setupCode: '9003' });

    const noPending = await postForm('/current/import/confirm', { accept: 'true' });
    expect(noPending.status).toBe(400);
    await expect(noPending.json()).resolves.toMatchObject({ error: 'no_import_pending' });

    const cancelGuarded = await postForm('/current/import/cancel', {}, { pendingOrderAck: { setupCode: '9003' } });
    expect(cancelGuarded.status).toBe(409);

    const cancelled = await postForm('/current/import/cancel', {});
    expect(cancelled.status).toBe(200);
    await expect(cancelled.json()).resolves.toEqual({ cancelled: true });
});

test('import confirm rejects rows that fail parser validation', async () => {
    const validationError = 'Przy trzech zamówieniach co najmniej dwa muszą mieć tę samą długość arkusza i poziom noża.';
    orders.validate.mockResolvedValue({ result: null, error: validationError });

    const response = await postForm('/current/import/confirm', { accept: 'true' }, { current: [['parsed-row']], currentBackend: 'classic' });

    expect(response.status).toBe(400);
    await expect(response.json()).resolves.toEqual({
        accepted: false,
        error: 'invalid_import_rows',
        invalid: [{ index: 0, error: validationError }]
    });
    expect(orders.validate).toHaveBeenCalledWith(['parsed-row'], expect.anything(), expect.anything(), expect.anything(), 'classic');
});

test('import confirm allows zero-quantity rows to be skipped without validating them', async () => {
    const row = Array(55).fill('');
    row[10] = 'ORDER-ZERO';
    row[19] = '0';

    const response = await postForm('/current/import/confirm', { accept: 'true' }, { current: [row] });

    expect(response.status).toBe(200);
    await expect(response.json()).resolves.toMatchObject({ accepted: true, current: 0, steps: 1 });
    expect(orders.validate).not.toHaveBeenCalled();
});

test('DAT score update writes converted crease measurements and revalidates the row', async () => {
    orders.validate.mockResolvedValue({ result: ['converted'], error: null });
    const row = Array(42).fill('');
    row[10] = 'ORDER-1';
    row[11] = '8000';
    row[18] = '10000';
    row[20] = '1';

    const response = await postForm('/current/import/update-scores', {
        index: '0',
        scoresByOrder: JSON.stringify({ 0: '300\n500' })
    }, { current: [row], currentBackend: 'extended_orders' });

    expect(response.status).toBe(200);
    const body = await response.json();
    expect(body.saved).toBe(true);
    expect(body.row.slice(12, 17)).toEqual(['3000 5000', '', '', '', '']);
    expect(body.invalid).toBeNull();
    expect(orders.validate).toHaveBeenCalledWith(body.row, expect.anything(), expect.anything(), expect.anything(), 'extended_orders');
});

test('DAT score update returns validation details when an edited crease remains invalid', async () => {
    const validationError = 'Zamówienie 1: bigi muszą być dodatnie i nie mogą przekraczać szerokości arkusza.';
    orders.validate.mockResolvedValue({ result: null, error: validationError, fieldIndexes: [12] });
    const row = Array(42).fill('');
    row[10] = 'ORDER-1';
    row[11] = '8000';
    row[18] = '10000';
    row[20] = '1';

    const response = await postForm('/current/import/update-scores', {
        index: '0',
        scoresByOrder: JSON.stringify({ 0: '800' })
    }, { current: [row] });

    expect(response.status).toBe(200);
    await expect(response.json()).resolves.toMatchObject({
        saved: true,
        invalid: { error: validationError, fieldIndexes: [12] }
    });
});

test('DAT score update enforces the twenty-two crease limit on the server', async () => {
    const row = Array(42).fill('');
    row[10] = 'ORDER-1';
    row[11] = '8000';
    row[18] = '10000';
    row[20] = '1';

    const response = await postForm('/current/import/update-scores', {
        index: '0',
        scoresByOrder: JSON.stringify({ 0: Array(23).fill('100').join('\n') })
    }, { current: [row] });

    expect(response.status).toBe(400);
    await expect(response.json()).resolves.toMatchObject({ saved: false, error: 'Setup zawiera 23 bigów; limit to 22.' });
    expect(orders.validate).not.toHaveBeenCalled();
});

const creaseRow = (saved = '') => {
    const row = Array(42).fill('');
    row[10] = 'ORDER-1';
    row[11] = '8000';
    row[12] = saved;
    row[18] = '10000';
    row[20] = '1';
    return row;
};

test('DAT score update rejects edited creases that do not add up to the sheet width', async () => {
    const row = creaseRow('8000');
    const session = { current: [row] };

    const response = await postForm('/current/import/update-scores', { index: '0', scoresByOrder: JSON.stringify({ 0: '123\n45' }) }, session);

    expect(response.status).toBe(400);
    await expect(response.json()).resolves.toMatchObject({ saved: false, error: 'Zamówienie 1: suma bigów (168 mm) musi być równa szerokości arkusza (800 mm).' });
    expect(orders.validate).not.toHaveBeenCalled();
});

test('DAT score update still allows clearing creases and keeps an unchanged order off the sum', async () => {
    orders.validate.mockResolvedValue({ result: ['converted'], error: null });

    const cleared = await postForm('/current/import/update-scores', { index: '0', scoresByOrder: JSON.stringify({ 0: '' }) }, { current: [creaseRow('1000 2000')] });
    expect(cleared.status).toBe(200);

    const unchanged = await postForm('/current/import/update-scores', { index: '0', scoresByOrder: JSON.stringify({ 0: '100\n200' }) }, { current: [creaseRow('1000 2000')] });
    expect(unchanged.status).toBe(200);
});

test('parsed setup can be removed from the pending import session', async () => {
    const response = await postForm('/current/import/remove', { index: '1' }, { current: [{ setupCode: 'SETUP-1' }, { setupCode: 'SETUP-2' }], step: 0 });
    expect(response.status).toBe(200);
    await expect(response.json()).resolves.toEqual({ removed: true, remaining: 1 });
});

test('parsed setup removal rejects invalid indices, missing imports and uncertain ACK', async () => {
    const missing = await postForm('/current/import/remove', { index: '0' });
    expect(missing.status).toBe(400);
    await expect(missing.json()).resolves.toMatchObject({ error: 'no_import_pending' });

    const invalid = await postForm('/current/import/remove', { index: '-1' }, { current: [{}] });
    expect(invalid.status).toBe(400);
    await expect(invalid.json()).resolves.toMatchObject({ error: 'invalid_import_index' });

    const uncertain = await postForm('/current/import/remove', { index: '0' }, { current: [{}], pendingOrderAck: { setupCode: 'SETUP-3' } });
    expect(uncertain.status).toBe(409);
    await expect(uncertain.json()).resolves.toMatchObject({ error: 'order_ack_unknown', setupCode: 'SETUP-3' });
});

test('import status handles missing, unavailable, unresolved and resolved acknowledgements', async () => {
    const missing = await postForm('/current/import/status');
    expect(missing.status).toBe(400);

    orders.order_exists.mockResolvedValue(null);
    const unavailable = await postForm('/current/import/status', {}, { pendingOrderAck: { setupCode: '9004.2', step: 1 } });
    expect(unavailable.status).toBe(503);

    orders.order_exists.mockResolvedValue(false);
    const unresolved = await postForm('/current/import/status', {}, { pendingOrderAck: { setupCode: '9004.2', step: 1 } });
    expect(unresolved.status).toBe(200);
    await expect(unresolved.json()).resolves.toMatchObject({ resolved: false, error: 'order_ack_unknown' });

    orders.order_exists.mockResolvedValue(true);
    const resolved = await postForm('/current/import/status', {}, { pendingOrderAck: { setupCode: '9004.2', step: 1 }, current: [{}, {}] });
    await expect(resolved.json()).resolves.toEqual({ resolved: true, current: 1, steps: 2, complete: false });
});

test('batch endpoint rejects absent, pending, out-of-order and completed imports', async () => {
    const absent = await fetch(`${baseUrl}/current/import/batch/1`, { headers: { Connection: 'close' } });
    expect(absent.status).toBe(400);

    const pending = await fetch(`${baseUrl}/current/import/batch/1`, {
        headers: { Connection: 'close', 'X-Test-Session': JSON.stringify({ current: [{}], pendingOrderAck: { setupCode: '9005.2' } }) }
    });
    expect(pending.status).toBe(409);

    const outOfOrder = await fetch(`${baseUrl}/current/import/batch/2`, {
        headers: { Connection: 'close', 'X-Test-Session': JSON.stringify({ current: [{}], step: 0 }) }
    });
    expect(outOfOrder.status).toBe(409);

    const complete = await fetch(`${baseUrl}/current/import/batch/2`, {
        headers: { Connection: 'close', 'X-Test-Session': JSON.stringify({ current: [{}], step: 1 }) }
    });
    await expect(complete.json()).resolves.toMatchObject({ error: 'import_already_complete' });
});

test('batch endpoint skips zero-quantity setups without sending them to Syncro', async () => {
    const row = Array(55).fill('');
    row[10] = 'ORDER-ZERO';
    row[19] = '0';
    const response = await fetch(`${baseUrl}/current/import/batch/1`, {
        headers: { Connection: 'close', 'X-Test-Session': JSON.stringify({ current: [row], step: 0 }) }
    });

    expect(response.status).toBe(200);
    await expect(response.json()).resolves.toMatchObject({ status: true, current: 1, steps: 1, skipped: true });
    expect(orders.mac2syncro).not.toHaveBeenCalled();
    expect(orders.orders_add).not.toHaveBeenCalled();
});

test('clear endpoint deletes orders and redirects', async () => {
    orders.orders_delete_all.mockResolvedValue(true);
    const response = await postForm('/current/clear');
    expect(response.status).toBe(302);
    expect(response.headers.get('location')).toBe('/current/');
    expect(orders.orders_delete_all).toHaveBeenCalledTimes(1);
});

test('revalidate reports rows whose quality or flute is missing from the database', async () => {
    const row = Array(55).fill('');
    row[3] = 'NOPE';
    row[4] = 'b';
    row[10] = 'A1';
    row[19] = '10';
    quality.quality_params_by_id.mockResolvedValue({ rows: [] });
    flute.flute_params_by_id.mockResolvedValue({ rows: [{ flute: 'B' }] });
    orders.orders_all.mockResolvedValue([]);
    orders.validate.mockResolvedValue({ result: null, error: 'Quality NOPE was not found in the database' });

    const response = await postForm('/current/import/revalidate', {}, { current: [row], currentBackend: 'classic' });

    expect(response.status).toBe(200);
    await expect(response.json()).resolves.toEqual({
        invalid: { 0: { result: null, error: 'Quality NOPE was not found in the database' } },
        warnings: {},
        missingMaterials: { 0: [{ type: 'quality', id: 'NOPE' }] }
    });
});

test('revalidate does not call a failed database lookup a missing material', async () => {
    const row = Array(55).fill('');
    row[3] = 'Q1';
    row[4] = 'B';
    quality.quality_params_by_id.mockResolvedValue(null);
    flute.flute_params_by_id.mockResolvedValue(null);
    orders.orders_all.mockResolvedValue([]);
    orders.validate.mockResolvedValue({ result: null, error: 'mac2syncro: failed to fetch parameters' });

    const response = await postForm('/current/import/revalidate', {}, { current: [row], currentBackend: 'classic' });

    await expect(response.json()).resolves.toMatchObject({ missingMaterials: {} });
});

test('revalidate needs a pending import that has not started', async () => {
    const none = await postForm('/current/import/revalidate', {});
    expect(none.status).toBe(400);
    const started = await postForm('/current/import/revalidate', {}, { current: [['x']], step: 0 });
    expect(started.status).toBe(409);
});
