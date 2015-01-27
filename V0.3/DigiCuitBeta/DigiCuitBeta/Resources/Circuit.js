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


var circuit = new function () {
    this.prototype = new Array();
    this.isRunning = false;
    this.run = function () {
        this.isRunning = true;
        var comps;
        var comp;
        var conn = new connectors();
        comp.prototype = new component;
        while (this.isRunning) {
            comps = this.slice(0);
            for (var i = 0; i < comps.length; i++) {
                comp = comps[i]
                comp.run();
                var cList;
                cList.prototype = {"X": 0, "Y": 0, "Connector": 0, "DC": undefined}
                var j = 0;
                for (j = 0; j < comp.plugs; j++) {
                    cList = comp.plugs[j];
                    conn.push(cList.X, cList.Y, true, {"comp": i, "conn": j});
                }
                for (j = 0; j < comp.sockets; j++) {
                    cList = comp.sockets[j];
                    conn.push(cList.X, cList.Y, false, {"comp": i, "conn": j});
                }
            }
            for (var x = 0; x < conn.length; x++) {
                for (var y = 0; y < conn[x].length; y++) {
                    if (conn[x][y].socket !== undefined && conn[x][y].plug !== undefined) {
                        var s = conn[x][y].socket;
                        var p = conn[x][y].plug;
                        var dc = comp[s.comp].sockets[s.conn].DC;
                        comp[s.comp].sockets[s.conn].DC = comp[p.comp].plugs[p.conn].DC;
                        comp[p.comp].plugs[p.conn].DC = dc;
                    }
                }
            }
        }
    };
};

function connectors() {
    this.prototype = new Array(0);
    this.push = function (X, Y, isPlug, pointer) {
        if (X >= this.length) {
            var k = X - this.length;
            while ((k--) + 1) {
                this.prototype.push.apply(this, undefined);
            }
        }
        if (this[X] === undefined)
            this[X] = new Array(Y + 1);
        if (this[X][Y] === undefined)
            this[X][Y] = {"plug": undefined, "socket": undefined};
        isPlug ? this[X][Y].plug = pointer : this[X][Y].socket = pointer;
        return this.length;
    };
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