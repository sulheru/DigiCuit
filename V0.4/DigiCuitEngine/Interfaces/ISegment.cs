using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCuitEngine.Interfaces
{
    public abstract class ISegment : IComponent
    {
        public virtual class ICurrent{
            public virtual double Amperes { get; set; }
            public virtual double Voltage { get; set; }

            public static ICurrent operator +(ICurrent c1, ICurrent c2)
            {
                c1.Voltage += c2.Voltage;
                c1.Amperes += c2.Amperes;
                return c1;
            }

            public static ICurrent operator -(ICurrent c1, ICurrent c2)
            {
                c1.Voltage += c2.Voltage;
                c1.Amperes += c2.Amperes;
                return c1;
            }
        }
        public abstract INode Node1 { get; set; }
        public abstract INode Node2 { get; set; }
        public virtual ICurrent Current { get; set; }

        public virtual void AddVoltage(INode node, double value)
        { AddVoltage(node.Id, value); }

        public virtual void AddVoltage(string nodeId, double value)
        {
            if (nodeId == Node1.Id) { Current.Voltage += value; }
            else if (nodeId == Node2.Id) { Current.Voltage -= value; }
            else { throw new Exception(DigiCuitEngine.Properties.Resources.NodeNotValid); }
        }


        public virtual void AddAmperes(INode node, double value)
        { AddAmperes(node.Id, value); }

        public virtual void AddAmperes(string nodeId, double value)
        {
            if (nodeId == Node1.Id) { Current.Voltage += value; }
            else if (nodeId == Node2.Id) { Current.Voltage -= value; }
            else { throw new Exception(DigiCuitEngine.Properties.Resources.NodeNotValid); }
        }

        public virtual void AddCurrent(INode node, ICurrent value)
        { AddCurrent(node.Id, value); }

        public virtual void AddCurrent(string nodeId, ICurrent value)
        {            
            if (nodeId == Node1.Id) { Current += value; }
            else if (nodeId == Node2.Id) { Current -= value; }
            else { throw new Exception(DigiCuitEngine.Properties.Resources.NodeNotValid); }
        }

        public ISegment(Jint.Engine engine)
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
