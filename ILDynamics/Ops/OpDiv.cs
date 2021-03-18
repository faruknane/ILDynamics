using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILDynamics.Ops
{
    public class OpDiv : ILObject
    {
        public Method ILFunction { get; private set; }

        public ILObject Val1, Val2;

        public OpDiv(Method f, ILObject val1, ILObject val2)
        {
            this.ILFunction = f;
            this.Val1 = val1;
            this.Val2 = val2;
        }

        public override void Load()
        {
            Val1.Load();
            Val2.Load();
            ILFunction.OpCodes.Emit(OpCodes.Div);
        }

        public override void Store()
        {
            throw new NotImplementedException();
        }
    }
}
