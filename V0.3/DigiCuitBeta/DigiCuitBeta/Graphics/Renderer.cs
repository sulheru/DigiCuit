using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigiCuitBeta.Graphics
{
    public class Renderer
    {
        public delegate void RendererStatusChangeEventHandler(object sender, RendererStatusChangeEventArgs e);
        public class RendererStatusChangeEventArgs : EventArgs
        { 
            public bool IsRendering { get; private set; }
            public RendererStatusChangeEventArgs(bool isRendering) { this.IsRendering = isRendering; }
        }
        public event RendererStatusChangeEventHandler RendererStatusChange;

        private System.ComponentModel.BackgroundWorker _renderer; 

        public Electronics.Circuit Circuit { get; private set; }
        public Size GirdSize { get; set; }

        public Color BackgroundColor { set; get; }
        public Color GirdColor { set; get; }
        public bool IsGirdVisible { set; get; }

        public BufferedGraphics Canvas { get; private set; }
        public System.Windows.Forms.Control Layout { get; private set; }

        public bool IsRendering { get; private set; }

        public Renderer(System.Windows.Forms.Control Layout)
        {
            IsRendering = false;
            Circuit = new Electronics.Circuit();
            GirdSize = new Size(15, 15);
            this.Layout = Layout;

            BufferedGraphicsContext context = BufferedGraphicsManager.Current;
            context.MaximumBuffer = Layout.Size;
            Canvas = context.Allocate(Layout.CreateGraphics(), new Rectangle(new Point(0, 0), Layout.Size));
            
            BackgroundColor = Color.White;
            GirdColor = Color.FromArgb(240, 240, 240);
            _renderer = new System.ComponentModel.BackgroundWorker();
            _renderer.DoWork += new System.ComponentModel.DoWorkEventHandler(_renderer_DoWork);
            _renderer.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(_renderer_RunWorkerCompleted);
        }
        public void StartRendering() { IsRendering = true; _renderer.RunWorkerAsync(); _onRendererStatusChange(); }
        public void StopRendering() { IsRendering = false; }

        void _onRendererStatusChange()
        {
            if (RendererStatusChange != null)
            { RendererStatusChange(this, new RendererStatusChangeEventArgs(IsRendering)); }
        }
        void _renderer_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e) { _onRendererStatusChange(); }
        void _renderer_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (IsRendering)
            {                
                Canvas.Graphics.Clear(BackgroundColor);
                if (IsGirdVisible) { DrawGird(); }
                int len = Int32.Parse(_getLength().ToString());
                for (int i = 0; i < len; i++)
                {
                    string cmd = String.Format("circuit.Components[{0}].Rendering;", i.ToString());
                    string json = Circuit.Command(cmd);
                    Component comp = new Component(json, GirdSize);
                    if (comp.Available)
                        Canvas.Graphics.DrawImage(comp.ComponentImage, comp.AbsolutePosition);
                }
                Canvas.Render();
            }
        }

        private void DrawGird()
        {
            for (int v = 0; v < Layout.Width; v += GirdSize.Width)
            {
                Point a = new Point(v, 0);
                Point b = new Point(v, Layout.Height);
                Canvas.Graphics.DrawLine(new Pen(GirdColor), a, b);
            }
            for (int h = 0; h < Layout.Height; h += GirdSize.Width)
            {
                Point a = new Point(0, h);
                Point b = new Point(Layout.Width, h);
                Canvas.Graphics.DrawLine(new Pen(GirdColor), a, b);
            }
        }

        double _getLength()
        {
            Jint.Native.JsValue val = Circuit.Execute("circuit.Components.length");
            if (val.IsNumber())
            { return val.AsNumber(); }
            else { throw new NullReferenceException(); }
        }
    }
}
