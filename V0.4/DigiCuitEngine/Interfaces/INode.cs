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

        public override bool PathFinding(string[] VisitedIds, string endId, ref string[] PathIds, ref List<string[]>PathCollection)
        {
            List<string> vIds = new List<string>();
            vIds.AddRange(VisitedIds);
            
            List<string> pIds = new List<string>();
            List<bool> hasExit = new List<bool>();

            foreach (KeyValuePair<string, IPath> path in Paths)
            {
                if (!vIds.Contains(path.Key))
                {
                    pIds.AddRange(PathIds);
                    pIds.Add(path.Key);
                    string[] pathIds = pIds.ToArray();
                    bool isExit = path.Value.PathFinding(vIds.ToArray(), endId, ref pathIds, ref PathCollection);
                    hasExit.Add(isExit);
                    if (isExit) { PathCollection.Add(pathIds); }
                    pIds.Clear();
                }
            }
            return hasExit.Contains(true);
        }
    }
}
