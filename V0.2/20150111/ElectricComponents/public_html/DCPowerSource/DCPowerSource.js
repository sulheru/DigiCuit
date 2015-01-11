/**
 * Circuito integrado Fuente de alimentación\n
 * Puerta logica AND de 2 entradas (x4)
 * @returns {Component}
 */
function Component() {
    this.Name = "Fuente de Alimentación (CC)";
    this.Description = "Una Fuente de alimentación de corriente continua.\nIncoming: {\"voltage\": \"5.0\", \"amperes\": \"0.2\", \"ohms\": \"25.0\"}";
    this.Parent = "Circuitos Integrados";
    this.ComponentImage = "C:\\Users\\Theos\\Documents\\NetBeansProjects\\ElectricComponents\\public_html\\IC7408\\ComponentImage.png";
    this.ComponentSymbol = "C:\\Users\\Theos\\Documents\\NetBeansProjects\\ElectricComponents\\public_html\\IC7408\\ComponentSymbol.png";
    this.InOut = [new DirectCurrent(), new DirectCurrent()];
    this.Properties = {
        Incoming: {"voltage": "5,0", "amperes": "0,2", "ohms": "25,0"}
    };
    this.Run = function () {
        this.InOut[0].voltage = this.Properties.Incoming.voltage;
        this.InOut[0].amperes = this.Properties.Incoming.amperes;
        this.InOut[0].ohms = this.Properties.Incoming.ohms;
        return True;
    };

    this.reset = function () {
        this.InOut = [new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent()];
    };

    this.toString = function () {
        return JSON.stringify(this);
    };
}