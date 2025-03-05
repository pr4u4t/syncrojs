const express = require('express');
const config = require('config');
const loglevel = config.get("log.level") || "info";
const logger = require('pino')({"level": loglevel});
const cookieParser = require("cookie-parser");
const sessions = require('express-session');
const template = require('./templates');

const port = config.get('server.port') || "127.0.0.1";
const host = config.get('server.host') || 80;
const mountRoutes = require('./routes');
const secret = config.get('server.secret');

const app = express();
const oneDay = 1000 * 60 * 60 * 24;

app.use(sessions({
  secret: secret,
  saveUninitialized:true,
  cookie: { maxAge: oneDay },
  resave: false 
}));

app.use(cookieParser());
mountRoutes(app);
app.use(express.static('public'));

app.use(function(req,res,next){
  logger.error("HTTP 404");
  logger.error(req.url);
  template.render('error.twig', {
    'title': 'HTTP 404 Not Found',
    'msg': 'Page not found'
  }).then((output) => {
    res.end(output);
  });
})

app.use(function(err,req,res,next){
  logger.error("HTTP 500");
  logger.error(err);
  logger.error(req.url);
  template.render('error.twig', {
    'title': 'HTTP 500 Internal Error',
    'msg': 'Fatal error occurred, please contact support'
  }).then((output) => {
    res.end(output);
  });
})

app.listen(port, host, () => {
  logger.debug('Node.js Express server listening on '+host+":"+port)
})
