using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILDynamics.Ops
{
    public class OpCall : ILObject
    {
        public RefableObject Object;
        public MethodInfo MethodInfo;
        public ILObject[] Parameters;

        public OpCall(Method m, RefableObject obj, MethodInfo mi, params ILObject[] parameters) : base(m)
        {
            this.Object = obj;
            this.MethodInfo = mi;
            this.Parameters = parameters;
        }

        public override void Load()
        {
            if(Object != null)
                Object.LoadAddress();

            foreach (var item in Parameters)
                item.Load();

            Method.OpCodes.Emit(OpCodes.Call, MethodInfo);
        }

        public override void Store()
        {
            throw new NotImplementedException();
        }
    }
}
