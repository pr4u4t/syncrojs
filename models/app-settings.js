const db = require('../db');

const isDatabaseSetting = (path) => /(^|\.)(database|db)(\.|$)|^PG(?:HOST|PORT|DATABASE|USER|PASSWORD)$/i.test(path);

const initialize = async function(){
    await db.query(`
        CREATE TABLE IF NOT EXISTS public.app_settings (
            setting_key text PRIMARY KEY,
            setting_value jsonb NOT NULL,
            updated_at timestamptz NOT NULL DEFAULT now()
        )
    `);
};

const toNestedObject = function(entries){
    const settings = {};
    for(const [path,value] of Object.entries(entries)){
        if(path === 'server.secret' || isDatabaseSetting(path)){
            continue;
        }
        const parts = path.split('.');
        let current = settings;
        for(const part of parts.slice(0,-1)){
            current[part] = current[part] || {};
            current = current[part];
        }
        current[parts[parts.length-1]] = value;
    }
    return settings;
};

const load = async function(){
    const {rows} = await db.query('SELECT setting_key, setting_value FROM public.app_settings');
    const entries = Object.fromEntries(rows.map((row) => [row.setting_key, row.setting_value]));
    return toNestedObject(entries);
};

const save = async function(entries){
    const allowedEntries = Object.fromEntries(
        Object.entries(entries).filter(([path]) => path !== 'server.secret' && !isDatabaseSetting(path))
    );
    if(Object.keys(allowedEntries).length === 0){
        return;
    }
    await db.query(`
        INSERT INTO public.app_settings (setting_key, setting_value)
        SELECT key, value FROM jsonb_each($1::jsonb)
        ON CONFLICT (setting_key) DO UPDATE
        SET setting_value = EXCLUDED.setting_value, updated_at = now()
    `, [JSON.stringify(allowedEntries)]);
};

module.exports = {
    initialize,
    load,
    save,
    toNestedObject,
    isDatabaseSetting
};