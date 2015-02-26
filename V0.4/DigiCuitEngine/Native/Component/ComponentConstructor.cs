using Jint;
using Jint.Native;
using Jint.Native.Object;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCuitEngine.Native.Component
{
    public class ComponentConstructor : Interfaces.INode, IConstructor
    {
        private JsValue _location;
        private Jint.Engine _engine;
        private JsValue _isActive;

        public ComponentConstructor(Engine engine)
            : base(engine)
        { }

        public override JsValue Call(JsValue thisObject, JsValue[] arguments)
        {
            throw new NotImplementedException();
        }

        public Jint.Native.Object.ObjectInstance Construct(JsValue[] arguments)
        {
            throw new NotImplementedException();
        }

        public override System.Drawing.Point Location
        {
            get
            {
                return new Point(
                    Int32.Parse(_location.AsObject().Get("X").ToString()), 
                    Int32.Parse(_location.AsObject().Get("Y").ToString()));
            }
            set
            {
                ObjectInstance obj = new ObjectInstance(_engine);
                obj.Put("X", new JsValue(value.X.ToString()),true);
                obj.Put("Y", new JsValue(value.Y.ToString()), true);
                _location = new JsValue(obj);
            }
        }

        public override Interfaces.IPathCollection Paths { get; set; }

        public override bool IsActive
        {
            get { return _isActive.IsBoolean() && _isActive.AsBoolean(); }
            set { _isActive = new JsValue(value); }
        }

        public override string Id { get; private set; }

        public override void Run()
        {
            
        }
    }
}
