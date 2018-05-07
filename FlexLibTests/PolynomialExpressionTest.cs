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

        }

        [TestMethod]
        public void TestAntiderivative()
        {

        }
    }
}
