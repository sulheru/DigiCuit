using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DigiCuit_alpha2.ElectricComponents
{
    public class Circuit
    {
        private BackgroundWorker bck = new BackgroundWorker();
        private bool isRunning = false;
        public ComponentCollection startUpPosition;

        public ComponentCollection Components { set; get; }
        public LinkerCollection Links { set; get; }
        public List<Component.Marker> SocketsMatrix { get; private set; }
        public List<Component.Marker> PlugMatrix { get; private set; }
        public List<string> Log { get; private set; }
        public bool WriteToConsole { get; private set; }
        public bool AutoReset { set; get; }
        private bool IsRunning { get { return this.isRunning; } }

        /*
        private BackgroundWorker CompoRunner();
        private BackgroundWorker LinksRunner();
        private BackgroundWorker PlugsRunner();
        */

        public event RunWorkerCompletedEventHandler Finished;

        public Circuit()
        {
            this.Initialize();
        }

        public void Run()
        { this.isRunning = true; bck.RunWorkerAsync(); }

        public void Run(bool WriteToConsole)
        { this.WriteToConsole = WriteToConsole; this.Run(); }

        public void Stop()
        { this.isRunning = false; }

        private void Initialize()
        {
            bck.DoWork += new DoWorkEventHandler(bck_DoWork);
            bck.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bck_RunWorkerCompleted);
            this.Components = new ComponentCollection();
            this.Links = new LinkerCollection();
            this.Log = new List<string>();
            this.SocketsMatrix = new List<Component.Marker>();
            this.PlugMatrix = new List<Component.Marker>();
        }

        public void Reset() { this.Components = this.startUpPosition; }

        private void bck_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this.Finished != null)
                this.Finished(sender, e);
            if(this.AutoReset)
            { this.Reset(); }
        }

        private void bck_DoWork(object sender, DoWorkEventArgs e)
        {
            this.startUpPosition = this.Components;
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            this.RunComponents();
            while (this.isRunning)
            {
                sw.Reset();
                Thread.Sleep(20);
                sw.Start();
                this.RunLinks();
                this.RunPlugs();
                this.RunComponents();
                sw.Stop();
                string log = String.Format("{0} Milisegundos", sw.ElapsedMilliseconds);
                this.Log.Add(log);
                if(this.WriteToConsole)
                { Console.WriteLine(log); }
            }
        }

        private void RunComponents()
        {
            this.PlugMatrix.Clear();
            this.SocketsMatrix.Clear();
            foreach (Component comp in this.Components)
            {
                comp.Run();
                this.SocketsMatrix.AddRange(comp.Sockets.ToArray());
                this.PlugMatrix.AddRange(comp.Plugs.ToArray());
            }
        }

        private void RunLinks()
        {
            foreach (Linker link in this.Links)
            { link.Run(this.Components); }
        }

        private void RunPlugs()
        {
            foreach (Component.Marker cPlug in this.PlugMatrix)
            {
                foreach (Component.Marker cSock in this.SocketsMatrix)
                {
                    if (cPlug.X == cSock.X && cPlug.Y == cSock.Y)
                    {
                        Linker link = new Linker();
                        link.InOut1 = (Linker.Pointer)cPlug;
                        link.InOut2 = (Linker.Pointer)cSock;
                        link.Run(this.Components);
                    }
                }
            }
        }

    }
}
