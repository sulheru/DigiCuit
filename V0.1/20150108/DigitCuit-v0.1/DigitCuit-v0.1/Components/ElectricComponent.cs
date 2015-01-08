using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigitCuit_v0._1.Components
{
    public class ElectricComponent
    {
        public class ClassFile : ElectricComponent
        {
            public string FileName { get; private set; }
            public ClassFile(string FileName)
                : base(ElectricComponent.FromFile(FileName).RawJS)
            { this.FileName = FileName; }

            public ElectricComponent CreatNewObject()
            { return ElectricComponent.FromFile(this.FileName); }

            public override string ToString()
            { return this.Description; }
        }

        private Jint.Engine jScript;
        private Dictionary<string,string> Log = new Dictionary<string,string>();

        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Parent { get; private set; }
        public string RawJS { get; private set; }

        public List<DirectCurrent> InOut = new List<DirectCurrent>();
        public List<string> Settings = new List<string>();


        public string ComponentId { get; private set; }
        public Point Position { get; set; }

        public Image ComponentImage;
        public Image ComponentSymbol;

        public static ElectricComponent FromFile(string FileName)
        {
            using (TextReader txt = File.OpenText(FileName))
            {
                ElectricComponent newEC = new ElectricComponent(txt.ReadToEnd());
                return newEC;
            }
        }

        public ElectricComponent(string Javascript)
        {
            this.jScript = new Jint.Engine();
            this.Initialize(Javascript);
        }

        private Image LoadBitmap(string FileName)
        {
            Image newImg = new Bitmap(50, 50);
            if (File.Exists(FileName))
            { newImg = new Bitmap(FileName); }
            else
            { this.jsCommand("FAILED: File '" + FileName + "' doesn´t exist"); }
            return newImg;
        }

        private void Initialize(string Javascript)
        {
            this.RawJS = Javascript;
            this.jsCommand(DigitCuit_v0._1.Properties.Resources.jsGlobals);
            this.jsCommand(Javascript);
            this.jsCommand("var comp=new Component();");
            this.Name = this.jsCommand("comp.Name");
            this.Description = this.jsCommand("comp.Description");
            this.Parent = this.jsCommand("comp.Parent");
            string ComponentImage = this.jsCommand("comp.ComponentImage");
            string ComponentSymbol = this.jsCommand("comp.ComponentSymbol");
            this.ComponentImage = this.LoadBitmap(ComponentImage);
            this.ComponentSymbol = this.LoadBitmap(ComponentSymbol);
            string unique = Guid.NewGuid().ToString("N");
            this.ComponentId = this.Name + unique;
            this.Position = new Point(0, 0);
            this.jsRefresh();
        }

        private void jsRefresh()
        {
            int ioLength = Int32.Parse(this.jsCommand("comp.InOut.length"));
            int stLength = Int32.Parse(this.jsCommand("comp.Settings.length"));
            this.InOut.Clear();
            for (int i = 0; i < ioLength; i++)
            {
                string Voltage = this.jsCommand("comp.InOut[" + i.ToString() + "].voltage");
                string Amperes = this.jsCommand("comp.InOut[" + i.ToString() + "].amperes");
                string Ohms = this.jsCommand("comp.InOut[" + i.ToString() + "].ohms");
                DirectCurrent item = new DirectCurrent();
                item.Index = i;
                item.Voltage = Double.Parse(Voltage);
                item.Amperes = Double.Parse(Amperes);
                item.Ohms = Double.Parse(Ohms);
                this.InOut.Add(item);
            }
            this.Settings.Clear();
            for (int i = 0; i < stLength; i++)
            { this.Settings.Add(this.jsCommand("comp.Settings[" + i.ToString() + "]")); }
        }

        private string jsCommand(string cmd)
        {
            string result = "";
            try { result = this.jScript.Execute(cmd).GetCompletionValue().ToString(); }
            catch (Exception e) { result = e.ToString(); }
            string tKey = DateTime.Now.ToString() + "(ID-" + Guid.NewGuid().ToString("N") + ")";
            this.Log.Add(tKey, result);
            return result;
        }

        public void Run()
        { this.jsCommand("comp.Run();"); this.jsRefresh(); }

        public void Reset()
        { this.jsCommand("comp.reset();"); this.jsRefresh(); }

        public override string ToString()
        { return this.jsCommand("comp.toString()"); }
    }
}
