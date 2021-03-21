using ILDynamics.Ops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ILDynamics
{
    public abstract class ILObject
    {
        public Method Method { get; private set; }

        public ILObject(Method m)
        {
            Method = m;
        }

        public abstract void Load();
        public abstract void Store();

        public void Assign(ILObject v)
        {
            v.Load();
            this.Store();
        }
    }
}
