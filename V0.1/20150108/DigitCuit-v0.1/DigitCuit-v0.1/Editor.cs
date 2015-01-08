using DigitCuit_v0._1.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigitCuit_v0._1
{
    public partial class Editor : Form
    {
        private Cursor bStatus = Cursors.Default;
        private Graphics graph;
        public CompBar CircuitItems;

        public Editor()
        {
            InitializeComponent();
            graph = this.Canvas.CreateGraphics();
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Cursor.Current = this.bStatus;
                if (this.bStatus == Cursors.SizeNWSE)
                { Canvas.Width = e.X; Canvas.Height = e.Y; }
                else if (this.bStatus == Cursors.SizeNS)
                { Canvas.Height = e.Y; }
                else if (this.bStatus == Cursors.SizeWE)
                { Canvas.Width = e.X; }
            }
            else
            {
                if (e.Y > Canvas.Height - 10 && e.X > Canvas.Width - 20)
                { this.bStatus = Cursor.Current = Cursors.SizeNWSE; }
                else if (e.Y > Canvas.Height - 10)
                { this.bStatus = Cursor.Current = Cursors.SizeNS; }
                else if (e.X > Canvas.Width - 10)
                { this.bStatus = Cursor.Current = Cursors.SizeWE; }
                else
                { this.bStatus = Cursor.Current = Cursors.Default; }
            }
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            
        }
        public void DrawItems(ListView.ListViewItemCollection items)
        {
            this.DrawItems(items, true);
        }
        public void DrawItems(ListView.ListViewItemCollection items, bool stopRedraw)
        {
            if (stopRedraw)
            { this.Canvas.SuspendLayout(); }

            this.graph = Canvas.CreateGraphics();
            this.graph.Clear(Color.White);
            foreach (ListViewItem item in items)
            {
                if (item.Tag != null && item.Tag.GetType() == typeof(ElectricComponent))
                {
                    ElectricComponent comp = (ElectricComponent)item.Tag;
                    this.graph.DrawImage(comp.ComponentImage, comp.Position);
                }
            }
            if (stopRedraw)
            { this.Canvas.ResumeLayout(); }
        }

        private void Canvas_Resize(object sender, EventArgs e)
        {
            this.Canvas.SuspendLayout();
            Main mn = (Main)this.MdiParent;
            this.DrawItems(this.CircuitItems.CircuitItems);
            this.Canvas.ResumeLayout();
        }
    }
}
