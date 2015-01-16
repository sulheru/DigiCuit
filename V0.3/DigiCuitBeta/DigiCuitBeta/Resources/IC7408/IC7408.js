/**
 * Circuito integrado IC7408\n
 * Puerta logica AND de 2 entradas (x4)
 * @returns {Component}
 */
function IC7408() {
    this.Name = "IC7408";
    this.Description = "Puerta logica AND de 2 entradas (x4)";
    this.Version = "1.7.2015.01.14";
    this.Parent = "Circuitos Integrados";
    this.IsWire = false;
    this.Plugs = [
        {"X": 0, "Y": 0, "Connector": 13, "DC": new DirectCurrent()},
        {"X": 0, "Y": 1, "Connector": 12, "DC": new DirectCurrent()},
        {"X": 0, "Y": 2, "Connector": 11, "DC": new DirectCurrent()},
        {"X": 0, "Y": 3, "Connector": 10, "DC": new DirectCurrent()},
        {"X": 0, "Y": 4, "Connector": 9, "DC": new DirectCurrent()},
        {"X": 0, "Y": 5, "Connector": 8, "DC": new DirectCurrent()},
        {"X": 0, "Y": 6, "Connector": 7, "DC": new DirectCurrent()},
        {"X": 3, "Y": 6, "Connector": 6, "DC": new DirectCurrent()},
        {"X": 3, "Y": 5, "Connector": 5, "DC": new DirectCurrent()},
        {"X": 3, "Y": 4, "Connector": 4, "DC": new DirectCurrent()},
        {"X": 3, "Y": 3, "Connector": 3, "DC": new DirectCurrent()},
        {"X": 3, "Y": 2, "Connector": 2, "DC": new DirectCurrent()},
        {"X": 3, "Y": 1, "Connector": 1, "DC": new DirectCurrent()},
        {"X": 3, "Y": 0, "Connector": 0, "DC": new DirectCurrent()}
    ];
    this.Sockets = [];
    this.Rendering = {
        "ComponentImage": "IC7408\ComponentImage.png",
        "GraphicShifting": {"X": 15, "Y": 4},
        "Location": {"X": 0, "Y": 0},
        toString: function () {
            return JSON.stringify(this);
        }
    };
    this.Schema = {
        "ComponentSymbol": "IC7408\ComponentSymbol.png"
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

Components.push(new IC7408());
