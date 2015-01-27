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

        void _listener_DoWork(object sender, DoWorkEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public string Command(string cmd) { return _circuit.Execute(cmd).GetCompletionValue().ToString(); }
        public Jint.Native.JsValue Execute(string cmd) { return _circuit.Execute(cmd).GetCompletionValue(); }

        public void Run() { }
        public void Stop() { }
    }
}
