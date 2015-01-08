
var NULL_DCV = new DirectCurrent();

function DirectCurrent() {
    this.voltage = "0.0";
    this.amperes = "0.0";
    this.ohms = "0.0";

    this.Voltage = function () {
        this.voltage = parseFloat(this.amperes * this.ohms);
        return (this.voltage === "NaN") ? this.voltage : "0";
    };
    this.Amperes = function () {
        this.amperes = parseFloat(this.voltage / this.ohms);
        return (this.amperes === "NaN") ? this.amperes : "0";
    };
    this.Ohms = function () {
        this.ohms = parseFloat(this.voltage / this.amperes);
        return (this.ohms === "NaN") ? this.ohms : "0";
    };

    this.toString = function () {
        return JSON.stringify(this);
    };
}