const extendedOuts = require('../../models/orders/extended_outs');

test('validate passes a flat order to the converter', async () => {
    const row = Array.from({ length: 42 }, (_, index) => `field-${index}`);
    const converted = { result: ['converted'], error: null };
    const parent = {
        mac2syncro: jest.fn().mockResolvedValue(converted)
    };

    await expect(extendedOuts.validate(row, null, null, null, parent)).resolves.toBe(converted);
    expect(parent.mac2syncro).toHaveBeenCalledWith(row, null, null, null);
});

test('validate rejects non-flat input without calling the converter', async () => {
    const parent = {
        mac2syncro: jest.fn()
    };

    await expect(extendedOuts.validate([['nested']], null, null, null, parent)).resolves.toMatchObject({
        result: null,
        error: expect.any(String)
    });
    expect(parent.mac2syncro).not.toHaveBeenCalled();
});