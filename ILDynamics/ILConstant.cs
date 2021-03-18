using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILDynamics
{
    public class ILConstant<T> : ILObject
    {
        public ILMethod ILFunction { get; private set; }
        public Type Type { get; private set; }

        public readonly T Value;

        public ILConstant(ILMethod f, T val)
        {
            ILFunction = f;
            Type = typeof(T);
            Value = val;
        }

        public override void Load()
        {
            ILHelper.LoadConstant<T>(ILFunction.OpCodes, Value);
        }

        public override void Store()
        {
            throw new NotImplementedException();
        }
    }
}
