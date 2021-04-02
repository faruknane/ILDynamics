using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILDynamics.MethodGen.Ops
{
    public class OpSequence : ILOp
    {
        public ILOp[] Operations { get; private set; }

        public OpSequence(params ILOp[] ops)
        {
            Operations = ops;
        }

        public override void Load(Method m)
        {
            for (int i = 0; i < Operations.Length; i++)
                Operations[i].Load(m);
        }

        public override void Store(Method m)
        {
            throw new NotImplementedException();
        }
    }
}
