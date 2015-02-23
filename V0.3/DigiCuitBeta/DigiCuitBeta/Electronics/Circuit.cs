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
        
        Jint.Engine _circuit;
        BackgroundWorker _Runner;
        public bool IsRunning
        {
            get
            {
                this.IsRunning = _Runner.IsBusy;
                Jint.Native.JsValue jsVal = _circuit.Execute("circuit.isRunning").GetCompletionValue();
                return (jsVal.IsBoolean() && jsVal.AsBoolean());
            }
            private set
            { _circuit.SetValue("circuit.isRunning", value); }
        }

        public long Timer
        {
            get
            {
                Jint.Native.JsValue jsVal = _circuit.Execute("circuit.timer").GetCompletionValue();
                return jsVal.IsNumber() ? Int64.Parse(jsVal.AsString()) : -1;
            }
            set
            { _circuit.SetValue("circuit.timer", value); }
        }

        public Circuit()
        {
            _circuit = new Jint.Engine();
            _circuit.Execute(DigiCuitBeta.Properties.Resources.Circuit);
            _circuit.Execute(DigiCuitBeta.Properties.Resources.ComponentPrototype);
            _Runner = new BackgroundWorker();
            _Runner.DoWork += new DoWorkEventHandler(_Runner_DoWork);
        }

        void _Runner_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            while (IsRunning)
            {
                this.Execute("circuit.run();");
                this.Timer = sw.ElapsedMilliseconds;
                sw.Restart();
            }
        }
        

        public string Command(string cmd) { return this.Execute(cmd).ToString(); }
        public Jint.Native.JsValue Execute(string cmd) { return _circuit.Execute(cmd).GetCompletionValue(); }

        public void Run() { this.IsRunning = true; _Runner.RunWorkerAsync(); }
        public void Stop() { this.IsRunning = false; }
    }
}
