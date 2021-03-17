using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILDynamics
{
    public static class ILHelper
    {
        //todo need all primitives
        public static void LoadValueByRef(ILOpCodes codes, Type t)
        {
            if (t == typeof(float))
                codes.Emit(OpCodes.Ldind_R4);
            if (t == typeof(int))
                codes.Emit(OpCodes.Ldind_I4);
            else
                codes.Emit(OpCodes.Ldind_Ref);
        }

        //todo need all primitives
        public static void StoreValueByRef(ILOpCodes codes, Type t)
        {
            if (t == typeof(float))
                codes.Emit(OpCodes.Stind_R4);
            if (t == typeof(int))
                codes.Emit(OpCodes.Stind_I4);
            else
                codes.Emit(OpCodes.Stind_Ref);
        }

        //todo need all primitives
        public static void GetRef(ILOpCodes codes, Type t)
        {
            throw new NotImplementedException();
        }

        //todo need all primitives
        public static void LoadConstant<T>(ILOpCodes codes, T val)
        {
            if (typeof(T) == typeof(int))
            {
                codes.Emit(OpCodes.Ldc_I4, (int)(object)val);
            }
            if (typeof(T) == typeof(float))
            {
                codes.Emit(OpCodes.Ldc_R4, (float)(object)val);
            }
            else
                throw new NotImplementedException();
        }

    }
}
