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

namespace DigiCuitBetaTester
{
    public partial class TesterUI : Form
    {
        public const string JS_FILE = "Archivo Javascript (*.js)|*.js";

        DigiCuitBeta.Electronics.Circuit _circuit;

        public TesterUI()
        {
            InitializeComponent();
            _circuit = new DigiCuitBeta.Electronics.Circuit();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = JS_FILE;
            dlg.Multiselect = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in dlg.FileNames)
                {
                    if(File.Exists(file))
                    {
                        string result = "";
                        try { result = _circuit.Command(File.ReadAllText(file)); }
                        catch (Jint.Runtime.JavaScriptException ex) { result = ex.ToString(); }
                        ListViewItem lvi = new ListViewItem("");
                        lvi.SubItems.Add(String.Format("Archivo Cargado: '{0}' {1}", (new FileInfo(file)).Name, (result == "undefined") ? "Cargado" : result));
                        lvi.SubItems.Add(DateTime.Now.ToString());
                        listView1.Items.Insert(0, lvi);
                    }
                }
            }
        }

        private void toolStripComboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            sug.Visible = false;
            if (e.KeyChar == '\n' || e.KeyChar == '\r')
            {
                e.Handled = true;
                string result = "";
                try { result = _circuit.Command(toolStripComboBox1.Text); }
                catch (Jint.Runtime.JavaScriptException ex) { result = ex.ToString(); }
                ListViewItem lvi = new ListViewItem(toolStripComboBox1.Text);
                lvi.SubItems.Add(result);
                lvi.SubItems.Add(DateTime.Now.ToString());
                listView1.Items.Insert(0, lvi);
                toolStripComboBox1.Items.Remove(toolStripComboBox1.Text);
                toolStripComboBox1.Items.Insert(0, toolStripComboBox1.Text);
                toolStripComboBox1.Text = "";
            }          
        }

        void sug_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\n' || e.KeyChar == '\r')
            {
                e.Handled = true;
                sug_DoubleClick(sender, (EventArgs)e);
            }
        }

        void sug_DoubleClick(object sender, EventArgs e)
        {
            string sel = sug.SelectedItem.ToString();
            toolStripComboBox1.Text += sel;
            sug.Visible = false;
            toolStripComboBox1.Focus();
            toolStripComboBox1.SelectionStart = toolStripComboBox1.Text.Length;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            _circuit = new DigiCuitBeta.Electronics.Circuit();
            listView1.Items.Clear();
        }
    }
}
