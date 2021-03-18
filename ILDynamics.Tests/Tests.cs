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
            StaticMethod f = new StaticMethod(typeof(int));
            var p = f.NewParam(typeof(int));
            var v = f.NewVar(typeof(int));

            v.Assign(f.Sum(p, f.Constant(2), f.Constant(3)));

            f.Return(f.Sum(v, p));

            f.Create();

            int val = (int)f[10]; // execute the method!
            Assert.AreEqual(val, 25);
        }

        [TestMethod]
        public void TestMethod2()
        {
            StaticMethod f = new StaticMethod(typeof(int));
            var v = f.NewVar(typeof(int));

            v.Assign(f.Sum(f.Constant(2), f.Constant(3)));

            var p = f.NewParam(typeof(int));

            f.Return(f.Sum(v, p));

            f.Create();

            int val = (int)f[10];
            Assert.AreEqual(val, 15);
        }

        [TestMethod]
        public void TestRef()
        {
            StaticMethod f = new StaticMethod(typeof(int));
            var a = f.NewVar(typeof(int));
            a.Assign(f.Constant(5));

            var b = f.NewRefVar(a);
            b.RefAssign(f.Constant(3));
            f.Return(a);

            f.Create();

            int val = (int)f[null];
            Assert.AreEqual(val, 3);
        }

        [TestMethod]
        public void TestRef2()
        {
            StaticMethod f = new StaticMethod(typeof(int));
            var a = f.NewVar(typeof(int));
            a.Assign(f.Constant(5));

            var b = f.NewRefVar(a);
            b.RefAssign(f.Constant(3));

            var c = f.NewVar(typeof(int));
            c.Assign(f.Constant(15));

            b.Assign(f.GetRefByVar(c));
            b.RefAssign(f.Constant(5));

            f.Return(f.Sum(a, c));

            f.Create();

            int val = (int)f[null];
            Assert.AreEqual(val, 8);
        }
    }
}
