using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILDynamics
{
    public class ILOperatorPlus : ILObject
    {
        public ILMethod ILFunction { get; private set; }
        public ILObject[] Values;
        public ILOperatorPlus(ILMethod f, params ILObject[] values)
        {
            this.ILFunction = f;
            this.Values = values;
        }
        
        public override void Load()
        {
            Values[0].Load();

            for (int i = 1; i < Values.Length; i++)
            {
                var item = Values[i];
                item.Load();
                ILFunction.OpCodes.Emit(OpCodes.Add);
            }
        }

        public override void Store()
        {
            throw new NotImplementedException();
        }
    }
}
