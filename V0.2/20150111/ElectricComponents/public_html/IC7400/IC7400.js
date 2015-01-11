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
    this.Properties = {};
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
        if (!(this.InOut[0].voltage != 0 && this.InOut[1].voltage != 0))
        {
            this.InOut[2] = this.InOut[13];
            outs++;
        }
        if (!(this.InOut[3].voltage != 0 && this.InOut[4].voltage != 0))
        {
            this.InOut[5] = this.InOut[13];
            outs++;
        }
        if (!(this.InOut[8].voltage != 0 && this.InOut[9].voltage != 0))
        {
            this.InOut[7] = this.InOut[13];
            outs++;
        }
        if (!(this.InOut[11].voltage != 0 && this.InOut[12].voltage != 0))
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
        return JSON.stringify(this);
    };
}