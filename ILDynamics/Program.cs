using System;

namespace ILDynamics
{
    class Program
    {

        static void Main(string[] args)
        {
            StaticMethod f = new StaticMethod(typeof(int));
            var a = f.NewVariable(typeof(int));
            a.Assign(f.Constant(5));

            var b = f.Reference(a);
            b.RefAssign(f.Constant(3));
            f.Return(f.ValueByRef(b));

            var method = f.Create();

            int val = (int)method.Invoke(null, new object[] { });
            Console.WriteLine(val);
        }
    }

}
