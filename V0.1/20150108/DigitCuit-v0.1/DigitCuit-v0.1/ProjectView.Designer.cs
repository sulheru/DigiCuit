namespace DigitCuit_v0._1
{
    partial class ProjectView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectView));
            this.ProjectFile = new System.Data.DataSet();
            this.FileIconsSmall = new System.Windows.Forms.ImageList(this.components);
            this.CircuitMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.copiarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renombrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.izquierdaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.derechaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProjectViewer = new DigitCuit_v0._1.XmlTreeView();
            ((System.ComponentModel.ISupportInitialize)(this.ProjectFile)).BeginInit();
            this.CircuitMenuStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProjectFile
            // 
            this.ProjectFile.DataSetName = "NewDataSet";
            // 
            // FileIconsSmall
            // 
            this.FileIconsSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("FileIconsSmall.ImageStream")));
            this.FileIconsSmall.TransparentColor = System.Drawing.Color.Transparent;
            this.FileIconsSmall.Images.SetKeyName(0, "ProjectFile.ico");
            this.FileIconsSmall.Images.SetKeyName(1, "Folder.ico");
            this.FileIconsSmall.Images.SetKeyName(2, "dcFileIcon.ico");
            // 
            // CircuitMenuStrip
            // 
            this.CircuitMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirToolStripMenuItem,
            this.cerrarToolStripMenuItem,
            this.toolStripMenuItem1,
            this.copiarToolStripMenuItem,
            this.eliminarToolStripMenuItem,
            this.renombrarToolStripMenuItem});
            this.CircuitMenuStrip.Name = "CircuitMenuStrip";
            this.CircuitMenuStrip.Size = new System.Drawing.Size(165, 120);
            this.CircuitMenuStrip.Text = "Circuit";
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.abrirToolStripMenuItem.Text = "Abrir";
            // 
            // cerrarToolStripMenuItem
            // 
            this.cerrarToolStripMenuItem.Name = "cerrarToolStripMenuItem";
            this.cerrarToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.cerrarToolStripMenuItem.Text = "Cerrar";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(161, 6);
            // 
            // copiarToolStripMenuItem
            // 
            this.copiarToolStripMenuItem.Name = "copiarToolStripMenuItem";
            this.copiarToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.copiarToolStripMenuItem.Text = "Copiar";
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            // 
            // renombrarToolStripMenuItem
            // 
            this.renombrarToolStripMenuItem.Name = "renombrarToolStripMenuItem";
            this.renombrarToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.renombrarToolStripMenuItem.Text = "Cambiar nombre";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(284, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.izquierdaToolStripMenuItem,
            this.derechaToolStripMenuItem});
            this.toolStripDropDownButton1.Image = global::DigitCuit_v0._1.Properties.Resources.ScreenIconNone;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton1.Text = "Acoplamiento";
            // 
            // izquierdaToolStripMenuItem
            // 
            this.izquierdaToolStripMenuItem.Image = global::DigitCuit_v0._1.Properties.Resources.ScreenIconLeft;
            this.izquierdaToolStripMenuItem.Name = "izquierdaToolStripMenuItem";
            this.izquierdaToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.izquierdaToolStripMenuItem.Text = "Izquierda";
            this.izquierdaToolStripMenuItem.Click += new System.EventHandler(this.izquierdaToolStripMenuItem_Click);
            // 
            // derechaToolStripMenuItem
            // 
            this.derechaToolStripMenuItem.Image = global::DigitCuit_v0._1.Properties.Resources.ScreenIconRight;
            this.derechaToolStripMenuItem.Name = "derechaToolStripMenuItem";
            this.derechaToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.derechaToolStripMenuItem.Text = "Derecha";
            this.derechaToolStripMenuItem.Click += new System.EventHandler(this.derechaToolStripMenuItem_Click);
            // 
            // ProjectViewer
            // 
            this.ProjectViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProjectViewer.ImageIndex = 0;
            this.ProjectViewer.ImageList = this.FileIconsSmall;
            this.ProjectViewer.Location = new System.Drawing.Point(0, 25);
            this.ProjectViewer.Name = "ProjectViewer";
            this.ProjectViewer.SelectedImageIndex = 0;
            this.ProjectViewer.ShowLines = false;
            this.ProjectViewer.ShowPlusMinus = false;
            this.ProjectViewer.ShowRootLines = false;
            this.ProjectViewer.Size = new System.Drawing.Size(284, 272);
            this.ProjectViewer.TabIndex = 1;
            this.ProjectViewer.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.ProjectViewer_NodeMouseDoubleClick);
            // 
            // ProjectView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 297);
            this.Controls.Add(this.ProjectViewer);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ProjectView";
            this.Text = "Vista de Proyecto";
            this.Shown += new System.EventHandler(this.CompList_Shown);
            this.ResizeBegin += new System.EventHandler(this.CompList_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.CompList_ResizeEnd);
            ((System.ComponentModel.ISupportInitialize)(this.ProjectFile)).EndInit();
            this.CircuitMenuStrip.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private XmlTreeView ProjectViewer;
        private System.Data.DataSet ProjectFile;
        private System.Windows.Forms.ImageList FileIconsSmall;
        private System.Windows.Forms.ContextMenuStrip CircuitMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cerrarToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem copiarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renombrarToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem izquierdaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem derechaToolStripMenuItem;
    }
}