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

test('mac_parse', () => {
    let macs = fs.readdirSync('tests/models');
    macs = macs.filter(file => {
        const ext = path.extname(file).toLowerCase();
        return ext === '.dat';
    });


    for(let i = 0; i < macs.length; i++){
        const data = fs.readFileSync('tests/models/'+macs[i], 'utf8');
        expect(mac_parse(data).length).toBeGreaterThan(0);
    }
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