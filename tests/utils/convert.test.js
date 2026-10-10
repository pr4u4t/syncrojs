jest.mock('@aternus/csv-to-xlsx', () => ({ convertCsvToXlsx: jest.fn() }));

const { convertCsvToXlsx } = require('@aternus/csv-to-xlsx');
const { csv_to_xlsx } = require('../../utils/convert');

test('forwards the CSV buffer and fixed workbook options to the converter', () => {
    const input = Buffer.from('order,quantity\nA1,2');
    const output = Buffer.from('xlsx');
    convertCsvToXlsx.mockReturnValue(output);

    expect(csv_to_xlsx(input)).toBe(output);
    expect(convertCsvToXlsx).toHaveBeenCalledWith(input, '', { sheetName: '', overwrite: false }, false);
});
