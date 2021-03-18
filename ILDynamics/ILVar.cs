using System;
using System.Reflection.Emit;

namespace ILDynamics
{
    public class ILVar : ILObject, ILReffable
    {
        public ILMethod ILFunction { get; private set; }
        public readonly int Index;
        public Type Type { get; private set; }

        public ILVar(ILMethod function, Type type)
        {
            this.ILFunction = function;
            this.Type = type;
            this.Index = this.ILFunction.NewVariable(this);
        }

        public void LoadAddress()
        {
            ILFunction.OpCodes.Emit(OpCodes.Ldloca_S, Index);
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