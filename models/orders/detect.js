const path = require('path');
const { fitsRecordLength } = require('./fixed_width_record');

// Order 1's fixed "20" field: offset per DAT record length (classic 587, extended 589 and 740).
const ORDER1_FIXED_FIELD = { 587: 253, 589: 254, 740: 304 };

const csvHasColumn = (firstLine, separator, column) => firstLine.split(separator).map((field) => field.trim()).includes(column);

// Picks the parser for an uploaded order file from its content, so an import does not depend on the configured parser.
// Returns 'extended_orders' (every CSV, the parser rejects an unknown header), 'classic' or null when unknown.
const detectOrderFormat = (data, fileName = '') => {
    const text = String(data);
    const firstLine = text.split(/\r\n|\r|\n/, 1)[0].replace(/^﻿/, '').toLowerCase();
    if (csvHasColumn(firstLine, ',', 'setup_code') || csvHasColumn(firstLine, ';', 'program_number_drcorr')
        || path.extname(fileName).toLowerCase() === '.csv') {
        return 'extended_orders';
    }

    const lengths = Object.keys(ORDER1_FIXED_FIELD).map(Number).filter((length) => fitsRecordLength(text, length));
    // Several lengths fit only a single record; the position of order 1's fixed "20" field decides.
    const record = text.replace(/^[\r\n]+/, '');
    const length = lengths.length === 1
        ? lengths[0]
        : lengths.find((candidate) => record.slice(ORDER1_FIXED_FIELD[candidate], ORDER1_FIXED_FIELD[candidate] + 2) === '20');
    if (length === undefined) return null;
    return length === 587 ? 'classic' : 'extended_orders';
};

module.exports = { detectOrderFormat };
