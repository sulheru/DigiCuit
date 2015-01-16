using Jint.Native;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCuitBeta.Graphics
{
    public class Component
    {
        public Point Location { get; private set; }
        public Point GraphShifting { get; private set; }
        public Bitmap ComponentImage { get; private set; }

        private Size _girdSize;
        public Point AbsolutePosition
        {
            get
            {
                Point loc = new Point(Location.X * _girdSize.Width, Location.Y * _girdSize.Height);
                return new Point(loc.X - GraphShifting.X, loc.Y - GraphShifting.Y);
            }
        }

        public Component(string json, Size GirdSize)
        {
            _girdSize = GirdSize;
            Jint.Engine comp = new Jint.Engine();
            JsValue gra = comp.Execute(json).GetCompletionValue();
            if (gra.IsObject())
            {
                SetLocation(gra.AsObject().Get("Location"));
                SetGraphShifting(gra.AsObject().Get("GraphShifting"));
                SetComponentImage(gra.AsObject().Get("ComponentImage"));
            }
        }

        private void SetComponentImage(JsValue image)
        {
            if (image.IsString())
            {
                string file = image.AsString();
                System.IO.FileInfo fInfo = new System.IO.FileInfo(file);
                if (fInfo.Exists)
                { ComponentImage = new Bitmap(fInfo.FullName); }
            }
        }

        private void SetGraphShifting(JsValue shift)
        {
            if (shift.IsObject())
            {
                JsValue xVal = shift.AsObject().Get("X");
                JsValue yVal = shift.AsObject().Get("Y");
                if (xVal.IsNumber() && yVal.IsNumber())
                {
                    double x = xVal.AsNumber();
                    double y = yVal.AsNumber();
                    Location = new Point(Int32.Parse(x.ToString()), Int32.Parse(y.ToString()));
                }
            }
        }

        private void SetLocation(JsValue loc)
        {
            if (loc.IsObject())
            {
                JsValue xVal = loc.AsObject().Get("X");
                JsValue yVal = loc.AsObject().Get("Y");
                if (xVal.IsNumber() && yVal.IsNumber())
                {
                    double x = xVal.AsNumber();
                    double y = yVal.AsNumber();
                    Location = new Point(Int32.Parse(x.ToString()), Int32.Parse(y.ToString()));
                }
            }
        }
    }
}
