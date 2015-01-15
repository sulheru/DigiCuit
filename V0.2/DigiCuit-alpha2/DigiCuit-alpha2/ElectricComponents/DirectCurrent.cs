using Jint;
using Jint.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCuit_alpha2.ElectricComponents
{
    public class DirectCurrent
    {
        public double Voltage { set; get; }
        public double Amperes { set; get; }
        public double Ohms
        {
            get
            {
                if (this.Amperes == 0 || this.Voltage == 0)
                { return 0; }
                else
                { return this.Voltage / this.Amperes; }
            }
            set
            {
                if (value == 0)
                { this.Amperes = 0; }
                else 
                { this.Amperes = this.Voltage / value; }
            }
        }
        public double Watts { get { return this.Voltage * this.Amperes; } }

        private string jsGlobals;

        public double CalculateVoltage(double amperes, double ohms)
        { return this.CalculateVoltage(amperes, ohms, true); }

        public double CalculateVoltage(double amperes, double ohms, bool setValues)
        {
            double voltage = amperes * ohms;
            if (setValues)
            {
                this.Voltage = voltage;
                this.Amperes = amperes;
            }
            return voltage;
        }

        public DirectCurrent(string jsGlobals)
        { 
            this.Amperes = this.Voltage = 0.0;
            this.jsGlobals = jsGlobals;
        }

        public static DirectCurrent CreatFromJSON(string jsGlobals,string JSON)
        {
            DirectCurrent dcv = new DirectCurrent(jsGlobals);
            dcv.CreatFromJSON(JSON);
            return dcv;
        }

        public string CreatFromJSON(string JSON)
        {
            Engine jsEngine = new Engine();
            JsValue jsResult = new JsValue();
            string result = "success";
            jsEngine.Execute(jsGlobals);

            try { jsEngine.Execute(String.Format("var dcv = {0};", JSON)); }
            catch (Exception e) { result = e.ToString(); }

            try { jsEngine.Execute("dcv.voltage;"); }
            catch (Exception e) { result = e.ToString(); }
            jsResult = jsEngine.GetCompletionValue();
            try { this.Voltage = Double.Parse(jsResult.ToString()); }
            catch (Exception e) { result = e.ToString(); }

            try { jsEngine.Execute("dcv.amperes;"); }
            catch (Exception e) { result = e.ToString(); }
            jsResult = jsEngine.GetCompletionValue();
            try { this.Amperes = Double.Parse(jsResult.ToString()); }
            catch (Exception e) { result = e.ToString(); }

            return result;
        }

        public override string ToString()
        {
            Engine jsEngine = new Engine();
            JsValue jsResult = new JsValue();

            jsEngine.Execute(jsGlobals);
            jsEngine.Execute("var dcv = new DirectCurrent();");
            jsEngine.Execute(String.Format("dcv.voltage = \"{0}\";", this.Voltage.ToString()));
            jsEngine.Execute(String.Format("dcv.amperes = \"{0}\";", this.Amperes.ToString()));
            jsEngine.Execute(String.Format("dcv.ohms = \"{0}\";", this.Ohms.ToString()));
            jsEngine.Execute("JSON.stringify(dcv);");
            jsResult = jsEngine.GetCompletionValue();

            return jsResult.ToString();
        }

        public static DirectCurrent operator +(DirectCurrent p1, DirectCurrent p2)
        {
            p1.Voltage += p2.Voltage;
            p1.Amperes += p2.Amperes;
            return p1;
        }

        public static bool operator <(DirectCurrent p1, DirectCurrent p2)
        { return p1.Voltage < p2.Voltage; }

        public static bool operator >(DirectCurrent p1, DirectCurrent p2)
        { return p1.Voltage > p2.Voltage; }
    }
}
