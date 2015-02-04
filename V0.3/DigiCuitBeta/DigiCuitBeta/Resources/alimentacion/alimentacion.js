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


function batería() {
    this.info = componentInfo('Batería', 'Batería similar a la de un teléfono movil', 'Alimentación', '1.0.0.0')
    this = new component();
    this.plugs = new conectorList();
    this.plugs.add(1, 1, 1, new DirectCurent());
    this.plugs.add(1, 2, 2, new DirectCurent());
    this.Properties.add('number', 'V', 5);
    this.Properties.add('number', 'mAh', 3200);
    this.run = function () {
        if (this.Properties["mAh"] > 0 && this.Properties["V"] > 0) {
            var amps = (this.Properties["mAh"] / 3600) * (circuit.timer / 1000);
            this.plugs[0].DC.volts = this.Properties["V"];
            this.plugs[0].DC.ohms = this.Properties["V"] / amps;
            this.plugs[0].DC.ms = circuit.timer;
        }
    };

}