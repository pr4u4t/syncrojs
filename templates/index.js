//const { TwingEnvironment, TwingLoaderFilesystem } = require('twing');
//let loader = new TwingLoaderFilesystem('./templates');
//let twing = new TwingEnvironment(loader);

const {createEnvironment, createFilesystemLoader} = require('twing');
import * as fs from "fs";

const loader = createFilesystemLoader('./templates', fs);
const twing = createEnvironment(loader);

module.exports = {
    render: (tpl, params) => twing.render(tpl, params),
}
