const { TwingEnvironment, TwingLoaderFilesystem } = require('twing');
let loader = new TwingLoaderFilesystem('./templates');
let twing = new TwingEnvironment(loader);

module.exports = {
    render: (tpl, params) => twing.render(tpl, params),
}
