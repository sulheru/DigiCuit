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
var line = 0;
var circuit = new Array();
circuit.isRunning = false;
circuit.timer = 0;
circuit.run = function () {
    var comp;
    
    var conn = new Array();
    comp = new component;
    for (var i = 0; i < circuit.length; i++) {
        
        comp = this[i];
        comp.run();
        var cList;
        cList = {"X": 0, "Y": 0, "Connector": 0, "DC": undefined};
        var j = 0;
        
        for (j = 0; j < comp.plugs; j++) {
            cList = comp.plugs[j];
            conn = connector(conn, cList.X, cList.Y, true, {"comp": i, "conn": j});
        }
        for (j = 0; j < comp.sockets; j++) {
            cList = comp.sockets[j];
            
            conn = connector(conn, cList.X, cList.Y, false, {"comp": i, "conn": j});
        }
    }
    this.conn = conn;
    for (var x = 0; x < conn.length; x++) {
        
        for (var y = 0; y < conn[x].length; y++) {
            
            if (conn[x][y].socket !== undefined && conn[x][y].plug !== undefined) {
                var s = conn[x][y].socket;
                
                var p = conn[x][y].plug;
                this[s.conn].sockets[s.conn].Linked = p;
                this[p.conn].sockets[p.conn].Linked = s;
            }
        }
    }
    
};

function connector(array, X, Y, isPlug, pointer) {
    if (X >= array.length) {
        var k = X - array.length;
        while ((k--) + 1) {
            array.push(undefined);
        }
    }
    
    if (array[X] === undefined)
        array[X] = new Array(Y + 1);
    if (array[X][Y] === undefined)
        array[X][Y] = {"plug": undefined, "socket": undefined};
    
    isPlug ? array[X][Y].plug = pointer : array[X][Y].socket = pointer;
    return array;
}

Array.prototype.move = function (old_index, new_index) {
    if (new_index >= this.length) {
        var k = new_index - this.length;
        while ((k--) + 1) {
            this.push(undefined);
        }
    }
    this.splice(new_index, 0, this.splice(old_index, 1)[0]);
    return this; // for testing purposes
};