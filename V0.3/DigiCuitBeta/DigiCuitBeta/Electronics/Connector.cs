using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DigiCuitBeta.Electronics
{
    public class Connector
    {
        Point Location { set; get; }
        DirectCurrent DirectCurrent { set; get; }
    }

    public class ConnectorCollection : IList<Connector>
    {
        public int IndexOf(DigiCuitBeta.Electronics.Connector item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, DigiCuitBeta.Electronics.Connector item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public DigiCuitBeta.Electronics.Connector this[int index]
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

        public void Add(DigiCuitBeta.Electronics.Connector item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(DigiCuitBeta.Electronics.Connector item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(DigiCuitBeta.Electronics.Connector[] array, int arrayIndex)
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

        public bool Remove(DigiCuitBeta.Electronics.Connector item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<DigiCuitBeta.Electronics.Connector> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
