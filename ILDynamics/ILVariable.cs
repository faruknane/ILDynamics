using System;
using System.Reflection.Emit;

namespace ILDynamics
{
    public class ILVariable : ILObject
    {
        public ILFunction ILFunction { get; private set; }
        public readonly int Index;
        public Type Type { get; private set; }

        public ILVariable(ILFunction function, Type type)
        {
            this.ILFunction = function;
            this.Type = type;
            this.Index = this.ILFunction.NewVariable(this);
        }

        public override void Load()
        {
            ILFunction.OpCodes.Emit(OpCodes.Ldloc_S, Index);
        }

        public override void Store()
        {
            ILFunction.OpCodes.Emit(OpCodes.Stloc_S, Index);
        }

    }
}