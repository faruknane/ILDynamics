using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILDynamics
{
    public class Param : ILObject, IReffable
    {
        public StaticMethod ILFunction { get; private set; }
        public readonly int Index;
        public Type Type { get; private set; }

        public Param(StaticMethod function, Type type)
        {
            this.ILFunction = function;
            this.Type = type;
            this.Index = this.ILFunction.NewParameter(this);
        }

        public void LoadAddress()
        {
            ILFunction.OpCodes.Emit(OpCodes.Ldarga_S, Index);
        }

        public override void Load()
        {
            ILFunction.OpCodes.Emit(OpCodes.Ldarg_S, Index);
        }

        public override void Store()
        {
            ILFunction.OpCodes.Emit(OpCodes.Starg_S, Index);
        }

    }

}

