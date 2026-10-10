const MAX_TOTAL_OUTS = 14;

const totalOuts = (outs) => outs.reduce((total, value) => {
    const number = Number(value);
    return total + (Number.isFinite(number) ? number : 0);
}, 0);

module.exports = { MAX_TOTAL_OUTS, totalOuts };
