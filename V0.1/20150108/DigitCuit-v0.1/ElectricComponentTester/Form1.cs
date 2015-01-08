using Jint.Native;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectricComponentTester
{
    public partial class Form1 : Form
    {
        private Jint.Engine jint = new Jint.Engine();

        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Javascript File (*.js)|*.js";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.LoadFile(dlg.FileName, RichTextBoxStreamType.PlainText);
                this.jint = new Jint.Engine();
                this.jint.Execute(DigitCuit_v0._1.Properties.Resources.jsGlobals);
                this.jint.Execute(this.richTextBox1.Text);
                this.jint.Execute("var comp=new Component();");
            }
        }

        private void jsRefresh()
        {
            textBox1.Text = this.jint.Execute("comp.Name").GetCompletionValue().ToString();
            textBox2.Text = this.jint.Execute("comp.Description").GetCompletionValue().ToString();
            textBox3.Text = this.jint.Execute("comp.Parent").GetCompletionValue().ToString();
            int ioLength = Int32.Parse(this.jint.Execute("comp.InOut.length").GetCompletionValue().ToString());
            listView1.BeginUpdate();
            listView1.Items.Clear();
            for (int i = 0; i < ioLength; i++)
            {
                string Voltage = this.jint.Execute("comp.InOut[" + i.ToString() + "].voltage").GetCompletionValue().ToString();
                string Amperes = this.jint.Execute("comp.InOut[" + i.ToString() + "].amperes").GetCompletionValue().ToString();
                string Ohms = this.jint.Execute("comp.InOut[" + i.ToString() + "].ohms").GetCompletionValue().ToString();
                ListViewItem item = new ListViewItem();
                item.Text = "PortIndex " + (i + 1).ToString();
                item.SubItems.Add(Voltage);
                item.SubItems.Add(Amperes);
                item.SubItems.Add(Ohms);
                listView1.Items.Add(item);
            }
            listView1.EndUpdate();
        }
        private void jsCommand(string cmd)
        {
            string result = "";
            try { result= this.jint.Execute(cmd).GetCompletionValue().ToString(); }
            catch (Exception e) { MessageBox.Show(e.ToString()); }
            this.listBox1.Items.Add(result);            
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        { this.jsRefresh(); }

        private void toolStripButton3_Click(object sender, EventArgs e)
        { this.jsCommand("comp.Run();"); this.jsRefresh(); }

        private void button1_Click(object sender, EventArgs e)
        { this.jsCommand(textBox7.Text); }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string val1 = listView1.SelectedItems[0].SubItems[1].Text;
                string val2 = listView1.SelectedItems[0].SubItems[2].Text;
                string val3 = listView1.SelectedItems[0].SubItems[3].Text;

                textBox6.Value = Decimal.Parse(val1);
                textBox5.Value = Decimal.Parse(val2);
                textBox4.Value = Decimal.Parse(val3);
            }
        }

        private void lvItem_TextChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int i = this.listView1.SelectedItems[0].Index;
                this.jsCommand(
                    "comp.InOut[" + i.ToString() + "].voltage = \"" + textBox6.Value.ToString() + "\";" +
                    "comp.InOut[" + i.ToString() + "].amperes = \"" + textBox5.Value.ToString() + "\";" +
                    "comp.InOut[" + i.ToString() + "].ohms = " + textBox4.Value.ToString());

                listView1.Items[i].SubItems[1].Text = textBox6.Value.ToString();
                listView1.Items[i].SubItems[2].Text = textBox5.Value.ToString();
                listView1.Items[i].SubItems[3].Text = textBox4.Value.ToString();
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        { this.jsCommand("comp.reset()"); this.jsRefresh(); }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int i = this.listView1.SelectedItems[0].Index;
                this.jsCommand("comp.InOut[" + i.ToString() + "] = new DirectCurrent()"); this.jsRefresh();
            }
        }
    }
}
