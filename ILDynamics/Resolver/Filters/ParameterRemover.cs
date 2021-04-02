using ILDynamics.MethodGen.IL;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;


namespace ILDynamics.Resolver.Filters
{
    public class ParameterRemover : Filter
    {
        public int[] NewIndices;
        public bool[] RemoveIndices;

        public ParameterRemover(MethodInfo info, ILGenerator il, params int[] indices)
        {
            this.SetNewIndices(indices);
            this.Initialize(info, il);
        }

        public ParameterRemover(params int[] indices)
        {
            this.SetNewIndices(indices);
        }

        private void SetNewIndices(params int[] indices)
        {
            Array.Sort(indices);
            this.RemoveIndices = new bool[indices.Last() + 1];

            for (int i = 0; i < RemoveIndices.Length; i++)
                RemoveIndices[i] = false;

            foreach (var item in indices)
                RemoveIndices[item] = true;

            this.NewIndices = new int[indices.Last() + 1];

            for (int i = 0; i < this.NewIndices.Length; i++)
            {
                if (i > 0 && RemoveIndices[i - 1])
                    NewIndices[i] = NewIndices[i - 1];
                else
                    NewIndices[i] = (i > 0 ? NewIndices[i - 1] : -1) + 1;
            }
        }
        public int GetIndex(int index)
        {
            if (NewIndices.Length <= index)
                return NewIndices.Last() + index - NewIndices.Length + (RemoveIndices.Last() ? 0 : 1);

            if (RemoveIndices[index])
                throw new Exception("the function contains a parameter which is to be removed!");

            return NewIndices[index];
        }

        public override bool Apply(OpCode code, int operandsize, Span<byte> operands)
        {
            if (ILHelper.IsArgS(code))
            {
                if (operandsize == 4)
                {
                    int val = BinaryPrimitives.ReadInt32LittleEndian(operands);
                    IL.Emit(code, GetIndex(val));
                }
                else if (operandsize == 2)
                {
                    short val = BinaryPrimitives.ReadInt16LittleEndian(operands);
                    IL.Emit(code, (short)GetIndex(val));
                }
                else if (operandsize == 1)
                {
                    byte val = operands[0];
                    IL.Emit(code, (byte)GetIndex(val));
                }
            }
            else if (ILHelper.IsArgNotS(code))
            {
                (OpCode code2, int val) = ILHelper.ConvertToS(code);
                IL.Emit(code2, (byte)GetIndex(val));
            }
            else
                return false;

            return true;
        }
        
    }

}
