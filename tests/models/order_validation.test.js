const { validateSyncroPayload } = require('../../models/order-validation');

const makePayload = ({
    outs = [1, 1, 1],
    lengths = [12000, 12000, 9090],
    quantities = [400, 400, 528],
    levels = [3, 3, 1]
} = {}) => {
    const payload = Array(102).fill(' ');
    const webWidth = 25000;
    const sheetWidth = 8000;
    payload[0] = '9001.2';
    payload[1] = String(webWidth);
    payload[2] = '0';
    payload[3] = '30';
    payload[4] = 'Q1';
    payload[5] = 'A';
    payload[6] = '285';
    payload[7] = '27';
    payload[8] = String(webWidth - outs.reduce((sum, value) => sum + sheetWidth * value, 0));

    for (let index = 0; index < 3; index += 1) {
        const start = 9 + index * 31;
        payload[start] = `ORDER-${index + 1}`;
        payload[start + 2] = `Customer ${index + 1}`;
        payload[start + 3] = String(sheetWidth);
        payload[start + 4] = String(outs[index]);
        payload[start + 5] = String(sheetWidth);
        payload[start + 6] = 'A';
        payload[start + 7] = '2';
        payload[start + 10] = String(levels[index]);
        payload[start + 11] = String(lengths[index]);
        payload[start + 12] = String(quantities[index]);
        payload[start + 13] = '100';
    }
    return payload;
};

test('accepts a valid three-order setup with shared levels and a sufficiently long run', () => {
    expect(validateSyncroPayload(makePayload())).toBeNull();
});

test('returns a validation error for an invalid trim type without throwing', () => {
    const payload = makePayload();
    payload[2] = '9';

    expect(validateSyncroPayload(payload)).toMatchObject({ error: 'Setup ma nieprawidłowy typ trimu.' });
});

test('rejects total outs above fourteen and maps the error back to DAT fields', () => {
    const source = Array(55).fill('');
    const error = validateSyncroPayload(makePayload({ outs: [9, 6, 0] }), source);

    expect(error.error).toMatch(/łączna liczba użytków.*15.*14/);
    expect(error.fieldIndexes).toEqual([20, 35]);
});

test('rejects different cut lengths assigned to one knife level', () => {
    const error = validateSyncroPayload(makePayload({ levels: [3, 1, 1] }));

    expect(error.error).toMatch(/tym samym poziomie.*tę samą długość/);
});

test('rejects sheet cuts shorter than the configured knife minimum', () => {
    const error = validateSyncroPayload(makePayload({ lengths: [4000, 12000, 9090], quantities: [1000, 400, 528] }));

    expect(error.error).toMatch(/mniejsza od minimum 500 mm/);
});

test('uses the configured maximum cut length for the selected physical knife level', () => {
    const error = validateSyncroPayload(makePayload({
        lengths: [36000, 36000, 9090],
        quantities: [400, 400, 528],
        levels: [2, 2, 1]
    }));

    expect(error.error).toMatch(/przekracza maksimum 3500 mm/);
});

test('rejects crease measurements that are zero, malformed, or wider than the sheet', () => {
    const payload = makePayload();
    payload[14] = '0';
    expect(validateSyncroPayload(payload).error).toMatch(/bigi muszą być dodatnie/);

    payload[14] = '8001';
    expect(validateSyncroPayload(payload).error).toMatch(/przekraczać szerokości arkusza/);
});

test('limits total crease measurements across the setup to twenty-two', () => {
    const payload = makePayload();
    payload[14] = Array(23).fill('1000').join(' ');

    expect(validateSyncroPayload(payload).error).toMatch(/zawiera 23 bigów; limit to 22/);
});

test('rejects total setup length below the Syncro AOC minimum', () => {
    const payload = makePayload({
        outs: [1, 1, 0],
        lengths: [12000, 12000, 0],
        quantities: [1, 1, 0],
        levels: [3, 3, 0]
    });
    payload[1] = '16160';
    payload[8] = '160';
    const error = validateSyncroPayload(payload);

    expect(error.error).toMatch(/mniejsza od wymaganego minimum 29380 mm/);
});

test('matches Syncro equalization of a shorter first order before the setup-length check', () => {
    const payload = makePayload({
        outs: [1, 1, 0],
        lengths: [10000, 10000, 0],
        quantities: [10, 40, 0],
        levels: [3, 3, 0]
    });
    payload[1] = '18100';
    payload[8] = '2100';

    expect(validateSyncroPayload(payload)).toBeNull();
});

test('does not equalize a run when its difference is less than one sheet length', () => {
    const payload = makePayload({
        outs: [1, 1, 0],
        lengths: [50000, 50000, 0],
        quantities: [4, 5, 0],
        levels: [3, 3, 0]
    });
    payload[1] = '18100';
    payload[8] = '2100';

    const error = validateSyncroPayload(payload);
    expect(error.error).toMatch(/mniejsza od wymaganego minimum 29380 mm/);
});

test('checks configured Twin web width, minimum setup width, and normal trim limits', () => {
    const valid = makePayload();
    valid[1] = '25500';
    valid[8] = '1500';
    expect(validateSyncroPayload(valid).error).toMatch(/przekracza maksimum 2543 mm/);

    const tooNarrow = makePayload({ outs: [1, 0, 0] });
    tooNarrow[1] = '9000';
    tooNarrow[8] = '1000';
    expect(validateSyncroPayload(tooNarrow).error).toMatch(/mniejsza od minimum 1000 mm/);

    const narrowTrim = makePayload();
    narrowTrim[1] = '24050';
    narrowTrim[8] = '50';
    expect(validateSyncroPayload(narrowTrim).error).toMatch(/mniejszy od minimum 16 mm/);
});
