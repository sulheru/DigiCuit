
var openJsFile = "openjsfile";
var quit = "quit";
var canvas= "_canvas";

var events = {
    alert: "",
    prompt: ""
};

function prompt(message) {
    events.prompt = message;
    //while(events.prompt === message);
    return events.prompt;
}

function alert(message) {
    events.alert = message;
}

function toString() {
    return JSON.stringify(this);
}
