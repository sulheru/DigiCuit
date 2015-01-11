
function exit() {
    return "exit";
}

function toString() {
    return JSON.stringify(this);
}

var NULL_DCV = new DirectCurrent();

function DirectCurrent() {
    this.voltage = "0.0";
    this.amperes = "0.0";
    this.ohms = "0.0";

    this.toString = function () {
        return JSON.stringify(this);
    };
}