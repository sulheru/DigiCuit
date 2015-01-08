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
using System.Xml;

namespace DigitCuit_v0._1
{
    public partial class ProjectView : Form
    {

        public Main mainParent;
        private Point StartLocation;

        public ProjectView()
        {
            InitializeComponent();
            this.Dock = DockStyle.Top;
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

        public void LoadFile(string FileName)
        { this.ProjectViewer.loadFile(FileName); }

        public void SaveFile(string FileName)
        { this.ProjectViewer.saveFile(FileName); }

        public void NewProject() 
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = Main.FILTER_PROJECT;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if(true)  // código comprobación de cambios
                {
                    TreeNode NewCircuit = new TreeNode();
                    TreeNode Circuits = new TreeNode();
                    TreeNode Project = new TreeNode();

                    DCFile dcProj = new DCFile(dlg.FileName);
                    DCFile dcFile = new DCFile(dlg.FileName);

                    NewCircuit.Text = dcFile.FileName = "NewCircuit1.dcf";
                    Project.Text = dcProj.FileName;
                    Circuits.Text = "Circuitos";

                    dcFile.FilePath += "\\Circuitos";
                    NewCircuit.Tag = dcFile;
                    Project.Tag = dcProj;                    

                    Project.ImageIndex = 0;
                    Circuits.ImageIndex = 1;
                    NewCircuit.ImageIndex = 2;

                    Project.SelectedImageIndex = 0;
                    Circuits.SelectedImageIndex = 1;
                    NewCircuit.SelectedImageIndex = 2;

                    Circuits.Nodes.Add(NewCircuit);
                    Project.Nodes.Add(Circuits);

                    ProjectViewer.Nodes.Clear();
                    ProjectViewer.Nodes.Add(Project);
                }
            }
        }

        private void ProjectViewer_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (ProjectViewer.SelectedNode.Tag != null)
            {
                bool fNode = ProjectViewer.SelectedNode.Tag.GetType() == typeof(DCFile);
                bool sNode = ProjectViewer.SelectedNode.Tag.GetType() == typeof(string);
                if (sNode) { ProjectViewer.SelectedNode.Tag = new DCFile(ProjectViewer.SelectedNode.Tag.ToString()); }
                this.mainParent.cBar.FileLoad(ProjectViewer.SelectedNode.Tag.ToString());
            }
        }
    }
}
