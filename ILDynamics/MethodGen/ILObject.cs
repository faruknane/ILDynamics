using ILDynamics.MethodGen.Ops;
using ILDynamics.MethodGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ILDynamics.MethodGen
{
    public abstract class ILOp
    {

        public ILOp()
        {

        }

        public abstract void Load(Method m);
        public abstract void Store(Method m);

        public OpAssign Assign(ILOp v)
        {
            return new OpAssign(this, v);
            //v.Load();
            //this.Store();
        }
    }
}
