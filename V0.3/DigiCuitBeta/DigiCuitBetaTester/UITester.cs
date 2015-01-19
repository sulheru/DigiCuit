using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigiCuitBetaTester
{
    public partial class UITester : Form
    {
        public DigiCuitBeta.Graphics.Renderer Renderer;
        private ArrayList _log = new ArrayList();
        private int _lIndex = 0;

        public UITester()
        {
            InitializeComponent();
            Renderer = new DigiCuitBeta.Graphics.Renderer(Canvas);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (Renderer.IsRendering) { Renderer.StopRendering(); }
            else { Renderer.StartRendering(); }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\n' || e.KeyChar == '\r')
            {
                string result = "";
                try { result = Renderer.Circuit.Command(textBox1.Text); e.Handled = true; }
                catch (Exception ex) { result = ex.ToString(); }
                Console.WriteLine(result);
                _log.Add(textBox1.Text);
                textBox1.Text = "";
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down && _lIndex < _log.Count - 1)
            {
                _lIndex++;
                textBox1.Text = _log[_lIndex].ToString();
            }
            else if (e.KeyCode == Keys.Up && _lIndex > 0)
            {
                _lIndex--;
                textBox1.Text = _log[_lIndex].ToString();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Archivos de Javascript (*.js)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                foreach (string jsFile in dlg.FileNames)
                {
                    string js = File.ReadAllText(jsFile);
                    try { Renderer.Circuit.Command(js); }
                    catch (Exception ex) { Console.WriteLine(ex.ToString()); }                    
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
