using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILDynamics
{
    public class ILGetValueByRef : ILObject
    {
        public ILFunction ILFunction { get; private set; }
        public Type Type;
        public ILObject Object;

        public ILGetValueByRef(ILFunction f, ILObject obj, Type t)
        {
            this.ILFunction = f;
            this.Type = t;
            this.Object = obj;
        }

        public override void Load()
        {
            Object.Load();
            ILHelper.LoadValueByRef(ILFunction.OpCodes, Type);
        }

        public override void Store()
        {
            throw new NotImplementedException();
        }

    }
}
