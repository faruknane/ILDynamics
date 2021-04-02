using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILDynamics.MethodGen.Ops
{
    public class OpNeg : ILOp
    {
        public ILOp Val;

        public OpNeg(ILOp val) 
        {
            this.Val = val;
        }

        public override void Load(Method Method)
        {
            this.Val.Load(Method);
            Method.OpCodes.Emit(OpCodes.Neg);
        } 

        public override void Store(Method Method)
        {
            throw new NotImplementedException();
        }
    }
}
