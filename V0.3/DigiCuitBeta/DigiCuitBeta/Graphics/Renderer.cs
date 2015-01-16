using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public System.Drawing.Graphics Canvas { get; private set; }
        public System.Windows.Forms.Control Layout { get; private set; }

        public bool IsRendering { set; get; }

        public Renderer(System.Windows.Forms.Control Layout)
        {
            this.Layout = Layout;
            this.Canvas = this.Layout.CreateGraphics();
            BackgroundColor = Color.White;
            GirdColor = Color.FromArgb(240, 240, 240);
            _renderer = new System.ComponentModel.BackgroundWorker();
            _renderer.DoWork += new System.ComponentModel.DoWorkEventHandler(_renderer_DoWork);
            _renderer.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(_renderer_RunWorkerCompleted);
        }
        public void StartRendering() { IsRendering = true; _renderer.RunWorkerAsync(); _onRendererStatusChange(); }

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
                Canvas.Clear(BackgroundColor);
                if (IsGirdVisible) { DrawGird(); }
                int len = Int32.Parse(_getLength().ToString());
                for (int i = 0; i < len; i++)
                {
                    string cmd = String.Format("Component[{0}].Rendering;");
                    string json = Circuit.Command(cmd);
                    Component comp = new Component(json,GirdSize);
                    Canvas.DrawImage(comp.ComponentImage, comp.AbsolutePosition);
                }
            }
        }

        private void DrawGird()
        {
            for (int v = 0; v < Layout.Width; v += GirdSize.Width)
            {
                Point a = new Point(v, 0);
                Point b = new Point(v, Layout.Height);
                Canvas.DrawLine(new Pen(GirdColor), a, b);
            }
            for (int h = 0; h < Layout.Height; h += GirdSize.Width)
            {
                Point a = new Point(0, h);
                Point b = new Point(Layout.Width, h);
                Canvas.DrawLine(new Pen(GirdColor), a, b);
            }
        }

        double _getLength()
        {
            Jint.Native.JsValue val = Circuit.Execute("Components.length");
            if (val.IsNumber())
            { return val.AsNumber(); }
            else { throw new NullReferenceException(); }
        }
    }
}
