using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigiCuitEngine.Interfaces
{
    public abstract class IPathCollection : IDictionary<string, IPath>
    {
        public void Add(string key, IPath value)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(string key)
        {
            throw new NotImplementedException();
        }

        public ICollection<string> Keys
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(string key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(string key, out IPath value)
        {
            throw new NotImplementedException();
        }

        public ICollection<IPath> Values
        {
            get { throw new NotImplementedException(); }
        }

        public IPath this[string key]
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

        public void Add(KeyValuePair<string, IPath> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<string, IPath> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<string, IPath>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(KeyValuePair<string, IPath> item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<string, IPath>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
