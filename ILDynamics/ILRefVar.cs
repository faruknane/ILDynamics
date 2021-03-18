using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILDynamics
{
    public class ILRefVar : ILVar, ILReffable
    {
        public ILObject Var;
        public Type VarType;

        public ILRefVar(ILMethod method, ILVar v) : base(method, PointerOf(v.Type))
        {
            this.Var = v;
            this.VarType = v.Type;
            v.LoadAddress();
            this.Store();
        }

        public ILRefVar(ILMethod method, ILParam v) : base(method, PointerOf(v.Type))
        {
            this.Var = v;
            this.VarType = v.Type;
            v.LoadAddress();
            this.Store();
        }

        public void RefAssign(ILObject val)
        {
            this.Load();
            val.Load();
            ILHelper.StoreValueByRef(this.ILFunction.OpCodes, this.VarType);
        }

        public static Type PointerOf(Type t)
        {
            return t.MakeByRefType();
        }
    }
}
