using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCuitBeta.Electronics
{
    public class Circuit
    {
        public class ConsoleEventArgs : EventArgs
        {
            public string Type { get; private set; }
            public string Prompt { get; private set; }
            public string Result { get { return _result; } set {
                _result = value;
                if (Returning != null)
                    Returning(this, new EventArgs());
            } }
            string _result;

            public event EventHandler Returning;

            public ConsoleEventArgs(string type,string prompt)
            { this.Type = type; this.Prompt = prompt; }
        }

        public event EventHandler<ConsoleEventArgs> RaiseEvent;

        Jint.Engine _circuit;
        BackgroundWorker _listener;
        public Circuit()
        {
            _circuit = new Jint.Engine();
            _circuit.Execute(DigiCuitBeta.Properties.Resources.Console);
            _circuit.Execute(DigiCuitBeta.Properties.Resources.Circuit);
            _listener = new BackgroundWorker();
            _listener.DoWork += new DoWorkEventHandler(_listener_DoWork);            
        }

        bool _isListening = false;
        public void ConsoleStartListening() { _isListening = true; _listener.RunWorkerAsync(); }
        public void ConsoleStopListening() { _isListening = false; }

        void _listener_DoWork(object sender, DoWorkEventArgs e)
        {
                Jint.Native.JsValue events = this.Execute("console.events.args");
                bool isRun;
                while (_isListening)
                {
                    if (_circuit.Execute("console.events.args.IsRunning").GetCompletionValue().IsBoolean())
                    {
                        while (!_circuit.Execute("console.events.args.IsRunning").GetCompletionValue().AsBoolean()) ;
                        events = _circuit.Execute("console.events.args").GetCompletionValue();
                        if (events.IsObject() && events.AsObject().Get("type").IsString() && events.AsObject().Get("prompt").IsString())
                        {
                            ConsoleEventArgs console = new ConsoleEventArgs(events.AsObject().Get("type").AsString(), events.AsObject().Get("prompt").AsString());
                            console.Returning += new EventHandler(console_Returning);
                            if (this.RaiseEvent != null)
                                this.RaiseEvent(this, console);
                        }
                    }
                }
        }

        void console_Returning(object sender, EventArgs e)
        {
            string result = ((ConsoleEventArgs)sender).Result;
            this.DoNotLogExecute(String.Format("console.events.args.result = {0}; console.events.args.IsRunning = false;", result));
        }

        public string Command(string cmd) { return this.Execute(cmd).ToString(); }
        public Jint.Native.JsValue Execute(string cmd) { return _circuit.Execute(String.Format("console.command(\"{0}\");", cmd)).GetCompletionValue(); }
        public Jint.Native.JsValue DoNotLogExecute(string cmd) { return _circuit.Execute(cmd).GetCompletionValue(); }

        public void Run() { Command("circuit.run();"); }
        public void Stop() { Command("circuit.isRunning = false;"); }
    }
}
