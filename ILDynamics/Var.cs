using System;
using System.Reflection.Emit;

namespace ILDynamics
{
    public class Var : RefableObject
    {
        public readonly int Index;

        public Var(Method m, Type type) : base(m)
        {
            this.Type = type;
            this.Index = this.Method.NewVar(this);
        }

        public override void LoadAddress()
        {
            Method.OpCodes.Emit(OpCodes.Ldloca_S, Index);
        }

        public override void Load()
        {
            Method.OpCodes.Emit(OpCodes.Ldloc_S, Index);
        }

        public override void Store()
        {
            Method.OpCodes.Emit(OpCodes.Stloc_S, Index);
        }

    }
}