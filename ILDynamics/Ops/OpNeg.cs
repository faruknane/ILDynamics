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
        public Method ILFunction { get; private set; }
        public ILObject Val;

        public OpNeg(Method f, ILObject val)
        {
            this.ILFunction = f;
            this.Val = val;
        }

        public override void Load()
        {
            this.Val.Load();
            ILFunction.OpCodes.Emit(OpCodes.Neg);
        } 

        public override void Store()
        {
            throw new NotImplementedException();
        }
    }
}
