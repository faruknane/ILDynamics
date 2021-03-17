using System;

namespace ILDynamics
{
    class Program
    {
        static void Main(string[] args)
        {
            ILMethod f = new ILMethod(typeof(int));
            var p = f.NewParameter(typeof(int));
            var v = f.NewVariable(typeof(int));
            v.Assign(f.Sum(p, f.Constant(2), f.Constant(3)));
            f.Return(f.Sum(v, p));
            var method = f.Create();

            Console.WriteLine(method.Invoke(null, new object[] { 10 }));
        }
    }
}
