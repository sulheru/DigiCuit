using Jint;
using Jint.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DigiCuitBeta.Electronics
{
    public class DirectCurrentCollection : ICollection<DirectCurrent>
    {
        private Component comp;
        
        public DirectCurrentCollection(Component comp)
        { this.comp = comp; }

        public DirectCurrent this[int Index]
        {
            set { this.comp.Command(String.Format("comp.InOut[{0}]={1}", Index, value.ToString())); }
            get { return DirectCurrent.CreatFromJSON(this.comp.jsGlobals, this.comp.Command(String.Format("JSON.stringify(comp.InOut[{0}])", Index))); }
        }

        public void Add(DirectCurrent item)
        { this.Add((object)item); }
        
        public void Add(object item)
        { this.comp.Command(String.Format("comp.InOut[comp.InOut.length] = {0}", item.ToString())); }

        public void AddRange(DirectCurrent[] items) 
        { this.AddRange((object[])items); }

        public void AddRange(object[] items)
        {
            foreach (object item in items)
            { this.Add(item); }
        }

        public void Clear()
        { this.comp.Command("comp.InOut=[]"); }

        public bool Contains(DirectCurrent item)
        { return IndexOf(item) >= 0; }

        public int Count
        {
            get
            {
                int r = 0 - 1;
                try
                { r = Int32.Parse(this.comp.Command("comp.InOut.length")); }
                catch (Exception e) { }
                return r;
            }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(DirectCurrent item)
        {
            int index = this.IndexOf(item);
            return this.RemoveAt(index);
        }

        public bool RemoveAt(int Index)
        {
            string cmd = String.Format("comp.InOut.splice({0},1);", Index.ToString());
            return this.comp.Command(cmd).Trim().Length != 0;
        }

        public int IndexOf(DirectCurrent item)
        {
            string cmd = String.Format("comp.InOut.indexOf({1})", item.ToString());
            return Int32.Parse(this.comp.Command(cmd));
        }

        public IEnumerator<DirectCurrent> GetEnumerator()
        { return new DirectCurrentEnumerator(this.comp); }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        { return (System.Collections.IEnumerator)this.GetEnumerator(); }

        public void CopyTo(DirectCurrent[] array, int arrayIndex)
        {
            object[] obj = (object[])array;
            string JSON = String.Join(",", obj);
            string cmd = String.Format("comp.InOut.splice({1},0,{0})", JSON, arrayIndex.ToString());
            this.comp.Command(cmd);
        }

        public override string ToString()
        { return this.comp.Command("JSON.stringify(comp.InOut)"); }

        public object[] ToArray()
        {
            object[] obj = this.comp.Command(@"
                var array=[];
                for(var i=0;i<comp.InOut.length;i++)
                { array[i]=JSON.stringify(comp.InOut[i]); }
                array.join(';');
                ").Split(';');
            return obj;
        }

        public class DirectCurrentEnumerator : IEnumerator<DirectCurrent>
        {
            int position = -1;
            Component comp;

            public DirectCurrentEnumerator(Component comp)
            { this.comp = comp; }

            public DirectCurrent Current
            {
                get 
                {
                    string cmd = String.Format("JSON.stringify(comp.InOut[{0}])", position.ToString());
                    return DirectCurrent.CreatFromJSON(this.comp.jsGlobals, this.comp.Command(cmd)); 
                }
            }

            public void Dispose()
            { }

            object System.Collections.IEnumerator.Current
            {
                get { return this.Current; }
            }

            public bool MoveNext()
            {
                position++;
                int len = -1;
                try { len = Int32.Parse(this.comp.Command("comp.InOut.length")); }
                catch (Exception e) { e.ToString(); }
                return position < len;
            }

            public void Reset()
            { position = -1; }
        }
    }
}