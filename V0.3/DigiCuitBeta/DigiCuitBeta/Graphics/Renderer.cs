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
        
        public bool OnlyOnce { get; set; }
        public Jint.Native.JsValue Circuit { get; private set; }
        public Size GirdSize { get; set; }

        public Color BackgroundColor { set; get; }
        public Color GirdColor { set; get; }
        public bool IsGirdVisible { set; get; }

        public BufferedGraphics Canvas { get; private set; }
        public System.Windows.Forms.Control Layout { get; private set; }

        public bool IsRendering { get; private set; }

        public Renderer(System.Windows.Forms.Control Layout, Jint.Native.JsValue Circuit)
        {
            IsRendering = false;
            this.Circuit = Circuit;
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
                int len = Int32.Parse(Circuit.AsArray().Get("length").AsString());
                for (int i = 0; i < len; i++)
                {
                    Jint.Native.JsValue jsComp = Circuit.AsArray().Get(i.ToString());
                    Component comp = new Component(jsComp, GirdSize);
                    if (comp.Available)
                        Canvas.Graphics.DrawImage(comp.ComponentImage, comp.AbsolutePosition);
                }
                if (OnlyOnce)
                { IsRendering = false; break; }
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
    }
}
