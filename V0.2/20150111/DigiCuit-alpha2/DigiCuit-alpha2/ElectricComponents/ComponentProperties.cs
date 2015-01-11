using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCuit_alpha2.ElectricComponents
{
    public class ComponentProperties : IDictionary<string,string>
    {
        private Component comp;

        public ComponentProperties(Component comp)
        { this.comp = comp; }

        public void Add(string key, string value)
        {
            string cmd = String.Format("comp.Properties.{0}='{1}'", key, value);
            this.comp.Command(cmd);
        }

        public bool ContainsKey(string key)
        {
            string cmd = String.Format("'{0}' in comp.Properties", key);
            bool result=false;
            try { result = Boolean.Parse(this.comp.Command(cmd)); }
            catch (Exception e) { result = false; }
            return result;
        }

        public ICollection<string> Keys
        {
            get
            {
                StringCollection keys = new StringCollection();
                string cmd = "Object.keys(comp.Properties).join(';')";
                keys.AddRange(this.comp.Command(cmd).Split(';'));
                return (ICollection<string>)keys;
            }
        }

        public bool Remove(string key)
        {
            string cmd = String.Format("delete comp.Properties.{0}", key);
            bool result = false;
            try { result = Boolean.Parse(this.comp.Command(cmd)); }
            catch (Exception e) { result = !this.ContainsKey(key); }
            return result;
        }

        public bool TryGetValue(string key, out string value)
        {
            string cmd = String.Format("delete comp.Properties.{0}", key);
            value = this.comp.Command(cmd);
            bool result = value != "undefined";
            return result;
        }

        public ICollection<string> Values
        {
            get
            {
                StringCollection keys = new StringCollection();
                string cmd = "Object.keys(comp.Properties).map(function(key){ return comp.Properties[key]; }).join(';')";
                keys.AddRange(this.comp.Command(cmd).Split(';'));
                return (ICollection<string>)keys;
            }
        }

        public string this[string key]
        {
            get
            {
                string cmd = String.Format("delete comp.Properties.{0}", key);
                return this.comp.Command(cmd);
            }
            set
            {
                string cmd = String.Format("delete comp.Properties.{0}={1}", key,value);
                this.comp.Command(cmd);
            }
        }

        public void Add(KeyValuePair<string, string> item)
        { this[item.Key] = item.Value; }

        public void AddRange(KeyValuePair<string, string>[] array)
        {
            foreach (KeyValuePair<string, string> item in array)
            { this.Add(item); }
        }

        public void Clear()
        {
            string cmd = String.Format("delete comp.Properties={}");
            this.comp.Command(cmd);
        }

        public bool Contains(KeyValuePair<string, string> item)
        {
            string value;
            bool key= this.TryGetValue(item.Key, out value);
            bool val = item.Value == value;
            return key && val;
        }

        private bool TryGetValue(string p1, string p2)
        { return this.TryGetValue(p1, p2); }

        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        { this.AddRange(array); }

        public int Count
        {
            get
            {
                string cmd = String.Format("comp.Properties.length");
                string length = this.comp.Command(cmd);
                int count=-1;
                try { count = Int32.Parse(length); }
                catch (Exception e) { count = -1; }
                return count;
            }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(KeyValuePair<string, string> item)
        {
            if(this.Contains(item))
            { return this.Remove(item.Key); }
            return false;
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        { return new ComponentPropertiesEnumerator(this.comp); }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        { return (System.Collections.IEnumerator)this.GetEnumerator(); }

        public object[] ToArray()
        {
            ArrayList array = new ArrayList();
            foreach (KeyValuePair<string, string> property in this)
            { array.Add(property); }
            return array.ToArray();
        }

    }

    public class ComponentPropertiesEnumerator : IEnumerator<KeyValuePair<string, string>>
    {
        int position = -1;
        Component comp;

        public ComponentPropertiesEnumerator(Component comp)
        { this.comp = comp; }

        public KeyValuePair<string, string> Current
        {
            get
            {
                string cmd = String.Format(@"
                    var key = Object.keys(comp.Properties)[{0}];
                    var val = comp.Properties[key];
                    key + ';' + val;
                    ", position.ToString());
                string[] result = this.comp.Command(cmd).Split(';');
                return new KeyValuePair<string, string>(result[0], result[1]);
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
            try { len = Int32.Parse(this.comp.Command("comp.Properties.length")); }
            catch (Exception e) { e.ToString(); }
            return position < len;
        }

        public void Reset()
        { position = -1; }

    } 
}
