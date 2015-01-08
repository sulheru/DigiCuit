namespace DigitCuit_v0._1
{
    partial class CompBar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CompBar));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.izquierdaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.derechaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CompSymbLarge = new System.Windows.Forms.ImageList(this.components);
            this.CompSymbSmall = new System.Windows.Forms.ImageList(this.components);
            this.CircuitFile = new DigitCuit_v0._1.XmlListView();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(214, 25);
            this.toolStrip1.TabIndex = 0;
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
            // CompSymbLarge
            // 
            this.CompSymbLarge.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("CompSymbLarge.ImageStream")));
            this.CompSymbLarge.TransparentColor = System.Drawing.Color.Transparent;
            this.CompSymbLarge.Images.SetKeyName(0, "IntegratedComponent.ico");
            // 
            // CompSymbSmall
            // 
            this.CompSymbSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("CompSymbSmall.ImageStream")));
            this.CompSymbSmall.TransparentColor = System.Drawing.Color.Transparent;
            this.CompSymbSmall.Images.SetKeyName(0, "IntegratedComponent.ico");
            // 
            // CircuitFile
            // 
            this.CircuitFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CircuitFile.LargeImageList = this.CompSymbLarge;
            this.CircuitFile.Location = new System.Drawing.Point(0, 25);
            this.CircuitFile.Name = "CircuitFile";
            this.CircuitFile.Size = new System.Drawing.Size(214, 254);
            this.CircuitFile.SmallImageList = this.CompSymbSmall;
            this.CircuitFile.TabIndex = 1;
            this.CircuitFile.UseCompatibleStateImageBehavior = false;
            this.CircuitFile.View = System.Windows.Forms.View.Tile;
            // 
            // CompBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(214, 279);
            this.Controls.Add(this.CircuitFile);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximumSize = new System.Drawing.Size(300, 10000);
            this.Name = "CompBar";
            this.Text = "Barra de Componentes";
            this.Shown += new System.EventHandler(this.CompBar_Shown);
            this.ResizeBegin += new System.EventHandler(this.CompBar_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.CompBar_ResizeEnd);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem izquierdaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem derechaToolStripMenuItem;
        private XmlListView CircuitFile;
        private System.Windows.Forms.ImageList CompSymbLarge;
        private System.Windows.Forms.ImageList CompSymbSmall;
    }
}