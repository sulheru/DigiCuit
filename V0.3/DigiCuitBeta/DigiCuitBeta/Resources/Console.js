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
 * <strong>WARNING!!!</strong> 
 * <p>The console object should not be used with out a proper JS engine listener.
 * It can cause infinite loop failure.</p>
 * <p>You can develope a listener pointing to <i>console.events.args</i> and 
 * stoping the loop by changing <i>console.events.args.HasRetuned = true</i> 
 */

var console = new function () {
    this.events = {
        "args": {"type": "alert", "prompt": "", "IsRunning": false, "result": null},
        "raise": function (prompt, type) {
            if (typeof type === "undefined")
                type = "alert";
            console.events.args.prompt = prompt;
            console.events.args.type = type;
            console.events.args.IsRunning = true;
            while (console.events.args.IsRunning)
                ;
            console.events.args.IsRunning = false;
            return this.events.args.result;
        }
    };
    this.log = new Array();

    this.command = function (cmd) {
        var res;
        var err;
        cmd = cmd.replace(new RegExp("'", 'g'), '"');
        try {
            res = eval(cmd);
        }
        catch (err) {
            res = err.message;
        }
        if (res === undefined)
            res = "undefined";
        this.log.add(cmd, res);
        return res;
    };
};

console.log.limit = 0;
console.log.add = function () {
    var res = 0;
    res = this.push({"cmd": arguments[0], "res": arguments[1]});

    if (this.limit > 0) {
        var l = this.length - this.limit;
        if (l > 0)
            res = this.splice(0, l).length;
    }

    return res;
};

console.log.toString = function () {
    return this.join("\n");
}

function test() {
    return console.log.add('test()', 'Hello World!!');
}