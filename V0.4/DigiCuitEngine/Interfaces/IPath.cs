using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCuitEngine.Interfaces
{
    public abstract class IPath : IComponent
    {
        public abstract INode Node1 { get; set; }
        public abstract INode Node2 { get; set; }

        public IPath(Jint.Engine engine)
            : base(engine)
        { }

        public override bool PathFinding(string[] VisitedIds, string endId, ref string[] PathIds, ref List<string[]> PathCollection)
        {
            List<string> pIds = new List<string>();
            pIds.AddRange(PathIds);

            if (pIds.Last() == Node1.Id) { return Node2.PathFinding(VisitedIds, endId, ref  PathIds, ref PathCollection); }
            else if (pIds.Last() == Node2.Id) { return Node1.PathFinding(VisitedIds, endId, ref  PathIds, ref PathCollection); }

            return false;
        }
    }
}
