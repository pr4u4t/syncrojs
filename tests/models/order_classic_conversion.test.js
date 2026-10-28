jest.mock('../../models/quality', () => ({
  quality_params_by_id: jest.fn().mockResolvedValue({
    rows: [{ weightfactoruwg_m: 285, thickness_uddmm: 27, scoregap_uddmm: 30 }]
  })
}));

jest.mock('../../models/flute', () => ({
  flute_params_by_id: jest.fn().mockResolvedValue({ rows: [{}] })
}));

jest.mock('../../utils/syncro', () => ({
  connect: jest.fn()
}));

jest.mock('../../utils/logger', () => ({
  debug: jest.fn(),
  info: jest.fn(),
  warn: jest.fn(),
  error: jest.fn()
}));

const fs = require('node:fs');
const path = require('node:path');
const flute = require('../../models/flute');
const syncro = require('../../utils/syncro');
const orders = require('../../models/order');
const classicParser = require('../../models/orders/classic');
const { classicLevels } = require('../../utils/classic-levels');
const { validateSyncroPayload } = require('../../models/order-validation');

test('converts each classic order using its own sheet length and quantity', async () => {
  const row = Array(42).fill('');
  row[2] = '9001';
  row[3] = 'Q1';
  row[4] = 'A';
  row[5] = '25000';

  row[8] = 'First customer';
  row[10] = 'ORDER-1';
  row[11] = '8000';
  row[12] = '1000';
  row[13] = '0';
  row[14] = '0';
  row[15] = '0';
  row[16] = '0';
  row[17] = '100';
  row[18] = '10000';
  row[19] = '250';
  row[20] = '2';

  row[23] = 'Second customer';
  row[25] = 'ORDER-2';
  row[26] = '8000';
  row[27] = '1200';
  row[28] = '0';
  row[29] = '0';
  row[30] = '0';
  row[31] = '0';
  row[32] = '120';
  row[33] = '15000';
  row[34] = '300';
  row[35] = '1';

  const converted = await orders.mac2syncro(row);

  expect(converted.error).toBeNull();
  expect(converted.result).toHaveLength(102);
  expect(flute.flute_params_by_id).toHaveBeenCalledWith('A');
  expect(converted.result[5]).toBe('A');
  expect(converted.result[20]).toBe('10000');
  expect(converted.result[21]).toBe('250');
  expect(converted.result[51]).toBe('15000');
  expect(converted.result[52]).toBe('300');

  const serialized = orders.m2s_serialize(converted.result);
  const tokens = serialized.slice(serialized.indexOf('(') + 1, -1).split('\u241e');
  expect(tokens).toHaveLength(104);
  expect(tokens[0]).toBe(' ');
  expect(tokens[1]).toBe('9001.2');
  expect(tokens[102]).toBe('False');
  expect(tokens[103]).toBe('');
});

test('converts more than five DAT crease measurements packed in the first score field', async () => {
  const row = Array(42).fill('');
  Object.assign(row, {
    2: '9011', 3: 'Q1', 4: 'A', 5: '25000',
    8: 'Customer', 10: 'ORDER-MANY-SCORES', 11: '8000',
    12: '1000 2000 3000 4000 5000 6000', 17: '100',
    18: '12000', 19: '400', 20: '1'
  });

  const converted = await orders.mac2syncro(row);

  expect(converted.error).toBeNull();
  expect(converted.result[14]).toBe('1000 2000 3000 4000 5000 6000');
});

test('converts the classic third-order trailer into the third Syncro order block', async () => {
  const row = Array(55).fill('');
  Object.assign(row, {
    2: '9008', 3: 'Q1', 4: 'A', 5: '25000',
    8: 'First', 10: 'ORDER-1', 11: '8000', 12: '1000', 13: '0', 14: '0', 15: '0', 16: '0',
    17: '100', 18: '1000', 19: '10', 20: '2',
    23: 'Second', 25: 'ORDER-2', 26: '6000', 27: '500', 28: '0', 29: '0', 30: '0', 31: '0',
    32: '100', 33: '1000', 34: '10', 35: '1',
    38: 'Third customer', 39: 'C3', 40: 'ORDER-3', 41: '1000',
    42: '300', 43: '0', 44: '0', 45: '0', 46: '0', 47: '80', 48: '800', 49: '12', 50: '1', 51: '12', 52: '10'
  });

  const converted = await orders.mac2syncro(row);

  expect(converted.error).toBeNull();
  expect(converted.result).toHaveLength(102);
  expect(converted.result[8]).toBe(2000);
  expect([converted.result[71], converted.result[73], converted.result[74], converted.result[75], converted.result[76]])
    .toEqual(['ORDER-3', 'Third customer', 1000, 1, '300']);
});

test('converts a real classic DAT record that contains a third order', async () => {
  await orders.switch_backend('classic');
  const filename = path.join(__dirname, 'classic_data', '61b094fe8a894d9fdeef6a2f6b626091.dat');
  const parsed = orders.parse(fs.readFileSync(filename, 'utf8'));
  const source = parsed.find((row) => row[40] !== '');

  expect(source).toBeDefined();
  const converted = await orders.mac2syncro(source);

  expect(converted.error).toBeNull();
  expect(converted.result).toHaveLength(102);
  expect(converted.result[71]).toBe(source[40]);
  expect(converted.result[73]).toBe(source[38]);
  expect(converted.result[74]).toBe(Number(source[41]));
  expect(converted.result[75]).toBe(Number(source[50]));
});

test('matches all three parsed DAT order blocks to their serialized Syncro tokens', async () => {
  await orders.switch_backend('classic');
  const filename = path.join(__dirname, 'classic_data', '61b094fe8a894d9fdeef6a2f6b626091.dat');
  const parsed = orders.parse(fs.readFileSync(filename, 'utf8'));
  const source = parsed.find((row) => row[40] !== '');
  expect(source).toBeDefined();

  const converted = await orders.mac2syncro(source);
  expect(converted.error).toBeNull();
  const command = orders.m2s_serialize(converted.result);
  const tokens = command.slice(command.indexOf('(') + 1, -1).split('\u241e');
  const expectedLevels = classicLevels([source[18], source[33], source[48]]);
  const sourceOrders = [
    { start: 8, width: 11, score: 12, stack: 17, length: 18, quantity: 19, outs: 20, level: expectedLevels[0] },
    { start: 23, width: 26, score: 27, stack: 32, length: 33, quantity: 34, outs: 35, level: expectedLevels[1] },
    { start: 38, width: 41, score: 42, stack: 47, length: 48, quantity: 49, outs: 50, level: expectedLevels[2] }
  ];

  expect(tokens[1]).toBe(`${source[2]}.2`);
  sourceOrders.forEach((order, index) => {
    const tokenStart = 10 + index * 31;
    const measures = source.slice(order.score, order.score + 5)
      .filter((value) => value !== '' && value !== '0')
      .map((value) => String(Number.parseInt(value, 10)))
      .join(' ') || '0';
    expect({
      orderNumber: tokens[tokenStart],
      customer: tokens[tokenStart + 2],
      width: tokens[tokenStart + 3],
      outs: tokens[tokenStart + 4],
      measures: tokens[tokenStart + 5],
      level: tokens[tokenStart + 10],
      sheetLength: tokens[tokenStart + 11],
      quantity: tokens[tokenStart + 12],
      sheetsPerStack: tokens[tokenStart + 13]
    }).toEqual({
      orderNumber: source[order.start + 2],
      customer: source[order.start],
      width: String(Number.parseInt(source[order.width], 10)),
      outs: String(Number.parseInt(source[order.outs], 10)),
      measures,
      level: order.level,
      sheetLength: source[order.length],
      quantity: source[order.quantity],
      sheetsPerStack: String(Number.parseInt(source[order.stack], 10) / 2)
    });
  });
});

test('reports a descriptive validation error when three DAT orders have no matching length and level pair', async () => {
  const row = Array(55).fill('');
  row[10] = 'ORDER-1';
  row[25] = 'ORDER-2';
  row[40] = 'ORDER-3';
  const converted = { result: Array(102).fill('0'), error: null };
  converted.result[19] = '3';
  converted.result[20] = '12000';
  converted.result[50] = '1';
  converted.result[51] = '12000';
  converted.result[81] = '1';
  converted.result[82] = '9090';

  const result = await classicParser.validate(row, null, null, null, {
    mac2syncro: jest.fn().mockResolvedValue(converted)
  });

  expect(result.result).toBeNull();
  expect(result.error).toMatch(/tę samą długość arkusza i poziom noża/);
  expect(result.fieldIndexes).toEqual([18, 33, 48]);
});

test('accepts a three-order DAT setup when repeated lengths share a level', async () => {
  const row = Array(55).fill('');
  row[10] = 'ORDER-1';
  row[25] = 'ORDER-2';
  row[40] = 'ORDER-3';
  const converted = { result: Array(102).fill('0'), error: null };
  converted.result[19] = '3';
  converted.result[20] = '12000';
  converted.result[50] = '3';
  converted.result[51] = '12000';
  converted.result[81] = '1';
  converted.result[82] = '9090';

  const result = await classicParser.validate(row, null, null, null, {
    mac2syncro: jest.fn().mockResolvedValue(converted)
  });

  expect(result).toEqual(converted);
});

test('groups classic DAT order levels by repeated sheet length', () => {
  expect(classicLevels(['12000', '12000', '9090'])).toEqual(['3', '3', '1']);
  expect(classicLevels(['9090', '12000', '12000'])).toEqual(['3', '1', '1']);
  expect(classicLevels(['12000', '9090', '12000'])).toEqual(['3', '1', '3']);
  expect(classicLevels(['12000', '12000', '12000'])).toEqual(['3', '3', '3']);
  expect(classicLevels(['12000', '11000', '9000'])).toEqual(['3', '1', '1']);
});

test('limits total outs across all classic DAT orders to fourteen', async () => {
  const row = Array(42).fill('');
  row[20] = '9';
  row[35] = '5';
  const converted = { result: Array(102).fill('0'), error: null };
  converted.result[13] = '9';
  converted.result[44] = '5';
  const valid = validateSyncroPayload(converted.result, row);
  expect(valid).toBeNull();

  converted.result[44] = '6';
  const rejected = validateSyncroPayload(converted.result, row);
  expect(rejected.error).toMatch(/łączna liczba użytków.*15.*limit to 14/i);
  expect(rejected.fieldIndexes).toEqual([20, 35]);
});

test('preflight rejects total outs above fourteen for manually built and extended CSV setups', async () => {
  const setup = await orders.manual_setup_to_syncro({
    setupCode: '9010',
    webWidthMm: '1300',
    qualityId: 'Q1',
    fluteId: 'A',
    orders: [5, 5, 5].map((outs, index) => ({
      orderNumber: `ORDER-${index + 1}`,
      customerName: `Customer ${index + 1}`,
      sheetWidthMm: '80',
      outs: String(outs),
      sheetLengthMm: '900',
      quantitySheets: '100',
      sheetsPerStack: '100',
      scores: []
    }))
  });

  expect(setup.error).toBeNull();
  const validation = validateSyncroPayload(setup.result);
  expect(validation.error).toMatch(/łączna liczba użytków.*15.*limit to 14/i);
});

test('converts every real classic DAT record with a third order into Syncro block three', async () => {
  await orders.switch_backend('classic');
  const directory = path.join(__dirname, 'classic_data');
  const thirdOrderRows = fs.readdirSync(directory)
    .filter((filename) => filename.toLowerCase().endsWith('.dat'))
    .flatMap((filename) => {
      const parsed = orders.parse(fs.readFileSync(path.join(directory, filename), 'utf8'));
      return parsed
        .filter((row) => row.length === 55 && row[40] !== '')
        .map((row) => ({ filename, row }));
    });

  expect(thirdOrderRows.length).toBeGreaterThan(0);
  for (const { filename, row } of thirdOrderRows) {
    const converted = await orders.mac2syncro(row);
    if (converted.error) throw new Error(`${filename}: ${converted.error}`);
    expect(converted.error).toBeNull();
    expect(converted.result).toHaveLength(102);
    expect(converted.result[71]).toBe(row[40]);
    expect(converted.result[73]).toBe(row[38]);
    expect(converted.result[74]).toBe(Number(row[41]));
    expect(converted.result[75]).toBe(Number(row[50]));
  }
});

test('builds a direct Syncro setup with three manually entered orders', async () => {
  const setup = await orders.manual_setup_to_syncro({
    setupCode: '9002',
    webWidthMm: '2500',
    qualityId: 'Q1',
    fluteId: 'A',
    orders: [1, 2, 3].map((index) => ({
      orderNumber: `ORDER-${index}`,
      customerName: `Customer ${index}`,
      level: index === 1 ? 3 : 1,
      sheetWidthMm: '800',
      outs: '1',
      sheetLengthMm: `${900 + index * 100}`,
      quantitySheets: `${10 + index}`,
      sheetsPerStack: '100',
      scores: index === 2 ? Array.from({ length: 15 }, (_, scoreIndex) => String((scoreIndex + 1) * 10)) : []
    }))
  });

  expect(setup.error).toBeNull();
  expect(setup.setupCode).toBe('9002.2');
  expect(setup.trim).toBe(1000);
  expect(setup.result).toHaveLength(102);
  expect(setup.result[0]).toBe('9002.2');
  expect([setup.result[13], setup.result[44], setup.result[75]]).toEqual([1, 1, 1]);
  expect([setup.result[12], setup.result[43], setup.result[74]]).toEqual([8000, 8000, 8000]);
  expect([setup.result[14], setup.result[45], setup.result[76]]).toEqual([
    '8000',
    Array.from({ length: 15 }, (_, index) => String((index + 1) * 100)).join(' '),
    '8000'
  ]);
  expect([setup.result[19], setup.result[50], setup.result[81]]).toEqual(['3', '1', '1']);
  expect([setup.result[21], setup.result[52], setup.result[83]]).toEqual([11, 12, 13]);
});

test('preserves repeated crease measurements for one order', async () => {
  const setup = await orders.manual_setup_to_syncro({
    setupCode: '9004',
    webWidthMm: '1700',
    qualityId: 'Q1',
    fluteId: 'A',
    orders: [{
      orderNumber: 'ORDER-1',
      customerName: 'Customer',
      sheetWidthMm: '800',
      outs: '2',
      sheetLengthMm: '1000',
      quantitySheets: '10',
      sheetsPerStack: '100',
      scores: ['200', '200', '400']
    }]
  });

  expect(setup.error).toBeNull();
  expect(setup.trim).toBe(1000);
  expect(setup.result[13]).toBe(2);
  expect(setup.result[14]).toBe('2000 2000 4000');
});

test('rejects crease measurements wider than the setup web', async () => {
  const setup = await orders.manual_setup_to_syncro({
    setupCode: '9005',
    webWidthMm: '1000',
    qualityId: 'Q1',
    fluteId: 'A',
    orders: [{
      orderNumber: 'ORDER-1',
      customerName: 'Customer',
      sheetWidthMm: '800',
      outs: '1',
      sheetLengthMm: '1000',
      quantitySheets: '10',
      sheetsPerStack: '100',
      scores: ['1000.1']
    }]
  });

  expect(setup.result).toBeNull();
  expect(setup.error).toMatch(/web width/);
});

test('limits crease measures to 22 across the setup', async () => {
  const setup = await orders.manual_setup_to_syncro({
    setupCode: '9003',
    webWidthMm: '2500',
    qualityId: 'Q1',
    fluteId: 'A',
    orders: [1, 2].map((index) => ({
      orderNumber: `ORDER-${index}`,
      customerName: `Customer ${index}`,
      sheetWidthMm: '800',
      outs: '1',
      sheetLengthMm: '1000',
      quantitySheets: '10',
      sheetsPerStack: '100',
      scores: Array.from({ length: index === 1 ? 12 : 11 }, (_, scoreIndex) => String(scoreIndex + 1))
    }))
  });

  expect(setup.result).toBeNull();
  expect(setup.error).toMatch(/22/);
});

test('limits crease measures to 15 per manually entered order', async () => {
  const setup = await orders.manual_setup_to_syncro({
    setupCode: '9006',
    webWidthMm: '2500',
    qualityId: 'Q1',
    fluteId: 'A',
    orders: [{
      orderNumber: 'ORDER-1',
      customerName: 'Customer',
      sheetWidthMm: '800',
      outs: '1',
      sheetLengthMm: '1000',
      quantitySheets: '10',
      sheetsPerStack: '100',
      scores: Array.from({ length: 16 }, (_, scoreIndex) => String(scoreIndex + 1))
    }]
  });

  expect(setup.result).toBeNull();
  expect(setup.error).toMatch(/15/);
});

test('treats the SyncroC ok line as an accepted order', async () => {
  const socket = {
    write: jest.fn(async (command) => command.length),
    end: jest.fn(async () => {}),
    readAll: jest.fn().mockResolvedValue(Buffer.from('ok\n'))
  };
  syncro.connect.mockResolvedValue(socket);

  await expect(orders.orders_add(['order-data'])).resolves.toBe(true);
});

test('parses current orders from Syncro and returns an empty list for an empty queue', async () => {
  const socket = {
    write: jest.fn(async (command) => command.length),
    readAll: jest.fn()
      .mockResolvedValueOnce(Buffer.from('<NewDataSet><DryEndList><Position>1</Position><SetupCode>9001.2</SetupCode></DryEndList><DryEndList><Position>2</Position><SetupCode>9002.2</SetupCode></DryEndList></NewDataSet>'))
      .mockResolvedValueOnce(Buffer.from('<NewDataSet />')),
    destroy: jest.fn()
  };
  syncro.connect.mockResolvedValue(socket);

  await expect(orders.orders_all()).resolves.toEqual([
    { Position: 1, SetupCode: '9001.2' },
    { Position: 2, SetupCode: '9002.2' }
  ]);
  await expect(orders.orders_all()).resolves.toEqual([]);
  expect(socket.write).toHaveBeenNthCalledWith(1, 'Orders.Get');
  expect(socket.write).toHaveBeenNthCalledWith(2, 'Orders.Get');
  expect(socket.destroy).toHaveBeenCalledTimes(2);
});

test('does not treat a missing SyncroC acknowledgement as acceptance', async () => {
  const socket = {
    write: jest.fn(async (command) => command.length),
    end: jest.fn(async () => {}),
    readAll: jest.fn().mockResolvedValue(Buffer.from('timeout\n'))
  };
  syncro.connect.mockResolvedValue(socket);

  await expect(orders.orders_add(['order-data'])).resolves.toBeNull();
});

test('checks SyncroC for the existing setup code', async () => {
  const socket = {
    write: jest.fn(async (command) => command.length),
    readAll: jest.fn().mockResolvedValue(Buffer.from('true\n')),
    destroy: jest.fn()
  };
  syncro.connect.mockResolvedValue(socket);

  await expect(orders.order_exists('9001.2')).resolves.toBe(true);
  expect(socket.write).toHaveBeenCalledWith('Orders.Has:9001.2');
  expect(socket.destroy).toHaveBeenCalled();
});