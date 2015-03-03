using Jint;
using Jint.Native;
using Jint.Native.Object;
using Jint.Runtime;
using Jint.Runtime.Interop;
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
        private Jint.Engine jsval;

        #region "JsObject Methods"

        public static ComponentConstructor CreateComponentConstructor(Engine engine)
        {
            var obj = new ComponentConstructor(engine);

            obj.Extensible = true;

            obj.Prototype = engine.Function.PrototypeObject;
            obj.PrototypeObject = ComponentPrototype.CreateComponentPrototype(engine, obj);

            obj.FastAddProperty("length", 1, false, false, false);

            obj.FastAddProperty("prototype", obj.PrototypeObject, false, false, false);

            return obj;
        }

        public void Configure() { ComponentFastConfigure(); }

        public ComponentConstructor(DigiCuitEngine.Engine engine)
            : base(engine)
        { _engine = engine; }

        public ComponentConstructor(Jint.Engine engine)
            : base(engine)
        {
            // TODO: Complete member initialization
            this.jsval = engine;
        }

        public override JsValue Call(JsValue thisObject, JsValue[] arguments)
        { return Construct(arguments); }

        public Jint.Native.Object.ObjectInstance Construct(JsValue[] arguments)
        {
            var instance = new ComponentInstance(_engine);
            instance.Prototype = PrototypeObject;
            instance.Extensible = true;

            return instance;
        }

        public ComponentPrototype PrototypeObject { get; set; }

        #endregion

        private void ComponentFastConfigure()
        {
            this.FastAddProperty("location", _location, true, false, true);
            this.FastAddProperty("setLocation", new ClrFunctionInstance(_engine, setLocation), false, false, false);
            this.FastAddProperty("isActive", _isActive, true, false, true);
            this.FastAddProperty("run", new ClrFunctionInstance(_engine, _run), true, false, true);
        }

        private JsValue _run(JsValue thisObject, JsValue[] arguments)
        {
            // throw new NotImplementedException();
            return JsValue.True;
        }

        private JsValue setLocation(JsValue thisObject, JsValue[] arguments)
        {
            if (arguments.Length == 2)
            {
                int x = TypeConverter.ToInt32(arguments.At(0));
                int y = TypeConverter.ToInt32(arguments.At(1));
                Location = new Point(x, y);
                return JsValue.True;
            }
            return JsValue.False;
        }


        public override Point Location
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
                obj.Put("X", new JsValue(value.X.ToString()), true);
                obj.Put("Y", new JsValue(value.Y.ToString()), true);
                _location = new JsValue(obj);
            }
        }

        public override bool IsActive
        {
            get { return _isActive.IsBoolean() && _isActive.AsBoolean(); }
            set { _isActive = new JsValue(value); }
        }

        public override Interfaces.ISegmentCollection Paths { get; set; }

        public override string Id { get; protected set; }

        public override void Run()
        {
            var run = _engine.Invoke("run", this, new object[0]);
            var callable = run.TryCast<ICallable>();
            if (callable != null)
            {
                callable.Call(this, new JsValue[0]);
            }
        }
    }
}
