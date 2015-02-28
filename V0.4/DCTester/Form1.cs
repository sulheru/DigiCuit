using DigiCuitEngine.Native.Component;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCTester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var engine = new DigiCuitEngine.Engine();
            engine.Execute("var comp = new Component()");
            ((Button)sender).Text = engine.Execute("Component").GetCompletionValue().ToString();
            var jsval = engine.Execute("comp");
            var comp = new ComponentConstructor(jsval);
            comp.Run();
        }
    }
}
