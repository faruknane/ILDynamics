using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILDynamics.MethodGen.Ops
{
    public class OpRefByVar : ILOp
    {
        public RefableObject Var;

        public OpRefByVar(RefableObject v)
        {
            this.Var = v;
        }

        public override void Load(Method Method)
        {
            this.Var.LoadAddress(Method);
        }

        public override void Store(Method Method)
        {
            throw new NotImplementedException();
        }
    }
}
