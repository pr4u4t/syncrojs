const db = require('../db');
const cache = require('./cache');
const orders = require('../models/order');

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

const clearMaterialCache = (name) => {
    const invalidatedKeys = cache.keys().filter((key) => key.startsWith(`${name}.`));
    if (invalidatedKeys.length) cache.del(invalidatedKeys);
};

const materialError = (code, details) => Object.assign(new Error(code), { code, ...details });

const isSameId = (first, second) => String(first ?? '').trim().toLowerCase() === String(second ?? '').trim().toLowerCase();

const PAPER_SLOTS = [1, 2, 3, 4, 5, 6, 7];

// The database has no foreign keys, so the links between materials are enforced here.
const qualitiesUsingPaper = async (paperId, weight) => {
    const conditions = PAPER_SLOTS.map((slot) => `(lower(btrim(paper${slot}id)) = lower(btrim($1)) AND paper${slot}weightfactor_uwg_m = $2)`);
    const result = await db.query(`SELECT qualityid FROM syncro_db_3892.quality WHERE ${conditions.join(' OR ')} ORDER BY qualityid`, [paperId, weight]);
    return result.rows.map((row) => row.qualityid);
};

const qualitiesUsingFlute = async (fluteId) => {
    const result = await db.query('SELECT qualityid FROM syncro_db_3892.quality WHERE lower(btrim(flute)) = lower(btrim($1)) ORDER BY qualityid', [fluteId]);
    return result.rows.map((row) => row.qualityid);
};

// Fails closed: a queue that cannot be read, or rows without the field, block the change.
const queuedSetupsUsing = async (field, id) => {
    const queue = await orders.orders_all();
    if (!Array.isArray(queue) || queue.some((row) => !(field in row))) throw materialError('MATERIAL_USAGE_UNKNOWN');
    return queue.filter((row) => isSameId(row[field], id)).map((row) => String(row.SetupCode ?? '').trim());
};

const assertNotUsed = (type, ids) => {
    if (ids.length) throw materialError('MATERIAL_IN_USE', { usedBy: { type, ids } });
};

const assertDeletable = async (name, [id, weight]) => {
    if (name === 'papers') {
        assertNotUsed('quality', await qualitiesUsingPaper(id, weight));
    } else if (name === 'flute') {
        assertNotUsed('quality', await qualitiesUsingFlute(id));
        assertNotUsed('queue', await queuedSetupsUsing('Flute', id));
    } else if (name === 'quality') {
        assertNotUsed('queue', await queuedSetupsUsing('QualityID', id));
    }
};

const isBlankReference = (id) => /^-*$/.test(String(id ?? '').trim());

// A quality may only point at papers and a flute that exist; blank and '---' mean an unused slot.
const assertQualityReferences = async (values) => {
    const missing = [];
    for (const slot of PAPER_SLOTS) {
        const id = values[`paper${slot}id`];
        const weight = values[`paper${slot}weightfactor_uwg_m`];
        if (isBlankReference(id)) continue;
        const found = await db.query('SELECT 1 FROM syncro_db_3892.papers WHERE lower(btrim(paperid)) = lower(btrim($1)) AND weightfactor_uwg_m = $2', [id, Number(weight)]);
        if (found.rows.length === 0) missing.push(`paper ${String(id).trim()} / ${weight}`);
    }
    if (!isBlankReference(values.flute)) {
        const found = await db.query('SELECT 1 FROM syncro_db_3892.flute WHERE lower(btrim(flute)) = lower(btrim($1))', [values.flute]);
        if (found.rows.length === 0) missing.push(`flute ${String(values.flute).trim()}`);
    }
    if (missing.length) throw materialError('MATERIAL_REFERENCE_MISSING', { missing });
};

const createMaterial = async (name, values) => {
    const definition = getDefinition(name);
    if (!values || typeof values !== 'object' || Array.isArray(values)) {
        throw new Error('Invalid material');
    }

    const fields = getEditorFields(name);
    const writableFields = fields.filter((field) => !field.readOnly);
    const parameters = writableFields.map((field) => {
        const value = parseValue(field, values[field.name]);
        if (field.type !== 'text') return value;
        if (field.primaryKey && !value.trim()) throw new Error(`Invalid value for ${field.name}`);
        // A blank text column holds ' ', the schema default.
        return value.trim() || ' ';
    });
    if (name === 'quality') await assertQualityReferences(values);
    // lastused is read-only in the form and NOT NULL in the table.
    const readOnlyFields = fields.filter((field) => field.readOnly);
    const columns = [...writableFields, ...readOnlyFields].map((field) => field.name);
    const placeholders = [...writableFields.map((_, index) => `$${index + 1}`), ...readOnlyFields.map(() => 'NOW()')];
    const query = `INSERT INTO ${definition.table} (${columns.join(', ')}) VALUES (${placeholders.join(', ')}) RETURNING *`;
    const result = await db.query(query, parameters);

    clearMaterialCache(name);
    return result.rows[0];
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
    if (name === 'quality') await assertQualityReferences(values);
    // The weight is part of the paper key, and qualities point at the paper by id and weight.
    if (name === 'papers' && Number(values.weightfactor_uwg_m) !== keyValues[1]) {
        assertNotUsed('quality', await qualitiesUsingPaper(keyValues[0], keyValues[1]));
    }
    const conditions = definition.keyFields.map((field, index) => `${field} = $${editableFields.length + index + 1}`);
    const query = `UPDATE ${definition.table} SET ${assignments.join(', ')} WHERE ${conditions.join(' AND ')} RETURNING *`;
    const result = await db.query(query, [...parameters, ...keyValues]);
    if (result.rowCount === 0) return null;

    clearMaterialCache(name);
    return result.rows[0];
};

const deleteMaterial = async (name, key) => {
    const definition = getDefinition(name);
    if (!key || typeof key !== 'object') throw new Error('Invalid material delete');

    const fields = getEditorFields(name);
    const keyValues = definition.keyFields.map((name) => {
        const field = fields.find((candidate) => candidate.name === name);
        if (!(name in key)) throw new Error(`Missing key field: ${name}`);
        return parseValue(field, key[name]);
    });
    await assertDeletable(name, keyValues);
    const conditions = definition.keyFields.map((field, index) => `${field} = $${index + 1}`);
    const result = await db.query(`DELETE FROM ${definition.table} WHERE ${conditions.join(' AND ')}`, keyValues);
    if (result.rowCount === 0) return false;

    clearMaterialCache(name);
    return true;
};

const sendWriteError = (res, error, invalidCode) => {
    if (error.code === '23505') return res.status(409).json({ error: 'material_conflict' });
    if (error.code === 'MATERIAL_IN_USE') return res.status(409).json({ error: 'material_in_use', usedBy: error.usedBy });
    if (error.code === 'MATERIAL_USAGE_UNKNOWN') return res.status(503).json({ error: 'material_usage_unknown' });
    if (error.code === 'MATERIAL_REFERENCE_MISSING') return res.status(400).json({ error: 'material_reference_missing', missing: error.missing });
    if (!error.code) return res.status(400).json({ error: invalidCode });
    throw error;
};

const attachEditorRoute = (router, name) => {
    router.post('/create', async (req, res) => {
        try {
            await createMaterial(name, req.body && req.body.values);
            return res.status(201).json({ saved: true });
        } catch (error) {
            return sendWriteError(res, error, 'invalid_material_update');
        }
    });
    router.post('/delete', async (req, res) => {
        try {
            const deleted = await deleteMaterial(name, req.body && req.body.key);
            if (!deleted) return res.status(404).json({ error: 'material_not_found' });
            return res.json({ deleted: true });
        } catch (error) {
            return sendWriteError(res, error, 'invalid_material_delete');
        }
    });
    router.post('/update', async (req, res) => {
        try {
            const row = await updateMaterial(name, req.body && req.body.key, req.body && req.body.values);
            if (!row) return res.status(404).json({ error: 'material_not_found' });
            return res.json({ saved: true });
        } catch (error) {
            return sendWriteError(res, error, 'invalid_material_update');
        }
    });
};

module.exports = {
    getEditorFields,
    getDefinition,
    createMaterial,
    updateMaterial,
    deleteMaterial,
    attachEditorRoute
};
