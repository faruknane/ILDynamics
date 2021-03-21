using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILDynamics
{
    public class Constant<T> : ILObject
    {
        public Type Type { get; private set; }

        public readonly T Value;

        public Constant(Method m, T val) : base(m)
        {
            Type = typeof(T);
            Value = val;
        }

        public override void Load()
        {
            ILHelper.LoadConstant<T>(Method.OpCodes, Value);
        }

        public override void Store()
        {
            throw new NotImplementedException();
        }

    }
}
