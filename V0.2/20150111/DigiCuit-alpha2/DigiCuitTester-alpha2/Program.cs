using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigiCuit_alpha2.ElectricComponents;
using System.Reflection;

namespace DigiCuitTester_alpha2
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            bool willExit = false;
            ShowMenu();
            while (!willExit)
            {
                Console.WriteLine();
                Console.Write("Elija su opción (H para ayuda): ");
                switch (Console.Read())
                {
                    case (int)'q': willExit = true; break;
                    case (int)'w': DirectCurrentTest(); break;
                    case (int)'e': jsElectricComponentTest(); break;
                    case (int)'r': StartGUI(); break;
                    case (int)'t': ClassLoader(); break;
                    case (int)'h': ShowMenu(); break;
                    default: Console.Read(); break;
                }
            }
        }

        static void ClassLoader()
        {
            Console.Read();
            string dlg = "";
            string classRef = dlg.GetType().ToString() +", " + dlg.GetType().Assembly;

            Type t = Type.GetType(classRef);
            object ndlg = null;
            ndlg = Activator.CreateInstance(t);
            
            if (ndlg != null)
                Console.WriteLine(ndlg.ToString());
            else
                Console.WriteLine("is null");

        }

        static void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Presione {0} para {1}", "Q", "Salir");
            Console.WriteLine("Presione {0} para {1}", "W", "'DirectCurrentTest()'");
            Console.WriteLine("Presione {0} para {1}", "E", "'ElectricComponentTest()'");
            Console.WriteLine("Presione {0} para {1}", "R", "'StartGUI()'");
                     
            Console.WriteLine("Presione {0} para {1}", "H", "Para mostrar este menú");
        }

        static void jsElectricComponentTest()
        {
            string result = "";
            Component comp=new Component("C:\\Users\\Theos\\Documents\\NetBeansProjects\\ElectricComponents\\public_html\\jsGlobals.js","C:\\Users\\Theos\\Documents\\NetBeansProjects\\ElectricComponents\\public_html\\IC7400\\IC7400.js");
            while (result != "exit")
            {
                result = comp.Command(Console.ReadLine());
                Console.WriteLine(result);
            }
        }

        static void DirectCurrentTest()
        {
            Console.WriteLine();
            string FullFileName = "C:\\Users\\Theos\\Documents\\NetBeansProjects\\ElectricComponents\\public_html\\jsGlobals.js";
            DirectCurrent dcv = new DirectCurrent(File.ReadAllText(FullFileName));
            dcv.Voltage = 5.0;
            dcv.Amperes = 0.2;
            string json = dcv.ToString();
            Console.WriteLine(json);
            dcv = new DirectCurrent(File.ReadAllText(FullFileName));
            string result= dcv.CreatFromJSON(json);
            Console.WriteLine(result);
            json = dcv.ToString();
            Console.WriteLine(json);

            Console.Read();
        }

        static void StartGUI()
        {
            UserInterface ui = new UserInterface();
            ui.ShowDialog();
        }
    }
}
