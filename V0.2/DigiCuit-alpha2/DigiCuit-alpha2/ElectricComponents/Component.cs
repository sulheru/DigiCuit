using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jint;
using  Jint.Native;
using System.IO;
using DigiCuit_alpha2.Rendering;

namespace DigiCuit_alpha2.ElectricComponents
{
    public class Component
    {
        public class Marker : Linker.Pointer
        {
            public int X { set; get; }
            public int Y { set; get; }

            public static explicit operator Marker(Graph.InOutPoint point)
            {
                Marker mark = new Marker();
                mark.X = point.X;
                mark.Y = point.Y;
                mark.InOutIndex = point.InOutIndex;
                return mark;
            }
        }

        private Engine jsEngine = new Engine();

        public FileInfo ClassFile { get; private set; }
        public FileInfo FrameWorkFile { get; private set; }
        public string ComponentVersion { get; private set; }
        public string LibraryVersion { get; private set; }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Index { get; private set; }

        public string JavaScript { get; private set; }
        public string jsGlobals { get; private set; }

        public ComponentProperties Properties { get; set; }
        public System.Drawing.Point Position { get; set; }

        public Dictionary<LogDateTime, JsValue> Log { get; private set; }

        public DirectCurrentCollection InOut { set; get; }

        public List<Marker> Plugs { get; set; }
        public List<Marker> Sockets { get; set; }

        public Component(string FrameWorkFile, string ClassFile, int Index)
        {
            this.jsGlobals = File.ReadAllText(FrameWorkFile);
            this.JavaScript = File.ReadAllText(ClassFile);
            this.Index = Index;
            this.Log = new Dictionary<LogDateTime, JsValue>();
            this.ClassFile = new FileInfo(ClassFile);
            this.FrameWorkFile = new FileInfo(FrameWorkFile);
            this.Initialize();
            this.Name = this.Command("comp.Name");
            this.Description = this.Command("comp.Description");
            this.ComponentVersion = this.Command("comp.Version");
            this.LibraryVersion = this.Command("this.Version");
        }

        public void Initialize()
        {
            this.Command(this.jsGlobals);
            this.Command(this.JavaScript);
            this.InOut = new DirectCurrentCollection(this);
            this.Properties = new ComponentProperties(this);
            string jsonGraphics = this.Command("JSON.stringify(comp.Properties.Graphic)");
            System.IO.Directory.SetCurrentDirectory(this.ClassFile.DirectoryName);
            object jsObj = ElectricComponents.ComponentProperties.GetStringType(jsonGraphics);
            if (jsObj.GetType() == typeof(Dictionary<string, object>))
            {
                Dictionary<string, object> csObj = (Dictionary<string, object>)jsObj;
                object obj = null;
                if (csObj.TryGetValue("Plugs", out obj)) { this.Plugs = this.GetFromDictionaryList(obj); }
                if (csObj.TryGetValue("Sockets", out obj)) { this.Sockets = this.GetFromDictionaryList(obj); }
            }
        }

        private List<Marker> GetFromDictionaryList(object obj)
        {
            List<Marker> list = new List<Marker>();

            if (obj.GetType() == typeof(List<object>))
            {
                foreach (object item in (List<object>)obj)
                {
                    if (item.GetType() == typeof(Dictionary<string, object>))
                    {
                        Marker pnt = (Marker)new Graph.InOutPoint((Dictionary<string, object>)item);
                        pnt.ComponentIndex = this.Index;
                        list.Add(pnt);
                    }
                }
            }
            return list;
        }

        public string Command(string cmd)
        {
            string result = "";
            try
            { jsEngine.Execute(cmd); }
            catch (Exception e)
            { Console.WriteLine("Error in {0}: {1} (Command: '{2}')", this.Name, e.ToString(), cmd); }
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