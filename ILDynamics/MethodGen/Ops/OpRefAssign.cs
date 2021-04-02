using ILDynamics.MethodGen.IL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILDynamics.MethodGen.Ops
{
    public class OpRefAssign : ILOp
    {
        public RefVar Assignee;
        public ILOp Assigned;

        public OpRefAssign(RefVar a, ILOp b)
        {
            this.Assignee = a;
            this.Assigned = b;
            // b is assigned to a
            // a is the assignee
        }

        public override void Load(Method Method)
        {
            Assignee.Load(Method);
            Assigned.Load(Method);
            ILHelper.StoreValueByRef(Method.OpCodes, Assignee.VarType);
        }

        public override void Store(Method m)
        {
            throw new NotImplementedException();
        }
    }
}
