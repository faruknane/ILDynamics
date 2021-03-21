using System;

namespace ILDynamics
{

    class Program
    {

        public static string F(int a)
        {
            string res = a.ToString();
            return res;
        }

        static void Main(string[] args)
        {
            Method<string> f = new Method<string>();
            var a = f.NewParam<int>();

            var mmm = typeof(int).GetMethod("ToString", Array.Empty<Type>());

            f.Return(a.Call(mmm));

            f.Create();

            string val = f[5];
            Console.WriteLine(val);
        }
    }

}
