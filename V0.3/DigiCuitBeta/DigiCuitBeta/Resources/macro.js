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

myComp.prototype = new component();
function myComp() {
    this.info = componentInfo('nodo', 'nodo para conexión en paralelo.', 'Componentes básicos', '1.0.0.0');
    this.Properties.add('number', 'width', 1);
    this.Properties.add('number', 'height', 1);
    this.run = function () {
        var dc = new DirectCurrent();
        var ohms1 = 1;
        var ohms2 = 0;
        line = 28;
        for (var i = 0; i < this.sockets.length; i++) {
            ohms1 *= this.sockets[i].DC.ohms;
            ohms2 += this.sockets[i].DC.ohms;
            if (dc.ms !== this.sockets[i].DC.ms) {
                dc.voltage += this.sockets[i].DC.voltage;
                dc.ms = this.sockets[i].DC.ms;
            }
        }
        line = 33;
        dc.ohms = ohms1 / ohms2;
        for (var i = 0; i < this.sockets.length; i++) {
            this.sockets[i].DC.ohms = dc.ohms;
        }
    };

    this.reload = function () {
        var len = this.Properties.width * this.Properties.height;
        this.sockets = new Array(len);
        var conn = {"X": 0, "Y": 0, "Connector": 0, "Linked": false, "DC": new DirectCurrent()};
        for (var i = 0; i < len; i++) {
            conn.Connector = i + 1;
            this.sockets[i] = conn;
        }
    };
}