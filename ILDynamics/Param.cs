using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILDynamics
{
    public class Param : RefableObject
    {
        public readonly int Index;

        public Param(Method m, Type type) : base(m)
        {
            this.Type = type;
            this.Index = this.Method.NewParam(this);
        }

        public override void LoadAddress()
        {
            Method.OpCodes.Emit(OpCodes.Ldarga_S, Index);
        }

        public override void Load()
        {
            Method.OpCodes.Emit(OpCodes.Ldarg_S, Index);
        }

        public override void Store()
        {
            Method.OpCodes.Emit(OpCodes.Starg_S, Index);
        }

    }

}

