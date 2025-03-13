const { Pool } = require('pg')
const config = require('config')
const host = config.get('db.host')
const port = config.get('db.port')
const user = config.get('db.user')
const pass = config.get('db.password')
const db = config.get('db.database')

const pgpool = new Pool({
    user: user,
    host: host,
    database: db,
    password: pass,
    port: port,
});

const query = async function(text, params){
    return await pgpool.query(text,params);
};

module.exports = {
    query
}
