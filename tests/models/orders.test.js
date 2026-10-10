const orders = require('../../models/order.js');
const config = require('config');
const timeout = config.get("syncro.timeout") || 30000;
const syncroTest = process.env.SYNCRO_DESTRUCTIVE_TESTS === '1' ? test : test.skip;

test('available backends', async () => {
    const backends = await orders.available_backends();
    expect(Array.isArray(backends)).toEqual(true);
    expect(backends).toContain('extended_orders.js');
    expect(backends).not.toContain('extended_outs.js');
    expect(backends).not.toContain('fixed_width_record.js');
});

test('fields', async () => {
    const fields = await orders.fields;
    expect(Array.isArray(fields)).toEqual(true);
});

test('current backend', async () => {
    const current = await orders.current_backend();
    expect(current).toEqual("classic");
});

test('default order', async () => {
    const default_order = await orders.default_order();
    expect(default_order.length).toBeGreaterThan(0);
});

test('order serialize', async () => {
    const default_order = await orders.default_order();
    const serialized = orders.m2s_serialize(default_order);
    expect(serialized.length).toBeGreaterThan(0);
});

test('switch backend', async () => {
    const result = await orders.switch_backend('extended_orders');
    expect(result).toEqual(true);
    const current = orders.current_backend();
    expect(current).toEqual('extended_orders');
});

test('legacy extended_outs backend name remains an alias', async () => {
    await expect(orders.switch_backend('extended_outs')).resolves.toBe(true);
    expect(orders.current_backend()).toBe('extended_orders');
});

//test('mac2syncro', () => {
//
//});

syncroTest('orders_delete_all', async () => {
    const dall = await orders.orders_delete_all();
    expect(dall).toEqual(true);
}, timeout*2);

syncroTest('orders_all', async () => {
    const all = await orders.orders_all();
    expect(Array.isArray(all)).toBe(true);
}, timeout*2);

syncroTest('orders_add', async () => {
    const default_order = await orders.default_order();
    const add = await orders.orders_add(default_order);
    expect(add).toBe(true);
}, timeout*2);