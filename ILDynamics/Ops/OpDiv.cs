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
        public ILObject Val1, Val2;

        public OpDiv(Method m, ILObject val1, ILObject val2) : base(m)
        {
            this.Val1 = val1;
            this.Val2 = val2;
        }

        public override void Load()
        {
            Val1.Load();
            Val2.Load();
            Method.OpCodes.Emit(OpCodes.Div);
        }

        public override void Store()
        {
            throw new NotImplementedException();
        }
    }
}
