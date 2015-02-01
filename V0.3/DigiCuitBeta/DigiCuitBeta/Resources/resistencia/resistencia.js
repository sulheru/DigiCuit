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


function resistencia() {
    this.prototype = new component();
    this.plugs = new conectorList();
    this.plugs.add(1, 1, 1, new DirectCurent());
    this.plugs.add(1, 2, 2, new DirectCurent());
    this.rVal;
    this.run = function () {
        if (this.rVal !== 0) {
            this.plugs[1].DC = this.plugs[0].DC;
            this.plugs[1].DC.current.ohms = rVal;
        }
        else {
            this.plugs[1].DC = this.plugs[0].DC;
        }
    }

}