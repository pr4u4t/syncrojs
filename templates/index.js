//const { TwingEnvironment, TwingLoaderFilesystem } = require('twing');
//let loader = new TwingLoaderFilesystem('./templates');
//let twing = new TwingEnvironment(loader);

const {createEnvironment, createFilesystemLoader} = require('twing');
const fs = require('fs');

const loader = createFilesystemLoader(fs);
const twing = createEnvironment(loader);

module.exports = {
    render: (tpl, params) => twing.render(tpl, params),
}
