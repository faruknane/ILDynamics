using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ILDynamics;

namespace ILDynamics.Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void TestAdd1()
        {
            Method<int> f = new Method<int>();
            var p = f.NewParam<int>();
            var v = f.NewVar<int>();

            v.Assign(f.Add(p, f.Constant(2), f.Constant(3)));

            f.Return(f.Add(v, p));

            f.Create();

            int val = f[10]; // execute the method!
            Assert.AreEqual(val, 25);
        }

        [TestMethod]
        public void TestAdd2()
        {
            Method<int> f = new Method<int>();
            var v = f.NewVar<int>();

            v.Assign(f.Add(f.Constant(2), f.Constant(3)));

            var p = f.NewParam<int>();

            f.Return(f.Add(v, p));

            f.Create();

            int val = f[10];
            Assert.AreEqual(val, 15);
        }

        [TestMethod]
        public void TestSub1()
        {
            Method<int> f = new Method<int>();
            var v = f.NewVar<int>();
            v.Assign(f.Sub(f.Constant(2), f.Constant(3)));
            f.Return(v);
            f.Create();
            int val = f[null]; // execute the method!
            Assert.AreEqual(val, -1);
        }

        [TestMethod]
        public void TestSub2()
        {
            Method<int> f = new Method<int>();
            var v = f.NewVar<int>();
            v.Assign(f.Sub(f.Constant(2), f.Sub(f.Constant(5), f.Constant(3))));
            f.Return(v);
            f.Create();
            int val = f[null]; // execute the method!
            Assert.AreEqual(val, 0);
        }

        [TestMethod]
        public void TestRef()
        {
            Method<int> f = new Method<int>();
            Var a = f.NewVar<int>();
            a.Assign(f.Constant(5));

            RefVar b = f.NewRefVar(a);
            b.RefAssign(f.Constant(3));
            f.Return(a);

            f.Create();

            int val = f[null];
            Assert.AreEqual(val, 3);
        }

        [TestMethod]
        public void TestRefOp()
        {
            Method<int> f = new Method<int>();
            Var a = f.NewVar<int>();
            a.Assign(f.Constant(5));

            RefVar b = f.NewRefVar(a);
            b.RefAssign(f.Constant(3));

            Var c = f.NewVar<int>();
            c.Assign(f.Constant(15));

            b.Assign(f.GetRefByVar(c));
            b.RefAssign(f.Constant(5));

            f.Return(f.Add(a, c));

            f.Create();

            int val = f[null];
            Assert.AreEqual(val, 8);
        }

        [TestMethod]
        public void TestOpCall1()
        {
            Method<string> f = new Method<string>();
            var a = f.NewParam<int>();
            var tostr = typeof(int).GetMethod("ToString", Array.Empty<Type>());
            f.Return(a.Call(tostr));
            f.Create();
            string val = f[5];
            Assert.AreEqual(val, "5");
        }

    }
}
