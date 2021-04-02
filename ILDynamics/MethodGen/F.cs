using ILDynamics.MethodGen;
using ILDynamics.MethodGen.Ops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ILDynamics.MethodGen
{
    public static class F
    {
        public static OpReturn Return()
        {
            return new OpReturn();
        }

        public static OpReturn Return(ILOp v)
        {
            return new OpReturn(v);
        }

        public static ILOp Constant<T>(T v)
        {
            return new Constant<T>(v);
        }

        public static ILOp Add(params ILOp[] objs)
        {
            return new OpAdd(objs);
        }

        public static ILOp Sub(ILOp val1, ILOp val2)
        {
            return new OpSub(val1, val2);
        }

        public static ILOp Mul(params ILOp[] objs)
        {
            return new OpMul(objs);
        }

        public static ILOp Div(ILOp val1, ILOp val2)
        {
            return new OpDiv(val1, val2);
        }

        public static ILOp GetValueByRef(RefableObject obj)
        {
            return new OpValueByRef(obj);
        }

        public static ILOp GetRefByVar(RefableObject obj)
        {
            return new OpRefByVar(obj);
        }

        public static OpCall StaticCall(MethodInfo objm, params ILOp[] parameters)
        {
            return new OpCall(null, objm, parameters);
        }
    }
}
