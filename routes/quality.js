const Router = require('express-promise-router');
const router = new Router();
const quality = require('../models/quality.js');
const renderDataListView = require('../utils/data-list-view');
const materialEditor = require('../utils/material-editor');

module.exports = router;

const renderPage = (req,res) => renderDataListView(req,res,{
    url: '/quality/',
    load: async (page) => {
        const [data, countResult] = await Promise.all([
            quality.quality_paginated_all((page-1)*25),
            quality.quality_count()
        ]);
        const count = countResult && countResult.rows && countResult.rows[0] ? countResult.rows[0].count : 0;
        return {
            unavailable: !data || !countResult,
            rows: data && data.rows,
            fields: data && data.fields,
            editFields: materialEditor.getEditorFields('quality'),
            keyFields: materialEditor.getDefinition('quality').keyFields,
            total: Math.ceil(Number(count)/25)
        };
    }
});

router.get('/', renderPage);
router.get('/:page([0-9]+)', renderPage);
materialEditor.attachEditorRoute(router, 'quality');
