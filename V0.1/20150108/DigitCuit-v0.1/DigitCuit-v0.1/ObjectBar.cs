using DigitCuit_v0._1.Components;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigitCuit_v0._1
{   
    public partial class CompBar : Form
    {
        private DCFile cFile = null;
        private ElectricComponentOld sComp = null;
        public Main mainParent;
        public Editor mainEditor = new Editor();
        public ListView.ListViewItemCollection CircuitItems { get { return this.CircuitFile.Items; } }
        private Point StartLocation;

        public CompBar()
        {
            InitializeComponent();
            this.Dock = DockStyle.Top;
        }

        public void FileSave()
        {
            if (cFile == null || cFile.ToString().Trim().Length <= 0)
            { this.FileSaveAs(); }
            else
            { this.CircuitFile.saveFile(this.cFile.ToString()); }
        }

        public void FileSaveAs()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = Main.FILTER_DCFILE;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.cFile = new DCFile(dlg.FileName);
                this.CircuitFile.saveFile(this.cFile.ToString());
            }
        }

        public void FileLoad(string Filename)
        {
            this.cFile = new DCFile(Filename);
            if (this.cFile.Extension == "dcf")
            {
                if (!File.Exists(Filename))
                { this.CircuitFile.saveFile(Filename); }

                CircuitFile.Items.Clear();
                CircuitFile.loadFile(Filename);
                this.mainEditor = new Editor();
                this.mainEditor.CircuitItems = this;
                this.mainParent.beamToolBar(this.mainEditor, Main.tbPanel.Center);
                this.mainEditor.Show();
            }
        }
        public void FileOpen()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = Main.FILTER_DCFILE;
            if (dlg.ShowDialog() == DialogResult.OK)
            { this.FileLoad(dlg.FileName); }
        }

        private void LoadComponent(ElectricComponentOld comp)
        { this.sComp = comp; }

        private void izquierdaToolStripMenuItem_Click(object sender, EventArgs e)
        { mainParent.beamToolBar(this, Main.tbPanel.Left); }

        private void derechaToolStripMenuItem_Click(object sender, EventArgs e)
        { mainParent.beamToolBar(this, Main.tbPanel.Right); }

        private void CompBar_ResizeEnd(object sender, EventArgs e)
        {
            int EndSize = this.Height;
            if (this.Location != this.StartLocation)
            { this.Dock = DockStyle.None; this.Dock = DockStyle.Top; this.Height = EndSize; }
        }

        private void CompBar_ResizeBegin(object sender, EventArgs e)
        { this.StartLocation = this.Location; }

        private void CompBar_Shown(object sender, EventArgs e)
        { this.Dock = DockStyle.None; this.Dock = DockStyle.Top; }

        public void newComponent(ElectricComponent dcComp)
        { 
            this.CircuitFile.Items.Add(dcComp.ComponentId, dcComp.Name, 0).Tag = dcComp;
            this.mainEditor.DrawItems(this.CircuitFile.Items);
        }
    }
}
