const config = require('config');
const logger = require('../../utils/logger');
const { orderFieldsFromMatch, fitsRecordLength } = require('./fixed_width_record');

function isFlatArray(data) {
    return Array.isArray(data) && data.every(item => !Array.isArray(item));
}

const previewFields = ['Setup code', 'Web width (mm)', 'Quality ID', 'Flute ID', 'Order count', 'Crease measures (total)'];
const requiredCsvFields = [
    'setup_code', 'web_width_mm', 'quality_id', 'flute', 'order_number', 'customer_name',
    'sheet_width_mm', 'outs', 'sheet_length_mm', 'quantity_sheets', 'sheets_per_stack', 'score_measures_mm'
];

const requiredOzdanFields = [
    'program_number_drcorr', 'fosberoid', 'quality', 'flute', 'web_width', 'z1_customer_name', 'z1_order_number_optima',
    'z1_width', 'z1_length', 'z1_pieces', 'z1_out_box', 'z1_sheets_per_stack'
];

const parseCsv = (data, delimiter = ',') => {
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
        } else if (character === delimiter) {
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
    const headers = csvHeaders(records[0]);
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
        setup.preview = setupPreview(setup);
    }
    return [...setups.values()];
};

const csvHeaders = (record) => record.map((header, index) => (index === 0 ? header.replace(/^\uFEFF/, '') : header).trim().toLowerCase());

const setupPreview = (setup) => [
    setup.setupCode,
    setup.webWidthMm,
    setup.qualityId,
    setup.fluteId,
    setup.orders.map((order) => order.orderNumber).join(', '),
    setup.orders.reduce((total, order) => total + order.scores.length, 0)
];

// zN_level 20 / 10 is the DAT fixed field Ozdan copies into the CSV, not a knife level.
const OZDAN_DAT_FIXED_LEVELS = ['10', '20'];
const OZDAN_MAX_ORDER_NUMBER = 30;
const OZDAN_MAX_CUSTOMER_NAME = 15;
const EMPTY_OZDAN_ORDER = ['', '', '', ...Array(8).fill('0'), '', '', '', ''];

// Levels are written only when the file sets one: a missing one copies an order with the same length, otherwise order 1 -> 3, later -> 1.
// Without any, the DAT fixed values stay and mac2syncro derives the levels as it does for Ord_mac.dat.
const resolveOzdanLevels = (blocks) => {
    const levels = blocks.map((block) => (OZDAN_DAT_FIXED_LEVELS.includes(block.level) ? '' : block.level));
    if (!levels.some(Boolean)) return blocks.map((block) => block.level);
    return levels.map((level, index) => {
        if (level) return level;
        const twin = levels.findIndex((other, otherIndex) => other && blocks[otherIndex].length === blocks[index].length);
        return twin >= 0 ? levels[twin] : (index === 0 ? '3' : '1');
    });
};

// DAT has 5 score fields where Ozdan has 17; measures beyond the fifth are packed into the last one, which scorer() splits again.
const ozdanScoreFields = (scores, sheetWidth) => {
    if (scores.length === 0) return [sheetWidth, '0', '0', '0', '0'];
    const fields = [...scores.slice(0, 4), scores.slice(4).join(' ') || '0'];
    while (fields.length < 5) fields.splice(fields.length - 1, 0, '0');
    return fields;
};

// Ozdan OrdList.csv: one setup per row with up to three z1-z3 order blocks, separated by ';'.
// Each row becomes the 55-field row Ord_mac.dat parses into; dimensions already are tenths of a millimeter.
const parseOzdanCsv = (records) => {
    const headers = csvHeaders(records[0]);
    if (new Set(headers).size !== headers.length) throw new Error('Duplicate column in Ozdan orders CSV');
    const missingHeaders = requiredOzdanFields.filter((header) => !headers.includes(header));
    if (missingHeaders.length) throw new Error(`Missing Ozdan orders CSV columns: ${missingHeaders.join(', ')}`);
    return records.slice(1).map((record, index) => {
        // Ozdan terminates every data row with an extra ';'.
        const values = record.length === headers.length + 1 && record[headers.length].trim() === '' ? record.slice(0, -1) : record;
        if (values.length !== headers.length) throw new Error(`Ozdan orders CSV row ${index + 2} has the wrong number of columns`);
        const row = Object.fromEntries(headers.map((header, column) => [header, values[column].trim()]));
        if (!row.fosberoid) throw new Error(`Ozdan orders CSV row ${index + 2} has no FOSBEROID`);

        const blocks = [];
        for (let block = 1; block <= 3; block += 1) {
            const field = (name) => row[`z${block}_${name}`] || '';
            if (!field('order_number_optima')) continue;
            if (field('order_number_optima').length > OZDAN_MAX_ORDER_NUMBER) {
                throw new Error(`Ozdan orders CSV row ${index + 2} has an order number longer than ${OZDAN_MAX_ORDER_NUMBER} characters`);
            }
            const scores = headers
                .filter((header) => header.startsWith(`z${block}_score_`))
                .map((header) => row[header])
                .filter((score) => score !== '' && Number(score) !== 0);
            blocks.push({ field, scores, length: field('length'), level: field('level') });
        }
        const levels = resolveOzdanLevels(blocks);

        const orderFields = blocks.map(({ field, scores }, blockIndex) => [
            field('customer_name').slice(0, OZDAN_MAX_CUSTOMER_NAME),
            field('customer_number_drcorr'),
            field('order_number_optima'),
            field('width'),
            ...ozdanScoreFields(scores, field('width')),
            field('sheets_per_stack'),
            field('length'),
            field('pieces'),
            field('out_box'),
            row.order_length_m || '',
            levels[blockIndex]
        ]);
        while (orderFields.length < 3) orderFields.push(EMPTY_OZDAN_ORDER);

        const papers = [1, 2, 3, 4, 5].map((paper) => row[`paper_${paper}`] || '').join('');
        return [row.program_number_drcorr, papers, row.fosberoid, row.quality, row.flute, row.web_width, row.order_length_m || '', 'G', ...orderFields.flat(), '', ''];
    });
};

const DAT740_LENGTH = 740;
const DAT740_ORDER_WIDTHS = [15, 12, 30, 5, ...Array(15).fill(5), 4, 5, 6, 2, 8, 2];

// Maps a 740-character record onto the 55-field row used by mac2syncro; the 15 creases are packed into the first score field.
const dat740Row = (record) => {
    const read = (offset, width) => record.slice(offset, offset + width).replace(/  +/g, ' ').trim();
    const row = [];
    let offset = 0;
    for (const width of [11, 44, 12, 36, 23, 5, 10, 1]) {
        row.push(read(offset, width));
        offset += width;
    }
    for (let order = 0; order < 3; order += 1) {
        const fields = DAT740_ORDER_WIDTHS.map((width) => {
            const value = read(offset, width);
            offset += width;
            return value;
        });
        const scores = fields.slice(4, 19).filter((value) => value !== '' && value !== '0');
        row.push(...fields.slice(0, 4), scores.join(' '), '', '', '', '', ...fields.slice(19));
    }
    row.push(read(offset, 8), read(offset + 8, 8));
    return row;
};

const mac_parse = function(data){
    logger.debug('extended_orders: mac_parse');
    const firstLine = String(data).split(/\r\n|\r|\n/, 1)[0].replace(/^\uFEFF/, '').toLowerCase();
    if (firstLine.split(',').map((field) => field.trim()).includes('setup_code')) {
        const records = parseCsv(String(data));
        if (records.length < 2) throw new Error('Extended orders CSV contains no order rows');
        return parseExtendedOrdersCsv(records);
    }
    if (firstLine.split(';').map((field) => field.trim()).includes('program_number_drcorr')) {
        const records = parseCsv(String(data), ';');
        if (records.length < 2) throw new Error('Ozdan orders CSV contains no order rows');
        return parseOzdanCsv(records);
    }

    const fits589 = fitsRecordLength(data, 589);
    const fits740 = fitsRecordLength(data, DAT740_LENGTH);
    // A CSV with an unknown header must not be read as fixed-width records.
    if (!fits589 && !fits740 && /[,;]/.test(firstLine)) {
        throw new Error('Unrecognised orders CSV: expected a setup_code or program_number_drcorr header');
    }
    // A single 740 record with an empty third order also fits 589; order 1's fixed "20" field breaks the tie.
    const firstRecord = String(data).replace(/^[\r\n]+/, '');
    const prefer740 = fits740 && (!fits589 || (firstRecord.slice(304, 306) === '20' && firstRecord.slice(254, 256) !== '20'));
    if (prefer740) {
        return (data.match(new RegExp(`[^\\r\\n]{${DAT740_LENGTH}}`, 'g')) || [])
            .filter((record) => record.trim() !== '')
            .map(dat740Row);
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
