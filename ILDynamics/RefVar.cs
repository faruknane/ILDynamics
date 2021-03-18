using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILDynamics
{
    public class RefVar : Var, IReffable
    {
        public Type VarType;

        public RefVar(Method method, IReffable v) : base(method, PointerOf(v.Type))
        {
            this.VarType = v.Type;
            v.LoadAddress();
            this.Store();
        }

        public RefVar(Method method, Type t) : base(method, PointerOf(t))
        {
            this.VarType = t;
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
