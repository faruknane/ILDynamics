using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILDynamics.Ops
{
    public class OpNeg : ILObject
    {
        public ILObject Val;

        public OpNeg(Method m, ILObject val) : base(m)
        {
            this.Val = val;
        }

        public override void Load()
        {
            this.Val.Load();
            Method.OpCodes.Emit(OpCodes.Neg);
        } 

        public override void Store()
        {
            throw new NotImplementedException();
        }
    }
}
