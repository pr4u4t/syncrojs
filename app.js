const express = require('express');
const config = require('config');
const port = config.get('server.port') || "127.0.0.1";
const host = config.get('server.host') || 80;
const mountRoutes = require('./routes')

const app = express()
mountRoutes(app)

app.listen(port, host, () => {
  console.log('Node.js Express server listening on :'+host+":"+port);
});
