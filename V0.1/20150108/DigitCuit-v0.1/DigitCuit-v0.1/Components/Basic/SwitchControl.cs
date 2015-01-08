using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitCuit_v0._1.Components.Basic
{
    class SwitchControl : ElectricComponentOld
    {
        public bool isOn
        {
            get { return this.ison; }
            set { this.disignImage = (this.ison = value) ? DigitCuit_v0._1.Properties.Resources.SwitchOn : DigitCuit_v0._1.Properties.Resources.SwitchOff; }
        }
        private bool ison;

        public SwitchControl(PortLinker vcc, PortLinker gnd) : base(vcc, gnd) { }

        protected override void initialize(PortLinker vcc, PortLinker gnd)
        {
            base.initialize(vcc, gnd);            
        }
        public override void Run()
        {
            if (this.ison)
            {
                base.Run();
            }
        }
    }
}
