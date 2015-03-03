using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCuitEngine.Interfaces
{
    public abstract class IMesh : IComponent
    {
        public abstract INodeCollection Nodes { get; set; }
        public abstract ISegmentCollection Paths { get; set; }

        public override bool IsActive { get; set; }
        public override string Id { get; protected set; }

        public IMesh(Jint.Engine engine)
            : base(engine)
        { }

        public override void Run()
        {
            throw new NotImplementedException();
        }

        public override bool PathFinding(string[] VisitedIds, string endId, ref string[] PathIds, ref List<string[]> PathCollection)
        {
            throw new NotImplementedException();
        }
    }
}
