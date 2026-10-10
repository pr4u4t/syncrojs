const sendAppShell = require('./app-shell');

const renderDataListView = async function(req, res, options){
    const requestedPage = Math.max(1, Number.parseInt(req.params.page, 10) || 1);

    if(req.query.api === '1'){
        let page = requestedPage;
        let data = await options.load(page);
        const total = Math.max(0, Number(data && data.total) || 0);
        const lastPage = Math.max(1, total);
        if (data && page > lastPage) {
            page = lastPage;
            data = await options.load(page);
        }
        res.set('Cache-Control', 'no-store').json({
            unavailable: !data || data.unavailable === true,
            rows: data && data.rows || [],
            fields: data && data.fields || [],
            editFields: data && data.editFields || [],
            keyFields: data && data.keyFields || [],
            total: Math.max(0, Number(data && data.total) || 0),
            current: page,
            url: options.url
        });
        return;
    }

    sendAppShell(req,res);
};

module.exports = renderDataListView;