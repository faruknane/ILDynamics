using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILDynamics
{
    public class ILConstant : ILObject
    {
        public ILMethod ILFunction { get; private set; }
        public Type Type { get; private set; } 
        public ILObject Constant { get; private set; }

        private ILConstant()
        {

        }

        public static ILConstant From<T>(ILConstant<T> val)
        {
            return new ILConstant() { ILFunction = val.ILFunction, Type = val.Type, Constant = val };
        }

        public static ILConstant From<T>(ILMethod f, T v)
        {
            ILConstant<T> val = new ILConstant<T>(f, v);
            return new ILConstant() { ILFunction = val.ILFunction, Type = val.Type, Constant = val };
        }

        public override void Load()
        {
            Constant.Load();
        }

        public override void Store()
        {
            throw new NotImplementedException();
        }
    }

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
            ILHelper.LoadConstant(ILFunction.OpCodes, Value);
        }

        public override void Store()
        {
            throw new NotImplementedException();
        }
    }
}
