const {
    fields,
    mac_parse,
    m2s_serialize,
    mac2syncro,
    orders_all,
    orders_delete_all,
    orders_add,
    validate
} = require('../../models/current_order.js');

const fs = require('node:fs');
const path = require('path');

const papers = require('../models/papers.js')
const quality = require('../models/quality.js')
const flute = require('../models/flute.js')

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

test('validate', () => {
    let macs = fs.readdirSync('tests/models');
    macs = macs.filter(file => {
        const ext = path.extname(file).toLowerCase();
        return ext === '.dat';
    });

    validate(mac_parse(macs), papers, quality, flute);

});
