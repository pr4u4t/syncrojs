const fs = require('node:fs');
const path = require('node:path');
const { detectOrderFormat } = require('../../models/orders/detect');

const read = (...parts) => fs.readFileSync(path.join(__dirname, ...parts), 'utf8');
const datRecord = (length, fixedFieldOffset) => {
    const record = Array(length).fill(' ');
    record[141] = 'G';
    record[fixedFieldOffset] = '2';
    record[fixedFieldOffset + 1] = '0';
    return record.join('');
};

test('detects CSV layouts from their header', () => {
    expect(detectOrderFormat(read('ozdan_data', 'OrdList.csv'), 'OrdList.csv')).toBe('extended_orders');
    expect(detectOrderFormat(read('..', '..', 'tools', 'example_extended_orders.csv'), 'orders.csv')).toBe('extended_orders');
    expect(detectOrderFormat(`﻿SETUP_CODE,web_width_mm\nA,2500`, 'orders.txt')).toBe('extended_orders');
    expect(detectOrderFormat(read('..', '..', 'tools', 'example_orders.csv'), 'example_orders.csv')).toBe('extended_orders');
});

test('detects classic 587-character DAT files, including the Ozdan export', () => {
    expect(detectOrderFormat(read('ozdan_data', 'Ord_mac.dat'), 'Ord_mac.dat')).toBe('classic');
    expect(detectOrderFormat(read('classic_data', 'Ord_mac.dat'), 'Ord_mac.dat')).toBe('classic');
});

test('detects extended DAT records and uses order 1 fixed field when one record fits several lengths', () => {
    expect(detectOrderFormat(datRecord(587, 253).repeat(2), 'a.dat')).toBe('classic');
    expect(detectOrderFormat(datRecord(589, 254).repeat(2), 'a.dat')).toBe('extended_orders');
    expect(detectOrderFormat(datRecord(740, 304).repeat(2), 'a.dat')).toBe('extended_orders');
    expect(detectOrderFormat(datRecord(589, 254) + ' '.repeat(151), 'a.dat')).toBe('extended_orders');
});

test('returns null for files it cannot recognise', () => {
    expect(detectOrderFormat('Select Z.asort from orders', 'query.dat')).toBeNull();
    expect(detectOrderFormat(' '.repeat(1000), 'blank.dat')).toBeNull();
});

test('extended_orders rejects a CSV with an unknown header instead of reading it as DAT records', () => {
    const extendedOrders = require('../../models/orders/extended_orders');
    expect(() => extendedOrders.mac_parse(read('..', '..', 'tools', 'example_orders.csv'))).toThrow(/Unrecognised orders CSV/);
});
