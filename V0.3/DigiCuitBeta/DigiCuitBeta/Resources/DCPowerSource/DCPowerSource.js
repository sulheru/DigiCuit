/**
 * Circuito integrado Fuente de alimentación\n
 * Puerta logica AND de 2 entradas (x4)
 * @returns {Component}
 */
function DCPowerSource() {
    this.Name = "DCPowerSource";
    this.Description = "Una Fuente de alimentación de corriente continua.\nIncoming: {\"voltage\": \"5.0\", \"amperes\": \"0.2\", \"ohms\": \"25.0\"}";
    this.Version = "1.0.2015.01.19";
    this.Parent = "Alimentación";
    this.Plugs = [
        {"X": 0, "Y": 0, "Connector": 2, "DC": new DirectCurrent()},
        {"X": 0, "Y": 1, "Connector": 1, "DC": new DirectCurrent()}
    ];
    this.Sockets = [];
    this.Rendering = {
        "ComponentImage": "DCPowerSource\\ComponentImage.png",
        "GraphicShifting": {"X": 0, "Y": 0},
        "Location": {"X": 0, "Y": 0},
        "toString": function () {
            return JSON.stringify(this);
        }
    };
    this.Schema = {
        "ComponentSymbol": "DCPowerSource\\ComponentSymbol.png"
    };
    this.Properties = {
        "Incoming": new DirectCurrent()
    }
    this.Run = function () {
        this.Plugs[0].voltage = this.Properties.Incoming.voltage;
        this.Plugs[0].amperes = this.Properties.Incoming.amperes;
        this.Plugs[0].ohms = this.Properties.Incoming.ohms;
        return true;
    };
    this.reset = function () {
        this.Plugs = [
            {"X": 0, "Y": 0, "Connector": 2, "DC": new DirectCurrent()},
            {"X": 0, "Y": 1, "Connector": 1, "DC": new DirectCurrent()}
        ];
    };
    this.toString = function () {
        var str = {Name: "", Description: "", Version: ""};
        str.Name = this.Name;
        str.Description = this.Description;
        str.Version = this.Version;
        return JSON.stringify(str);
    };
}
