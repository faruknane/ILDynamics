using System;

namespace ILDynamics
{
    class Program
    {
        static int a;
        public static ref int f()
        {
            return ref a;
        }

        static void Main(string[] args)
        {
            ILMethod f = new ILMethod(typeof(int));
            var a = f.NewVariable(typeof(int));
            a.Assign(f.Constant(5));

            var b = f.CreateReference(a);
            b.RefAssign(f.Constant(3));
            f.Return(a);

            var method = f.Create();

            Console.WriteLine(method.Invoke(null, new object[] {  }));
        }
    }
}
