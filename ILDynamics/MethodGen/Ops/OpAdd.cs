using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILDynamics.MethodGen.Ops
{
    public class OpAdd : ILOp
    {
        public ILOp[] Values;
        public OpAdd(params ILOp[] values) 
        {
            this.Values = values;
        }
        
        public override void Load(Method Method)
        {
            Values[0].Load(Method);

            for (int i = 1; i < Values.Length; i++)
            {
                var item = Values[i];
                item.Load(Method);
                Method.OpCodes.Emit(OpCodes.Add);
            }
        }

        public override void Store(Method Method)
        {
            throw new NotImplementedException();
        }
    }
}
