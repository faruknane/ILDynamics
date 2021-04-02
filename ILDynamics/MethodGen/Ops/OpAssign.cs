using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILDynamics.MethodGen.Ops
{
    public class OpAssign : ILOp
    {
        public ILOp Assignee;
        public ILOp Assigned;

        public OpAssign(ILOp a, ILOp b)
        {
            this.Assignee = a;
            this.Assigned = b;
            // b is assigned to a
            // a is the assignee
        }

        public override void Load(Method m)
        {
            Assigned.Load(m);
            Assignee.Store(m);
        }

        public override void Store(Method m)
        {
            throw new NotImplementedException();
        }
    }
}
