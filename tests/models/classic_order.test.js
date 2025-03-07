const {
    fields,
    mac_parse,
    m2s_serialize,
    mac2syncro,
    orders_all,
    orders_delete_all,
    orders_add,
    validate
} = require('../../models/orders/classic.js');

const fs = require('node:fs');
const path = require('path');

const papers = require('../../models/papers.js')
const quality = require('../../models/quality.js')
const flute = require('../../models/flute.js')

test('fields', () => {
    expect(fields.length).toBeGreaterThan(0);
});

test('mac_parse_5_bigs', () => {
    const data = fs.readFileSync('tests/models/Ord_mac 5 bigow.dat', 'utf8');
    expect(data.length).toBeGreaterThan(0);
    const parsed = mac_parse(data);
    expect(parsed.length).toEqual(2);
    const order1 = parsed[0];
    const order2 = parsed[1];
    expect(parseInt(order1[20])).toEqual(2); //number of outs 1 to be equal 2
});

test('mac_parse_10_belts_0_trimm', () => {
    const data = fs.readFileSync('tests/models/Ord_mac 10 pasow 0 trimm.dat', 'utf8');
    const parsed = mac_parse(data);
    expect(parsed.length).toBeGreaterThan(0);
});

test('mac_parse_12_belts', () => {
    const data = fs.readFileSync('tests/models/Ord_mac 12 pasow.dat', 'utf8');
    const parsed = mac_parse(data);
    expect(parsed.length).toBeGreaterThan(0);
});

test('mac_parse_single', () => {
    const data = fs.readFileSync('tests/models/Ord_mac 1 zam.dat', 'utf8');
    const parsed = mac_parse(data);
    expect(parsed.length).toBeGreaterThan(0);
});

test('validate_5_bigs', async () => {
    const data = fs.readFileSync('tests/models/Ord_mac 5 bigow.dat', 'utf8');
    //expect(
    let parsed = mac_parse(data);
    let res = await validate(parsed, papers, quality, flute);
    //.toBeGreaterThan(0);
});

test('validate_10_belts_0_trimm', async () => {
    const data = fs.readFileSync('tests/models/Ord_mac 10 pasow 0 trimm.dat', 'utf8');
    //expect(
    let parsed = mac_parse(data);
    let res = await validate(parsed, papers, quality, flute);
    //.toBeGreaterThan(0);
});

test('validate_12_belts', async () => {
    const data = fs.readFileSync('tests/models/Ord_mac 12 pasow.dat', 'utf8');
    //expect(
    let parsed = mac_parse(data);
    let res = await validate(parsed, papers, quality, flute);
    //.toBeGreaterThan(0);
});

test('validate_single', async () => {
    const data = fs.readFileSync('tests/models/Ord_mac 1 zam.dat', 'utf8');
    //expect(
    let parsed = mac_parse(data);
    let res = await validate(parsed, papers, quality, flute);
    //.toBeGreaterThan(0);
});