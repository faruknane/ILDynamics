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

        public override bool ApplyFilter(OpCode code, int operandsize, Span<byte> operands)
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

namespace ILDynamics
{
    public static partial class ILHelper
    {
        public static int GetOperandSize(this OpCode code, Span<byte> arr)
        {
            return code.OperandType switch
            {
                OperandType.InlineBrTarget => 4,
                OperandType.InlineField => 4,
                OperandType.InlineI => 4,
                OperandType.InlineI8 => 8,
                OperandType.InlineMethod => 4,
                OperandType.InlineR => 8,
                OperandType.InlineString => 4,
                OperandType.InlineSwitch => BinaryPrimitives.ReadInt32LittleEndian(arr) * 4 + 4,
                OperandType.InlineTok => 4,
                OperandType.InlineType => 4,
                OperandType.InlineVar => 2,
                OperandType.ShortInlineBrTarget => 1,
                OperandType.ShortInlineI => 1,
                OperandType.ShortInlineR => 4,
                OperandType.ShortInlineVar => 1,
                OperandType.InlineNone => 0,
                OperandType.InlinePhi => throw new NotImplementedException(),
                OperandType.InlineSig => throw new NotImplementedException(),
                _ => throw new NotImplementedException("Unsupported Operand Type!"),
            };
        }

        public static bool IsArgS(OpCode code)
        {
            try
            {
                return code.Name.EndsWith("arg.s") && code.GetOperandSize(null) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsArgNotS(OpCode code)
        {
            try
            {
                int l = code.Name.Length;
                int val = int.Parse(code.Name.Last().ToString());
                return code.Name.AsSpan(0, l - 1).EndsWith("arg.") && 0 <= val && val < 10 && code.GetOperandSize(null) == 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsLocS(OpCode code)
        {
            try
            {
                return code.Name.EndsWith("loc.s") && code.GetOperandSize(null) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsLocNotS(OpCode code)
        {
            try
            {
                int l = code.Name.Length;
                int val = int.Parse(code.Name.Last().ToString());
                return code.Name.AsSpan(0, l - 1).EndsWith("loc.") && 0 <= val && val < 10 && code.GetOperandSize(null) == 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsS(OpCode code)
        {
            try
            {
                return code.Name.EndsWith("s") && code.GetOperandSize(null) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsArg(OpCode code)
        {
            return code.Name.Contains("arg");
        }
        public static bool IsLoc(OpCode code)
        {
            return code.Name.Contains("loc");
        }

        public static (OpCode, int) ConvertToS(OpCode code)
        {
            string name = code.Name;
            string sub = name.Substring(0, name.Length - 1);
            int arg = int.Parse(name.Last().ToString());

            FieldInfo[] fields = typeof(OpCodes).GetFields();
            foreach (var f in fields)
            {
                if (f.FieldType == typeof(OpCode))
                {
                    if (f.GetValue(null) is OpCode c)
                        if (c.Name == sub + "s")
                            return (c, arg);
                }
            }
            throw new Exception("Not Found!");
        }

        public static OpCode ConvertFromS(OpCode code, int arg)
        {
            string name = code.Name;
            string sub = name.Substring(0, name.Length - 1);

            FieldInfo[] fields = typeof(OpCodes).GetFields();
            foreach (var f in fields)
            {
                if (f.FieldType == typeof(OpCode))
                {
                    if (f.GetValue(null) is OpCode c)
                        if (c.Name == sub + arg.ToString())
                            return c;
                }
            }
            throw new Exception("Not Found!");
        }
    }
}