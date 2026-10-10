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

module.exports = { orderFieldsFromMatch };
