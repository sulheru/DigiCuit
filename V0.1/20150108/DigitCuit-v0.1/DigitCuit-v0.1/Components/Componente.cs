using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigitCuit_v0._1.Components
{
    public class ElectricComponentOld
    {
        public DirectCurrent Vcc;
        public DirectCurrent Gnd;

        public List<PortLinker> Input 
        {
            get { return this.input; }
            set
            {
                if (MaxInputs != 0 || MaxInOuts!=0)
                {
                    if (this.MaxInputs <= value.Count && this.MaxInOuts <= value.Count + this.output.Count) { this.input = value; }
                }
                else
                {
                    this.input = value;
                }
            }
        }
        public List<PortLinker> Output
        {
            get { return this.output; }
            set
            {
                if (MaxOutputs != 0 || MaxInOuts != 0)
                {
                    if (this.MaxOutputs <= value.Count && this.MaxInOuts <= value.Count + this.input.Count) { this.output = value; }
                }
                else
                {
                    this.output = value; 
                }
            }
        }

        protected List<PortLinker> input;
        protected List<PortLinker> output;

        public int MaxInputs { get; set; }
        public int MaxOutputs { get; set; }
        public int MaxInOuts { get; set; }

        public string ComponentId { get { return this.id; } }
        public Point Position { get; set; }

        protected string id;
        protected Image disignImage;
        protected Image schemaImage;

        public ElectricComponentOld(PortLinker vcc, PortLinker gnd)
        { this.initialize(vcc, gnd); }

        public ElectricComponentOld(string VccId, string VccPort, string GndId, string GndPort)
        { this.initialize(VccId, VccPort, GndId, GndPort); }

        protected virtual void initialize(PortLinker vcc, PortLinker gnd)
        {
            string unique = Guid.NewGuid().ToString("N");
            string name = this.GetType().Name;
            this.id = name + unique;
            this.Input.Add(vcc);
            this.Output.Add(gnd);
        }

        protected virtual void initialize(string VccId, string VccPort,string GndId,string GndPort)
        { this.initialize(new PortLinker(VccId, VccPort), new PortLinker(GndId, GndPort)); }

        public virtual void Run()
        {
            int gndConn = 0;
            foreach (PortLinker iLink in this.Input)
            { if (iLink.isVcc) this.Vcc += iLink.Current; }

            foreach (PortLinker oLink in this.Output)
            { if (oLink.isGnd) gndConn++; }

            this.Gnd = this.Vcc / gndConn;

            foreach (PortLinker oLink in this.Output)
            { if (oLink.isGnd) oLink.Current = this.Gnd; }
        }

        public virtual void ShowDialog()
        {
            string str = String.Format("El Objeto '{0}' no tiene ventana de configuración.", this.GetType().Name);
            MessageBox.Show(str);
        }
    }
}
