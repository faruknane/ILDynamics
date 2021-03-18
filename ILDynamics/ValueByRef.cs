using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILDynamics
{
    public class ValueByRef : ILObject
    {
        public StaticMethod ILFunction { get; private set; }
        public IReffable Object;

        public ValueByRef(StaticMethod f, IReffable obj)
        {
            this.ILFunction = f;
            this.Object = obj;
        }

        public override void Load()
        {
            Object.Load();
            ILHelper.LoadValueByRef(ILFunction.OpCodes, this.Object.Type);
        }

        public override void Store()
        {
            throw new NotImplementedException();
        }

    }
}
