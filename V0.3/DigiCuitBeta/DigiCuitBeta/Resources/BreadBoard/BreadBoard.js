/**
 * Circuito integrado BreadBoard\n
 * Puerta logica AND de 2 entradas (x4)
 * @returns {Component}
 */
function Component() {

    this.Name = "BreadBoard";
    this.Description = "Puerta logica NAND de 2 entradas (x4)";
    this.Parent = "Elementos de Prueba";
    this.InOut = [];
    this.Properties = {
        "ComponentImage": "ComponentImage.png",
        "ComponentSymbol": "ComponentSymbol.png"
    };
    this.Run = function () {
    };

    this.reset = function () {
        resetingComp(this);
    };

    this.toString = function () {
        return JSON.stringify(this);
    };
}

function resetingComp(compo) {
    

}
