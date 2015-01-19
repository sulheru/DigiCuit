/**
 * Circuito integrado BreadBoard\n
 * Puerta logica AND de 2 entradas (x4)
 * @returns {Component}
 */
function Wire() {
    var sock = new DirectCurrent();
    this.Name = "Wire";
    this.Description = "Puerta logica NAND de 2 entradas (x4)";
    this.Parent = "Sockets";
    this.Version = "1.0.2015.01.18";
    this.Plugs = [
        {"X": 0, "Y": 0, "Connector": 0, "DC": new DirectCurrent()},
        {"X": 0, "Y": 0, "Connector": 1, "DC": new DirectCurrent()}
    ];
    this.Sockets = [];
    this.Rendering = {
        "isWire": true,
        "Wire": {
            "X": 1,
            "Y": 1,
            "Brush": {
                "color": "#FF0000",
                "width": 3
            }
        },
        "GraphicShifting": {"X": -7, "Y": -7},
        "Location": {"X": 0, "Y": 0},
        "toString": function () {
            return JSON.stringify(this);
        },
    };
    this.Schema = {
        "ComponentSymbol": "IC7408\ComponentSymbol.png"
    };
    this.Run = function () {

    };

    this.reset = function () {
        this.Sockets = [];
        for (var x = 0; x < this.Rendering.BreadBoard.width; x++) {
            for (var y = 0; y < this.Rendering.BreadBoard.height; y++) {
                var con = String.fromCharCode(y + 65) + x.toString();
                this.Sockets.push({"X": x, "Y": y, "Connector": con, "DC": sock});
            }
        }
    };

    this.toString = function () {
        return JSON.stringify(this);
    };
}

function DrawWire(X1, Y1, X2, Y2) {
    var Index = Components.length;
    Components.push(new Wire);
    Components[Index].Rendering.Location.X = Components[Index].Plugs[0].X = X1;
    Components[Index].Rendering.Location.Y = Components[Index].Plugs[0].Y = Y1;
    Components[Index].Rendering.Wire.X = Components[Index].Plugs[1].X = X2;
    Components[Index].Rendering.Wire.Y = Components[Index].Plugs[1].Y = Y2;
}
