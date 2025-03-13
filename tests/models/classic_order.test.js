const orders = require('../../models/order.js');
const fs = require('node:fs');
const path = require('path');
const config = require('config');
const timeout = config.get("syncro.timeout") || 30000;

beforeAll(async () => {
    await orders.switch_backend('classic');
});

// positive tests
test('parser', async () => {
    const result = await orders.switch_backend('classic');
    expect(result).toBe(true);
});

test('fields', () => {
    expect(orders.fields).toBeDefined();
    expect(orders.fields.length).toBeGreaterThan(0);
});

test('Ord_mac 1 zam.dat', async () => {
    const data = fs.readFileSync('tests/models/classic_data/Ord_mac 1 zam.dat', 'utf8');
    expect(data.length).toBeGreaterThan(0);
    const parsed = orders.parse(data);
    expect(parsed.length).toEqual(2);
    const order0 = parsed[0];
    const order1 = parsed[1];
    expect(parseInt(order0[20])).toEqual(2); //number of outs 1 to be equal 2
    expect(parseInt(order1[20])).toEqual(3); //number of outs 1 to be equal 2
    const result0 = await orders.validate(order0);
    expect(Array.isArray(result0.result)).toEqual(true);
    const result1 = await orders.validate(order1);
    expect(Array.isArray(result1.result)).toEqual(true);
}, timeout);

test('Ord_mac 2 zam.dat', async () => {
    const data = fs.readFileSync('tests/models/classic_data/Ord_mac 2 zam.dat', 'utf8');
    expect(data.length).toBeGreaterThan(0);
    const parsed = orders.parse(data);
    expect(parsed.length).toEqual(2);
    const order0 = parsed[0];
    const order1 = parsed[1];
    expect(parseInt(order0[20])).toEqual(2); //number of outs 1 to be equal 2
    expect(parseInt(order1[20])).toEqual(2); //number of outs 1 to be equal 2
    const result0 = await orders.validate(order0);
    expect(Array.isArray(result0.result)).toEqual(true);
    const result1 = await orders.validate(order1);
    expect(Array.isArray(result1.result)).toEqual(true);

}, timeout);

test('Ord_mac bigi gora dol.dat', async () => {
    const data = fs.readFileSync('tests/models/classic_data/Ord_mac bigi gora dol.dat', 'utf8');
    expect(data.length).toBeGreaterThan(0);
    const parsed = orders.parse(data);
    expect(parsed.length).toEqual(2);
    const order0 = parsed[0];
    const order1 = parsed[1];
    expect(parseInt(order0[20])).toEqual(2); //number of outs 1 to be equal 2
    expect(parseInt(order1[20])).toEqual(2); //number of outs 1 to be equal 2
    const result0 = await orders.validate(order0);
    expect(Array.isArray(result0.result)).toEqual(true);
    const result1 = await orders.validate(order1);
    expect(Array.isArray(result1.result)).toEqual(true);
}, timeout);

test('Ord_mac bigi gora.dat', async () => {
    const data = fs.readFileSync('tests/models/classic_data/Ord_mac bigi gora.dat', 'utf8');
    expect(data.length).toBeGreaterThan(0);
    const parsed = orders.parse(data);
    expect(parsed.length).toEqual(2);
    const order0 = parsed[0];
    const order1 = parsed[1];
    expect(parseInt(order0[20])).toEqual(2); //number of outs 1 to be equal 2
    expect(parseInt(order1[20])).toEqual(3); //number of outs 1 to be equal 3
    const result0 = await orders.validate(order0);
    expect(Array.isArray(result0.result)).toEqual(true);
    const result1 = await orders.validate(order1);
    expect(Array.isArray(result1.result)).toEqual(true);
}, timeout);

test('Ord_mac.dat', async () => {
    const data = fs.readFileSync('tests/models/classic_data/Ord_mac.dat', 'utf8');
    expect(data.length).toBeGreaterThan(0);
    const parsed = orders.parse(data);
    expect(parsed.length).toEqual(11);
    const order0 = parsed[0];
    const order1 = parsed[1];
    expect(parseInt(order0[20])).toEqual(1); //number of outs 1 to be equal 2
    expect(parseInt(order1[20])).toEqual(4); //number of outs 1 to be equal 3
    const result0 = await orders.validate(order0);
    expect(Array.isArray(result0.result)).toEqual(true);
    const result1 = await orders.validate(order1);
    expect(Array.isArray(result1.result)).toEqual(true);
}, timeout);

test('Ord_mac1.dat', async () => {
    const data = fs.readFileSync('tests/models/classic_data/Ord_mac1.dat', 'utf8');
    expect(data.length).toBeGreaterThan(0);
    const parsed = orders.parse(data);
    expect(parsed.length).toEqual(24);
    const order0 = parsed[0];
    const order1 = parsed[1];
    expect(parseInt(order0[20])).toEqual(3); //number of outs 1 to be equal 2
    expect(parseInt(order1[20])).toEqual(1); //number of outs 1 to be equal 3
    const result0 = await orders.validate(order0);
    expect(Array.isArray(result0.result)).toEqual(true);
    const result1 = await orders.validate(order1);
    expect(Array.isArray(result1.result)).toEqual(true);
}, timeout);

test('Ord_mac2.dat', async () => {
    const data = fs.readFileSync('tests/models/classic_data/Ord_mac2.dat', 'utf8');
    expect(data.length).toBeGreaterThan(0);
    const parsed = orders.parse(data);
    expect(parsed.length).toEqual(26);
    const order0 = parsed[0];
    const order1 = parsed[1];
    expect(parseInt(order0[20])).toEqual(2); //number of outs 1 to be equal 2
    expect(parseInt(order1[20])).toEqual(2); //number of outs 1 to be equal 3
    const result0 = await orders.validate(order0);
    expect(Array.isArray(result0.result)).toEqual(true);
    const result1 = await orders.validate(order1);
    expect(Array.isArray(result1.result)).toEqual(true);
}, timeout);

test('Ord_mac3.dat', async () => {
    const data = fs.readFileSync('tests/models/classic_data/Ord_mac3.dat', 'utf8');
    expect(data.length).toBeGreaterThan(0);
    const parsed = orders.parse(data);
    expect(parsed.length).toEqual(45);
    const order0 = parsed[0];
    const order1 = parsed[1];
    expect(parseInt(order0[20])).toEqual(1); //number of outs 1 to be equal 2
    expect(parseInt(order1[20])).toEqual(3); //number of outs 1 to be equal 3
    const result0 = await orders.validate(order0);
    expect(Array.isArray(result0.result)).toEqual(true);
    const result1 = await orders.validate(order1);
    expect(Array.isArray(result1.result)).toEqual(true);
}, timeout);

test('Ord_mac4.dat', async () => {
    const data = fs.readFileSync('tests/models/classic_data/Ord_mac4.dat', 'utf8');
    expect(data.length).toBeGreaterThan(0);
    const parsed = orders.parse(data);
    expect(parsed.length).toEqual(16);
    const order0 = parsed[0];
    const order1 = parsed[1];
    expect(parseInt(order0[20])).toEqual(2); //number of outs 1 to be equal 2
    expect(parseInt(order1[20])).toEqual(3); //number of outs 1 to be equal 3
    const result0 = await orders.validate(order0);
    expect(Array.isArray(result0.result)).toEqual(true);
    const result1 = await orders.validate(order1);
    expect(Array.isArray(result1.result)).toEqual(true);
}, timeout);

test('Ord_mac5.dat', async () => {
    const data = fs.readFileSync('tests/models/classic_data/Ord_mac5.dat', 'utf8');
    expect(data.length).toBeGreaterThan(0);
    const parsed = orders.parse(data);
    expect(parsed.length).toEqual(38);
    const order0 = parsed[0];
    const order1 = parsed[1];
    expect(parseInt(order0[20])).toEqual(2); //number of outs 1 to be equal 2
    expect(parseInt(order1[20])).toEqual(3); //number of outs 1 to be equal 3
    const result0 = await orders.validate(order0);
    expect(Array.isArray(result0.result)).toEqual(true);
    const result1 = await orders.validate(order1);
    expect(Array.isArray(result1.result)).toEqual(true);
}, timeout);

test('mac_parse_5_bigs', async () => {
    const data = fs.readFileSync('tests/models/classic_data/Ord_mac 5 bigow.dat', 'utf8');
    expect(data.length).toBeGreaterThan(0);
    const parsed = orders.parse(data);
    expect(parsed.length).toEqual(2);
    const order1 = parsed[0];
    const order2 = parsed[1];
    expect(parseInt(order1[20])).toEqual(2); //number of outs 1 to be equal 2
    expect(parseInt(order2[20])).toEqual(1);
    const result0 = await orders.validate(order1);
    expect(Array.isArray(result0.result)).toEqual(true);
    const result1 = await orders.validate(order2);
    expect(Array.isArray(result1.result)).toEqual(true);
}, timeout);

test('mac_parse_12_belts', async () => {
    const data = fs.readFileSync('tests/models/extended_data/Ord_mac 12 pasow.dat', 'utf8');
    expect(data.length).toBeGreaterThan(0);
    const parsed = orders.parse(data);
    expect(parsed.length).toEqual(2);
    const order1 = parsed[0];
    const order2 = parsed[1];
    //expect(parseInt(order1[20])).toEqual(2); //number of outs 1 to be equal 2
    //expect(parseInt(order2[20])).toEqual(1);
    const result0 = await orders.validate(order1);
    expect(Array.isArray(result0.result)).toEqual(true);
    const result1 = await orders.validate(order2);
    expect(result1.result === null && result1.error !== null).toEqual(true); //this test should fail as number of outs is invalid
}, timeout);

test('mac_parse_10_belts', async () => {
    const data = fs.readFileSync('tests/models/extended_data/Ord_mac 10 pasow 0 trimm.dat', 'utf8');
    expect(data.length).toBeGreaterThan(0);
    const parsed = orders.parse(data);
    expect(parsed.length).toEqual(2);
    const order1 = parsed[0];
    const order2 = parsed[1];
    //expect(parseInt(order1[20])).toEqual(2); //number of outs 1 to be equal 2
    //expect(parseInt(order2[20])).toEqual(1);
    const result0 = await orders.validate(order1);
    expect(Array.isArray(result0.result)).toEqual(true);
    const result1 = await orders.validate(order2);
    expect(result1.result === null && result1.error !== null).toEqual(true); //this test should fail as number of outs is invalid
}, timeout);
