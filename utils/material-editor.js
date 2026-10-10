const db = require('../db');
const cache = require('./cache');

const definitions = {
    papers: {
        table: 'syncro_db_3892.papers',
        keyFields: ['paperid', 'weightfactor_uwg_m'],
        fields: [
            ['paperid', 'text', 15, true],
            ['addedbydownload', 'boolean'],
            ['maxsplicedbspeed_uvm_m', 'integer'],
            ['maxsplicesf1linerspeed_uvm_m', 'integer'],
            ['maxsplicesf1flutingspeed_uvm_m', 'integer'],
            ['maxsplicesf2linerspeed_uvm_m', 'integer'],
            ['maxsplicesf2flutingspeed_uvm_m', 'integer'],
            ['maxsplicesf3linerspeed_uvm_m', 'integer'],
            ['maxsplicesf3flutingspeed_uvm_m', 'integer'],
            ['corediameter_uddmm', 'integer'],
            ['weightfactor_uwg_m', 'integer'],
            ['defaultthickness_uddmmm', 'integer'],
            ['text', 'text', 30],
            ['lastused', 'datetime-local'],
            ['paperclassification', 'text', 36]
        ]
    },
    quality: {
        table: 'syncro_db_3892.quality',
        keyFields: ['qualityid'],
        fields: [
            ['qualityid', 'text', 36, true],
            ['qualitytext', 'text', 100],
            ['bundlelayersperstack', 'integer'],
            ['weightfactoruwg_m', 'integer'],
            ['bundlereversemode', 'boolean'],
            ['addedbydownload', 'boolean'],
            ['preprint', 'boolean'],
            ['inclinatelastbelt', 'boolean'],
            ['lastused', 'datetime-local'],
            ['paper1id', 'text', 12],
            ['paper1weightfactor_uwg_m', 'integer'],
            ['paper2id', 'text', 12],
            ['paper2weightfactor_uwg_m', 'integer'],
            ['paper3id', 'text', 12],
            ['paper3weightfactor_uwg_m', 'integer'],
            ['paper4id', 'text', 12],
            ['paper4weightfactor_uwg_m', 'integer'],
            ['paper5id', 'text', 12],
            ['paper5weightfactor_uwg_m', 'integer'],
            ['paper6id', 'text', 12],
            ['paper6weightfactor_uwg_m', 'integer'],
            ['paper7id', 'text', 12],
            ['paper7weightfactor_uwg_m', 'integer'],
            ['flute', 'text', 30],
            ['sfsposition', 'text', 3],
            ['flutetext', 'text', 100],
            ['scoregap_uddmm', 'integer'],
            ['grindingparameter_udm', 'integer'],
            ['lubricationparameter_udm', 'integer'],
            ['trimgrindingsmultiplier', 'integer'],
            ['grindingdistance_udm', 'integer'],
            ['lubricationtime_utds', 'integer'],
            ['productionspeed_uvm_m', 'integer'],
            ['aocspeed_uvm_m', 'integer'],
            ['cruisingspeed_uvm_m', 'integer'],
            ['thickness_uddmm', 'integer'],
            ['terminalflutecode', 'integer'],
            ['twin400internalflapdisabled', 'integer']
        ]
    },
    flute: {
        table: 'syncro_db_3892.flute',
        keyFields: ['flute'],
        fields: [
            ['flute', 'text', 30, true],
            ['sfsposition', 'text', 3],
            ['flutetext', 'text', 100],
            ['scoregap_uddmm', 'integer'],
            ['grindingparameter_udm', 'integer'],
            ['lubricationparameter_udm', 'integer'],
            ['trimgrindingsmultiplier', 'integer'],
            ['grindingdistance_udm', 'integer'],
            ['lubricationtime_utds', 'integer'],
            ['productionspeed_uvm_m', 'integer'],
            ['aocspeed_uvm_m', 'integer'],
            ['cruisingspeed_uvm_m', 'integer'],
            ['thickness_uddmm', 'integer'],
            ['terminalflutecode', 'integer'],
            ['twin400internalflapdisabled', 'integer']
        ]
    }
};

const getDefinition = (name) => {
    const definition = definitions[name];
    if (!definition) throw new Error(`Unknown material type: ${name}`);
    return definition;
};

const getEditorFields = (name) => getDefinition(name).fields.map(([field, type, length, primaryKey]) => ({
    name: field,
    type,
    ...(length ? { maxLength: length } : {}),
    ...(primaryKey ? { primaryKey: true } : {}),
    ...(field === 'lastused' ? { readOnly: true } : {})
}));

const parseValue = (field, value) => {
    if (field.type === 'text') {
        if (typeof value !== 'string' || (field.maxLength && value.length > field.maxLength)) {
            throw new Error(`Invalid value for ${field.name}`);
        }
        return value;
    }
    if (field.type === 'boolean') {
        if (value === true || value === 'true') return true;
        if (value === false || value === 'false') return false;
        throw new Error(`Invalid value for ${field.name}`);
    }
    if (field.type === 'integer') {
        if (value === null || value === undefined || String(value).trim() === '') {
            throw new Error(`Invalid value for ${field.name}`);
        }
        const number = Number(value);
        if (!Number.isInteger(number) || number < -2147483648 || number > 2147483647) {
            throw new Error(`Invalid value for ${field.name}`);
        }
        return number;
    }
    if (field.type === 'datetime-local') {
        if (typeof value !== 'string' || !/^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}(?::\d{2}(?:\.\d{1,6})?)?$/.test(value) || Number.isNaN(Date.parse(value))) {
            throw new Error(`Invalid value for ${field.name}`);
        }
        return value.replace('T', ' ');
    }
    throw new Error(`Unsupported field type: ${field.type}`);
};

const updateMaterial = async (name, key, values) => {
    const definition = getDefinition(name);
    if (!key || typeof key !== 'object' || !values || typeof values !== 'object' || Array.isArray(values)) {
        throw new Error('Invalid material update');
    }

    const fields = getEditorFields(name);
    const editableFields = fields.filter((field) => !field.primaryKey && !field.readOnly);
    const assignments = editableFields.map((field, index) => `${field.name} = $${index + 1}`);
    const parameters = editableFields.map((field) => parseValue(field, values[field.name]));
    const keyValues = definition.keyFields.map((name) => {
        const field = fields.find((candidate) => candidate.name === name);
        if (!(name in key)) throw new Error(`Missing key field: ${name}`);
        return parseValue(field, key[name]);
    });
    const conditions = definition.keyFields.map((field, index) => `${field} = $${editableFields.length + index + 1}`);
    const query = `UPDATE ${definition.table} SET ${assignments.join(', ')} WHERE ${conditions.join(' AND ')} RETURNING *`;
    const result = await db.query(query, [...parameters, ...keyValues]);
    if (result.rowCount === 0) return null;

    const invalidatedKeys = cache.keys().filter((key) => key.startsWith(`${name}.`));
    if (invalidatedKeys.length) cache.del(invalidatedKeys);
    return result.rows[0];
};

const attachEditorRoute = (router, name) => {
    router.post('/update', async (req, res) => {
        try {
            const row = await updateMaterial(name, req.body && req.body.key, req.body && req.body.values);
            if (!row) return res.status(404).json({ error: 'material_not_found' });
            return res.json({ saved: true });
        } catch (error) {
            if (error.code === '23505') return res.status(409).json({ error: 'material_conflict' });
            if (!error.code) return res.status(400).json({ error: 'invalid_material_update' });
            throw error;
        }
    });
};

module.exports = {
    getEditorFields,
    getDefinition,
    updateMaterial,
    attachEditorRoute
};
