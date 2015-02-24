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
