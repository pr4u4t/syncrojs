const config = require('config');
const { MAX_TOTAL_OUTS, MAX_CREASES_PER_ORDER, MAX_CREASES_PER_SETUP, totalOuts } = require('../utils/order-constraints');

const HEADER_SIZE = 9;
const ORDER_SIZE = 31;
const ORDER_COUNT = 3;
const DAT_FIELDS = {
    orderNumber: [10, 25, 40],
    sheetWidth: [11, 26, 41],
    scores: [[12, 13, 14, 15, 16], [27, 28, 29, 30, 31], [42, 43, 44, 45, 46]],
    sheetLength: [18, 33, 48],
    quantity: [19, 34, 49],
    outs: [20, 35, 50]
};

const configuredLimit = (name, fallback) => {
    try {
        const value = Number(config.get(`orders.validation.${name}`));
        return Number.isFinite(value) ? value : fallback;
    } catch {
        return fallback;
    }
};

const configuredLevelLimit = (name, level, fallback) => {
    try {
        const configured = config.get(`orders.validation.${name}`);
        const value = Number(Array.isArray(configured) ? configured[level - 1] : configured);
        return Number.isFinite(value) ? value : fallback;
    } catch {
        return fallback;
    }
};

const physicalKnifeLevel = (levelName, levelCount) => {
    if(levelName < 1 || levelName > 3) return null;
    if(levelCount === 1) return 1;
    if(levelCount === 2){
        if(levelName === 2) return null;
        return levelName === 1 ? 1 : 2;
    }
    return levelName;
};

const sourceFieldIndexes = (source, field, orderIndexes) => {
    if(!Array.isArray(source)) return undefined;
    const fields = DAT_FIELDS[field];
    if(!fields || orderIndexes.length === 0) return undefined;
    return orderIndexes.flatMap((index) => Array.isArray(fields[index]) ? fields[index] : [fields[index]])
        .filter((index) => index < source.length);
};

const invalid = (error, source, field, orderIndexes = [0, 1, 2]) => ({
    error,
    ...(field ? { fieldIndexes: sourceFieldIndexes(source, field, orderIndexes) } : {})
});

const validateSyncroPayload = (payload, source = null) => {
    if(!Array.isArray(payload) || payload.length !== HEADER_SIZE + ORDER_SIZE * ORDER_COUNT){
        return invalid('Nieprawidłowy format danych setupu Syncro.');
    }

    const setupCode = String(payload[0] || '').trim();
    const webWidth = Number(payload[1]);
    const trimType = Number(payload[2]);
    const qualityId = String(payload[4] || '').trim();
    const fluteId = String(payload[5] || '').trim();
    if(!setupCode) return invalid('Setup nie ma kodu.', source);
    if(!Number.isFinite(webWidth) || webWidth <= 0) return invalid('Szerokość wstęgi musi być dodatnia.', source, 'sheetWidth', []);
    if(!Number.isInteger(trimType) || (trimType !== 0 && trimType !== 1)){
        return invalid('Setup ma nieprawidłowy typ trimu.', source, 'trimType', []);
    }
    if(!qualityId || !fluteId) return invalid('Setup musi mieć przypisaną jakość i falę.', source);

    const orders = Array.from({ length: ORDER_COUNT }, (_, index) => {
        const start = HEADER_SIZE + index * ORDER_SIZE;
        return {
            index,
            orderNumber: String(payload[start] || '').trim(),
            sheetWidth: Number(payload[start + 3]),
            outs: Number(payload[start + 4]),
            scores: String(payload[start + 5] || '').trim(),
            levelName: Number(payload[start + 10]),
            sheetLength: Number(payload[start + 11]),
            quantity: Number(payload[start + 12]),
            sheetsPerStack: Number(payload[start + 13])
        };
    });

    for(const order of orders){
        if(!Number.isInteger(order.outs) || order.outs < 0){
            return invalid(`Zamówienie ${order.index + 1}: liczba użytków musi być nieujemną liczbą całkowitą.`, source, 'outs', [order.index]);
        }
    }

    const activeOrders = orders.filter((order) => order.outs > 0);
    if(activeOrders.length === 0 || !activeOrders.some((order) => order.index === 0)){
        return invalid('Setup musi zawierać pierwsze zamówienie.', source, 'orderNumber', [0]);
    }
    if(orders[2].outs > 0 && orders[1].outs <= 0){
        return invalid('Nie można pominąć drugiego zamówienia, jeśli podano trzecie.', source, 'orderNumber', [1, 2]);
    }

    let totalCreaseMeasures = 0;
    for(const order of activeOrders){
        if(!order.orderNumber) return invalid(`Zamówienie ${order.index + 1}: brak numeru zamówienia.`, source, 'orderNumber', [order.index]);
        if(!Number.isFinite(order.sheetWidth) || order.sheetWidth <= 0){
            return invalid(`Zamówienie ${order.index + 1}: szerokość arkusza musi być dodatnia.`, source, 'sheetWidth', [order.index]);
        }
        if(!Number.isInteger(order.levelName) || order.levelName < 1 || order.levelName > 3){
            return invalid(`Zamówienie ${order.index + 1}: nieprawidłowy poziom noża.`, source);
        }
        if(!Number.isFinite(order.sheetLength) || order.sheetLength <= 0){
            return invalid(`Zamówienie ${order.index + 1}: długość arkusza musi być dodatnia.`, source, 'sheetLength', [order.index]);
        }
        if(!Number.isInteger(order.quantity) || order.quantity <= 0){
            return invalid(`Zamówienie ${order.index + 1}: liczba arkuszy musi być dodatnia.`, source, 'quantity', [order.index]);
        }
        if(!order.scores){
            return invalid(`Zamówienie ${order.index + 1}: brak pomiarów bigowania.`, source, 'scores', [order.index]);
        }
        if(order.scores !== '0' && order.scores !== String(order.sheetWidth)){
            const measures = order.scores.split(/\s+/).map(Number);
            if(measures.some((measure) => !Number.isInteger(measure) || measure <= 0 || measure > order.sheetWidth)){
                return invalid(`Zamówienie ${order.index + 1}: bigi muszą być dodatnie i nie mogą przekraczać szerokości arkusza.`, source, 'scores', [order.index]);
            }
            if(measures.length > MAX_CREASES_PER_ORDER){
                return invalid(`Zamówienie ${order.index + 1} zawiera ${measures.length} bigów; limit to ${MAX_CREASES_PER_ORDER} na zamówienie.`, source, 'scores', [order.index]);
            }
            totalCreaseMeasures += measures.length;
        }
    }
    if(totalCreaseMeasures > MAX_CREASES_PER_SETUP){
        return invalid(`Setup zawiera ${totalCreaseMeasures} bigów; limit to ${MAX_CREASES_PER_SETUP}.`, source, 'scores', activeOrders.map((order) => order.index));
    }

    const maxTotalOuts = configuredLimit('max_total_outs', MAX_TOTAL_OUTS);
    const outsSum = totalOuts(activeOrders.map((order) => order.outs));
    if(outsSum > maxTotalOuts){
        return invalid(`Łączna liczba użytków w zestawie wynosi ${outsSum}; limit to ${maxTotalOuts}.`, source, 'outs', activeOrders.map((order) => order.index));
    }

    const maxKnifeLevel = configuredLimit('knife_level_count', 3);
    for(const order of activeOrders){
        order.knifeLevel = physicalKnifeLevel(order.levelName, maxKnifeLevel);
        if(order.knifeLevel === null){
            return invalid(`Zamówienie ${order.index + 1}: poziom noża nie jest dostępny na tej maszynie.`, source);
        }
    }

    for(const order of activeOrders){
        const minimumSheetLength = configuredLevelLimit('min_cut_length_tenths_mm', order.knifeLevel, 5000);
        const maximumSheetLength = configuredLevelLimit('max_cut_length_tenths_mm', order.knifeLevel, 50000);
        if(order.sheetLength < minimumSheetLength){
            return invalid(`Zamówienie ${order.index + 1}: długość cięcia ${order.sheetLength / 10} mm jest mniejsza od minimum ${minimumSheetLength / 10} mm.`, source, 'sheetLength', [order.index]);
        }
        if(order.sheetLength > maximumSheetLength){
            return invalid(`Zamówienie ${order.index + 1}: długość cięcia ${order.sheetLength / 10} mm przekracza maksimum ${maximumSheetLength / 10} mm.`, source, 'sheetLength', [order.index]);
        }
    }

    const lengthByLevel = new Map();
    for(const order of activeOrders){
        const previousLength = lengthByLevel.get(order.knifeLevel);
        if(previousLength !== undefined && previousLength !== order.sheetLength){
            const indexes = activeOrders
                .filter((item) => item.knifeLevel === order.knifeLevel)
                .map((item) => item.index);
            return invalid(`Zamówienia na tym samym poziomie noża muszą mieć tę samą długość arkusza.`, source, 'sheetLength', indexes);
        }
        lengthByLevel.set(order.knifeLevel, order.sheetLength);
    }

    const usedWidth = activeOrders.reduce((sum, order) => sum + order.sheetWidth * order.outs, 0);
    const maxWebWidth = configuredLimit('max_web_width_tenths_mm', 25430);
    const minSetupWidth = configuredLimit('min_setup_width_tenths_mm', 10000);
    if(webWidth > maxWebWidth){
        return invalid(`Szerokość wstęgi ${webWidth / 10} mm przekracza maksimum ${maxWebWidth / 10} mm.`, source, 'sheetWidth', []);
    }
    if(usedWidth < minSetupWidth){
        return invalid(`Szerokość zestawu ${usedWidth / 10} mm jest mniejsza od minimum ${minSetupWidth / 10} mm.`, source, 'sheetWidth', activeOrders.map((order) => order.index));
    }
    if(usedWidth > webWidth){
        return invalid('Suma szerokości zamówień przekracza szerokość wstęgi.', source, 'sheetWidth', activeOrders.map((order) => order.index));
    }

    const calculatedTrim = webWidth - usedWidth;
    if(trimType !== 0){
        if(webWidth !== usedWidth){
            return invalid('Przy typie NoTrim szerokość zestawu musi równać się szerokości wstęgi.', source, 'sheetWidth', activeOrders.map((order) => order.index));
        }
    }else{
        const minTrim = configuredLimit('min_trim_tenths_mm', 160);
        const maxTrim = configuredLimit('max_trim_tenths_mm', Number(config.get('orders.max_trim')) || 2300);
        let allowNarrowTrim = false;
        try {
            allowNarrowTrim = config.get('orders.validation.allow_narrow_trim') === true;
        } catch {}
        if(calculatedTrim > maxTrim){
            return invalid(`Trim ${calculatedTrim / 10} mm przekracza maksimum ${maxTrim / 10} mm.`, source, 'sheetWidth', activeOrders.map((order) => order.index));
        }
        if(calculatedTrim < minTrim && !allowNarrowTrim){
            return invalid(`Trim ${calculatedTrim / 10} mm jest mniejszy od minimum ${minTrim / 10} mm.`, source, 'sheetWidth', activeOrders.map((order) => order.index));
        }
    }

    const segmentCounts = activeOrders.map((order) => ({
        ...order,
        cuts: Math.ceil(order.quantity / order.outs),
        totalLengthMm: Math.floor(order.sheetLength / 10) * Math.ceil(order.quantity / order.outs)
    }));
    const maximumTotalLength = Math.max(...segmentCounts.map((order) => order.totalLengthMm));
    const firstOrder = segmentCounts[0];
    const firstOrderDifference = firstOrder.totalLengthMm - maximumTotalLength;
    let firstOrderTotalLength = firstOrder.totalLengthMm;
    if(Math.abs(firstOrderDifference) >= firstOrder.sheetLength){
        const equalizedCuts = Math.floor(maximumTotalLength * 10 / firstOrder.sheetLength);
        firstOrderTotalLength = Math.floor(firstOrder.sheetLength / 10) * equalizedCuts;
    }
    const minimumSetupLength = configuredLimit('min_setup_length_mm', 29380);
    if(firstOrderTotalLength < minimumSetupLength){
        return invalid(`Łączna długość setupu (${firstOrderTotalLength} mm) jest mniejsza od wymaganego minimum ${minimumSetupLength} mm.`, source, 'sheetLength', activeOrders.map((order) => order.index));
    }

    return null;
};

const validateConversion = (converted, source = null) => {
    if(!converted || !Array.isArray(converted.result)) return converted;
    const validation = validateSyncroPayload(converted.result, source);
    return validation ? { ...converted, result: null, ...validation } : converted;
};

module.exports = { validateSyncroPayload, validateConversion };
