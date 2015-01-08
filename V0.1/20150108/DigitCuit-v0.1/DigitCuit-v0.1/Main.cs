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
    public partial class Main : Form
    {
        public const string FILTER_PROJECT = "Archivo de projecto electrónico|*.dcproj";
        public const string FILTER_DCFILE = "Archivo de circuito electrónico|*.dcf";

        public enum tbPanel { Left, Right, Center, Bottom };
        
        public CompBar cBar = new CompBar();
        public CompList cList = new CompList();
        public ProjectView pView = new ProjectView();

        private SaveFileDialog prjFile;

        public Main()
        {
            InitializeComponent();

            cBar.MdiParent = cBar.mainParent = this;
            cList.MdiParent = cList.mainParent = this;
            pView.MdiParent = pView.mainParent = this;

            cBar.Show();
            cList.Show();
            pView.Show();

            this.lToolBar.Controls.Add(cBar);
            this.rToolBar.Controls.Add(cList);
            this.rToolBar.Controls.Add(pView);
        }

        public void beamToolBar(Form toolBar, tbPanel container)
        {
            toolBar.MdiParent = this;
            switch (container)
            {
                case tbPanel.Bottom: bToolBar.Controls.Add(toolBar); break;
                case tbPanel.Center: cToolBar.Controls.Add(toolBar); break;
                case tbPanel.Left: lToolBar.Controls.Add(toolBar); break;
                case tbPanel.Right: rToolBar.Controls.Add(toolBar); break;
            }
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = Main.FILTER_PROJECT;
            if (dlg.ShowDialog() == DialogResult.OK)
            { pView.LoadFile(dlg.FileName); }
        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            prjFile = new SaveFileDialog();
            prjFile.Filter = Main.FILTER_PROJECT;
            if (prjFile.ShowDialog() == DialogResult.OK)
            { pView.SaveFile(prjFile.FileName); }
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.prjFile == null)
            { this.guardarComoToolStripMenuItem_Click(sender, e); }
            else
            { this.pView.SaveFile(this.prjFile.FileName); }
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        { pView.NewProject(); }
    }
}