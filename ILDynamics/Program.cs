using ILDynamics.Resolver;
using ILDynamics.Resolver.Filters;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using ILDynamics.MethodGen.Ops;
using ILDynamics.MethodGen;
using static ILDynamics.MethodGen.F;

namespace ILDynamics
{

    public class Program
    {
        public static int experiment1 = 5;
        public static void Method1(int x)
        {
            experiment1 += x;
        }

        static void Main(string[] args)
        {
            Method f = new Method(null);
            Param p = new Param<int>();
            var seq = new OpSequence
            (   
                StaticCall(typeof(Program).GetMethod("Method1"), p),
                StaticCall(typeof(Console).GetMethod("WriteLine", new Type[] { typeof(int)}), p),
                Return()
            );
            seq.Load(f);
            f.Create();
            _ = f[3];
            Console.WriteLine(experiment1);
        }
    }

}
