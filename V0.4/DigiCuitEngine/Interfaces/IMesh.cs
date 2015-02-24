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
        public abstract INodeCollection<Point> Nodes { get; set; }
        public abstract IPathCollection Paths { get; set; }
    }
}
