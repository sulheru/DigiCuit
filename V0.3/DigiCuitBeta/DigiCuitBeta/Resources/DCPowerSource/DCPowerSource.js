/**
 * Circuito integrado Fuente de alimentación\n
 * Puerta logica AND de 2 entradas (x4)
 * @returns {Component}
 */
function Component() {
    this.Name = "Fuente de Alimentación (CC)";
    this.Description = "Una Fuente de alimentación de corriente continua.\nIncoming: {\"voltage\": \"5.0\", \"amperes\": \"0.2\", \"ohms\": \"25.0\"}";
    this.Parent = "Circuitos Integrados";
    this.InOut = [new DirectCurrent(), new DirectCurrent()];
    this.Properties = {
        "Incoming": {"voltage": "5,0", "amperes": "0,2", "ohms": "25,0"},
        "ComponentImage": "ComponentImage.png",
        "ComponentSymbol": "ComponentSymbol.png"
    };
    this.Run = function () {
        this.InOut[0].voltage = this.Properties.Incoming.voltage;
        this.InOut[0].amperes = this.Properties.Incoming.amperes;
        this.InOut[0].ohms = this.Properties.Incoming.ohms;
        return true;
    };

}
