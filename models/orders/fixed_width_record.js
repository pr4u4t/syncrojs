const normalizeField = (value) => value.replace(/  +/g, ' ').trim();

const splitFixedFields = (value, widths) => {
    const fields = [];
    let offset = 0;
    for (const width of widths) {
        fields.push(normalizeField(value.slice(offset, offset + width)));
        offset += width;
    }
    return fields;
};

const orderFieldsFromMatch = (match) => {
    const fields = match.slice(1, 39).map(normalizeField);
    fields.push(...splitFixedFields(match[39], [15, 12, 30]));
    fields.push(...splitFixedFields(match[40], [5, 5, 5, 5, 5, 5, 4, 5, 6, 1, 8, 2]));
    fields.push(normalizeField(match[41]), normalizeField(match[42]));
    return fields;
};

// A record length fits when every non-blank record has the header's constant 'G' and nothing but blanks is left over.
const fitsRecordLength = (data, length) => {
    const pattern = new RegExp(`[^\\r\\n]{${length}}`, 'g');
    const records = (data.match(pattern) || []).filter((record) => record.trim() !== '');
    return records.length > 0 && records.every((record) => record[141] === 'G') && data.replace(pattern, '').trim() === '';
};

module.exports = { orderFieldsFromMatch, fitsRecordLength };
