process.env.ALLOW_CONFIG_MUTATIONS = process.env.ALLOW_CONFIG_MUTATIONS || '1';

const express = require('express');
const config = require('config');
const loglevel = config.get("log.level") || "info";
const logger = require('pino')({"level": loglevel});
const cookieParser = require("cookie-parser");
const sessions = require('express-session');
const dbSettings = require('./models/app-settings');

const secret = process.env.SESSION_SECRET;

if (!secret) {
  throw new Error('SESSION_SECRET environment variable is required');
}

const start = async function(){
  try{
    const { migrate } = require('./db/migrate');
    await migrate();
  }catch(error){
    logger.error({error}, 'Unable to migrate database schema/data');
  }

  try{
    await dbSettings.initialize();
    config.util.extendDeep(config, await dbSettings.load());
  }catch(error){
    logger.error({error}, 'Unable to load database-backed settings; using file defaults');
  }

  const statusStream = require('./utils/status-stream');
  statusStream.start();
  const app = express();
  const oneDay = 1000 * 60 * 60 * 24;
  const mountRoutes = require('./routes');
  const port = config.get('server.port') || 3000;
  const host = config.get('server.host') || '0.0.0.0';

  app.use(sessions({
    secret: secret,
    saveUninitialized:true,
    cookie: { maxAge: oneDay },
    resave: false 
  }));

  app.use(cookieParser());
  app.use(express.json());
  app.use(express.urlencoded({ extended: false }));
  mountRoutes(app);
  app.use(express.static('public'));

  app.use(function(req,res,next){
    logger.error("HTTP 404");
    logger.error(req.url);
    res.status(404).json({ error: 'not_found' });
  });

  app.use(function(err,req,res,next){
    logger.error("HTTP 500");
    logger.error(err);
    logger.error(req.url);
    res.status(500).json({ error: 'internal_server_error' });
  });

  app.listen(port, host, () => {
    logger.debug('Node.js Express server listening on '+host+":"+port);
  });
};

start().catch((error) => {
  logger.fatal({error}, 'Application startup failed');
  process.exitCode = 1;
});
