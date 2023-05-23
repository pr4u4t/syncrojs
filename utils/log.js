var fname = function getFuncName() {
    return getFuncName.caller.name
}

var log_error = function(msg,err){
    console.log(msg)
    if(typeof err !== undefined){
        console.log(err.stack())
    }
}

module.exports = {
    error: log_error
}
