using ILDynamics.MethodGen.IL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILDynamics.MethodGen.Ops
{
    public class OpValueByRef : ILOp
    {
        public ILOp Object;
        public Type ObjectType;

        public OpValueByRef(ILOp obj, Type t) 
        {
            this.Object = obj;
            this.ObjectType = t;
        }

        public OpValueByRef(RefableObject obj) 
        {
            this.Object = obj;
            this.ObjectType = obj.Type;
        }

        public override void Load(Method Method)
        {
            Object.Load(Method);
            ILHelper.LoadValueByRef(Method.OpCodes, this.ObjectType);
        }

        public override void Store(Method Method)
        {
            throw new NotImplementedException();
        }

    }
}
