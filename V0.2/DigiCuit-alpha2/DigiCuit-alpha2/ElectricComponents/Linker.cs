using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCuit_alpha2.ElectricComponents
{
    public class Linker
    {
        public class Pointer
        {
            public int ComponentIndex { set; get; }
            public int InOutIndex { set; get; }
            public override string ToString()
            { return String.Format("{0}:{1}", this.ComponentIndex.ToString(), this.InOutIndex.ToString()); }
        }

        public Pointer InOut1 { set; get; }
        public Pointer InOut2 { set; get; }
        public DirectCurrent Multimeter { get; private set; }
        public bool isVisible { set; get; }

        public Linker()
        { this.InOut1 = new Pointer(); this.InOut2 = new Pointer(); }

        public void Run(ComponentCollection comps)
        {
            DirectCurrent dcv1 = comps[this.InOut1.ComponentIndex].InOut[this.InOut1.InOutIndex];
            DirectCurrent dcv2 = comps[this.InOut2.ComponentIndex].InOut[this.InOut2.InOutIndex];

            comps[this.InOut1.ComponentIndex].InOut[this.InOut1.InOutIndex] = dcv2;
            comps[this.InOut2.ComponentIndex].InOut[this.InOut2.InOutIndex] = dcv1;

            this.Multimeter = (dcv1 > dcv2) ? dcv1 : dcv2;
        }

        public override string ToString()
        { return "{" + String.Format("InOut1: {0}, InOut2: {1}", this.InOut1.ToString(), this.InOut2.ToString()) + "}"; }

        public string ToString(ComponentCollection comps)
        { return "{" + String.Format("InOut1: {0}, InOut2: {1}", comps.GetByInOutPointer(this.InOut1).ToString(), comps.GetByInOutPointer(this.InOut2).ToString()) + "}"; }
    }

    public class LinkerCollection : ICollection<Linker>
    {
        private List<Linker> Links = new List<Linker>();

        public Linker this[int Index]
        {
            set { this.Links[Index] = value; }
            get { return this.Links[Index]; }
        }

        public void Add(Linker item)
        { this.Links.Add(item); }

        public void AddRange(object[] items)
        { 
            if(items.GetType()==typeof(Linker[]))
            { this.AddRange((Linker[])items); }
        }

        public void AddRange(Linker[] items)
        {
            foreach (Linker item in items)
            { this.Add(item); }
        }

        public void Clear()
        { this.Links.Clear(); }

        public bool Contains(Linker item)
        { return this.Links.Contains(item); }

        public void CopyTo(Linker[] array, int arrayIndex)
        { this.Links.CopyTo(array, arrayIndex); }

        public void CopyTo(Linker[] array)
        { this.Links.CopyTo(array); }

        public int Count
        {
            get { return this.Links.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Linker item)
        { return this.Links.Remove(item); }

        public void RemoveAt(int Index)
        { this.Links.RemoveAt(Index); }

        public IEnumerator<Linker> GetEnumerator()
        { return this.Links.GetEnumerator(); }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        { return (System.Collections.IEnumerator)this.GetEnumerator(); }
    }
}
