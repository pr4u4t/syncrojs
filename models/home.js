const { XMLParser } = require("fast-xml-parser");
const parser = new XMLParser();
const logger = require('../utils/logger');
const { connect: syncroConnect } = require('../utils/syncro');

const home_info = async function(){
    const cmd = "Home.Info";
    let psocket;
    try {
        psocket = await syncroConnect();
        const written = await psocket.write(cmd);
        if(written !== Buffer.byteLength(cmd)){
            logger.warn('Incomplete Home.Info request write');
            return null;
        }
        const response = await psocket.readAll();
        const xml = response ? response.toString('utf8').trim() : '';
        if(!xml){
            logger.warn('Empty Home.Info response');
            return null;
        }
        const parsed = parser.parse(xml);
        if(!parsed || !parsed.NewDataSet || !parsed.NewDataSet.Home){
            logger.warn('Home.Info response did not contain NewDataSet.Home');
            return null;
        }
        return parsed.NewDataSet.Home;
    }catch(error){
        logger.error('Failed to fetch Home Info');
        logger.error(error);
        return null;
    }finally{
        if(psocket){
            psocket.destroy();
        }
    }
}

const requestVersion = async function(command){
    let psocket;
    try{
        psocket = await syncroConnect();
        await psocket.write(command);
        const response = await psocket.readAll();
        const version = response.toString().trim();
        return version || null;
    }catch(error){
        logger.debug({error, command}, 'Failed to fetch Syncro version');
        return null;
    }finally{
        if(psocket){
            psocket.destroy();
        }
    }
};

const syncro_versions = async function(){
    const [syncro, data, ite] = await Promise.all([
        requestVersion('Syncro.Version'),
        requestVersion('Syncro.DataVersion'),
        requestVersion('Syncro.IteVersion')
    ]);
    if(syncro === null && data === null && ite === null){
        return null;
    }
    return { syncro, data, ite };
};

module.exports = {
    home_info,
    syncro_versions
}
