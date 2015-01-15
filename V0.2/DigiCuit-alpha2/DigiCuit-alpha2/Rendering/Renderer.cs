using DigiCuit_alpha2.ElectricComponents;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCuit_alpha2.Rendering
{
    public class Renderer
    {
        private System.ComponentModel.BackgroundWorker bck;

        public Circuit Circuit { set; get; }

        public Size GridSize { set; get; }
        // para uso futuro
        // public bool GridVisible { set; get; }

        public event EventHandler OnRenderingStart;
        public event EventHandler OnRenderingFinish;

        public System.Windows.Forms.Control ParentControl { get; private set; }
        public Point Position { set; get; }

        public bool IsRendering { get; private set; }

        public Color Backcolor { set; get; }

        public Renderer(Circuit circuit, System.Windows.Forms.Control parentControl)
        {
            this.Circuit = circuit;
            this.ParentControl = parentControl;
            this.GridSize = new Size(15, 15);
            this.Initialize();
        }

        public void Initialize()
        {
            bck = new System.ComponentModel.BackgroundWorker();
            bck.DoWork += new System.ComponentModel.DoWorkEventHandler(bck_DoWork);
            bck.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(bck_RunWorkerCompleted);            
        }

        void bck_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if(this.OnRenderingFinish!=null)
            { this.OnRenderingFinish(this, new EventArgs()); }
        }

        void bck_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Graphics graph = this.ParentControl.CreateGraphics();
            Rectangle Canvas = new Rectangle(this.Position, this.ParentControl.Size);
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            while (this.IsRendering)
            {
                sw.Reset();
                sw.Start();
                this.ParentControl.SuspendLayout();
                graph.Clear(this.Backcolor);
                foreach(Component comp in this.Circuit.Components)
                { DrawComponent(comp, Canvas, graph); }
                foreach (Linker link in this.Circuit.Links)
                {
                    DigiCuit_alpha2.Rendering.Graph.InOutPoint comp1 = (Graph.InOutPoint)this.Circuit.Components[link.InOut1.ComponentIndex].Sockets[link.InOut1.InOutIndex];
                    DigiCuit_alpha2.Rendering.Graph.InOutPoint comp2 = (Graph.InOutPoint)this.Circuit.Components[link.InOut2.ComponentIndex].Sockets[link.InOut2.InOutIndex];
                    Connector conn = new Connector((Point)comp1, (Point)comp2);
                }
                this.ParentControl.ResumeLayout();
                sw.Stop();
            }
        }

        void DrawComponent(Component comp, Rectangle Canvas, Graphics graph)
        {
            Graph g = new Graph(comp);
            if (g.IsInCanvasView(Canvas))
            {
                Graph.Vector v1 = (Graph.Vector)this.Position;
                Graph.Vector v2 = (Graph.Vector)g.Position;
                Graph.Vector v3 = v2 - v1;
                v3.X *= this.GridSize.Width;
                v3.Y *= this.GridSize.Height;
                graph.DrawImage((Image)g, (Point)v3);
                Component.Marker link = new Component.Marker();
                this.Circuit.PlugMatrix.Add(link);
            }
        }

        void DrawConnector(Connector conn, Rectangle Canvas, Graphics graph)
        {            
            if (conn.IsInCanvasView(Canvas))
            {
                Point[] pnts = { conn.Start, new Point(conn.Start.X, conn.End.Y), conn.End };
                graph.DrawLines(new Pen(conn.Color, conn.ConnectorWidth), pnts);
            }
        }

        public void RenderStart() {
            this.IsRendering = true;
            bck.RunWorkerAsync();
            if (this.OnRenderingStart != null)
                this.OnRenderingStart(this, new EventArgs());
        }

        public void RenderStop()
        { this.IsRendering = false; }
    }
}
