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
    public class ComponentPrototype : ComponentInstance
    {

        public ComponentPrototype(Engine engine)
            : base(engine)
        { }

        public static ComponentPrototype CreateComponentPrototype(Engine engine, ComponentConstructor componentConstructor)
        {
            var obj = new ComponentPrototype(engine);

            obj.FastAddProperty("constructor", componentConstructor, true, false, true);

            return obj;
        }

        public void Configure()
        {
            FastAddProperty("toString", new ClrFunctionInstance(Engine, ToSting), true, false, true);
        }

        private Jint.Native.JsValue ToSting(Jint.Native.JsValue arg1, Jint.Native.JsValue[] arg2)
        {
            return "[object Component]";
        }
    }
}
