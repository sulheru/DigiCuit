var console = new function () {
    this.Output = [];
    this.log = function (x) {
        this.Output.push(x);
    }
}

var circuit = new function () {
    this.Components = [];
    this.Version = "2.0.2015.01.20";
    this.Description = "";
    this.waiting = [];

    this.add = function (type) {
        this.waiting.push(new type());
    };

    this.insert = function (index, type) {
        Components.splice(index, 0, new type());
    };

    this.remove = function (index) {
        Components.splice(index, 1);
    };

    this.Run = function () {
        Connector = [];
        for (var i = 0; i < Components.length; i++) {
            Components[i].Run();
            var j;
            for (j = 0; j < Components[i].Plugs.length; j++) {
                if (Connector[Components[i].Plugs[j].X] === undefined) {
                    Connector[Components[i].Plugs[j].X] = new Array(Components[i].Plugs[j].Y + 1);
                    Connector[Components[i].Plugs[j].X][Components[i].Plugs[j].Y] = {"Plug": {"componentId": i, "ioId": j}, "Socket": undefined};
                }
            }
            for (j = 0; j < Components[i].Sockets.length; j++) {
                if (Connector[Components[i].Sockets[j].X] !== undefined
                        && Connector[Components[i].Sockets[j].X][Components[i].Sockets[j].Y] !== undefined
                        && Connector[Components[i].Sockets[j].X][Components[i].Sockets[j].Y].Plug !== undefined) {
                    console.log(Components[i].Sockets[j].X + ";" + Components[i].Sockets[j].Y)
                    Connector[Components[i].Sockets[j].X][Components[i].Sockets[j].Y].Socket = {"componentId": i, "ioId": j};
                }
            }
        }
        for (var x = 0; x < Connector.length; x++) {
            for (var y = 0; y < Connector[x].length; y++) {
                this.electricExchange(Connector[x][y].Plug, Connector[x][y].Socket);
            }
        }
        this.commit();        
    };
    
    this.commit=function(){        
        this.Components.splice(this.Components.length,0,this.waiting);
        this.waiting = [];
    }

    this.electricExchange = function (plug, socket) {
        var dc = this.Component[plug.componentId].Plugs[plug.ioId].DC;
        this.Component[plug.componentId].Plugs[plug.ioId].DC = Component[socket.componentId].Sockets[socket.ioId].DC;
        this.Component[socket.componentId].Sockets[socket.ioId].DC = dc;
    };

    this.chLoc = function (i, x, y) {
        Components[i].Rendering.Location.X = x;
        Components[i].Rendering.Location.Y = y;
    }
}

function DirectCurrent() {
    this.voltage = 0.0;
    this.amperes = 0.0;
    this.ohms = 0.0;

    this.toString = function () {
        return JSON.stringify(this);
    };

    this.parse = function (obj) {
        this.amperes = obj.amperes;
        this.ohms = obj.ohms;
        this.voltage = obj.voltage;
        return this;
    };

    this.add = function (dcv) {
        this.voltage += dcv.voltage;
        this.ohms += dcv.ohms;
        this.amperes += dcv.amperes;
    };
}

function Run() {
    circuit.Run();
}

function elecricExchange(plug, socket) {
    circuit.elecricExchange();
}