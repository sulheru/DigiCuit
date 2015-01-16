using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigiCuitBeta.Electronics
{
    public class Component
    {
        private Jint.Engine Engine;

        public ConnectorCollection Plugs { set; get; }
    }
}
