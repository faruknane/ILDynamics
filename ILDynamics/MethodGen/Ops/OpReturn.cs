using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILDynamics.MethodGen.Ops
{
    public class OpReturn : ILOp
    {
        public ILOp Val;

        public OpReturn(ILOp a = null)
        {
            this.Val = a;
            // b is assigned to a
            // a is the assignee
        }

        public override void Load(Method Method)
        {
            if(Val != null)
                Val.Load(Method);
            Method.OpCodes.Emit(System.Reflection.Emit.OpCodes.Ret);
        }

        public override void Store(Method m)
        {
            throw new NotImplementedException();
        }
    }
}
