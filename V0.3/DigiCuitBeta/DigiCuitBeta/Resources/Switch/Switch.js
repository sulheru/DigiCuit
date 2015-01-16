/**
 * Circuito integrado Fuente de alimentación\n
 * Puerta logica AND de 2 entradas (x4)
 * @returns {Component}
 */
function Component() {
    this.Name = "Switch";
    this.Description = "Interruptor comun";
    this.Parent = "Circuitos Integrados";
    this.InOut = [new DirectCurrent(), new DirectCurrent()];
    this.Properties = {
        "isOn": true,
        "ComponentImage": "ComponentImage.png",
        "ComponentSymbol": "ComponentSymbol.png"
    };
    this.Run = function () {
        if (this.Properties.isOn) {
            this.InOut[1] = this.InOut[0];
        }
        else {
            this.InOut[1] = new DirectCurrent();
        }
        return true;
    };

}
