const NodeCache = require("node-cache");
const config = require('config');
const ttl = config.get("cache.ttl") || 100;
const period = config.get("cache.period") || 120;
const cache = new NodeCache({ stdTTL: ttl, checkperiod: period });

module.exports = cache;