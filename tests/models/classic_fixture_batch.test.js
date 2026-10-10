const fs = require('node:fs');
const path = require('node:path');
jest.mock('../../utils/logger', () => ({
    debug: jest.fn(),
    info: jest.fn(),
    warn: jest.fn(),
    error: jest.fn()
}));
const orders = require('../../models/order');

const fixtureDirectory = path.join(__dirname, 'classic_data');
const fixtureFiles = fs.readdirSync(fixtureDirectory)
    .filter((name) => name.toLowerCase().endsWith('.dat'))
    .sort();
const nonOrderFixtures = new Set([
    '29c6a7aef0b3b6c52792859d9c2794c4.dat',
    '479f5b2ea145d72f33b4c077ca5690be.dat',
    '63570602c6fb991229c6534404872093.dat'
]);

test('classic parser processes every order .dat fixture into complete flat records', async () => {
    await orders.switch_backend('classic');
    expect(fixtureFiles.length).toBeGreaterThan(0);

    const failures = [];
    const recognizedNonOrderFixtures = new Set();
    let parsedRecordCount = 0;
    let thirdOrderRecordCount = 0;
    for (const filename of fixtureFiles) {
        try {
            const source = fs.readFileSync(path.join(fixtureDirectory, filename), 'utf8');
            const parsed = orders.parse(source);
            if (!Array.isArray(parsed) || parsed.length === 0) {
                if (!nonOrderFixtures.has(filename)) {
                    failures.push(`${filename}: no records returned`);
                    continue;
                }
                const content = source.trim();
                const expectedContent = filename.startsWith('29c6a7')
                    ? content === ''
                    : /^select\s/i.test(content);
                if (!expectedContent) failures.push(`${filename}: unexpected non-order fixture content`);
                recognizedNonOrderFixtures.add(filename);
                continue;
            }
            if (nonOrderFixtures.has(filename)) {
                failures.push(`${filename}: expected a known non-order file, got ${parsed.length} records`);
                continue;
            }
            const invalidRecord = parsed.find((record) => !Array.isArray(record) || record.length !== 55 || record.some(Array.isArray));
            if (invalidRecord) {
                failures.push(`${filename}: malformed record shape`);
                continue;
            }
            parsedRecordCount += parsed.length;
            thirdOrderRecordCount += parsed.filter((record) => record[40] !== '').length;
        } catch (error) {
            failures.push(`${filename}: ${error.message}`);
        }
    }

    expect(parsedRecordCount).toBeGreaterThan(0);
    expect(thirdOrderRecordCount).toBeGreaterThan(0);
    expect(recognizedNonOrderFixtures).toEqual(nonOrderFixtures);
    expect(failures).toEqual([]);
});
