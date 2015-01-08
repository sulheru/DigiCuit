using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitCuit_v0._1.Components
{
    public class PortLinker
    {
        public DirectCurrent Current { set; get; }
        public string ComponentId { set; get; }
        public string ComponentPort { set; get; }
        public bool isVcc { get; set; }
        public bool isGnd { get; set; }

        public Point Position { get; set; }

        public PortLinker(string ComponentId, string ComponentPort)
        {
            this.ComponentId = ComponentId;
            this.ComponentPort = ComponentPort;            
        }
    }
}
