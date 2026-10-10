jest.mock('node:fs/promises', () => ({ readFile: jest.fn() }));
jest.mock('../../db', () => ({ connect: jest.fn() }));

const fs = require('node:fs/promises');
const db = require('../../db');
const { migrate } = require('../../db/migrate');

const schema = `CREATE TABLE syncro_db_3892.papers (
    paperid text NOT NULL
);
ALTER TABLE ONLY syncro_db_3892.papers
    ADD CONSTRAINT papers_pkey PRIMARY KEY (paperid);`;
const backup = 'COPY syncro_db_3892.papers (paperid) FROM stdin;\n\\.\n';
const createClient = ({ hasRows = true, failSchema = false } = {}) => {
    const client = {
        queries: [],
        query: jest.fn(async (query) => {
            client.queries.push(query);
            if (failSchema && query === 'CREATE SCHEMA IF NOT EXISTS syncro_db_3892') throw new Error('schema denied');
            if (query.startsWith('SELECT EXISTS')) return { rows: [{ has_rows: hasRows }] };
            if (query.startsWith('SELECT 1 FROM pg_constraint')) return { rows: [] };
            return { rows: [] };
        }),
        release: jest.fn()
    };
    return client;
};

beforeEach(() => {
    jest.clearAllMocks();
    fs.readFile.mockResolvedValueOnce(schema).mockResolvedValueOnce(backup);
});

test('migrates an existing seeded table and adds missing primary key constraints', async () => {
    const client = createClient({ hasRows: true });
    db.connect.mockResolvedValue(client);

    await migrate();

    expect(client.queries).toContain('SELECT pg_advisory_lock($1)');
    expect(client.queries).toContain('CREATE SCHEMA IF NOT EXISTS syncro_db_3892');
    expect(client.queries).toContain('CREATE TABLE IF NOT EXISTS syncro_db_3892.papers (\n    paperid text NOT NULL\n);;');
    expect(client.queries).toContain('ALTER TABLE ONLY syncro_db_3892.papers ADD CONSTRAINT papers_pkey PRIMARY KEY (paperid)');
    expect(client.queries).toContain('SELECT pg_advisory_unlock($1)');
    expect(client.release).toHaveBeenCalledTimes(1);
});

test('rejects incomplete backup before opening a database connection', async () => {
    fs.readFile.mockReset();
    fs.readFile.mockResolvedValueOnce(schema).mockResolvedValueOnce('');

    await expect(migrate()).rejects.toThrow('Database dump is incomplete');
    expect(db.connect).not.toHaveBeenCalled();
});

test('releases the migration lock and client when migration fails', async () => {
    const client = createClient({ failSchema: true });
    db.connect.mockResolvedValue(client);

    await expect(migrate()).rejects.toThrow('schema denied');
    expect(client.queries).toContain('SELECT pg_advisory_unlock($1)');
    expect(client.release).toHaveBeenCalledTimes(1);
});
