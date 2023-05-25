const config = require('config')
const debug = config.get('app.debug')

var fname = function getFuncName() {
    return getFuncName.caller.name
}

var log_error = function(msg,err){
    console.log(new Date().toJSON()+" "+msg)
    if(typeof err !== undefined){
        console.log(err.stack)
    }
}

var log_debug = function(){
    if(debug != true){
        return
    }
    
    let msg = new Date().toJSON()+" "+arguments.join(" ")
    console.log(msg)
}

module.exports = {
    error: log_error,
    debug: log_debug
}
