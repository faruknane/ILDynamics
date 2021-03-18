using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILDynamics
{
    public class OpRefByVar : ILObject
    {
        public IReffable Var;
        public OpRefByVar(IReffable v)
        {
            this.Var = v;
        }

        public override void Load()
        {
            this.Var.LoadAddress();
        }

        public override void Store()
        {
            throw new NotImplementedException();
        }
    }
}
