using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILDynamics.MethodGen.Ops
{
    public class OpCall : ILOp
    {
        public RefableObject Object;
        public MethodInfo MethodInfo;
        public ILOp[] Parameters;

        public OpCall(RefableObject obj, MethodInfo mi, params ILOp[] parameters)
        {
            this.Object = obj;
            this.MethodInfo = mi;
            this.Parameters = parameters;
        }

        public override void Load(Method Method)
        {
            if(Object != null)
                Object.LoadAddress(Method);

            foreach (var item in Parameters)
                item.Load(Method);

            Method.OpCodes.Emit(OpCodes.Call, MethodInfo);
        }

        public override void Store(Method Method)
        {
            throw new NotImplementedException();
        }
    }
}
