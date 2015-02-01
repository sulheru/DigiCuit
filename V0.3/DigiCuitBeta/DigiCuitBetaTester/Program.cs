using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigiCuitBetaTester
{
    class Program
    {
        static DigiCuitBeta.Electronics.Circuit js;
        static bool quit = false;

        [STAThreadAttribute]
        static void Main(string[] args)
        {
            LoadJs();
            string result = "";
            while (!quit)
            {
                result = Console.ReadLine();
                Terminal(ref result);
                Console.WriteLine(result);
            }
        }

        private static void LoadJs()
        {
            js = new DigiCuitBeta.Electronics.Circuit();
            js.DoNotLogExecute(DigiCuitBetaTester.Properties.Resources.TesterEvents);
            js.ConsoleStartListening();
            js.RaiseEvent += new EventHandler<DigiCuitBeta.Electronics.Circuit.ConsoleEventArgs>(js_RaiseEvent);
        }

        static void js_RaiseEvent(object sender, DigiCuitBeta.Electronics.Circuit.ConsoleEventArgs e)
        {
            switch (e.Type)
            {
                case "openfile": e.Result = "'" + OpenFileDialog(e.Prompt) + "'"; break;
                default:
                    e.Result = "'" + MessageBox.Show(e.Prompt, "console message", MessageBoxButtons.YesNoCancel).ToString() + "'";
                    break;
            }
        }

        private static string OpenFileDialog(string p)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = p;
            if (dlg.ShowDialog() == DialogResult.OK)
            { js.DoNotLogExecute(File.ReadAllText(dlg.FileName)); return true.ToString(); }
            return false.ToString();
        }   

        static void Terminal(ref string result)
        { result = cmdHandler(result); }

        static string cmdHandler(string cmd)
        {
            try { return js.Command(cmd); }
            catch (Exception e) { return e.ToString(); }
        }
    }
}