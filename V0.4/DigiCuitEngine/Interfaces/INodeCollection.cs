using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigiCuitEngine.Interfaces
{
    public abstract class INodeCollection<T> : IDictionary<T, INode>
    {
        public void Add(T key, INode value)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(T key)
        {
            throw new NotImplementedException();
        }

        public ICollection<T> Keys
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(T key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(T key, out INode value)
        {
            throw new NotImplementedException();
        }

        public ICollection<INode> Values
        {
            get { throw new NotImplementedException(); }
        }

        public INode this[T key]
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

        public void Add(KeyValuePair<T, INode> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<T, INode> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<T, INode>[] array, int arrayIndex)
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

        public bool Remove(KeyValuePair<T, INode> item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<T, INode>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
