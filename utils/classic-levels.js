const classicLevels = (sheetLengths, knifeLevelCount = 3) => {
    const levels = ['3', '1', '1'];
    if(knifeLevelCount <= 1) return ['1', '1', '1'];
    const populated = Array.isArray(sheetLengths) && sheetLengths.length === 3 && sheetLengths.every((length) => String(length ?? '').trim() !== '');
    if(!populated) return levels;

    const groups = new Map();
    sheetLengths.forEach((length, index) => {
        const key = String(Number(length));
        if(!groups.has(key)) groups.set(key, []);
        groups.get(key).push(index);
    });

    if(groups.size === 1){
        return ['3', '3', '3'];
    }
    if(groups.size === 2){
        const primaryGroup = groups.values().next().value;
        for(const indexes of groups.values()){
            const level = indexes === primaryGroup ? '3' : '1';
            for(const index of indexes) levels[index] = level;
        }
    }

    return levels;
};

module.exports = { classicLevels };
