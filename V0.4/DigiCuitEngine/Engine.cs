using DigiCuitEngine.Native.Component;
using Jint.Runtime.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCuitEngine
{
    public class Engine : Jint.Engine
    {
        public Engine()
            : base()
        {
            Component = ComponentConstructor.CreateComponentConstructor(this);
            Component.Configure();
            Component.PrototypeObject.Configure();
            Global.FastAddProperty("Component", Component, true, false, true);
            Global.FastAddProperty("AddComponent", new ClrFunctionInstance(this, AddComponent), true, false, true);
        }

        private Jint.Native.JsValue AddComponent(Jint.Native.JsValue arg1, Jint.Native.JsValue[] arg2)
        {
            throw new NotImplementedException();
        }

        public ComponentConstructor Component { get; private set; }
    }
}