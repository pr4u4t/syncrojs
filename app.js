const express = require('express')
const config = require('config')
const cookieParser = require("cookie-parser");
const sessions = require('express-session');

const port = config.get('server.port') || "127.0.0.1"
const host = config.get('server.host') || 80
const mountRoutes = require('./routes')
const secret = config.get('server.secret')

const app = express()
const oneDay = 1000 * 60 * 60 * 24
app.use(sessions({
  secret: secret,
  saveUninitialized:true,
  cookie: { maxAge: oneDay },
  resave: false 
}))

app.use(cookieParser())
mountRoutes(app)
app.use(express.static('public'))

app.listen(port, host, () => {
  console.log('Node.js Express server listening on :'+host+":"+port);
});
