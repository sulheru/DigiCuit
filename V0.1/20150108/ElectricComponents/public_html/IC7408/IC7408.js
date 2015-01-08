/**
 * Circuito integrado IC7408\n
 * Puerta logica AND de 2 entradas (x4)
 * @returns {Component}
 */
function Component() {
    this.Name = "IC7408";
    this.Description = "Puerta logica NAND de 2 entradas (x4)";
    this.Parent = "Circuitos Integrados";
    this.ComponentImage = "C:\\Users\\Theos\\Documents\\NetBeansProjects\\ElectricComponents\\public_html\\IC7408\\ComponentImage.png";
    this.ComponentSymbol = "C:\\Users\\Theos\\Documents\\NetBeansProjects\\ElectricComponents\\public_html\\IC7408\\ComponentSymbol.png";
    this.InOut = [new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent()];
    this.Settings = [];
    this.Run = function () {
        if (this.InOut[2].toString() !== NULL_DCV.toString())
            return "{errnum: 500, port: 3}";
        if (this.InOut[5].toString() !== NULL_DCV.toString())
            return "{errnum: 500, port: 6}";
        if (this.InOut[7].toString() !== NULL_DCV.toString())
            return "{errnum: 500, port: 8}";
        if (this.InOut[10].toString() !== NULL_DCV.toString())
            return "{errnum: 500, port: 11}";
        if (this.InOut[13].toString() === NULL_DCV.toString())
            return "{errnum: 501, port: 14}";
        if (!(this.InOut[0].toString() === NULL_DCV.toString() && this.InOut[1].toString() === NULL_DCV.toString()))
            this.InOut[2] = this.InOut[13];
        if (!(this.InOut[3].toString() === NULL_DCV.toString() && this.InOut[4].toString() === NULL_DCV.toString()))
            this.InOut[5] = this.InOut[13];
        if (!(this.InOut[8].toString() === NULL_DCV.toString() && this.InOut[9].toString() === NULL_DCV.toString()))
            this.InOut[7] = this.InOut[13];
        if (!(this.InOut[11].toString() === NULL_DCV.toString() && this.InOut[12].toString() === NULL_DCV.toString()))
            this.InOut[10] = this.InOut[13];
    };

    this.reset = function () {
        this.InOut = [new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent(), new DirectCurrent()];
    }

    this.toString = function () {
        return JSON.stringify(this);
    };
}