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

        public static int experiment1 = 5;
        public static void Method1(int x)
        {
            experiment1 += x;
        }

        static void Main(string[] args)
        {
            Method f = new Method(null);
            var p = f.NewParam<int>();
            f.StaticCall(typeof(Program).GetMethod("Method1"), p);
            f.StaticCall(typeof(Console).GetMethod("WriteLine", new Type[] { typeof(int)}), p);
            //crete a static variable that represents static outsider variables. 
            f.Return();
            f.Create();
            _ = f[3];
            Console.WriteLine(experiment1);
        }
    }

}
