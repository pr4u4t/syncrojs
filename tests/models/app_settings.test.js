jest.mock('../../db', () => ({ query: jest.fn() }));

const db = require('../../db');
const settings = require('../../models/app-settings');

beforeEach(() => {
    jest.clearAllMocks();
});

test('identifies database connection settings case-insensitively', () => {
    for (const path of ['database.host', 'db.password', 'PGHOST', 'PGPORT', 'pgdatabase', 'PGUSER', 'PGPASSWORD']) {
        expect(settings.isDatabaseSetting(path)).toBe(true);
    }
    expect(settings.isDatabaseSetting('server.port')).toBe(false);
});

test('initializes the application settings table', async () => {
    db.query.mockResolvedValue({ rows: [] });

    await settings.initialize();

    expect(db.query.mock.calls[0][0]).toContain('CREATE TABLE IF NOT EXISTS public.app_settings');
});

test('builds nested settings while excluding secrets and database credentials', () => {
    expect(settings.toNestedObject({
        'server.host': '0.0.0.0',
        'orders.max_trim': 2300,
        'server.secret': 'hidden',
        'database.password': 'hidden',
        PGHOST: 'db'
    })).toEqual({
        server: { host: '0.0.0.0' },
        orders: { max_trim: 2300 }
    });
});

test('loads settings as a nested object', async () => {
    db.query.mockResolvedValue({ rows: [
        { setting_key: 'server.port', setting_value: 3000 },
        { setting_key: 'app.debug', setting_value: true },
        { setting_key: 'server.secret', setting_value: 'hidden' }
    ] });

    await expect(settings.load()).resolves.toEqual({
        server: { port: 3000 },
        app: { debug: true }
    });
});

test('saves only allowed settings and skips empty updates', async () => {
    await settings.save({ 'server.secret': 'hidden', 'database.password': 'hidden', PGHOST: 'db' });
    expect(db.query).not.toHaveBeenCalled();

    await settings.save({
        'server.port': 3001,
        'server.secret': 'hidden',
        'database.password': 'hidden',
        PGHOST: 'db'
    });

    expect(db.query).toHaveBeenCalledTimes(1);
    expect(JSON.parse(db.query.mock.calls[0][1][0])).toEqual({ 'server.port': 3001 });
    expect(db.query.mock.calls[0][0]).toContain('ON CONFLICT (setting_key) DO UPDATE');
});
