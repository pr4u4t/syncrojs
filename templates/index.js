const {createEnvironment, createFilesystemLoader} = require('twing');
const fs = require('fs');
        
let loader = createFilesystemLoader(fs);
let twing = createEnvironment(loader);
loader.addPath('./templates');

module.exports = {
    render: (tpl, params) => twing.render(tpl, params),
}
