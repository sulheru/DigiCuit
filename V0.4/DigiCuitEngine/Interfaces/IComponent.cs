using Jint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCuitEngine.Interfaces
{
    public abstract class IComponent : Jint.Native.Function.FunctionInstance
    {
        public abstract bool IsActive { get; set; }
        public abstract string Id { get; protected set; }
        public abstract void Run();
        public abstract bool PathFinding(string[] VisitedIds, string endId, ref string[] PathIds, ref List<string[]>PathCollection);

        public IComponent(Engine engine)
            : base(engine, null, null, false)
        { }

        public IComponent(Jint.Engine engine)
            : base(engine, null, null, false)
        {
            Id = IComponent.GenerateUniqueId();
            this.Engine = engine;
        }

        public static string GenerateUniqueId()
        { return Guid.NewGuid().ToString("N"); }

        
    }
}
