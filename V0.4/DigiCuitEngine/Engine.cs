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
        public ComponentConstructor Component { get; private set; }

        public Engine()
            : base()
        {
            Component = ComponentConstructor.CreateComponentConstructor(this);
            Component.Configure();
            Component.PrototypeObject.Configure();
            Global.FastAddProperty("Component", Component, true, false, true);
        }
    }
}