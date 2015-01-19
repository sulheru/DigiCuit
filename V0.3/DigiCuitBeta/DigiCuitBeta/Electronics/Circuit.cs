using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCuitBeta.Electronics
{
    public class Circuit
    {
        public delegate void SwitchEventHandler(object sender, SwitchEventArgs e);
        public class SwitchEventArgs : EventArgs
        {
            public bool IsRunning { get; private set; }
            public SwitchEventArgs(bool isRunning) { this.IsRunning = isRunning; }
        }

        public class GirdNode
        {
            public class Pointer
            {
                public enum ioType { Plug, Socket }
                public int ComponentIndex { set; get; }
                public int ConnectionIndex { set; get; }
                public ioType Type { set; get; }
                public override string ToString()
                { return String.Format("{0}{1},{2}", this.Type.ToString(), this.ComponentIndex.ToString(), this.ConnectionIndex.ToString()); }
                public string ToString(bool jsObject)
                {
                    if (jsObject) { return String.Format("[{0},{1},{2}]", this.ComponentIndex.ToString(), this.ConnectionIndex.ToString(), this.Type.ToString()); }
                    else { return String.Format("{0}{1},{2}", this.Type.ToString(), this.ComponentIndex.ToString(), this.ConnectionIndex.ToString()); }
                }
            }
            public Pointer Plug { get; set; }
            public Pointer Socket { get; set; }
        }

        private Jint.Engine _engine = new Jint.Engine();
        private System.ComponentModel.BackgroundWorker _runner = new System.ComponentModel.BackgroundWorker();

        public GirdNode[,] Nodes { get; set; }
        public bool IsRunning { get; private set; }

        public Jint.Native.Array.ArrayInstance Components
        { get { return _engine.Execute("this.Components").GetCompletionValue().AsArray(); } }

        public event SwitchEventHandler Switching;

        public Circuit()
        {
            _engine.Execute("var Components=[];");
            _runner.DoWork += new System.ComponentModel.DoWorkEventHandler(_runner_DoWork);
            _runner.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(_runner_RunWorkerCompleted);
        }

        public string Command(string cmd) { return _engine.Execute(cmd).GetCompletionValue().ToString(); }
        public Jint.Native.JsValue Execute(string cmd) { return _engine.Execute(cmd).GetCompletionValue(); }

        public void Run() { IsRunning = true; _runner.RunWorkerAsync(); _onSwitch(); }
        public void Stop() { IsRunning = false; }

        void _runConnections()
        {
            foreach (GirdNode node in Nodes)
            {                
                if (node.Plug != null && node.Socket != null)
                {
                    string plug = node.Plug.ToString(true);
                    string sock = node.Socket.ToString(true);
                    string cmd = String.Format("elecricExchange({0},{1});", plug, sock);
                    Command(cmd);
                }
            }
        }
        void _runner_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (IsRunning)
            {
                _runConnections();
                Command("Run();");
            }
        }
        void _runner_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e) { _onSwitch(); }
        void _onSwitch() { if (Switching != null) { Switching(this, new SwitchEventArgs(IsRunning)); } }
    }
}
