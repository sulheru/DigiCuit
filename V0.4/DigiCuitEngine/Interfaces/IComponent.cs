using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCuitEngine.Interfaces
{
    public abstract class IComponent
    {
        public abstract bool IsActive { get; set; }
        public abstract string Id { get; set; }
        public abstract void Run();
        public abstract bool PathFinding(string[] VisitedIds, string endId, out string[] PathIds);
    }
}
