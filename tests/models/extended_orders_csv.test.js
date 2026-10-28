jest.mock('../../models/quality', () => ({
    quality_params_by_id: jest.fn().mockResolvedValue({ rows: [{ weightfactoruwg_m: 285, thickness_uddmm: 27, scoregap_uddmm: 30 }] })
}));
jest.mock('../../models/flute', () => ({
    flute_params_by_id: jest.fn().mockResolvedValue({ rows: [{}] })
}));

const extendedOrders = require('../../models/orders/extended_orders');
const orders = require('../../models/order');

const fifteenScores = Array.from({ length: 15 }, (_, index) => (index + 1) * 100).join(';');
const sevenScores = Array.from({ length: 7 }, (_, index) => (index + 1) * 50).join(';');
const csv = [
    'setup_code,web_width_mm,quality_id,flute,order_number,customer_name,sheet_width_mm,outs,sheet_length_mm,quantity_sheets,sheets_per_stack,level,score_measures_mm',
    `SETUP-A,2500,Q1,A,A-1,"Box, Inc.",800,1,1000,10,100,3,${sevenScores}`,
    'SETUP-B,2000,Q1,A,B-1,Customer B,1000,1,900,5,80,,',
    `SETUP-A,2500,Q1,A,A-2,Customer 2,700,1,950,12,100,1,${fifteenScores}`,
    'SETUP-A,2500,Q1,A,A-3,Customer 3,800,1,900,8,80,1,'
].join('\r\n');

test('groups three CSV orders by setup and retains 15 + 7 crease measures', async () => {
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
    expect(setups[0].orders[0].scores).toHaveLength(7);
    expect(setups[0].orders[1].scores).toHaveLength(15);
    expect(setups[0].preview).toEqual(['SETUP-A', '2500', 'Q1', 'A', 'A-1, A-2, A-3', 22]);
    expect(setups[1].orders).toHaveLength(1);
    expect(setups[1].orders[0].level).toBe('3');

    const converted = await orders.mac2syncro(setups[0]);
    expect(converted.error).toBeNull();
    expect(converted.setupCode).toBe('SETUP-A.2');
    expect(converted.trim).toBe(2000);
    expect(converted.result).toHaveLength(102);
    expect(converted.result[14]).toBe(Array.from({ length: 7 }, (_, index) => String((index + 1) * 500)).join(' '));
    expect(converted.result[45]).toBe(Array.from({ length: 15 }, (_, index) => String((index + 1) * 1000)).join(' '));
    expect([converted.result[19], converted.result[50], converted.result[81]]).toEqual(['3', '1', '1']);
});

test('rejects more than three orders in one setup', () => {
    const header = csv.split('\r\n')[0];
    const row = 'SETUP-X,2500,Q1,A,ORDER,Customer,800,1,1000,10,100,1,';
    expect(() => extendedOrders.mac_parse([header, row, row, row, row].join('\n'))).toThrow(/more than three orders/);
});

test('rejects more than 22 crease measures across an extended CSV setup', async () => {
    const source = csv.replace(`,${sevenScores}`, `,${Array.from({ length: 8 }, (_, index) => String(index + 1)).join(';')}`);
    const setup = extendedOrders.mac_parse(source).find((entry) => entry.setupCode === 'SETUP-A');
    const converted = await orders.mac2syncro(setup);

    expect(converted.result).toBeNull();
    expect(converted.error).toMatch(/22/);
});

test('rejects more than 15 crease measures on one extended CSV order', async () => {
    const source = csv
        .replace(`,${sevenScores}`, ',')
        .replace(fifteenScores, Array.from({ length: 16 }, (_, index) => String((index + 1) * 10)).join(';'));
    const setup = extendedOrders.mac_parse(source).find((entry) => entry.setupCode === 'SETUP-A');
    const converted = await orders.mac2syncro(setup);

    expect(converted.result).toBeNull();
    expect(converted.error).toMatch(/15/);
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

test('reads 740-character DAT records with up to 15 creases per order', () => {
    const text = (value, width) => String(value).padEnd(width);
    const number = (value, width) => String(value).padStart(width);
    const order = (customer, orderNumber, width, scores, stack, length, quantity, outs, fixed) => [
        text(customer, 15), text('', 12), text(orderNumber, 30), number(width, 5),
        ...Array.from({ length: 15 }, (_, index) => scores[index] === undefined ? text('', 5) : number(scores[index], 5)),
        number(stack, 4), number(length, 5), number(quantity, 6), number(outs, 2), text('', 8), fixed
    ].join('');
    const record = [
        text('', 11), text('', 44), text('4971', 12), text('FALA A', 36), text('A', 23), number(25000, 5), text('', 10), 'G',
        order('Klient Alfa', 'ORDER-1', 8000, [1500, 3000], 200, 10000, 10000, 1, '20'),
        order('Klient Beta', 'ORDER-2', 7000, Array.from({ length: 15 }, (_, index) => (index + 1) * 400), 200, 9500, 12000, 1, '10'),
        ' '.repeat(164),
        ' '.repeat(16 + 90)
    ].join('');
    expect(record).toHaveLength(740);

    const rows = extendedOrders.mac_parse(record + ' '.repeat(740));

    expect(rows).toHaveLength(1);
    expect(rows[0]).toHaveLength(55);
    expect(rows[0].slice(2, 6)).toEqual(['4971', 'FALA A', 'A', '25000']);
    expect(rows[0].slice(10, 13)).toEqual(['ORDER-1', '8000', '1500 3000']);
    expect(rows[0][27]).toBe(Array.from({ length: 15 }, (_, index) => String((index + 1) * 400)).join(' '));
    expect(rows[0].slice(32, 36)).toEqual(['200', '9500', '12000', '1']);
    expect(rows[0][40]).toBe('');
});
