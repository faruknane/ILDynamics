using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILDynamics
{
    public abstract class ILObject
    {
        public abstract void Load();
        public abstract void Store();

        public void Assign(ILObject v)
        {
            v.Load();
            this.Store();
        }
    }
}
