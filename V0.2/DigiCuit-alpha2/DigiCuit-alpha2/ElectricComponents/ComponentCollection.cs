using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCuit_alpha2.ElectricComponents
{
    public class ComponentCollection : ICollection<Component>
    {
        private List<Component> comps = new List<Component>();

        public Component this[int Index]
        {
            set { this.comps[Index] = value; }
            get { return this.comps[Index]; }
        }

        public void Add(Component item)
        { this.Add(item.FrameWorkFile.FullName, item.ClassFile.FullName); }

        public void Add(string FrameWorkFile, string ClassFile)
        {
            Component item = new Component(FrameWorkFile, ClassFile, this.comps.Count - 1);
            this.comps.Add(item); 
        }

        public void AddRange(object[] items)
        {
            if(items.GetType()==typeof(Component[]))
            { this.AddRange((Component[])items); }
        }

        public void AddRange(Component[] items)
        {
            foreach (Component item in items)
            { this.Add(item); }
        }

        public void Clear()
        { this.comps.Clear(); }

        public bool Contains(Component item)
        { return this.comps.Contains(item); }

        public void CopyTo(Component[] array, int arrayIndex)
        { this.comps.CopyTo(array, arrayIndex); }

        public void CopyTo(Component[] array)
        { this.comps.CopyTo(array); }

        public int Count
        {
            get { return this.comps.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Component item)
        { return this.comps.Remove(item); }

        public void RemoveAt(int Index)
        { this.comps.RemoveAt(Index); }

        public IEnumerator<Component> GetEnumerator()
        { return this.comps.GetEnumerator(); }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        { return (System.Collections.IEnumerator)this.comps.GetEnumerator(); }

        public Component GetByPointer(Linker.Pointer pointer)
        { return this[pointer.ComponentIndex]; }

        public DirectCurrent GetByInOutPointer(Linker.Pointer pointer)
        { return this.GetByPointer(pointer).InOut[pointer.InOutIndex]; }
    }
}
