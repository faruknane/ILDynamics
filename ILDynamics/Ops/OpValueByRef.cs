using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILDynamics.Operators
{
    public class OpValueByRef : ILObject
    {
        public ILObject Object;
        public Type ObjectType;

        public OpValueByRef(Method m, ILObject obj, Type t) : base(m)
        {
            this.Object = obj;
            this.ObjectType = t;
        }

        public OpValueByRef(Method m, RefableObject obj) : base(m)
        {
            this.Object = obj;
            this.ObjectType = obj.Type;
        }

        public override void Load()
        {
            Object.Load();
            ILHelper.LoadValueByRef(Method.OpCodes, this.ObjectType);
        }

        public override void Store()
        {
            throw new NotImplementedException();
        }

    }
}
