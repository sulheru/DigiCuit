using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitCuit_v0._1.Components
{
    public struct DirectCurrent
    {
        public int Index;

        private double voltage;
        private double amperes;
        private double ohms;

        public double Voltage
        {
            get { return this.amperes * this.ohms; }
            set { this.voltage = value; }
        }
        public double Amperes
        {
            get { return this.voltage / this.ohms; }
            set { this.amperes = value; }
        }
        public double Ohms
        {
            get { return this.voltage / this.amperes; }
            set { this.ohms = value; }
        }

        public void autoComplete()
        {
            if (this.voltage != 0.0 && this.amperes != 0.0 && this.ohms == 0.0) { this.ohms = this.Ohms; }
            if (this.voltage != 0.0 && this.amperes == 0.0 && this.ohms != 0.0) { this.amperes = this.Amperes; }
            if (this.voltage == 0.0 && this.amperes != 0.0 && this.ohms != 0.0) { this.voltage = this.Voltage; }
        }

        public static DirectCurrent operator +(DirectCurrent v1, DirectCurrent v2)
        {
            v1.ohms += v2.ohms;
            v1.voltage += v2.voltage;
            v1.amperes = 0;
            v1.autoComplete();
            return v1;
        }

        public static DirectCurrent operator -(DirectCurrent v1, DirectCurrent v2)
        {
            v1.ohms -= v2.ohms;
            v1.voltage -= v2.voltage;
            v1.amperes = 0;
            v1.autoComplete();
            return v1;
        }

        public static DirectCurrent operator /(DirectCurrent v1, double v2)
        {
            v1.ohms /= v2;
            v1.voltage /= v2;
            v1.amperes = 0;
            v1.autoComplete();
            return v1;
        }

        public static DirectCurrent operator *(DirectCurrent v1, double v2)
        {
            v1.ohms *= v2;
            v1.voltage *= v2;
            v1.amperes = 0;
            v1.autoComplete();
            return v1;
        }

        public override string ToString()
        {
            string str = String.Format("Voltage: {0} Volts\nAmperes: {1} Amps\nOhms: {2} Ohms", this.voltage, this.amperes, this.ohms);
            return str;
        }
    }
}
