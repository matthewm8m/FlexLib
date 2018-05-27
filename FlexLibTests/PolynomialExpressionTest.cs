using System;
using FlexLib;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlexLibTests
{
    [TestClass]
    public class PolynomialExpressionTest
    {
        private PolynomialExpression ExpressionA, ExpressionB, ExpressionC, ExpressionD, ExpressionE;

        [TestInitialize]
        public void TestInitialize()
        {
            // Create Expression A as 3x^5+2x^2+x
            ExpressionA = new PolynomialExpression(
            new PolynomialTerm[]
            {
                new PolynomialTerm(1.0, 1.0),
                new PolynomialTerm(2.0, 2.0),
                new PolynomialTerm(3.0, 5.0)
            });

            // Create Expression B as x^0.5+x^-0.5
            ExpressionB = new PolynomialExpression(
            new List<PolynomialTerm>()
            {
                new PolynomialTerm(1.0, 0.5),
                new PolynomialTerm(1.0, -0.5)
            });

            // Create Expression C as x+3
            ExpressionC = new PolynomialExpression(
            new double[]
            {
                1.0,
                3.0
            },
            new double[]
            {
                1.0,
                0.0
            });

            // Create Expression D as x^-1+5x^-2
            ExpressionD = new PolynomialExpression(
            new List<double>()
            {
                1.0,
                5.0
            },
            new List<double>()
            {
                -1.0,
                -2.0
            });

            // Attempt to create Expression E with incorrect number of properties
            Assert.ThrowsException<ArgumentException>(() =>
                ExpressionE = new PolynomialExpression(
                    new double[]
                    {
                        1.0
                    },
                    new double[]
                    {
                        1.0,
                        2.0
                    }));
            Assert.ThrowsException<ArgumentException>(() =>
                ExpressionE = new PolynomialExpression(
                    new List<double>()
                    {
                        1.0
                    },
                    new List<double>()
                    {
                        1.0,
                        2.0
                    }));
        }

        [TestMethod]
        public void TestEvaluation()
        {
            // Test Expression A
            Assert.AreEqual(0.0, ExpressionA.Evaluate(0.0));
            Assert.AreEqual(6.0, ExpressionA.Evaluate(1.0));
            Assert.AreEqual(-2.0, ExpressionA.Evaluate(-1.0));
            Assert.AreEqual(3108.0, ExpressionA.Evaluate(4.0));

            // Test Expression B
            Assert.AreEqual(double.PositiveInfinity, ExpressionB.Evaluate(0.0));
            Assert.AreEqual(2.0, ExpressionB.Evaluate(1.0));
            Assert.AreEqual(double.NaN, ExpressionB.Evaluate(-1.0));
            Assert.AreEqual(2.5, ExpressionB.Evaluate(4.0));

            // Test Expression C
            Assert.AreEqual(3.0, ExpressionC.Evaluate(0.0));
            Assert.AreEqual(4.0, ExpressionC.Evaluate(1.0));
            Assert.AreEqual(2.0, ExpressionC.Evaluate(-1.0));
            Assert.AreEqual(7.0, ExpressionC.Evaluate(4.0));

            // Test Expression D
            Assert.AreEqual(double.PositiveInfinity, ExpressionD.Evaluate(0.0));
            Assert.AreEqual(6.0, ExpressionD.Evaluate(1.0));
            Assert.AreEqual(4.0, ExpressionD.Evaluate(-1.0));
            Assert.AreEqual(0.5625, ExpressionD.Evaluate(4.0));
        }

        [TestMethod]
        public void TestDerivative()
        {
            // Test Expression A
            PolynomialExpression ExpressionAPrime = ExpressionA.Derivative();
            Assert.AreEqual(1.0, ExpressionAPrime.Evaluate(0.0));
            Assert.AreEqual(20.0, ExpressionAPrime.Evaluate(1.0));
            Assert.AreEqual(12.0, ExpressionAPrime.Evaluate(-1.0));
            Assert.AreEqual(3857.0, ExpressionAPrime.Evaluate(4.0));

            PolynomialExpression ExpressionAPrime3 = ExpressionA.Derivative(3);
            Assert.AreEqual(0.0, ExpressionAPrime3.Evaluate(0.0));
            Assert.AreEqual(180.0, ExpressionAPrime3.Evaluate(1.0));
            Assert.AreEqual(180.0, ExpressionAPrime3.Evaluate(-1.0));
            Assert.AreEqual(2880.0, ExpressionAPrime3.Evaluate(4.0));

            // Test Expression B
            PolynomialExpression ExpressionBPrime = ExpressionB.Derivative();
            Assert.AreEqual(double.NaN, ExpressionBPrime.Evaluate(0.0));
            Assert.AreEqual(0.0, ExpressionBPrime.Evaluate(1.0));
            Assert.AreEqual(double.NaN, ExpressionBPrime.Evaluate(-1.0));
            Assert.AreEqual(0.1875, ExpressionBPrime.Evaluate(4.0));

            PolynomialExpression ExpressionBPrime3 = ExpressionB.Derivative(3);
            Assert.AreEqual(double.NaN, ExpressionBPrime3.Evaluate(0.0));
            Assert.AreEqual(-1.5, ExpressionBPrime3.Evaluate(1.0));
            Assert.AreEqual(double.NaN, ExpressionBPrime3.Evaluate(-1.0));
            Assert.AreEqual(-0.0029296875, ExpressionBPrime3.Evaluate(4.0));

            // Test Expression C
            PolynomialExpression ExpressionCPrime = ExpressionC.Derivative();
            Assert.AreEqual(1.0, ExpressionCPrime.Evaluate(0.0));
            Assert.AreEqual(1.0, ExpressionCPrime.Evaluate(1.0));
            Assert.AreEqual(1.0, ExpressionCPrime.Evaluate(-1.0));
            Assert.AreEqual(1.0, ExpressionCPrime.Evaluate(4.0));

            PolynomialExpression ExpressionCPrime3 = ExpressionC.Derivative(3);
            Assert.AreEqual(0.0, ExpressionCPrime3.Evaluate(0.0));
            Assert.AreEqual(0.0, ExpressionCPrime3.Evaluate(1.0));
            Assert.AreEqual(0.0, ExpressionCPrime3.Evaluate(-1.0));
            Assert.AreEqual(0.0, ExpressionCPrime3.Evaluate(4.0));

            // Test Expression D
            PolynomialExpression ExpressionDPrime = ExpressionD.Derivative();
            Assert.AreEqual(double.NegativeInfinity, ExpressionDPrime.Evaluate(0.0));
            Assert.AreEqual(-11.0, ExpressionDPrime.Evaluate(1.0));
            Assert.AreEqual(9.0, ExpressionDPrime.Evaluate(-1.0));
            Assert.AreEqual(-0.21875, ExpressionDPrime.Evaluate(4.0));

            PolynomialExpression ExpressionDPrime3 = ExpressionD.Derivative(3);
            Assert.AreEqual(double.NegativeInfinity, ExpressionDPrime3.Evaluate(0.0));
            Assert.AreEqual(-126.0, ExpressionDPrime3.Evaluate(1.0));
            Assert.AreEqual(114.0, ExpressionDPrime3.Evaluate(-1.0));
            Assert.AreEqual(-0.140625, ExpressionDPrime3.Evaluate(4.0));
        }

        [TestMethod]
        public void TestAntiderivative()
        {
            // Test Expression A
            PolynomialExpression ExpressionAPrime = ExpressionA.Antiderivative();
            Assert.AreEqual(0.0, ExpressionAPrime.Evaluate(0.0));
            Assert.AreEqual(5.0 / 3.0, ExpressionAPrime.Evaluate(1.0), 1.0e-8);
            Assert.AreEqual(1.0 / 3.0, ExpressionAPrime.Evaluate(-1.0), 1.0e-8);
            Assert.AreEqual(6296.0 / 3.0, ExpressionAPrime.Evaluate(4.0), 1.0e-8);

            PolynomialExpression ExpressionAPrime3 = ExpressionA.Antiderivative(3);
            Assert.AreEqual(0.0, ExpressionAPrime3.Evaluate(0.0));
            Assert.AreEqual(1.0 / 112.0 + 1.0 / 30.0 + 1.0 / 24.0, ExpressionAPrime3.Evaluate(1.0), 1.0e-8);
            Assert.AreEqual(1.0 / 112.0 - 1.0 / 30.0 + 1.0 / 24.0, ExpressionAPrime3.Evaluate(-1.0), 1.0e-8);
            Assert.AreEqual(65536.0 / 112.0 + 1024.0 / 30.0 + 256.0 / 24.0, ExpressionAPrime3.Evaluate(4.0), 1.0e-8);

            // Test Expression B
            PolynomialExpression ExpressionBPrime = ExpressionB.Antiderivative();
            Assert.AreEqual(0.0, ExpressionBPrime.Evaluate(0.0));
            Assert.AreEqual(8.0 / 3.0, ExpressionBPrime.Evaluate(1.0), 1.0e-8);
            Assert.AreEqual(double.NaN, ExpressionBPrime.Evaluate(-1.0));
            Assert.AreEqual(28.0 / 3.0, ExpressionBPrime.Evaluate(4.0), 1.0e-8);

            PolynomialExpression ExpressionBPrime3 = ExpressionB.Antiderivative(3);
            Assert.AreEqual(0.0, ExpressionBPrime3.Evaluate(0.0));
            Assert.AreEqual(8.0 / 105.0 + 8.0 / 15.0, ExpressionBPrime3.Evaluate(1.0), 1.0e-8);
            Assert.AreEqual(double.NaN, ExpressionBPrime3.Evaluate(-1.0));
            Assert.AreEqual(1024.0 / 105.0 + 256.0 / 15.0, ExpressionBPrime3.Evaluate(4.0), 1.0e-8);

            // Test Expression C
            PolynomialExpression ExpressionCPrime = ExpressionC.Antiderivative();
            Assert.AreEqual(0.0, ExpressionCPrime.Evaluate(0.0));
            Assert.AreEqual(3.5, ExpressionCPrime.Evaluate(1.0));
            Assert.AreEqual(-2.5, ExpressionCPrime.Evaluate(-1.0));
            Assert.AreEqual(20.0, ExpressionCPrime.Evaluate(4.0));

            PolynomialExpression ExpressionCPrime3 = ExpressionC.Antiderivative(3);
            Assert.AreEqual(0.0, ExpressionCPrime3.Evaluate(0.0));
            Assert.AreEqual(13.0 / 24.0, ExpressionCPrime3.Evaluate(1.0));
            Assert.AreEqual(-11.0 / 24.0, ExpressionCPrime3.Evaluate(-1.0));
            Assert.AreEqual(256.0 / 24.0 + 32.0, ExpressionCPrime3.Evaluate(4.0));

            // Test Expression D
            PolynomialExpression ExpressionDPrime = ExpressionD.Antiderivative();
            Assert.AreEqual(double.NaN, ExpressionDPrime.Evaluate(0.0));
            Assert.AreEqual(double.NaN, ExpressionDPrime.Evaluate(1.0));
            Assert.AreEqual(double.NaN, ExpressionDPrime.Evaluate(-1.0));
            Assert.AreEqual(double.NaN, ExpressionDPrime.Evaluate(4.0));

            PolynomialExpression ExpressionDPrime3 = ExpressionD.Antiderivative(3);
            Assert.AreEqual(double.NaN, ExpressionDPrime3.Evaluate(0.0));
            Assert.AreEqual(double.NaN, ExpressionDPrime3.Evaluate(1.0));
            Assert.AreEqual(double.NaN, ExpressionDPrime3.Evaluate(-1.0));
            Assert.AreEqual(double.NaN, ExpressionDPrime3.Evaluate(4.0));
        }
    }
}
