using System;
using System.Collections;
using System.Collections.Generic;

using NUnit.Framework;

using FlexLib.Algebra;

namespace FlexLibTests.Algebra
{
    [TestFixture]
    public class RealFieldTests
    {
        [TestCase(0.0, 0.0, 0.0)]
        [TestCase(1.0, 0.0, 1.0)]
        [TestCase(-1.0, 2.0, 1.0)]
        [TestCase(2.0, -1.0, 1.0)]
        [TestCase(-57.34, 23.11, -34.23)]
        public void TestAddition(double a, double b, double c)
        {
            RealFieldElement elementA = new RealFieldElement(a);
            RealFieldElement elementB = new RealFieldElement(b);
            Assert.AreEqual(c, (elementA + elementB).Value, 0.005);
        }

        [TestCase(0.0, 0.0, 0.0)]
        [TestCase(1.0, 0.0, 1.0)]
        [TestCase(-1.0, 2.0, -3.0)]
        [TestCase(2.0, -1.0, 3.0)]
        [TestCase(-57.34, 23.11, -80.45)]
        public void TestSubtraction(double a, double b, double c)
        {
            RealFieldElement elementA = new RealFieldElement(a);
            RealFieldElement elementB = new RealFieldElement(b);
            Assert.AreEqual(c, (elementA - elementB).Value, 0.005);
        }

        [TestCase(0.0, 0.0, 0.0)]
        [TestCase(1.0, 0.0, 0.0)]
        [TestCase(-1.0, 2.0, -2.0)]
        [TestCase(2.0, -1.0, -2.0)]
        [TestCase(-57.34, 23.11, -1325.1274)]
        public void TestMultiplication(double a, double b, double c)
        {
            RealFieldElement elementA = new RealFieldElement(a);
            RealFieldElement elementB = new RealFieldElement(b);
            Assert.AreEqual(c, (elementA * elementB).Value, 0.005);
        }

        [TestCase(0.0, 1.0, 0.0)]
        [TestCase(-1.0, 2.0, -0.5)]
        [TestCase(2.0, -1.0, -2.0)]
        [TestCase(-57.34, 23.11, -2.481177)]
        public void TestDivision(double a, double b, double c)
        {
            RealFieldElement elementA = new RealFieldElement(a);
            RealFieldElement elementB = new RealFieldElement(b);
            Assert.AreEqual(c, (elementA / elementB).Value, 0.005);
        }

        [TestCase(0.0, 1.0, false)]
        [TestCase(1.0, 0.0, true)]
        [TestCase(0.0, 0.0, true)]
        [TestCase(1.0, 0.000005, false)]
        [TestCase(-1.0, -0.000005, false)]
        public void TestDivideByZero(double a, double b, bool throws)
        {
            RealFieldElement elementA = new RealFieldElement(a);
            RealFieldElement elementB = new RealFieldElement(b);
            RealFieldElement elementC;
            if (throws)
            {
                Assert.Throws<DivideByZeroException>(delegate
                {
                    elementC = elementA / elementB;
                });
            }
            else
            {
                Assert.DoesNotThrow(delegate
                {
                    elementC = elementA / elementB;
                });
            }
        }

        [TestCase(0.0, 0.0, 0.0)]
        [TestCase(1.55, 1.55, -1.55)]
        [TestCase(-3.76, -3.76, 3.76)]
        public void TestPositiveNegative(double x, double positiveX, double negativeX)
        {
            RealFieldElement element = new RealFieldElement(x);
            Assert.AreEqual(positiveX, (+element).Value, 0.005);
            Assert.AreEqual(negativeX, (-element).Value, 0.005);
        }

        [TestCase(0, 0, 0.0)]
        [TestCase(1, 0, 1.0)]
        [TestCase(0, 1, 1.0)]
        [TestCase(1, 1, 2.0)]
        public void TestAddIdentities(int a, int b, double c)
        {
            RealField reals = new RealField();
            RealFieldElement elementA = a == 0 ? reals.Zero() : reals.One();
            RealFieldElement elementB = b == 0 ? reals.Zero() : reals.One();
            Assert.AreEqual(c, (elementA + elementB).Value);
        }

        [TestCase(0, 0, 0.0)]
        [TestCase(1, 0, 0.0)]
        [TestCase(0, 1, 0.0)]
        [TestCase(1, 1, 1.0)]
        public void TestMultipleIdentities(int a, int b, double c)
        {
            RealField reals = new RealField();
            RealFieldElement elementA = a == 0 ? reals.Zero() : reals.One();
            RealFieldElement elementB = b == 0 ? reals.Zero() : reals.One();
            Assert.AreEqual(c, (elementA * elementB).Value);
        }
    }
}