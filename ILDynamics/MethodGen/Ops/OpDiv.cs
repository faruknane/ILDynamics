using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILDynamics.MethodGen.Ops
{
    public class OpDiv : ILOp
    {
        public ILOp Val1, Val2;

        public OpDiv(ILOp val1, ILOp val2)
        {
            this.Val1 = val1;
            this.Val2 = val2;
        }

        public override void Load(Method Method)
        {
            Val1.Load(Method);
            Val2.Load(Method);
            Method.OpCodes.Emit(OpCodes.Div);
        }

        public override void Store(Method Method)
        {
            throw new NotImplementedException();
        }
    }
}
