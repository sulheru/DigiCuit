using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitCuit_v0._1.Components.Basic
{
    public class LedLamp : ElectricComponentOld
    {
        public bool Red { get; set; }
        public bool Green { get; set; }
        public bool Blue { get; set; }

        private Bitmap baseImage;

        public LedLamp(PortLinker vcc, PortLinker gnd) : base(vcc, gnd) { }

        protected override void initialize(PortLinker vcc, PortLinker gnd)
        {
            base.initialize(vcc, gnd);
            this.baseImage = new Bitmap(DigitCuit_v0._1.Properties.Resources.LED);
            this.schemaImage = DigitCuit_v0._1.Properties.Resources.led1;
            this.Vcc.Ohms = 0.0;
            this.Vcc.Amperes = 0.0;
            this.Vcc.autoComplete();
            this.setLight(false);
        }

        public override void Run()
        {
            base.Run();
            this.setLight(this.Vcc.Voltage != 0.0);
        }

        private void setLight(bool isOn)
        {
            Bitmap bmp = new Bitmap(this.baseImage.Width, this.baseImage.Height);
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    if (Color.Black == this.baseImage.GetPixel(x, y))
                    { bmp.SetPixel(x, y, Color.FromArgb(0, Color.Black)); }
                    else
                    { bmp.SetPixel(x, y, this.lampState(isOn)); }
                }
            }   
        }
        private Color lampState(bool isOn)
        { return Color.FromArgb(this.Red ? (byte)255 : (byte)128, this.Green ? (byte)255 : (byte)128, this.Blue ? (byte)255 : (byte)128); }
    }
}
