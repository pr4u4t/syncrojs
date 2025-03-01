//const { TwingEnvironment, TwingLoaderFilesystem } = require('twing');
//let loader = new TwingLoaderFilesystem('./templates');
//let twing = new TwingEnvironment(loader);

const {createEnvironment, createFilesystemLoader} = require('twing');
const fs = require('fs');

//const {TwingEnvironment, TwingLoaderFilesystem} = require('twing');
        
let loader = createFilesystemLoader(fs);
let twing = createEnvironment(loader);
loader.addPath('./templates');
//const loader = createFilesystemLoader(fs);
//loader.addPath('./templates', 'templates');
//const twing = createEnvironment(loader);

module.exports = {
    render: (tpl, params) => twing.render(tpl, params),
}
