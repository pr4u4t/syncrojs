const MAX_TOTAL_OUTS = 14;
const MAX_CREASES_PER_ORDER = 15;
const MAX_CREASES_PER_SETUP = 22;

const totalOuts = (outs) => outs.reduce((total, value) => {
    const number = Number(value);
    return total + (Number.isFinite(number) ? number : 0);
}, 0);

module.exports = { MAX_TOTAL_OUTS, MAX_CREASES_PER_ORDER, MAX_CREASES_PER_SETUP, totalOuts };
