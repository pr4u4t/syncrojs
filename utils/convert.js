const {convertCsvToXlsx} = require('@aternus/csv-to-xlsx')

const csv2xlsx = function(buffer){
    return convertCsvToXlsx(buffer, "",{ sheetName:'', overwrite: false }, false)
}

module.exports = {
    csv_to_xlsx: csv2xlsx  
} 
