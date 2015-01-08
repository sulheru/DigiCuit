using DigitCuit_v0._1.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigitCuit_v0._1
{
    public partial class CompList : Form
    {
        public Main mainParent;
        private Point StartLocation;

        public CompList()
        {
            InitializeComponent();
            this.Dock = DockStyle.Top;
            ComponentsViewer.BeginUpdate();
            this.LoadComponents();
            ComponentsViewer.EndUpdate();
        }

        private void CompList_ResizeEnd(object sender, EventArgs e)
        {
            int EndSize = this.Height;
            if (this.Location != this.StartLocation)
            { this.Dock = DockStyle.None; this.Dock = DockStyle.Top; this.Height = EndSize; }
        }

        private void izquierdaToolStripMenuItem_Click(object sender, EventArgs e)
        { mainParent.beamToolBar(this, Main.tbPanel.Left); }

        private void derechaToolStripMenuItem_Click(object sender, EventArgs e)
        { mainParent.beamToolBar(this, Main.tbPanel.Right); }

        private void CompList_ResizeBegin(object sender, EventArgs e)
        { this.StartLocation = this.Location; }

        private void CompList_Shown(object sender, EventArgs e)
        { this.Dock = DockStyle.None; this.Dock = DockStyle.Top; }

        private void LoadComponents()
        {
            // Loader 
            string ec="C:\\Users\\Theos\\Documents\\NetBeansProjects\\ElectricComponents\\public_html";
            string[] dirs = Directory.GetDirectories(ec);
            ComponentsViewer.BeginUpdate();
            foreach (string dir in dirs)
            {
                TreeNode cNode = new TreeNode();
                string[] mDir = dir.Split('\\');
                string cmFile = dir + "\\" + mDir[mDir.Length - 1] + ".js";
                ElectricComponent.ClassFile comp = new ElectricComponent.ClassFile(cmFile);
                cNode.Tag = comp;
                cNode.Text = comp.Name;
                int i = ComponentsViewer.Nodes.IndexOfKey(comp.Parent);
                if (i < 0) 
                { 
                    i = ComponentsViewer.Nodes.Add(comp.Parent, comp.Parent).Index;
                    ComponentsViewer.Nodes[i].ImageIndex = ComponentsViewer.Nodes[i].SelectedImageIndex = 1;
                }
                ComponentsViewer.Nodes[i].Nodes.Add(cNode);
            }
            ComponentsViewer.EndUpdate();
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            if (this.ComponentsViewer.SelectedNode.Tag != null && this.ComponentsViewer.SelectedNode.Tag.GetType() == typeof(ElectricComponent.ClassFile))
            { this.mainParent.cBar.newComponent(((ElectricComponent.ClassFile)this.ComponentsViewer.SelectedNode.Tag).CreatNewObject()); }
        }
    }
}
