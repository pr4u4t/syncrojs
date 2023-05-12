const db = require('../db')

const papers_all = async function(){
    const {rows,fields} = await db.query('SELECT * FROM syncro_db_3892.quality')
    return {rows,fields}
}

const papers_params_by_id = async function(id){
    const {rows,fields} = await db.query('SELECT qualitytext, weightfactoruwg_m, thickness_uddmm FROM syncro_db_3892.quality WHERE qualityid = \''+id+'\'')
    return {rows,fields}
}

module.exports = {
    papers_all,
    papers_params_by_id
}
