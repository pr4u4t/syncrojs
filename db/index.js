const { Pool } = require('pg');
const config = require('config');
const host = config.get('db.host');
const port = config.get('db.port');
const user = config.get('db.user');
const pass = config.get('db.password');
const db = config.get('db.database');

const pgpool = new Pool({
    user: user,
    host: host,
    database: db,
    password: pass,
    port: port,
})

module.exports = {
    query: (text, params) => pgpool.query(text,params),
}
