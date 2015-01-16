using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCuitBeta.Electronics
{
    public class Circuit
    {
        public class GirdNode
        {
            public class Pointer
            {
                public int ComponentIndex { set; get; }
                public int InOutIndex { set; get; }
                public override string ToString()
                { return String.Format("{0}:{1}", this.ComponentIndex.ToString(), this.InOutIndex.ToString()); }
            }
            public Pointer Plug { get; set; }
            public Pointer Socket { get; set; }
        }
        
        public Size GirdSize { get; set; }
        public GirdNode[,] Nodes { get; set; }
        public ComponentCollection Components { set; get; }
    }
}
