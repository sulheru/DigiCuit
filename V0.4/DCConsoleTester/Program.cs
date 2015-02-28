using DigiCuitEngine.Native.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCConsoleTester
{
    class Program
    {
        static DigiCuitEngine.Engine _engine;
        static bool StillExecuting;
        static void Main(string[] args)
        {
            StillExecuting = true;
            _engine = new DigiCuitEngine.Engine();
            _engine.Window.EventRaise += new Jint.Native.Window.WindowEventHandler(Window_EventRaise);
            while (StillExecuting)
            {
                Jint.Native.JsValue res = "";
                try { res = _engine.Execute(Console.ReadLine()).GetCompletionValue(); }
                catch { res = _engine.Error.ToString(); }
                Console.WriteLine(res.ToString());
            }
        }

        static void Window_EventRaise(object sender, Jint.Native.Window.WindowEventArgs e)
        {
            if (e.arguments.Length > 0)
            {
                if (e.arguments[0].ToString() == "exit") { StillExecuting = false; e.Result = "Exiting..."; }
                if (e.arguments[0].ToString() == "run")
                {
                    if (e.arguments.Length > 1 && e.arguments[1].Is<ComponentInstance>())
                    {
                        e.arguments[1].As<ComponentInstance>().Run();
                        e.Result = "Done...";
                    }
                }
            }

            
        }
    }
}
