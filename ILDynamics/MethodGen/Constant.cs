using ILDynamics.MethodGen.IL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILDynamics.MethodGen
{
    public class Constant<T> : ILOp
    {
        public Type Type { get; private set; }

        public readonly T Value;

        public Constant(T val)
        {
            Type = typeof(T);
            Value = val;
        }

        public override void Load(Method Method)
        {
            ILHelper.LoadConstant<T>(Method.OpCodes, Value);
        }

        public override void Store(Method Method)
        {
            throw new NotImplementedException();
        }

    }
}
