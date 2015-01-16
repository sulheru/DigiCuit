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
        static DigiCuitBeta.Electronics.Circuit js = new DigiCuitBeta.Electronics.Circuit();

        [STAThread]
        static void Main(string[] args)
        {
            foreach (string file in args)
            { cmdHandler(File.ReadAllText(file)); }

            string result = "openjsfile";
            while (result.ToLower() != "quit")
            {
                switch (result)
                {
                    case "openjsfile": LoadJs(ref result); break;
                    case "msgalert": Alert(ref result); break;
                    default: Terminal(ref result); break;
                }
            }
        }

        private static void LoadJs(ref string result)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Archivos de Javascript (*.js)|*.js";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in dlg.FileNames)
                { cmdHandler(File.ReadAllText(file)); }
            }
            result = "";
        }

        private static void Alert(ref string result)
        {
            string msg = cmdHandler("this.message");
            System.Windows.Forms.MessageBox.Show(msg);
            result = "";
        }

        static void Terminal(ref string result)
        {            
            Console.WriteLine(result);
            string cmd = Console.ReadLine();
            result = cmdHandler(cmd);
        }

        static string cmdHandler(string cmd)
        {
            try { return js.Command(cmd); }
            catch (Exception e) { return e.ToString(); }
        }
    }
}
