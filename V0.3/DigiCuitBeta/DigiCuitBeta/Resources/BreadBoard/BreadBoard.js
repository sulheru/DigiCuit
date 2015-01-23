/**
 * Circuito integrado BreadBoard\n
 * Puerta logica AND de 2 entradas (x4)
 * @returns {Component}
 */
function Breadboard() {
    var sock = new DirectCurrent();
    this.Name = "BreadBoard";
    this.Description = "Puerta logica NAND de 2 entradas (x4)";
    this.Parent = "Sockets";
    this.Version = "1.0.2015.01.18";
    this.Plugs = [];
    this.Sockets = [
        {"X": 0, "Y": 0, "Connector": 0, "DC": new DirectCurrent()}
    ];
    this.Rendering = {
        "isBreadBoard": true,
        "Breadboard": {
            "width": 1,
            "height": 1
        },
        "GraphicShifting": {"X": 1, "Y": 1},
        "Location": {"X": 0, "Y": 0},
        "toString": function () {
            return JSON.stringify(this);
        }
    };
    this.Schema = {
        "ComponentSymbol": "IC7408\ComponentSymbol.png"
    };
    this.Run = function () {
        sock = new DirectCurrent();
        this.Sockets.forEach(function (dcv) {
            sock.add(dcv);
        });
        this.reset();
        this.Rendering.Breadboard.width
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

function loadbb(x, y, w, h) {
    console.log('trying(x:' + x + 'y:' + y + 'w:' + w + 'h:' + h + ')');
    circuit.add(Breadboard);
    console.log('added');
    circuit.commit();
    console.log('committed');
    circuit.Components[circuit.Components.length - 1].Rendering.Breadboard.height = h;
    console.log('h');
    circuit.Components[circuit.Components.length - 1].Rendering.Breadboard.width = w;
    console.log('w');
    circuit.Components[circuit.Components.length - 1].Rendering.Location.X = x;
    console.log('x');
    circuit.Components[circuit.Components.length - 1].Rendering.Location.Y = y;
    console.log('y');
}

function bbchSize(i, w, h) {
    if (circuit.Components[i].Rendering.isBreadBoard) {
        circuit.Components[i].Rendering.Breadboard.width = w;
        circuit.Components[i].Rendering.Breadboard.height = h;
    }
}