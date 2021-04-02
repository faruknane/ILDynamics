using ILDynamics.MethodGen.Ops;
using ILDynamics.MethodGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILDynamics.MethodGen
{
    public class RefInitJob
    {
        public RefableObject V;

        public RefInitJob(RefableObject v)
        {
            this.V = v;
        }
    }

    public class RefVar : Var
    {
        public Type VarType;
        private RefInitJob Job;

        public RefVar(RefableObject v) : base(PointerOf(v.Type))
        {
            this.VarType = v.Type;
            this.Job = new RefInitJob(v);
        }

        public RefVar(Type t) : base(PointerOf(t))
        {
            this.VarType = t;
        }

        public OpRefAssign RefAssign(ILOp val)
        {
            return new OpRefAssign(this, val);
        }

        public override void Init(Method Method)
        {
            base.Init(Method);
            if (this.Job != null)
            {
                RefableObject v = this.Job.V;
                this.Job = null;
                v.LoadAddress(Method);
                this.Store(Method);
            }
        }

        public static Type PointerOf(Type t)
        {
            return t.MakeByRefType();
        }
    }
}
