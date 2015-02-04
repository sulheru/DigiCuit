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
                        ListViewItem lvi = new ListViewItem(DateTime.Now.ToString());
                        lvi.SubItems.Add(String.Format("Archivo Cargado: '{0}' ", (new FileInfo(file)).Name));
                        lvi.SubItems.Add((result == "undefined") ? "Loaded" : result);
                        listView1.Items.Insert(0, lvi);
                    }
                }
            }
        }

        private void toolStripComboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\n' || e.KeyChar == '\r')
            {
                e.Handled = true;
                string result = "";
                try { result = _circuit.Command(toolStripComboBox1.Text); }
                catch (Jint.Runtime.JavaScriptException ex) { result = ex.ToString(); }
                ListViewItem lvi = new ListViewItem(DateTime.Now.ToString());
                lvi.SubItems.Add(toolStripComboBox1.Text);
                lvi.SubItems.Add(result);
                listView1.Items.Insert(0, lvi);
                toolStripComboBox1.Items.Add(toolStripComboBox1.Text);
                toolStripComboBox1.Text = "";
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            _circuit = new DigiCuitBeta.Electronics.Circuit();
            listView1.Items.Clear();
        }
    }
}
