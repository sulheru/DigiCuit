using Jint.Native;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCuitBeta.Graphics
{
    public class Component
    {
        public enum ImageType { File, Wire, Node, Macro }
        private JsValue _component;

        public string ImageFile
        {
            get
            {
                JsValue cImage = _component.AsObject().Get("ComponentImage");
                if (cImage.IsString()) { return cImage.AsString(); }
                else { throw new Exception(DigiCuitBeta.Properties.Resources.PropertyIsNotString); }
            }
            set
            { _component.AsObject().Set("ComponentImage", new JsValue(value)); }
        }

        public Point Location
        {
            get
            {
                JsValue loc = _component.AsObject().Get("Location");
                if (loc.IsObject())
                {
                    JsValue xLoc = loc.AsObject().Get("X");
                    JsValue yLoc = loc.AsObject().Get("Y");

                    double x=0;
                    double y=0;
                    
                    if (xLoc.IsNumber()) { x = xLoc.AsNumber(); }
                    else { throw new Exception(DigiCuitBeta.Properties.Resources.PropertyIsNotNumber); }

                    if (yLoc.IsNumber()) { y = yLoc.AsNumber(); }
                    else { throw new Exception(DigiCuitBeta.Properties.Resources.PropertyIsNotNumber); }

                    return new Point(Int32.Parse(x.ToString()), Int32.Parse(y.ToString()));
                }
                else { throw new Exception(DigiCuitBeta.Properties.Resources.PropertyIsNotObject); }
            }
        }

        public Size Node
        {
            get
            {
                JsValue loc = _component.AsObject().Get("Node");
                if (loc.IsObject())
                {
                    JsValue xLoc = loc.AsObject().Get("width");
                    JsValue yLoc = loc.AsObject().Get("height");

                    double x = 0;
                    double y = 0;

                    if (xLoc.IsNumber()) { x = xLoc.AsNumber(); }
                    if (yLoc.IsNumber()) { y = yLoc.AsNumber(); }

                    return new Size(Int32.Parse(x.ToString()), Int32.Parse(y.ToString()));
                }
                else { throw new Exception(DigiCuitBeta.Properties.Resources.PropertyIsNotObject); }
            }
        }

        public Point WireEndLocation
        {
            get
            {
                JsValue loc = _component.AsObject().Get("Wire");
                if (loc.IsObject())
                {
                    JsValue xLoc = loc.AsObject().Get("X");
                    JsValue yLoc = loc.AsObject().Get("Y");

                    double x = 0;
                    double y = 0;

                    if (xLoc.IsNumber()) { x = xLoc.AsNumber(); }
                    else { throw new Exception(DigiCuitBeta.Properties.Resources.PropertyIsNotNumber); }

                    if (yLoc.IsNumber()) { y = yLoc.AsNumber(); }
                    else { throw new Exception(DigiCuitBeta.Properties.Resources.PropertyIsNotNumber); }

                    return new Point(Int32.Parse(x.ToString()), Int32.Parse(y.ToString()));
                }
                else { throw new Exception(DigiCuitBeta.Properties.Resources.PropertyIsNotObject); }
            }
        }

        public Pen WirePen
        {
            get
            {
                Color color = Color.Red;
                int width = 1;
                JsValue jsValue = _component.AsObject().Get("Wire");
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
                        else
                        { throw new Exception(DigiCuitBeta.Properties.Resources.PropertyIsNotColorObject); }
                    }
                    else if (jsColor.IsString())
                    {
                        System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex("/^\\#([A-Fa-f\\d]{6,8})$/");
                        rex.IsMatch(jsColor.AsString());
                        System.Windows.Media.Color hex = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(jsColor.AsString());
                        color = Color.FromArgb(255, (int)hex.R, (int)hex.G, (int)hex.B);
                    }
                    else
                    { throw new Exception(DigiCuitBeta.Properties.Resources.PropertyIsNotColorObject); }

                    if (jsWidth.IsNumber())
                    { width = Int32.Parse(jsWidth.AsNumber().ToString()); }
                    else
                    { throw new Exception(DigiCuitBeta.Properties.Resources.PropertyIsNotNumber); }
                }
                return new Pen(color, width);
            }
        }

        public ImageType Type
        {
            get
            {
                if (_component.AsObject().Get("isWire").IsBoolean() && _component.AsObject().Get("isWire").AsBoolean()) { return ImageType.Wire; }
                else if (_component.AsObject().Get("isNode").IsBoolean() && _component.AsObject().Get("isNode").AsBoolean()) { return ImageType.Node; }
                else if (_component.AsObject().Get("isDrawMacro").IsBoolean() && _component.AsObject().Get("isDrawMacro").AsBoolean()) { return ImageType.Macro; }
                else { return ImageType.File; }
            }
        }
        public Image Image
        {
            get
            {
                Image img = new Bitmap(1, 1);
                switch (this.Type)
                {
                    case ImageType.File: img = _loadImageFile(); break;
                    case ImageType.Macro: img = _loadMacro(); break;
                    case ImageType.Node: img = _loadNode(); break;
                    case ImageType.Wire: img = _loadWire(); break;
                    default: img = _loadImageFile(); break;
                }
                return img;
            }
        }

        private System.Drawing.Image _loadWire()
        {
            int width = WireEndLocation.X - Location.X;
            int height = WireEndLocation.Y - Location.Y;

            if(width==0 ^ height==0)
            {
                if (width == 0) { width = 1; }
                else if (height == 0) { height = 1; }
            }
            else if(width==0 && height==0)
            { throw new Exception(DigiCuitBeta.Properties.Resources.WireHasNoLength); }

            Rectangle rect = new Rectangle(0, 0, Math.Abs(width), Math.Abs(height));
            int x1 = 0, x2 = 0;
            int y1 = 0, y2 = 0;
            if (width < 0) { x1 = width; x2 = 0; }
            if (width > 0) { x1 = 0; x2 = width; }
            if (height < 0) { y1 = width; y2 = 0; }
            if (height > 0) { y1 = 0; y2 = width; }

            Point pt1 = new Point(x1, y1);
            Point pt2 = new Point(x2, y2);

            Bitmap bmp = new Bitmap(width, height);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp);

            g.DrawLine(this.WirePen, pt1, pt2);

            return bmp;
        }

        private System.Drawing.Image _loadNode()
        {
            throw new NotImplementedException();
        }

        private System.Drawing.Image _loadMacro()
        {
            throw new NotImplementedException();
        }

        private System.Drawing.Image _loadImageFile()
        {
            FileInfo file = new FileInfo(this.ImageFile);
            Bitmap bmp;
            if (file.Exists) { bmp = new Bitmap(file.FullName); }
            else { bmp = _noImage(); }
            return bmp;
        }

        private Bitmap _noImage()
        {
            Bitmap bmp = new Bitmap(50, 50);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp);
            g.DrawString("No Image", new Font("Arial", 8), System.Drawing.Brushes.Black, new RectangleF(0, 0, 49, 49));
            return bmp;
        }

        public Component(JsValue JsValue)
        { _component = JsValue; }
    }
}
