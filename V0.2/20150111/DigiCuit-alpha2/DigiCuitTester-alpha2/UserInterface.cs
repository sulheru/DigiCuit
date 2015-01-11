using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigiCuitTester_alpha2
{
    public partial class UserInterface : Form
    {
        DigiCuit_alpha2.ElectricComponents.Component comp = null;
        DigiCuit_alpha2.ElectricComponents.DirectCurrent SelectedDcv = null;
        DigiCuit_alpha2.ElectricComponents.Circuit circuit = new DigiCuit_alpha2.ElectricComponents.Circuit();

        int SelectedDcvIndex = -1;
        bool isPlaying = false;

        string FrameWorkFile; string ClassFile;
        public const string JS_FILE = "Archivo Javascript (*.js)|*.js";

        public UserInterface()
        {
            InitializeComponent();
            LoadJsGlobals();
        }

        private void LoadJsGlobals()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = JS_FILE;
            if (dlg.ShowDialog() != DialogResult.OK)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            else
            { txtJsGlobals.LoadFile((this.FrameWorkFile = dlg.FileName), RichTextBoxStreamType.PlainText); }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = JS_FILE;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtCompJavascript.LoadFile((this.ClassFile = dlg.FileName), RichTextBoxStreamType.PlainText);
                this.comp = new DigiCuit_alpha2.ElectricComponents.Component(this.FrameWorkFile, this.ClassFile);
                this.SelectedDcv = new DigiCuit_alpha2.ElectricComponents.DirectCurrent(this.comp.jsGlobals);
                InOutView.Items.Clear();
                InOutView.Items.AddRange(this.comp.InOut.ToArray());
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = this.SelectedDcvIndex = InOutView.SelectedIndex;
            if (i >= 0)
            {
                string json = InOutView.Items[i].ToString();
                this.SelectedDcv.CreatFromJSON(json);
                this.VoltageControl.Value = Decimal.Parse(this.SelectedDcv.Voltage.ToString());
                this.AmperesControl.Value = Decimal.Parse(this.SelectedDcv.Amperes.ToString());
                this.OhmsControl.Value = Decimal.Parse(this.SelectedDcv.Ohms.ToString());
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.SelectedDcv.Voltage = Double.Parse(VoltageControl.Value.ToString());
            if (checkBox1.Checked)
            {
                this.AmperesControl.Value = Decimal.Parse(this.SelectedDcv.Amperes.ToString());
                this.OhmsControl.Value = Decimal.Parse(this.SelectedDcv.Ohms.ToString());
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            this.SelectedDcv.Amperes = Double.Parse(AmperesControl.Value.ToString());
            if (checkBox1.Checked)
            {
                this.VoltageControl.Value = Decimal.Parse(this.SelectedDcv.Voltage.ToString());
                this.OhmsControl.Value = Decimal.Parse(this.SelectedDcv.Ohms.ToString());
            }
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            this.SelectedDcv.Ohms = Double.Parse(OhmsControl.Value.ToString());
            if (checkBox1.Checked)
            {
                this.VoltageControl.Value = Decimal.Parse(this.SelectedDcv.Voltage.ToString());
                this.AmperesControl.Value = Decimal.Parse(this.SelectedDcv.Amperes.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.SelectedDcv.CalculateVoltage(Double.Parse(AmperesControl.Value.ToString()), Double.Parse(OhmsControl.Value.ToString()));
            this.VoltageControl.Value = Decimal.Parse(this.SelectedDcv.Voltage.ToString());
            this.AmperesControl.Value = Decimal.Parse(this.SelectedDcv.Amperes.ToString());
            this.OhmsControl.Value = Decimal.Parse(this.SelectedDcv.Ohms.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.InOutView.Items[this.SelectedDcvIndex] = this.SelectedDcv;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        { this.LoadJsGlobals(); }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            timer1.Enabled = this.isPlaying = !this.isPlaying;
            if (this.isPlaying)
            {
                BackgroundWorker bwrk = new BackgroundWorker();
                bwrk.DoWork += new DoWorkEventHandler(bwrk_DoWork);
                bwrk.RunWorkerAsync();
            }
        }

        private void bwrk_DoWork(object sender, DoWorkEventArgs e)
        {
            while (this.isPlaying)
            {
                Thread.Sleep(1000);
                Console.WriteLine(this.comp.Run());
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            object[] json = InOutView.Items.Cast<object>().ToArray();
            this.comp.InOut.Clear();
            this.comp.InOut.AddRange(json);

            InOutView.Items.Clear();
            InOutView.Items.AddRange(this.comp.InOut.ToArray());
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                Console.WriteLine(this.comp.Command(comboBox1.Text));
                comboBox1.Items.Add(comboBox1.Text);
                comboBox1.Text = "";
            }
        }

        private void UserInterface_FormClosing(object sender, FormClosingEventArgs e)
        { this.isPlaying = false; }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.VoltageControl.Value = Decimal.Parse("5,0");
            this.AmperesControl.Value = Decimal.Parse("0,2");
        }

        private void timer1_Tick(object sender, EventArgs e)
        { richTextBox2.Text = String.Join("\n", this.comp.InOut.ToArray()); }
        
        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            int i = cComponentList.SelectedIndex;
            if (i >= 0 && cComponentList.Items[i].GetType() == typeof(DigiCuit_alpha2.ElectricComponents.Component))
            {
                this.comp = (DigiCuit_alpha2.ElectricComponents.Component)cComponentList.Items[i];
                richTextBox1.Text = comp.JavaScript;
                this.SelectedDcv = new DigiCuit_alpha2.ElectricComponents.DirectCurrent(this.comp.jsGlobals);
                comboBox2.Items.Clear();
                comboBox2.Items.AddRange(this.comp.InOut.ToArray());
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = this.SelectedDcvIndex = comboBox2.SelectedIndex;
            if (i >= 0)
            {
                textBox1.Text = "InOutIndex: " + i.ToString();
                string json = comboBox2.Items[i].ToString();
                this.SelectedDcv.CreatFromJSON(json);
                this.cVoltageControl.Value = Decimal.Parse(this.SelectedDcv.Voltage.ToString());
                this.cAmperesControl.Value = Decimal.Parse(this.SelectedDcv.Amperes.ToString());
                this.cOhmsControl.Value = Decimal.Parse(this.SelectedDcv.Ohms.ToString());
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.cVoltageControl.Value = Decimal.Parse("5,0");
            this.cAmperesControl.Value = Decimal.Parse("0,2");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.cVoltageControl.Value = Decimal.Parse("0,0");
            this.cAmperesControl.Value = Decimal.Parse("0,0");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.SelectedDcv.CalculateVoltage(Double.Parse(cAmperesControl.Value.ToString()), Double.Parse(cOhmsControl.Value.ToString()));
            this.cVoltageControl.Value = Decimal.Parse(this.SelectedDcv.Voltage.ToString());
            this.cAmperesControl.Value = Decimal.Parse(this.SelectedDcv.Amperes.ToString());
            this.cOhmsControl.Value = Decimal.Parse(this.SelectedDcv.Ohms.ToString());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.comboBox2.Items[this.SelectedDcvIndex] = this.SelectedDcv;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            object[] json = comboBox2.Items.Cast<object>().ToArray();
            this.comp.InOut.Clear();
            this.comp.InOut.AddRange(json);

            comboBox2.Items.Clear();
            comboBox2.Items.AddRange(this.comp.InOut.ToArray());
        }

        private void cVoltageControl_ValueChanged(object sender, EventArgs e)
        {
            this.SelectedDcv.Voltage = Double.Parse(cVoltageControl.Value.ToString());
            if (checkBox2.Checked)
            {
                this.cAmperesControl.Value = Decimal.Parse(this.SelectedDcv.Amperes.ToString());
                this.cOhmsControl.Value = Decimal.Parse(this.SelectedDcv.Ohms.ToString());
            }
        }

        private void cAmperesControl_ValueChanged(object sender, EventArgs e)
        {
            this.SelectedDcv.Amperes = Double.Parse(cAmperesControl.Value.ToString());
            if (checkBox2.Checked)
            {
                this.cVoltageControl.Value = Decimal.Parse(this.SelectedDcv.Voltage.ToString());
                this.cOhmsControl.Value = Decimal.Parse(this.SelectedDcv.Ohms.ToString());
            }
        }

        private void cOhmsControl_ValueChanged(object sender, EventArgs e)
        {
            this.SelectedDcv.Ohms = Double.Parse(cOhmsControl.Value.ToString());
            if (checkBox2.Checked)
            {
                this.cVoltageControl.Value = Decimal.Parse(this.SelectedDcv.Voltage.ToString());
                this.cAmperesControl.Value = Decimal.Parse(this.SelectedDcv.Amperes.ToString());
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (this.comp!=null)
                richTextBox3.Text = String.Join("\n", this.comp.InOut.ToArray());
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                DigiCuit_alpha2.ElectricComponents.Component nComp
                    = new DigiCuit_alpha2.ElectricComponents.Component(this.FrameWorkFile, dlg.FileName);
                this.circuit.Components.Add(nComp);
                this.cComponentList.Items.Clear();
                this.comboBox3.Items.Clear();
                this.comboBox4.Items.Clear();
                object[] lComps = this.circuit.Components.Cast<object>().ToArray();
                this.cComponentList.Items.AddRange(lComps);
                this.comboBox3.Items.AddRange(lComps);
                this.comboBox4.Items.AddRange(lComps);
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            int i = cComponentList.SelectedIndex;
            if (i >= 0)
            {
                this.cComponentList.Items.RemoveAt(i);
                this.circuit.Components.Clear();
                object[] lComps = this.cComponentList.Items.Cast<object>().ToArray();
                this.circuit.Components.AddRange(lComps);

                this.comboBox3.Items.Clear();
                this.comboBox4.Items.Clear();
                this.comboBox3.Items.AddRange(lComps);
                this.comboBox4.Items.AddRange(lComps);
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            // Actualizar Circuito
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            toolStripButton8.Text = toolStripButton12.Text = this.isPlaying ? "4" : "<";
            timer2.Enabled = this.isPlaying = !this.isPlaying;
            if (this.isPlaying)
            { this.circuit.Run(true); }
            else
            { this.circuit.Stop(); }
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            this.circuit.Links.Add(new DigiCuit_alpha2.ElectricComponents.Linker());
            this.cLinkerList.Items.Clear();
            object[] lComps = this.circuit.Links.Cast<object>().ToArray();
            this.cLinkerList.Items.AddRange(lComps);
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            int i = this.cLinkerList.SelectedIndex;
            if (i >= 0)
            {
                this.cLinkerList.Items.RemoveAt(i);
                this.circuit.Links.Clear();
                object[] lComps = this.cLinkerList.Items.Cast<object>().ToArray();
                this.circuit.Links.AddRange(lComps);
            }
        }

        private void cLinkerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = cLinkerList.SelectedIndex;
            if (i >= 0)
            {
                DigiCuit_alpha2.ElectricComponents.Linker sLink = (DigiCuit_alpha2.ElectricComponents.Linker)cLinkerList.Items[i];
                comboBox3.SelectedIndex = sLink.InOut1.ComponentIndex;
                comboBox4.SelectedIndex = sLink.InOut2.ComponentIndex;

                numericUpDown1.Maximum = ((DigiCuit_alpha2.ElectricComponents.Component)comboBox3.Items[sLink.InOut1.ComponentIndex]).InOut.Count - 1;
                numericUpDown2.Maximum = ((DigiCuit_alpha2.ElectricComponents.Component)comboBox3.Items[sLink.InOut2.ComponentIndex]).InOut.Count - 1;

                decimal InOut1 = Decimal.Parse(sLink.InOut1.InOutIndex.ToString());
                decimal InOut2 = Decimal.Parse(sLink.InOut2.InOutIndex.ToString());

                numericUpDown1.Value = (InOut1 < numericUpDown1.Minimum) ? numericUpDown1.Minimum : InOut1;
                numericUpDown2.Value = (InOut2 < numericUpDown1.Minimum) ? numericUpDown2.Minimum : InOut2;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int i = cLinkerList.SelectedIndex;
            if (i >= 0)
            {
                int ioInd1 = Int32.Parse(numericUpDown1.Value.ToString());
                int ioInd2 = Int32.Parse(numericUpDown2.Value.ToString());

                DigiCuit_alpha2.ElectricComponents.Linker link = new DigiCuit_alpha2.ElectricComponents.Linker();

                link.InOut1.ComponentIndex = comboBox3.SelectedIndex;
                link.InOut2.ComponentIndex = comboBox4.SelectedIndex;
                link.InOut1.InOutIndex = ioInd1;
                link.InOut2.InOutIndex = ioInd2;

                cLinkerList.Items[i] = (object)link;

                this.circuit.Links.Clear();
                object[] lComps = this.cLinkerList.Items.Cast<DigiCuit_alpha2.ElectricComponents.Linker>().ToArray();
                this.circuit.Links.AddRange(lComps);
            }
        }

        private void toolStripComboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int i = cComponentList.SelectedIndex;
            if (i >= 0)
            {
                if (e.KeyChar == '\n' || e.KeyChar == '\r')
                {
                    e.Handled = true;
                    toolStripComboBox1.Items.Add(toolStripComboBox1.Text);
                    string result = ((DigiCuit_alpha2.ElectricComponents.Component)cComponentList.Items[i]).Command(toolStripComboBox1.Text);
                    Console.WriteLine(result);
                }
            }
        }
    }
}
