const config = require('config');
const logger = require('../../utils/logger');
const { orderFieldsFromMatch } = require('./fixed_width_record');

function isFlatArray(data) {
    return Array.isArray(data) && data.every(item => !Array.isArray(item));
}

const previewFields = ['Setup code', 'Web width (mm)', 'Quality ID', 'Flute ID', 'Order count', 'Crease measures (total)'];
const requiredCsvFields = [
    'setup_code', 'web_width_mm', 'quality_id', 'flute', 'order_number', 'customer_name',
    'sheet_width_mm', 'outs', 'sheet_length_mm', 'quantity_sheets', 'sheets_per_stack', 'score_measures_mm'
];

const parseCsv = (data) => {
    const records = [];
    let record = [];
    let field = '';
    let quoted = false;

    for (let index = 0; index < data.length; index += 1) {
        const character = data[index];
        if (quoted) {
            if (character === '"' && data[index + 1] === '"') {
                field += '"';
                index += 1;
            } else if (character === '"') {
                quoted = false;
            } else {
                field += character;
            }
        } else if (character === '"') {
            if (field.length !== 0) throw new Error('Unexpected quote in extended orders CSV');
            quoted = true;
        } else if (character === ',') {
            record.push(field);
            field = '';
        } else if (character === '\n' || character === '\r') {
            record.push(field);
            if (record.some((value) => value.trim() !== '')) records.push(record);
            record = [];
            field = '';
            if (character === '\r' && data[index + 1] === '\n') index += 1;
        } else {
            field += character;
        }
    }

    if (quoted) throw new Error('Unclosed quote in extended orders CSV');
    record.push(field);
    if (record.some((value) => value.trim() !== '')) records.push(record);
    return records;
};

const parseExtendedOrdersCsv = (records) => {
    const headers = records[0].map((header, index) => (index === 0 ? header.replace(/^\uFEFF/, '') : header).trim().toLowerCase());
    if (new Set(headers).size !== headers.length) throw new Error('Duplicate column in extended orders CSV');
    const missingHeaders = requiredCsvFields.filter((header) => !headers.includes(header));
    if (missingHeaders.length) throw new Error(`Missing extended orders CSV columns: ${missingHeaders.join(', ')}`);

    const setups = new Map();
    for (let index = 1; index < records.length; index += 1) {
        const values = records[index];
        if (values.length !== headers.length) throw new Error(`Extended orders CSV row ${index + 1} has the wrong number of columns`);
        const row = Object.fromEntries(headers.map((header, column) => [header, values[column].trim()]));
        const setupCode = row.setup_code;
        if (!setupCode) throw new Error(`Extended orders CSV row ${index + 1} has no setup_code`);

        let setup = setups.get(setupCode);
        if (!setup) {
            setup = {
                format: 'extended_orders_csv',
                setupCode,
                webWidthMm: row.web_width_mm,
                qualityId: row.quality_id,
                fluteId: row.flute,
                orders: [],
                preview: null
            };
            setups.set(setupCode, setup);
        } else if (setup.webWidthMm !== row.web_width_mm || setup.qualityId !== row.quality_id || setup.fluteId !== row.flute) {
            throw new Error(`Extended orders CSV setup ${setupCode} has inconsistent shared fields`);
        }

        if (setup.orders.length >= 3) throw new Error(`Setup ${setupCode} has more than three orders`);
        const scores = row.score_measures_mm ? row.score_measures_mm.split(';').map((score) => score.trim()).filter(Boolean) : [];
        setup.orders.push({
            orderNumber: row.order_number,
            customerName: row.customer_name,
            sheetWidthMm: row.sheet_width_mm,
            outs: row.outs,
            sheetLengthMm: row.sheet_length_mm,
            quantitySheets: row.quantity_sheets,
            sheetsPerStack: row.sheets_per_stack,
            level: row.level || (setup.orders.length === 0 ? '3' : '1'),
            scores
        });
    }

    for (const setup of setups.values()) {
        const measureCount = setup.orders.reduce((total, order) => total + order.scores.length, 0);
        setup.preview = [
            setup.setupCode,
            setup.webWidthMm,
            setup.qualityId,
            setup.fluteId,
            setup.orders.map((order) => order.orderNumber).join(', '),
            measureCount
        ];
    }
    return [...setups.values()];
};

const mac_parse = function(data){
    logger.debug('extended_orders: mac_parse');
    const firstLine = String(data).split(/\r\n|\r|\n/, 1)[0].replace(/^\uFEFF/, '').toLowerCase();
    if (firstLine.split(',').map((field) => field.trim()).includes('setup_code')) {
        const records = parseCsv(String(data));
        if (records.length < 2) throw new Error('Extended orders CSV contains no order rows');
        return parseExtendedOrdersCsv(records);
    }

    const rows = [];
    const recordPattern = /(.{589})/g;
    const recordFields = /(.{11})(.{44})(.{12})(.{36})(.{23})(.{5})(.{10})(.{1})(.{15})(.{12})(.{30})(.{5})(.{5})(.{5})(.{5})(.{5})(.{5})(.{4})(.{5})(.{6})(.{2})(.{8})(.{2})(.{15})(.{12})(.{30})(.{5})(.{5})(.{5})(.{5})(.{5})(.{5})(.{4})(.{5})(.{6})(.{2})(.{8})(.{2})(.{57})(.{56})(.{8})(.{8})/;

    for (const match of data.matchAll(recordPattern)) {
        const record = match[0];
        if (record.replace(/\s/g, '') === '') continue;
        rows.push(orderFieldsFromMatch(record.match(recordFields)));
    }

    return rows;
};

const validate = async function(data, papers, quality, flute, parent){
    logger.debug('extended_orders: Validation started');
    logger.info(`extended_orders: mac rows: ${data ? data.length : 0}`);

    if (data && data.format === 'extended_orders_csv') {
        return parent.manual_setup_to_syncro(data);
    }

    if (isFlatArray(data) !== true) {
        const err = 'invalid data parameter should be flat array containing one order in mac format';
        logger.error(err);
        return {result: null, error: err};
    }

    return parent.mac2syncro(data, papers, quality, flute);
};

module.exports = { mac_parse, validate, preview_fields: previewFields };
