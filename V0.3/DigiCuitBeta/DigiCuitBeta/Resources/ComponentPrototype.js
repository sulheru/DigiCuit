/* 
 * Copyright (C) 2015 Theos
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

/**
 * This is the component prototype.
 * @returns {component}
 */
function component() {
    this.info = {
        get name() {
            return "CamelCaseComponentName";
        },
        get description() {
            return "String component description";
        },
        get group() {
            return "String component parent group";
        },
        get version() {
            return "Version number string";
        }
    };
    this.plugs = new Array();
    this.sockets = new Array();
    this.Rendering = new componentImage();
    this.Schema = new componentImage();
    this.Properties = new properties();
    this.run = function () {
        // Dummy foo
    };
    this.load = function () {
        this.reload();
    };
    this.reload = function () {
        // Dummy foo
    };
    this.reset = function () {
        this.reload();
    };
    this.toString = function () {
        return JSON.stringify(this.info);
    };
    this.load();
}

/**
 * component image configures the rendering options.
 * @returns {componentImage}
 */
function componentImage() {
    // Ruta relativa o completa del archivo de imagen a usar
    this.ComponentImage = "ImageDirectory.png";

    // Desplazamiento de la esquina superior izquierda en pixels con relación a la cuadricula
    this.GraphicShifting = {"X": 8, "Y": 0};

    // Localización en la cuadricula
    this.Location = {"X": 0, "Y": 0};

    // Boolean. Define si es un cable o no
    this.isWire = false;

    // Boolean. Define si es un Node o no
    this.isNode = false;

    // Boolean. Define si el Randerizador uasara una macro de comandos o no
    this.isDrawMacro = false;

    // Objeto Node. Define el ancho y el alto del Node. Por unidad de ancho y alto tiene 1 pin de conexxión
    this.Node = {"width": 0, "height": 0};

    // Objeto Wire. Define en segundo extremo del cable y su color
    this.Wire = {"X": 0, "Y": 0, "Brush": "#FF000000", "width": 1};

    // Objeto macro. Genera un gráfico con una macro de comandos.
    this.DrawMacro = new Array();
}

/**
 * Component info returns a readonly object with component info
 * @param {string} name the component name
 * @param {string} description the component description
 * @param {string} group the component group
 * @param {string} version the component version
 * @returns {componentInfo.ComponentPrototypeAnonym$2}
 */
function componentInfo(name, description, group, version) {
    if (name === undefined ||
            description === undefined ||
            group === undefined ||
            version === undefined)
        throw new Error('All args are mandatory.')
    return {
        get name() {
            return name;
        },
        get description() {
            return description;
        },
        get group() {
            return group;
        },
        get version() {
            return version;
        }
    };
}

function connector(X, Y, Connector, DC) {
    this.X = X;
    this.Y = Y;
    this.Connector = Connector;
    this.DC = DC;
}

function DirectCurrent() {
    this.prototype = {
        "ms": 0,
        "ohms": 0,
        "voltage": 0,
        get amperes() {
            return  voltage / ohms;
        },
        get wats() {
            return voltage * amperes;
        }
    };

    this.toString = function () {
        return JSON.stringify(this);
    };
}

function properties() {
    this.add = function (type, key, value) {
        if ((typeof value === "string" && type.toLowerCase() === "string") ||
                (typeof value === "boolean" && type.toLowerCase() === "boolean") ||
                (typeof value === "number" && type.toLowerCase() === "number")) {
            this[key] = {
                "type": type,
                "key": key,
                "value": value
            };
            this[key].toString = function () {
                return this.value;
            };
        } else {
            var err = new Error("Type is not accepted. Only 'number', 'string' and 'boolean' are accepted.");
            err.message = "Type is not accepted. Only 'number', 'string' and 'boolean' are accepted.";
            throw err;
        }
    };
    this.checkItem = function (index) {
        return (this[index]["type"] !== undefined && this[index]["key"] !== undefined && this[index]["value"] !== undefined);
    };
}