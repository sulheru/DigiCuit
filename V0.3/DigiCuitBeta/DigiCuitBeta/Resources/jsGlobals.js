
var Version = "0.15.2015.01.14";
var Description = "";

function Connector() {
    this.componentId;
    this.ioId;
    this.ioType;

    this.toString = function () {
        return JSON.stringify(this);
    };

    this.parse = function (obj) {
        this.componentId = obj.componentId;
        this.ioId = obj.ioId;
        this.ioType = obj.isType;
        return this;
    };
}

function DirectCurrent() {
    this.voltage = "0.0";
    this.amperes = "0.0";
    this.ohms = "0.0";

    this.toString = function () {
        return JSON.stringify(this);
    };

    this.parse = function (obj) {
        this.amperes = obj.amperes;
        this.ohms = obj.ohms;
        this.voltage = obj.voltage;
        return this;
    };
}

function Run() {
    for (var i = 0; i < this.Components.length; i++) {
        Components[i].Run();
    }
}

function elecricExchange(plug, socket) {
    plug = {componentId: plug[0], ioId: plug[1], ioType: plug[2]};
    socket = {componentId: socket[0], ioId: socket[1], ioType: socket[2]};
    var conductor = new Connector();
    plug = conductor.parse(plug);
    socket = conductor.parse(socket);
    var dc = Component[plug.componentId].Plugs[plug.ioId].DC;
    Component[plug.componentId].Plugs[plug.ioId].DC = Component[socket.componentId].Sockets[socket.ioId].DC;
    Component[socket.componentId].Sockets[socket.ioId].DC = dc;
}

function WelcomeText() {
    return 'Hello World';
}

