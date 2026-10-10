const Router = require('express-promise-router')
const router = new Router()
const papers = require('../models/papers.js')
const renderDataListView = require('../utils/data-list-view');
const materialEditor = require('../utils/material-editor');

module.exports = router;

const renderPage = (req,res) => renderDataListView(req,res,{
    url: '/papers/',
    load: async (page) => {
        const [data, countResult] = await Promise.all([
            papers.papers_paginated_all((page-1)*25),
            papers.papers_count()
        ]);
        const count = countResult && countResult.rows && countResult.rows[0] ? countResult.rows[0].count : 0;
        return {
            unavailable: !data || !countResult,
            rows: data && data.rows,
            fields: data && data.fields,
            editFields: materialEditor.getEditorFields('papers'),
            keyFields: materialEditor.getDefinition('papers').keyFields,
            total: Math.ceil(Number(count)/25)
        };
    }
});

router.get('/', renderPage);
router.get('/:page([0-9]+)', renderPage);
materialEditor.attachEditorRoute(router, 'papers');
