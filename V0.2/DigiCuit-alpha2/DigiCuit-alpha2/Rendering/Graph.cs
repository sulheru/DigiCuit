using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using DigiCuit_alpha2.ElectricComponents;

namespace DigiCuit_alpha2.Rendering
{
    public class Graph
    {
        public class Vector
        {
            public int X { set; get; }
            public int Y { set; get; }

            public Vector()
            { this.X = 0; this.Y = 0; }

            public Vector(int X, int Y)
            { this.X = X; this.Y = Y; }

            public static explicit operator Vector(Point obj)
            { return new Vector(obj.X, obj.Y); }

            public static explicit operator Point(Vector obj)
            { return new Point(obj.X, obj.Y); }

            public static explicit operator Vector(Size obj)
            { return new Vector(obj.Width, obj.Height); }

            public static explicit operator Size(Vector obj)
            { return new Size(obj.X, obj.Y); }

            public static Vector operator +(Vector v1, Vector v2)
            { return new Vector(v1.X + v2.X, v1.Y + v2.Y); }

            public static Vector operator -(Vector v1, Vector v2)
            { return new Vector(v1.X - v2.X, v1.Y - v2.Y); }
        }

        public class InOutPoint : Vector
        {
            public int InOutIndex { get; private set; }

            public InOutPoint(int InOutIndex)
            { this.InOutIndex = InOutIndex; }
            public InOutPoint(string json)
            {
                Jint.Engine js = new Jint.Engine();
                js.Execute(String.Format("var ioInd={0}", json));
                object index = js.Execute("ioInd.InOutIndex").GetCompletionValue().ToString();
                object x = js.Execute("ioInd.x").GetCompletionValue().ToString();
                object y = js.Execute("ioInd.y").GetCompletionValue().ToString();
                index = ElectricComponents.ComponentProperties.GetStringType((string)index);
                x = ElectricComponents.ComponentProperties.GetStringType((string)x);
                y = ElectricComponents.ComponentProperties.GetStringType((string)y);
                if (index.GetType() == typeof(int)) { this.InOutIndex = (int)index; }
                if (x.GetType() == typeof(int)) { this.X = (int)x; }
                if (y.GetType() == typeof(int)) { this.Y = (int)y; }
            }

            public InOutPoint(Dictionary<string, object> dictionary)
            {

                object ioIndex = dictionary["InOutIndex"];
                object pntX = dictionary["X"];
                object pntY = dictionary["Y"];

                if (ioIndex.GetType() == typeof(int) && pntX.GetType() == typeof(int) && pntX.GetType() == typeof(int))
                {
                    this.InOutIndex = (int)ioIndex;
                    this.X = (int)pntX;
                    this.Y = (int)pntY;
                }
                else
                { throw new TypeLoadException(); }
            }

            public static explicit operator InOutPoint(Component.Marker point)
            {
                InOutPoint mark = new InOutPoint(point.InOutIndex);
                mark.X = point.X;
                mark.Y = point.Y;
                return mark;
            }
        }

        public Image ComponentImage { get; set; }
        public Image ComponentSymbol { get; set; }
        public Point Position { get; private set; }
        public bool isSchema { get; set; }

        public Graph(ElectricComponents.Component comp)
        {
            string jsonGraphics = comp.Command("JSON.stringify(comp.Properties.Graphic)");
            System.IO.Directory.SetCurrentDirectory(comp.ClassFile.DirectoryName);
            object jsObj = ElectricComponents.ComponentProperties.GetStringType(jsonGraphics);
            if (jsObj.GetType() == typeof(Dictionary<string, object>))
            {
                Dictionary<string, object> csObj = (Dictionary<string, object>)jsObj;
                object obj = null;
                if (csObj.TryGetValue("ComponentImage", out obj)) { this.ComponentImage = (Image)obj; }
                if (csObj.TryGetValue("ComponentSymbol", out obj)) { this.ComponentSymbol = (Image)obj; }
            }
            this.Position = comp.Position;
        } 

        public bool IsInCanvasView(Point TopLeft, Size BottomRight)
        { return this.IsInCanvasView(new Rectangle(TopLeft, BottomRight)); }
        
        public bool IsInCanvasView(Rectangle rect)
        {
            Bitmap bmp = new Bitmap(this.isSchema ? this.ComponentSymbol : this.ComponentImage);
            Point pnt1 = this.Position;
            Point pnt2 = new Point(this.Position.X + bmp.Height, this.Position.Y + bmp.Width);
            Point pnt3 = new Point(this.Position.X, this.Position.Y + bmp.Width);
            Point pnt4 = new Point(this.Position.X + bmp.Height, this.Position.Y);

            return (rect.Contains(pnt1) || rect.Contains(pnt2) || rect.Contains(pnt3) || rect.Contains(pnt4));
        }

        public bool OnImage(int X, int Y)
        { return this.OnImage(new Point(X, Y)); }

        public bool OnImage(Point pnt)
        {
            Image image = this.isSchema ? this.ComponentSymbol : this.ComponentImage;
            Rectangle r = new Rectangle(this.Position, image.Size);
            bool notTransparent = (new Bitmap(image).GetPixel(pnt.X, pnt.Y).A > 0);
            return r.Contains(pnt) && notTransparent;
        }

        public static explicit operator Image(Graph g)
        { return g.isSchema ? g.ComponentSymbol : g.ComponentImage; }

    }
}
