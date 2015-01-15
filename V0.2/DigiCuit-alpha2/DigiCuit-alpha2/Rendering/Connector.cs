using DigiCuit_alpha2.ElectricComponents;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCuit_alpha2.Rendering
{
    public class Connector
    {
        public Color Color { set; get; }
        public float ConnectorWidth { set; get; }
        public Point Start { set; get; }
        public Point End { set; get; }

        public Connector(Point Start, Point End)
        { this.Start = Start; this.End = End; }


        internal bool IsInCanvasView(Rectangle Canvas)
        { return Canvas.Contains(Start) || Canvas.Contains(End); }
    }
}
