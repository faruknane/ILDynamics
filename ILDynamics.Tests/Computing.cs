using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ILDynamics;
using ILDynamics.Compute;

namespace ILDynamics.Tests
{
    [TestClass]
    public class Computing
    {
        [TestMethod]
        public void ShapeTest1()
        {
            Shape s = new Shape(5);
            Shape s2 = new Shape(5);

            Assert.AreEqual(s, s2);


            Shape s3 = new Shape(5, 1);
            Shape s4 = new Shape(5);

            Assert.AreNotEqual(s3, s4);
        }
    }
}
