using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCuitEngine.Interfaces
{
    public abstract class INode : IComponent
    {
        public abstract Point Location { get; set; }
        public abstract IPathCollection Paths { get; set; }

        public override bool IsActive
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override string Id
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void Run()
        {
            throw new NotImplementedException();
        }

        public override bool PathFinding(string[] VisitedIds, string endId, out string[] PathIds)
        {
            throw new NotImplementedException();
        }
    }
}
