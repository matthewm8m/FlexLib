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
        private RealField Field;

        [SetUp]
        protected void SetUp()
        {
            // The tolerance is 2.0^-8
            Field = new RealField(0.00390625);
        }

        [TestCase(0.0, 0.0, 0.0)]
        [TestCase(1.0, 0.0, 1.0)]
        [TestCase(-1.0, 2.0, 1.0)]
        [TestCase(2.0, -1.0, 1.0)]
        [TestCase(-57.34, 23.11, -34.23)]
        public void TestAddition(double a, double b, double c)
        {
            RealFieldElement elementA = a;
            RealFieldElement elementB = b;
            RealFieldElement elementC = c;
            Assert.IsTrue(Field.ElementsEqual(Field.Add(elementA, elementB), elementC));
        }

        [TestCase(0.0, 0.0, 0.0)]
        [TestCase(1.0, 0.0, 1.0)]
        [TestCase(-1.0, 2.0, -3.0)]
        [TestCase(2.0, -1.0, 3.0)]
        [TestCase(-57.34, 23.11, -80.45)]
        public void TestSubtraction(double a, double b, double c)
        {
            RealFieldElement elementA = a;
            RealFieldElement elementB = b;
            RealFieldElement elementC = c;
            Assert.IsTrue(Field.ElementsEqual(Field.Add(elementA, Field.Negative(elementB)), elementC));
        }

        [TestCase(0.0, 0.0, 0.0)]
        [TestCase(1.0, 0.0, 0.0)]
        [TestCase(-1.0, 2.0, -2.0)]
        [TestCase(2.0, -1.0, -2.0)]
        [TestCase(-57.34, 23.11, -1325.1274)]
        public void TestMultiplication(double a, double b, double c)
        {
            RealFieldElement elementA = a;
            RealFieldElement elementB = b;
            RealFieldElement elementC = c;
            Assert.IsTrue(Field.ElementsEqual(Field.Multiply(elementA, elementB), elementC));
        }

        [TestCase(0.0, 1.0, 0.0)]
        [TestCase(-1.0, 2.0, -0.5)]
        [TestCase(2.0, -1.0, -2.0)]
        [TestCase(-57.34, 23.11, -2.481177)]
        public void TestDivision(double a, double b, double c)
        {
            RealFieldElement elementA = a;
            RealFieldElement elementB = b;
            RealFieldElement elementC = c;
            Assert.IsTrue(Field.ElementsEqual(Field.Multiply(elementA, Field.Inverse(elementB)), elementC));
        }

        [TestCase(1.0, false)]
        [TestCase(0.0, true)]
        [TestCase(0.000005, false)]
        [TestCase(-0.000005, false)]
        public void TestDivideByZero(double x, bool throws)
        {
            RealFieldElement element = x;
            RealFieldElement inverse;

            if (throws)
            {
                Assert.Throws<DivideByZeroException>(delegate
                {
                    inverse = Field.Inverse(element);
                });
            }
            else
            {
                Assert.DoesNotThrow(delegate
                {
                    inverse = Field.Inverse(element);
                });
            }
        }

        [TestCase(0, 0, 0.0)]
        [TestCase(1, 0, 1.0)]
        [TestCase(0, 1, 1.0)]
        [TestCase(1, 1, 2.0)]
        public void TestAddIdentities(int a, int b, double c)
        {
            RealFieldElement elementA = a == 0 ? Field.Zero() : Field.One();
            RealFieldElement elementB = b == 0 ? Field.Zero() : Field.One();
            Assert.IsTrue(Field.ElementsEqual(Field.Add(elementA, elementB), c));
        }

        [TestCase(0, 0, 0.0)]
        [TestCase(1, 0, 0.0)]
        [TestCase(0, 1, 0.0)]
        [TestCase(1, 1, 1.0)]
        public void TestMultiplyIdentities(int a, int b, double c)
        {
            RealFieldElement elementA = a == 0 ? Field.Zero() : Field.One();
            RealFieldElement elementB = b == 0 ? Field.Zero() : Field.One();
            Assert.IsTrue(Field.ElementsEqual(Field.Multiply(elementA, elementB), c));
        }
    }
}