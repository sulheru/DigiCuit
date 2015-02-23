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


resistencia.prototype = new component();
function resistencia() {
    this.info = componentInfo('Resistencia', 'Resistencia común para circuitos con corriente continua en serie y paralelo.', 'Componentes básicos', '1.0.0.0')
    this.plugs.push(new connector(1, 1, 1, new DirectCurrent()));
    this.plugs.push(new connector(1, 2, 2, new DirectCurrent()));
    this.Properties.add('number', 'resistencia', 90);
    this.run = function () {
        if (this.Properties.resistencia !== 0) {
            this.plugs[1].DC = this.plugs[0].DC;
            this.plugs[1].DC.ohms += this.Properties.resistencia;
        }
        else {
            this.plugs[1].DC = this.plugs[0].DC;
        }
    };
}