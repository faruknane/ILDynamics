using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ILDynamics;

namespace ILDynamics.Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void TestMethod1()
        {
            StaticMethod<int> f = new StaticMethod<int>();
            var p = f.NewParam<int>();
            var v = f.NewVar<int>();

            v.Assign(f.Sum(p, f.Constant(2), f.Constant(3)));

            f.Return(f.Sum(v, p));

            f.Create();

            int val = f[10]; // execute the method!
            Assert.AreEqual(val, 25);
        }

        [TestMethod]
        public void TestMethod2()
        {
            StaticMethod<int> f = new StaticMethod<int>();
            var v = f.NewVar<int>();

            v.Assign(f.Sum(f.Constant(2), f.Constant(3)));

            var p = f.NewParam<int>();

            f.Return(f.Sum(v, p));

            f.Create();

            int val = f[10];
            Assert.AreEqual(val, 15);
        }

        [TestMethod]
        public void TestRef()
        {
            StaticMethod<int> f = new StaticMethod<int>();
            var a = f.NewVar<int>();
            a.Assign(f.Constant(5));

            var b = f.NewRefVar(a);
            b.RefAssign(f.Constant(3));
            f.Return(a);

            f.Create();

            int val = f[null];
            Assert.AreEqual(val, 3);
        }

        [TestMethod]
        public void TestRef2()
        {
            StaticMethod<int> f = new StaticMethod<int>();
            var a = f.NewVar<int>();
            a.Assign(f.Constant(5));

            var b = f.NewRefVar(a);
            b.RefAssign(f.Constant(3));

            var c = f.NewVar<int>();
            c.Assign(f.Constant(15));

            b.Assign(f.GetRefByVar(c));
            b.RefAssign(f.Constant(5));

            f.Return(f.Sum(a, c));

            f.Create();

            int val = f[null];
            Assert.AreEqual(val, 8);
        }
    }
}
