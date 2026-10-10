jest.mock('../../models/quality', () => ({
    quality_params_by_id: jest.fn().mockResolvedValue({ rows: [{ weightfactoruwg_m: 285, thickness_uddmm: 27, scoregap_uddmm: 30 }] })
}));
jest.mock('../../models/flute', () => ({
    flute_params_by_id: jest.fn().mockResolvedValue({ rows: [{}] })
}));

const extendedOrders = require('../../models/orders/extended_orders');
const orders = require('../../models/order');

const csv = [
    'setup_code,web_width_mm,quality_id,flute,order_number,customer_name,sheet_width_mm,outs,sheet_length_mm,quantity_sheets,sheets_per_stack,level,score_measures_mm',
    'SETUP-A,2500,Q1,A,A-1,"Box, Inc.",800,1,1000,10,100,3,',
    'SETUP-B,2000,Q1,A,B-1,Customer B,1000,1,900,5,80,,',
    `SETUP-A,2500,Q1,A,A-2,Customer 2,700,1,950,12,100,1,${Array.from({ length: 22 }, (_, index) => (index + 1) * 100).join(';')}`,
    'SETUP-A,2500,Q1,A,A-3,Customer 3,800,1,900,8,80,1,'
].join('\r\n');

test('groups three CSV orders by setup and retains 22 crease measures on one order', async () => {
    const setups = extendedOrders.mac_parse(csv);

    expect(setups).toHaveLength(2);
    expect(setups[0]).toMatchObject({
        format: 'extended_orders_csv',
        setupCode: 'SETUP-A',
        webWidthMm: '2500',
        qualityId: 'Q1',
        fluteId: 'A'
    });
    expect(setups[0].orders).toHaveLength(3);
    expect(setups[0].orders[0].customerName).toBe('Box, Inc.');
    expect(setups[0].orders[0].level).toBe('3');
    expect(setups[0].orders[1].level).toBe('1');
    expect(setups[0].orders[1].scores).toHaveLength(22);
    expect(setups[0].preview).toEqual(['SETUP-A', '2500', 'Q1', 'A', 'A-1, A-2, A-3', 22]);
    expect(setups[1].orders).toHaveLength(1);
    expect(setups[1].orders[0].level).toBe('3');

    const converted = await orders.mac2syncro(setups[0]);
    expect(converted.error).toBeNull();
    expect(converted.setupCode).toBe('SETUP-A.2');
    expect(converted.trim).toBe(2000);
    expect(converted.result).toHaveLength(102);
    expect(converted.result[45]).toBe(Array.from({ length: 22 }, (_, index) => String((index + 1) * 1000)).join(' '));
    expect([converted.result[19], converted.result[50], converted.result[81]]).toEqual(['3', '1', '1']);
});

test('rejects more than three orders in one setup', () => {
    const header = csv.split('\r\n')[0];
    const row = 'SETUP-X,2500,Q1,A,ORDER,Customer,800,1,1000,10,100,1,';
    expect(() => extendedOrders.mac_parse([header, row, row, row, row].join('\n'))).toThrow(/more than three orders/);
});

test('rejects more than 22 crease measures when converting an extended CSV setup', async () => {
    const tooManyScores = Array.from({ length: 23 }, (_, index) => String(index + 1)).join(';');
    const source = csv.replace(/100;200;300;400;500;600;700;800;900;1000;1100;1200;1300;1400;1500;1600;1700;1800;1900;2000;2100;2200/, tooManyScores);
    const setup = extendedOrders.mac_parse(source).find((entry) => entry.setupCode === 'SETUP-A');
    const converted = await orders.mac2syncro(setup);

    expect(converted.result).toBeNull();
    expect(converted.error).toMatch(/22/);
});

test('rejects inconsistent shared setup fields and missing required columns', () => {
    const [header, firstRow] = csv.split('\r\n');
    expect(() => extendedOrders.mac_parse([header, firstRow, firstRow.replace(',2500,', ',2400,')].join('\n')))
        .toThrow(/inconsistent shared fields/);
    expect(() => extendedOrders.mac_parse('setup_code,order_number\nA,1')).toThrow(/Missing extended orders CSV columns/);
});

test('rejects malformed quoting and data rows with mismatched column counts', () => {
    expect(() => extendedOrders.mac_parse('setup_code,web_width_mm\n"unterminated,2500')).toThrow(/Unclosed quote/);
    const [header] = csv.split('\r\n');
    expect(() => extendedOrders.mac_parse(`${header}\nSETUP-A,2500`)).toThrow(/wrong number of columns/);
});
