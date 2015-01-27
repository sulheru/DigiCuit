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
        "args": {"type": "alert", "prompt": "", "HasRetuned": false, "result": undefined},
        "raise": function (prompt, type) {
            if (typeof type === "undefined")
                type = alert;
            this.events.args.prompt = prompt;
            this.events.args.type = type;
            while (!this.events.args.HasRetuned)
                ;
            events.args.HasRetuned = false;
            return this.events.args.result;
        }
    };

    this.log = [];
    this.logLimit = 0;

    this.command = function (cmd) {
        var res = eval(cmd);
        this.log.push({"cmd": cmd, "res": res});
        if (this.logLimit > 0) {
            var l = this.log.length - this.logLimit;
            if (l > 0)
                this.log.splice(0, l);
        }
    };
};