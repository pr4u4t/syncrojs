const { Pool } = require('pg')

const required = ['PGHOST', 'PGDATABASE', 'PGUSER', 'PGPASSWORD']
const missing = required.filter((name) => !process.env[name])

if (missing.length > 0) {
    throw new Error(`Missing required database environment variables: ${missing.join(', ')}`)
}

const pgpool = new Pool({
    user: process.env.PGUSER,
    host: process.env.PGHOST,
    database: process.env.PGDATABASE,
    password: process.env.PGPASSWORD,
    port: Number(process.env.PGPORT || 5432),
    connectionTimeoutMillis: Number(process.env.PGCONNECT_TIMEOUT || 3000),
    query_timeout: Number(process.env.PGQUERY_TIMEOUT || 5000),
});

const query = async function(text, params){
    return await pgpool.query(text,params);
};

module.exports = {
    query,
    connect: () => pgpool.connect()
}
