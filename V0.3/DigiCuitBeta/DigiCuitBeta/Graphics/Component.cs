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
        public class Breadboard : Component
        {
            public Size BreadboardSize
            {
                set { base._breadboard = value; }
                get { return base._breadboard; }
            }
            public Breadboard(string json, Size GirdSize)
                : base(json, GirdSize) { }
        }

        public class Wire : Component
        {
            public Pen WirePen
            {
                set { base._wirePen = value; }
                get { return base._wirePen; }
            }
            public Point WireEndPoint
            {
                set { base._wireEndPoint = value; }
                get { return base._wireEndPoint; }
            }
            public Wire(string json, Size GirdSize)
                : base(json, GirdSize) { }
        }

        public Point Location { get; set; }
        public Point GraphShifting { get; protected set; }
        public Bitmap ComponentImage { get; protected set; }

        public Point AbsolutePosition
        {
            get
            {
                Point loc = new Point(Location.X * _girdSize.Width, Location.Y * _girdSize.Height);
                return new Point(loc.X - GraphShifting.X, loc.Y - GraphShifting.Y);
            }
        }
        public bool Available = false;

        protected Size _girdSize;
        protected bool _isBreadBoard = false;
        protected bool _isWire = false;
        protected Size _breadboard;
        protected Pen _wirePen;
        protected Point _wireEndPoint;

        public Component(string json, Size GirdSize)
        {
            _girdSize = GirdSize;
            Jint.Engine comp = new Jint.Engine();
            JsValue gra = comp.Execute(String.Format("var rendering={0};rendering;", json)).GetCompletionValue();
            if (gra.IsObject())
            {
                bool location= SetLocation(gra.AsObject().Get("Location"));
                bool graphshift = SetGraphShifting(gra.AsObject().Get("GraphicShifting"));
                SetIsWire(gra.AsObject().Get("isWire"));
                SetIsBreadBoard(gra.AsObject().Get("isBreadBoard"));
                bool setPic = false;
                if (_isWire) {setPic= SetWire(gra.AsObject().Get("Wire")); }
                else if (_isBreadBoard) { setPic = SetBreadboard(gra.AsObject().Get("Breadboard")); }
                else { setPic = SetComponentImage(gra.AsObject().Get("ComponentImage")); }
                Available = (location && graphshift && setPic);
            }
        }

        protected bool SetBreadboard(JsValue jsValue)
        {
            if (jsValue.IsObject())
            {
                JsValue jsw = jsValue.AsObject().Get("width");
                JsValue jsh = jsValue.AsObject().Get("height");

                if (jsw.IsNumber() && jsh.IsNumber())
                {
                    int w = Int32.Parse(jsw.ToString()) * _girdSize.Width + 2;
                    int h = Int32.Parse(jsh.ToString()) * _girdSize.Height + 2;
                    ComponentImage = new Bitmap(w, h);
                    System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(ComponentImage);
                    g.Clear(Color.White);
                    _breadboard = new Size(Int32.Parse(jsw.ToString()), Int32.Parse(jsh.ToString()));
                    for (int x = 0; x < w; x += 15)
                    {
                        for (int y = 0; y < h; y += 15)
                        {
                            Rectangle r = new Rectangle(x + 5, y + 5, 5, 5);
                            g.DrawEllipse(new Pen(Color.Black), r);
                        }
                    }
                    g.DrawRectangle(new Pen(Color.Gray), 0, 0, w - 1, h - 1);
                    return true;
                }
            }
            return false;
        }

        protected bool SetWire(JsValue loc)
        {
            if (loc.IsObject())
            {
                JsValue xVal = loc.AsObject().Get("X");
                JsValue yVal = loc.AsObject().Get("Y");
                Pen pen = GetWireBrush(loc.AsObject().Get("Brush"));
                if (xVal.IsNumber() && yVal.IsNumber())
                {
                    double x = xVal.AsNumber();
                    double y = yVal.AsNumber();
                    Point pnt = new Point(Int32.Parse(x.ToString()) * _girdSize.Width, Int32.Parse(y.ToString()) * _girdSize.Height);
                    Size size = new Size(pnt.X - (Location.X * _girdSize.Width), pnt.Y - (Location.Y*_girdSize.Height));
                    ComponentImage = new Bitmap(size.Width, size.Height);
                    System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(ComponentImage);
                    g.Clear(Color.FromArgb(0, Color.Black));
                    g.DrawLine(pen, Location, pnt);
                    _wireEndPoint = pnt;
                    _wirePen = pen;
                    return true;
                }
            }
            return false;
        }

        protected Pen GetWireBrush(JsValue jsValue)
        {
            Color color = Color.Red;
            int width = 1;
            if (jsValue.IsObject())
            {
                JsValue jsColor = jsValue.AsObject().Get("color");
                JsValue jsWidth = jsValue.AsObject().Get("width");
                if (jsColor.IsObject())
                {
                    JsValue jsR = jsColor.AsObject().Get("R");
                    JsValue jsG = jsColor.AsObject().Get("G");
                    JsValue jsB = jsColor.AsObject().Get("B");
                    if (jsR.IsNumber() && jsG.IsNumber() && jsB.IsNumber())
                    { color = Color.FromArgb(Int32.Parse(jsR.AsNumber().ToString()), Int32.Parse(jsG.AsNumber().ToString()), Int32.Parse(jsB.AsNumber().ToString())); }
                }
                else if (jsColor.IsString())
                {
                    System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex("/^\\#([A-Fa-f\\d]{6,8})$/");
                    rex.IsMatch(jsColor.AsString());
                    System.Windows.Media.Color hex = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(jsColor.AsString());
                    color = Color.FromArgb(255, (int)hex.R, (int)hex.G, (int)hex.B);
                }
                if (jsWidth.IsNumber())
                { width = Int32.Parse(jsWidth.AsNumber().ToString()); }
            }
            return new Pen(color, width);
        }

        protected void SetIsWire(JsValue jsValue)
        { _isWire = (jsValue.IsBoolean() && jsValue.AsBoolean()); }

        protected void SetIsBreadBoard(JsValue jsValue)
        { _isBreadBoard = (jsValue.IsBoolean() && jsValue.AsBoolean()); }

        protected bool SetComponentImage(JsValue image)
        {
            if (image.IsString())
            {
                string file = image.AsString();
                System.IO.FileInfo fInfo = new System.IO.FileInfo(file);
                if (fInfo.Exists)
                {
                    ComponentImage = new Bitmap(fInfo.FullName);
                    return true;
                }
            }
            return false;
        }

        protected bool SetGraphShifting(JsValue shift)
        {
            GraphShifting = new Point(0, 0);
            if (shift.IsObject())
            {
                JsValue xVal = shift.AsObject().Get("X");
                JsValue yVal = shift.AsObject().Get("Y");
                if (xVal.IsNumber() && yVal.IsNumber())
                {
                    double x = xVal.AsNumber();
                    double y = yVal.AsNumber();
                    GraphShifting = new Point(Int32.Parse(x.ToString()), Int32.Parse(y.ToString()));
                    return true;
                }
            }
            return false;
        }

        protected bool SetLocation(JsValue loc)
        {
            Location = new Point(0, 0);
            if (loc.IsObject())
            {
                JsValue xVal = loc.AsObject().Get("X");
                JsValue yVal = loc.AsObject().Get("Y");
                if (xVal.IsNumber() && yVal.IsNumber())
                {
                    double x = xVal.AsNumber();
                    double y = yVal.AsNumber();
                    Location = new Point(Int32.Parse(x.ToString()), Int32.Parse(y.ToString()));
                    return true;
                }
            }
            return false;
        }
    }
}
