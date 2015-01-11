using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jint;
using  Jint.Native;
using System.IO;

namespace DigiCuit_alpha2.ElectricComponents
{
    public class Component
    {
        private Engine jsEngine = new Engine();

        public FileInfo ClassFile { get; private set; }
        public FileInfo FrameWorkFile { get; private set; }

        public string Name { get; private set; }
        public string Description { get; private set; }

        public string JavaScript { get; private set; }
        public string jsGlobals { get; private set; }

        public ComponentProperties Properties { get; set; }

        public Dictionary<LogDateTime, JsValue> Log { get; private set; }

        public DirectCurrentCollection InOut { set; get; }

        public Component(string FrameWorkFile, string ClassFile)
        {
            this.jsGlobals = File.ReadAllText(FrameWorkFile);
            this.JavaScript = File.ReadAllText(ClassFile);
            this.Log = new Dictionary<LogDateTime, JsValue>();
            this.ClassFile = new FileInfo(ClassFile);
            this.FrameWorkFile = new FileInfo(FrameWorkFile);
            this.Initialize();
            this.Name = this.Command("comp.Name");
            this.Description = this.Command("comp.Description");
        }

        public void Initialize()
        {
            this.Command(this.jsGlobals);
            this.Command(this.JavaScript);
            this.InOut = new DirectCurrentCollection(this);
            this.Command("var comp = new Component();");
        }

        public string Command(string cmd)
        {
            string result = "";
            try
            { jsEngine.Execute(cmd); }
            catch (Exception e) { }
            finally
            {
                this.Log.Add(new LogDateTime(), jsEngine.GetCompletionValue());
                result = jsEngine.GetCompletionValue().ToString();
            }
            return result;
        }

        public string Run()
        { return this.Command("comp.Run()"); }

        public class LogDateTime
        {
            public string Id { get; private set; }
            public DateTime DateTime { get; private set; }
            public LogDateTime()
            {
                this.Id = Guid.NewGuid().ToString("N");
                this.DateTime = DateTime.Now;
            }
        }

        public override string ToString()
        {
            string result = "{" + String.Format(" Name: {0}, Description: {1} ", this.Name, this.Description) + "}";
            return result;
        }
    }
}