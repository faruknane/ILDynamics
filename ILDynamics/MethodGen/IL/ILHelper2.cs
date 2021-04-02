using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILDynamics.MethodGen.IL
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
#pragma warning disable CS0618 // 'OperandType.InlinePhi' is obsolete: 'This API has been deprecated. https://go.microsoft.com/fwlink/?linkid=14202'
                OperandType.InlinePhi => throw new NotImplementedException(),
#pragma warning restore CS0618 // 'OperandType.InlinePhi' is obsolete: 'This API has been deprecated. https://go.microsoft.com/fwlink/?linkid=14202'
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
            string sub = name[0..^1];
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
            string sub = name[0..^1];

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
