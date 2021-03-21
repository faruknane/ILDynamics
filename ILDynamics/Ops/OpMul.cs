using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILDynamics.Ops
{
    public class OpMul : ILObject
    {
        public ILObject[] Values;
        public OpMul(Method m, params ILObject[] values) : base(m)
        {
            this.Values = values;
        }

        public override void Load()
        {
            Values[0].Load();

            for (int i = 1; i < Values.Length; i++)
            {
                var item = Values[i];
                item.Load();
                Method.OpCodes.Emit(OpCodes.Mul);
            }
        }

        public override void Store()
        {
            throw new NotImplementedException();
        }
    }
}
