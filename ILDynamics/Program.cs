using ILDynamics.Resolver;
using ILDynamics.Resolver.Filters;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace ILDynamics
{

    public class Program
    {
        public static int A(int a)
        {
            return B(a);
        }

        public static int B(int a)
        {
            return a + 5;
        }

        public static int C(int a)
        {
            return a * 5;
        }

        static void Main(string[] args)
        {
            var swapper = new MethodCallSwapper();
            swapper.AddSwap(typeof(Program).GetMethod("B"), typeof(Program).GetMethod("C"));
            var methodInfo = Resolver.Resolver.CopyMethod(typeof(Program).GetMethod("A"), swapper, new NoFilter());

            Console.WriteLine((int)methodInfo.Invoke(null, new object[] { 5 }));
        }
    }

}
