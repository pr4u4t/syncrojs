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

test('parses every Ozdan OrdList.csv row as one setup with its z1-z3 orders', () => {
    const setups = extendedOrders.mac_parse(csv);

    expect(setups.map((setup) => setup.setupCode)).toEqual(['5122', '5148', '5149', '5150', '5151', '5152']);
    expect(setups.map((setup) => setup.orders.length)).toEqual([1, 1, 2, 1, 3, 3]);
    expect(setups[0]).toMatchObject({
        format: 'extended_orders_csv',
        webWidthMm: '2450',
        qualityId: 'b450',
        fluteId: 'B'
    });
    expect(setups[0].orders[0]).toEqual({
        orderNumber: 'R952026103',
        customerName: 'POL-KARTON',
        sheetWidthMm: '396',
        outs: '6',
        sheetLengthMm: '1210',
        quantitySheets: '0',
        sheetsPerStack: '690',
        scores: ['118', '160', '118'],
        level: '3'
    });
    expect(setups[4].orders.map((order) => order.level)).toEqual(['3', '3', '1']);
    expect(setups[4].preview).toEqual(['5151', '2500', 'b280', 'B', 'R1102026101, R792026101, R682026061', 0]);
});

test('keeps knife levels 1-3 and derives the DAT fixed values 20 / 10 from the sheet lengths', () => {
    const [header, , , row] = csv.split('\r\n');
    const columns = header.split(';');
    const values = row.split(';');
    expect(values[columns.indexOf('z1_level')]).toBe('20');
    expect(extendedOrders.mac_parse(`${header}\r\n${row}\r\n`)[0].orders.map((order) => order.level)).toEqual(['3', '1']);

    values[columns.indexOf('z1_level')] = '2';
    values[columns.indexOf('z2_level')] = '';
    values[columns.indexOf('z2_length')] = values[columns.indexOf('z1_length')];
    expect(extendedOrders.mac_parse(`${header}\r\n${values.join(';')}\r\n`)[0].orders.map((order) => order.level)).toEqual(['2', '2']);
});

test('converts Ozdan CSV setups like the classic parser converts the same orders from Ord_mac.dat', async () => {
    const setups = extendedOrders.mac_parse(csv);
    const classicRows = classicParser.mac_parse(fixture('Ord_mac.dat'));
    expect(classicRows).toHaveLength(setups.length);

    for (const index of [1, 2, 3, 4, 5]) {
        const fromCsv = await orders.mac2syncro(setups[index]);
        const fromDat = await orders.mac2syncro(classicRows[index]);
        expect(fromCsv.error).toBeNull();
        expect(fromCsv.result).toHaveLength(102);

        // The .dat carries placeholder quality/flute; empty order slots differ only in blank vs '0' filler.
        const differences = fromCsv.result
            .map((value, token) => (String(value).trim() || '0') === (String(fromDat.result[token]).trim() || '0') ? null : token)
            .filter((token) => token !== null);
        expect(differences).toEqual([4, 5]);
        expect([fromCsv.result[4], fromCsv.result[5]]).toEqual([setups[index].qualityId, 'B']);
    }
});

test('rejects an Ozdan setup with zero sheets during validation', async () => {
    const setup = extendedOrders.mac_parse(csv)[0];
    const converted = await extendedOrders.validate(setup, null, null, null, orders);

    expect(converted.result).toBeNull();
    expect(converted.error).toMatch(/Order 1 quantity must be a positive integer/);
});

test('accepts more than five outs on one order while the setup total stays within 14', async () => {
    const setup = extendedOrders.mac_parse(csv)[0];
    expect(setup.orders[0].outs).toBe('6');
    const converted = validateConversion(
        await orders.mac2syncro({ ...setup, orders: [{ ...setup.orders[0], quantitySheets: '1000' }] })
    );

    expect(converted.error).toBeNull();
    expect(converted.result[13]).toBe(6);
});

test('rejects an Ozdan setup whose outs add up to more than 14', async () => {
    const setup = extendedOrders.mac_parse(csv)[4];
    // 15 × 160 mm leaves a 100 mm trim on the 2500 mm web, so only the outs total breaks a rule.
    const widen = (order, outs) => ({ ...order, outs, sheetWidthMm: '160', scores: [] });
    const [first, second, third] = setup.orders;
    const converted = validateConversion(
        await orders.mac2syncro({ ...setup, orders: [widen(first, '6'), widen(second, '6'), widen(third, '3')] })
    );

    expect(converted.result).toBeNull();
    expect(converted.error).toMatch(/Łączna liczba użytków w zestawie wynosi 15; limit to 14/);
});

test('rejects more sheets per stack than the stacker takes', async () => {
    const setup = extendedOrders.mac_parse(csv)[1];
    const converted = await orders.mac2syncro({ ...setup, orders: [{ ...setup.orders[0], sheetsPerStack: '1001' }] });

    expect(converted.result).toBeNull();
    expect(converted.error).toMatch(/sheets per stack must be a positive integer no greater than 1000/);
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
