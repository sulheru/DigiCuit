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


function nodo() {
    this.info = componentInfo('nodo', 'nodo mara conexión en paralelo.', 'Componentes básicos', '1.0.0.0')
    this = new component();
    this.sockets = new conectorList();
    this.Rendering = new componentImage();
    this.run = function () {
        var conn = {"X": 0, "Y": 0, "Connector": 0, "Linked": false, "DC": new DirectCurrent()};
        var dc = new DirectCurrent();
        var ohms1 = 1;var ohms2 = 0;
        for (conn in this.sockets) {
            ohms1 *= conn.DC.ohms;            
            ohms2 += conn.DC.ohms;
        }
        dc.ohms = ohms1 / ohms2;
        for (var i = 0; i < this.sockets.length; i++) {
            this.sockets[i].DC = dc;
        }
    };

}