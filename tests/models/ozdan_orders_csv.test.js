jest.mock('../../models/quality', () => ({
    quality_params_by_id: jest.fn().mockResolvedValue({ rows: [{ weightfactoruwg_m: 285, thickness_uddmm: 27, scoregap_uddmm: 30 }] })
}));
jest.mock('../../models/flute', () => ({
    flute_params_by_id: jest.fn().mockResolvedValue({ rows: [{}] })
}));

const fs = require('node:fs');
const path = require('node:path');
const extendedOrders = require('../../models/orders/extended_orders');
const classicParser = require('../../models/orders/classic');
const orders = require('../../models/order');
const { validateConversion } = require('../../models/order-validation');

const fixture = (name) => fs.readFileSync(path.join(__dirname, 'ozdan_data', name), 'latin1');
const csv = fixture('OrdList.csv');

const LEVEL_TOKENS = [19, 50, 81];

test('parses OrdList.csv into the same 55-field rows as Ord_mac.dat', () => {
    const rows = extendedOrders.mac_parse(csv);
    const datRows = classicParser.mac_parse(fixture('Ord_mac.dat'));

    expect(rows).toHaveLength(datRows.length);
    rows.forEach((row, index) => {
        expect(row).toHaveLength(orders.fields.length);
        // The .dat carries placeholder quality/flute in fields 3 and 4.
        const differences = row.map((value, field) => (value === datRows[index][field] ? null : field)).filter((field) => field !== null);
        expect(differences.filter((field) => field !== 3 && field !== 4)).toEqual([]);
    });
    expect(rows[0].slice(2, 6)).toEqual(['5122', 'b450', 'B', '24500']);
    expect(rows[0].slice(8, 23)).toEqual(['POL-KARTON', '407', 'R952026103', '3960', '1180', '1600', '1180', '0', '0', '690', '12100', '0', '6', '0', '20']);
});

test('keeps more creases than DAT has score fields and sends them all', async () => {
    const [header, row] = csv.split('\r\n');
    const columns = header.split(';');
    const values = row.split(';');
    const measures = [100, 200, 300, 400, 500, 600, 700];
    measures.forEach((measure, index) => { values[columns.indexOf(`z1_score_${index + 1}`)] = String(measure); });

    const [parsed] = extendedOrders.mac_parse(`${header}\r\n${values.join(';')}\r\n`);
    expect(parsed.slice(12, 17)).toEqual(['100', '200', '300', '400', '500 600 700']);
    expect((await orders.mac2syncro(parsed)).result[14]).toBe(measures.join(' '));
});

test('keeps knife levels 1-3 and derives the DAT fixed values 20 / 10 from the sheet lengths', async () => {
    const [header, , , row] = csv.split('\r\n');
    const columns = header.split(';');
    const values = row.split(';');
    expect(values[columns.indexOf('z1_level')]).toBe('20');
    const levels = async (source) => {
        const { result } = await orders.mac2syncro(extendedOrders.mac_parse(source)[0]);
        return LEVEL_TOKENS.slice(0, 2).map((token) => result[token]);
    };
    expect(await levels(`${header}\r\n${row}\r\n`)).toEqual(['3', '1']);

    values[columns.indexOf('z1_level')] = '2';
    values[columns.indexOf('z2_level')] = '';
    values[columns.indexOf('z2_length')] = values[columns.indexOf('z1_length')];
    expect(await levels(`${header}\r\n${values.join(';')}\r\n`)).toEqual(['2', '2']);
});

test('rejects an Ozdan knife level outside 1-3 and the DAT fixed values instead of replacing it', async () => {
    const [header, , , row] = csv.split('\r\n');
    const columns = header.split(';');
    const values = row.split(';');
    values[columns.indexOf('z1_level')] = '5';

    const converted = await orders.mac2syncro(extendedOrders.mac_parse(`${header}\r\n${values.join(';')}\r\n`)[0]);

    expect(converted.result).toBeNull();
    expect(converted.error).toBe('Zamówienie 1: nieprawidłowy poziom noża (5).');
});

test('converts Ozdan CSV rows like the classic parser converts the same orders from Ord_mac.dat', async () => {
    const rows = extendedOrders.mac_parse(csv);
    const classicRows = classicParser.mac_parse(fixture('Ord_mac.dat'));

    for (const index of [0, 1, 2, 3, 4, 5]) {
        const fromCsv = await orders.mac2syncro(rows[index]);
        const fromDat = await orders.mac2syncro(classicRows[index]);
        expect(fromCsv.error).toBeNull();
        expect(fromCsv.result).toHaveLength(102);

        const differences = fromCsv.result
            .map((value, token) => (value === fromDat.result[token] ? null : token))
            .filter((token) => token !== null);
        expect(differences).toEqual([4, 5]);
        expect([fromCsv.result[4], fromCsv.result[5]]).toEqual([rows[index][3], 'B']);
    }
});

test('sends quality and flute IDs as stored in the database, whatever their case in the file', async () => {
    const quality = require('../../models/quality');
    const flute = require('../../models/flute');
    quality.quality_params_by_id.mockResolvedValueOnce({ rows: [{ qualityid: 'B280', weightfactoruwg_m: 285, thickness_uddmm: 27, scoregap_uddmm: 30 }] });
    flute.flute_params_by_id.mockResolvedValueOnce({ rows: [{ flute: 'B' }] });

    const row = extendedOrders.mac_parse(csv)[3];
    const lowerCaseFlute = [...row];
    lowerCaseFlute[4] = 'b';
    const converted = await orders.mac2syncro(lowerCaseFlute);

    expect(row[3]).toBe('b280');
    expect(converted.result[4]).toBe('B280');
    expect(converted.result[5]).toBe('B');
});

test('reports an unknown quality on the row instead of failing the import', async () => {
    const quality = require('../../models/quality');
    quality.quality_params_by_id.mockResolvedValueOnce({ rows: [] });

    const converted = await orders.mac2syncro(extendedOrders.mac_parse(csv)[1]);

    expect(converted.result).toBeNull();
    expect(converted.error).toMatch(/Quality b500 was not found/);
});

test('rejects an Ozdan setup with zero sheets during validation', async () => {
    const converted = await orders.validate(extendedOrders.mac_parse(csv)[0], null, null, null, 'extended_orders');

    expect(converted.result).toBeNull();
    expect(converted.error).toMatch(/Zamówienie 1: liczba arkuszy musi być dodatnia/);
});

test('accepts more than five outs on one order while the setup total stays within 14', async () => {
    const row = [...extendedOrders.mac_parse(csv)[0]];
    expect(row[20]).toBe('6');
    row[19] = '1000';
    const converted = validateConversion(await orders.mac2syncro(row));

    expect(converted.error).toBeNull();
    expect(converted.result[13]).toBe(6);
});

test('rejects an Ozdan setup whose outs add up to more than 14', async () => {
    const row = [...extendedOrders.mac_parse(csv)[4]];
    // 15 × 160 mm leaves a 100 mm trim on the 2500 mm web, so only the outs total breaks a rule.
    [[8, '6'], [23, '6'], [38, '3']].forEach(([start, outs]) => {
        row[start + 3] = '1600';
        row[start + 4] = '1600';
        row[start + 12] = outs;
    });
    const converted = validateConversion(await orders.mac2syncro(row));

    expect(converted.result).toBeNull();
    expect(converted.error).toMatch(/Łączna liczba użytków w zestawie wynosi 15; limit to 14/);
});

test('rejects more sheets per stack than the stacker takes', async () => {
    const row = [...extendedOrders.mac_parse(csv)[1]];
    row[17] = '1001';
    const converted = validateConversion(await orders.mac2syncro(row));

    expect(converted.result).toBeNull();
    expect(converted.error).toMatch(/liczba arkuszy w stosie musi wynosić 0-1000/);
});

test('rejects an Ozdan row with the wrong number of columns', () => {
    const [header, row] = csv.split('\r\n');
    expect(() => extendedOrders.mac_parse(`${header}\r\n${row};extra\r\n`)).toThrow(/row 2 has the wrong number of columns/);
});

test('reports missing Ozdan columns', () => {
    const [header, row] = csv.split('\r\n');
    const withoutQuality = header.replace(';Quality;', ';Grade;');
    expect(() => extendedOrders.mac_parse(`${withoutQuality}\r\n${row}`)).toThrow(/Missing Ozdan orders CSV columns: quality/);
});
