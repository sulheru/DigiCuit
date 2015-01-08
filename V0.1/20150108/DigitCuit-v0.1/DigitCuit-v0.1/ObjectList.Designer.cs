namespace DigitCuit_v0._1
{
    partial class CompList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CompList));
            this.ComponentsViewer = new System.Windows.Forms.TreeView();
            this.CompSymb = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.izquierdaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.derechaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ComponentsViewer
            // 
            this.ComponentsViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ComponentsViewer.ImageIndex = 0;
            this.ComponentsViewer.ImageList = this.CompSymb;
            this.ComponentsViewer.Location = new System.Drawing.Point(0, 25);
            this.ComponentsViewer.Name = "ComponentsViewer";
            this.ComponentsViewer.SelectedImageIndex = 0;
            this.ComponentsViewer.ShowLines = false;
            this.ComponentsViewer.ShowPlusMinus = false;
            this.ComponentsViewer.ShowRootLines = false;
            this.ComponentsViewer.Size = new System.Drawing.Size(284, 266);
            this.ComponentsViewer.TabIndex = 1;
            this.ComponentsViewer.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
            // 
            // CompSymb
            // 
            this.CompSymb.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("CompSymb.ImageStream")));
            this.CompSymb.TransparentColor = System.Drawing.Color.Transparent;
            this.CompSymb.Images.SetKeyName(0, "IntegratedComponent.ico");
            this.CompSymb.Images.SetKeyName(1, "Cajonera.ico");
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
            // CompList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 291);
            this.Controls.Add(this.ComponentsViewer);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "CompList";
            this.Text = "Lista de Componentes";
            this.Shown += new System.EventHandler(this.CompList_Shown);
            this.ResizeBegin += new System.EventHandler(this.CompList_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.CompList_ResizeEnd);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView ComponentsViewer;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem izquierdaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem derechaToolStripMenuItem;
        private System.Windows.Forms.ImageList CompSymb;
    }
}