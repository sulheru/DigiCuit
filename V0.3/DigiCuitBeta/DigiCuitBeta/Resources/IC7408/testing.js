

function Circuit() {
    this.Components = [];
    this.Version = "2.0.2015.01.20";
    this.Description = "";

    this.add = function (type) {
        this.Components.push(new type());
    };

    this.insert = function (index, type) {
        Components.splice(index, 0, new type());
    };

    this.remove = function (index) {
        Components.splice(index, 1);
    };

    this.Run = function () {
        comp = this.Components;
        Connector = [];
        var j;
        console.log(comp);
        for (var i = 0; i < comp.length; i++) {
            comp[i].Run();
            console.log(comp[i]);
            for (j = 0; j < comp[i].Plugs.length; j++) {
                Connector[comp[i].Plugs[j].X][comp[i].Plugs[j].Y].Plug = {"componentId": i, "ioId": j};
            }
            for (j = 0; j < Components[i].Sockets.length; j++) {
                Connector[comp[i].Sockets[j].X][comp[i].Sockets[j].Y].Socket = {"componentId": i, "ioId": j};
            }
        }
        for (var x = 0; x < Connector.length; x++) {
            for (var y = 0; y < Connector[x].length; y++) {
                this.electricExchange(Connector[x][y].Plug, Connector[x][y].Socket);
            }
        }
    };

    this.electricExchange = function (plug, socket) {
        var conductor = new Connector();
        plug = conductor.parse(plug);
        socket = conductor.parse(socket);
        var dc = Component[plug.componentId].Plugs[plug.ioId].DC;
        Component[plug.componentId].Plugs[plug.ioId].DC = Component[socket.componentId].Sockets[socket.ioId].DC;
        Component[socket.componentId].Sockets[socket.ioId].DC = dc;
    };

    this.chLoc = function (i, x, y) {
        Components[i].Rendering.Location.X = x;
        Components[i].Rendering.Location.Y = y;
    };
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

var circuit = new Circuit();

function Run() {
    circuit.Run();
}

function elecricExchange(plug, socket) {
    circuit.elecricExchange();
}


function IC7408() {
    this.Name = "IC7408";
    this.Description = "Puerta logica AND de 2 entradas (x4)";
    this.Version = "1.7.2015.01.14";
    this.Parent = "Circuitos Integrados";
    this.Plugs = [
        {"X": 0, "Y": 0, "Connector": 14, "DC": new DirectCurrent()},
        {"X": 0, "Y": 1, "Connector": 13, "DC": new DirectCurrent()},
        {"X": 0, "Y": 2, "Connector": 12, "DC": new DirectCurrent()},
        {"X": 0, "Y": 3, "Connector": 11, "DC": new DirectCurrent()},
        {"X": 0, "Y": 4, "Connector": 10, "DC": new DirectCurrent()},
        {"X": 0, "Y": 5, "Connector": 9, "DC": new DirectCurrent()},
        {"X": 0, "Y": 6, "Connector": 8, "DC": new DirectCurrent()},
        {"X": 3, "Y": 6, "Connector": 7, "DC": new DirectCurrent()},
        {"X": 3, "Y": 5, "Connector": 6, "DC": new DirectCurrent()},
        {"X": 3, "Y": 4, "Connector": 5, "DC": new DirectCurrent()},
        {"X": 3, "Y": 3, "Connector": 4, "DC": new DirectCurrent()},
        {"X": 3, "Y": 2, "Connector": 3, "DC": new DirectCurrent()},
        {"X": 3, "Y": 1, "Connector": 2, "DC": new DirectCurrent()},
        {"X": 3, "Y": 0, "Connector": 1, "DC": new DirectCurrent()}
    ];
    this.Sockets = [];
    this.Rendering = {
        "ComponentImage": "IC7408\\ComponentImage.png",
        "GraphicShifting": {"X": 8, "Y": 0},
        "Location": {"X": 0, "Y": 0},
        "toString": function () {
            return JSON.stringify(this);
        }
    };
    this.Schema = {
        "ComponentSymbol": "IC7408\\ComponentSymbol.png"
    };
    this.Run = function () {
        if (this.InOut[2].voltage != 0)
            this.InOut[6] = this.InOut[2];
        if (this.InOut[5].voltage != 0)
            this.InOut[6] = this.InOut[5];
        if (this.InOut[7].voltage != 0)
            this.InOut[6] = this.InOut[7];
        if (this.InOut[10].voltage != 0)
            this.InOut[6] = this.InOut[10];
        if (this.InOut[13].voltage == 0)
            return "{errnum: 501, port: 14}";
        var outs = 0;
        if ((this.InOut[0].voltage != 0 && this.InOut[1].voltage != 0))
        {
            this.InOut[2] = this.InOut[13];
            outs++;
        }
        if ((this.InOut[3].voltage != 0 && this.InOut[4].voltage != 0))
        {
            this.InOut[5] = this.InOut[13];
            outs++;
        }
        if ((this.InOut[8].voltage != 0 && this.InOut[9].voltage != 0))
        {
            this.InOut[7] = this.InOut[13];
            outs++;
        }
        if ((this.InOut[11].voltage != 0 && this.InOut[12].voltage != 0))
        {
            this.InOut[10] = this.InOut[13];
            outs++;
        }
        return outs + " outputs";
    };
    this.reset = function () {
        this.InOut = [new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent()];
    };
    this.toString = function () {
        var str = {Name: "", Description: "", Version: ""};
        str.Name = this.Name;
        str.Description = this.Description;
        str.Version = this.Version;
        return JSON.stringify(str);
    };
}

circuit.add(IC7408);

Run();