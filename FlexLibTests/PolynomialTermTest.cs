using System;
using FlexLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlexLibTests
{
    [TestClass]
    public class PolynomialTermTest
    {
        private PolynomialTerm TermA, TermB, TermC, TermD, TermE, TermF, TermG;

        [TestInitialize]
        public void TestInitialize()
        {
            TermA = new PolynomialTerm(1.0, 1.0);
            TermB = new PolynomialTerm(5.0, 3.0);
            TermC = new PolynomialTerm(2.0, 2.0);
            TermD = new PolynomialTerm(-1.5, 0.5);
            TermE = new PolynomialTerm(7.0, -0.25);
            TermF = new PolynomialTerm(2.0, 0.0);
            TermG = new PolynomialTerm(5.0, -1.0);
        }

        [TestMethod]
        public void TestEvaluation()
        {
            // Test Term A
            Assert.AreEqual(1.0, TermA.Evaluate(1.0));
            Assert.AreEqual(2.0, TermA.Evaluate(2.0));
            Assert.AreEqual(-1.0, TermA.Evaluate(-1.0));
            Assert.AreEqual(0.0, TermA.Evaluate(0.0));

            // Test Term B
            Assert.AreEqual(5.0, TermB.Evaluate(1.0));
            Assert.AreEqual(40.0, TermB.Evaluate(2.0));
            Assert.AreEqual(-5.0, TermB.Evaluate(-1.0));
            Assert.AreEqual(0.0, TermB.Evaluate(0.0));

            // Test Term C
            Assert.AreEqual(2.0, TermC.Evaluate(1.0));
            Assert.AreEqual(8.0, TermC.Evaluate(2.0));
            Assert.AreEqual(2.0, TermC.Evaluate(-1.0));
            Assert.AreEqual(0.0, TermC.Evaluate(0.0));

            // Test Term D
            Assert.AreEqual(-1.5, TermD.Evaluate(1.0));
            Assert.AreEqual(-3.0, TermD.Evaluate(4.0));
            Assert.AreEqual(double.NaN, TermD.Evaluate(-1.0));
            Assert.AreEqual(0.0, TermD.Evaluate(0.0));

            // Test Term E
            Assert.AreEqual(7.0, TermE.Evaluate(1.0));
            Assert.AreEqual(3.5, TermE.Evaluate(16.0));
            Assert.AreEqual(double.NaN, TermE.Evaluate(-1.0));
            Assert.AreEqual(double.PositiveInfinity, TermE.Evaluate(0.0));

            // Test Term F
            Assert.AreEqual(2.0, TermF.Evaluate(1.0));
            Assert.AreEqual(2.0, TermF.Evaluate(2.0));
            Assert.AreEqual(2.0, TermF.Evaluate(-1.0));
            Assert.AreEqual(2.0, TermF.Evaluate(0.0));

            // Test Term G
            Assert.AreEqual(5.0, TermG.Evaluate(1.0));
            Assert.AreEqual(2.5, TermG.Evaluate(2.0));
            Assert.AreEqual(-5.0, TermG.Evaluate(-1.0));
            Assert.AreEqual(double.PositiveInfinity, TermG.Evaluate(0.0));
        }

        [TestMethod]
        public void TestDerivative()
        {
            // Test Term A
            PolynomialTerm TermAPrime = TermA.Derivative();
            Assert.AreEqual(1.0, TermAPrime.Coefficient);
            Assert.AreEqual(0.0, TermAPrime.Degree);

            PolynomialTerm TermAPrime3 = TermA.Derivative(3);
            Assert.AreEqual(0.0, TermAPrime3.Coefficient);

            // Test Term B
            PolynomialTerm TermBPrime = TermB.Derivative();
            Assert.AreEqual(15.0, TermBPrime.Coefficient);
            Assert.AreEqual(2.0, TermBPrime.Degree);

            PolynomialTerm TermBPrime3 = TermB.Derivative(3);
            Assert.AreEqual(30.0, TermBPrime3.Coefficient);
            Assert.AreEqual(0.0, TermBPrime3.Degree);

            // Test Term C
            PolynomialTerm TermCPrime = TermC.Derivative();
            Assert.AreEqual(4.0, TermCPrime.Coefficient);
            Assert.AreEqual(1.0, TermCPrime.Degree);

            PolynomialTerm TermCPrime3 = TermC.Derivative(3);
            Assert.AreEqual(0.0, TermCPrime3.Coefficient);

            // Test Term D
            PolynomialTerm TermDPrime = TermD.Derivative();
            Assert.AreEqual(-0.75, TermDPrime.Coefficient);
            Assert.AreEqual(-0.5, TermDPrime.Degree);

            PolynomialTerm TermDPrime3 = TermD.Derivative(3);
            Assert.AreEqual(-9.0 / 16.0, TermDPrime3.Coefficient, 1.0e-8);
            Assert.AreEqual(-5.0 / 2.0, TermDPrime3.Degree, 1.0e-8);

            // Test Term E
            PolynomialTerm TermEPrime = TermE.Derivative();
            Assert.AreEqual(-1.75, TermEPrime.Coefficient);
            Assert.AreEqual(-1.25, TermEPrime.Degree);

            PolynomialTerm TermEPrime3 = TermE.Derivative(3);
            Assert.AreEqual(-315.0 / 64.0, TermEPrime3.Coefficient, 1e-8);
            Assert.AreEqual(-13.0 / 4.0, TermEPrime3.Degree, 1e-8);

            // Test Term F
            PolynomialTerm TermFPrime = TermF.Derivative();
            Assert.AreEqual(0.0, TermFPrime.Coefficient);
            Assert.AreEqual(-1.0, TermFPrime.Degree);

            PolynomialTerm TermFPrime3 = TermF.Derivative(3);
            Assert.AreEqual(0.0, TermFPrime3.Coefficient);

            // Test Term G
            PolynomialTerm TermGPrime = TermG.Derivative();
            Assert.AreEqual(-5.0, TermGPrime.Coefficient);
            Assert.AreEqual(-2.0, TermGPrime.Degree);

            PolynomialTerm TermGPrime3 = TermG.Derivative(3);
            Assert.AreEqual(-30.0, TermGPrime3.Coefficient);
            Assert.AreEqual(-4.0, TermGPrime3.Degree);
        }

        [TestMethod]
        public void TestAntiderivative()
        {
            // Test Term A
            PolynomialTerm TermAPrime = TermA.Antiderivative();
            Assert.AreEqual(0.5, TermAPrime.Coefficient);
            Assert.AreEqual(2.0, TermAPrime.Degree);

            PolynomialTerm TermAPrime3 = TermA.Antiderivative(3);
            Assert.AreEqual(1.0 / 24.0, TermAPrime3.Coefficient, 1.0e-8);
            Assert.AreEqual(4.0, TermAPrime3.Degree);

            // Test Term B
            PolynomialTerm TermBPrime = TermB.Antiderivative();
            Assert.AreEqual(1.25, TermBPrime.Coefficient);
            Assert.AreEqual(4.0, TermBPrime.Degree);

            PolynomialTerm TermBPrime3 = TermB.Antiderivative(3);
            Assert.AreEqual(1.0 / 24.0, TermBPrime3.Coefficient, 1.0e-8);
            Assert.AreEqual(6.0, TermBPrime3.Degree);

            // Test Term C
            PolynomialTerm TermCPrime = TermC.Antiderivative();
            Assert.AreEqual(2.0 / 3.0, TermCPrime.Coefficient, 1.0e-8);
            Assert.AreEqual(3.0, TermCPrime.Degree);

            PolynomialTerm TermCPrime3 = TermC.Antiderivative(3);
            Assert.AreEqual(1.0 / 30.0, TermCPrime3.Coefficient, 1.0e-8);
            Assert.AreEqual(5.0, TermCPrime3.Degree);

            // Test Term D
            PolynomialTerm TermDPrime = TermD.Antiderivative();
            Assert.AreEqual(-1.0, TermDPrime.Coefficient);
            Assert.AreEqual(1.5, TermDPrime.Degree);

            PolynomialTerm TermDPrime3 = TermD.Antiderivative(3);
            Assert.AreEqual(-4.0 / 35.0, TermDPrime3.Coefficient, 1.0e-8);
            Assert.AreEqual(3.5, TermDPrime3.Degree);

            // Test Term E
            PolynomialTerm TermEPrime = TermE.Antiderivative();
            Assert.AreEqual(28.0 / 3.0, TermEPrime.Coefficient, 1.0e-8);
            Assert.AreEqual(0.75, TermEPrime.Degree);

            PolynomialTerm TermEPrime3 = TermE.Antiderivative(3);
            Assert.AreEqual(64.0 / 33.0, TermEPrime3.Coefficient, 1.0e-8);
            Assert.AreEqual(2.75, TermEPrime3.Degree);

            // Test Term F
            PolynomialTerm TermFPrime = TermF.Antiderivative();
            Assert.AreEqual(2.0, TermFPrime.Coefficient);
            Assert.AreEqual(1.0, TermFPrime.Degree);

            PolynomialTerm TermFPrime3 = TermF.Antiderivative(3);
            Assert.AreEqual(1.0 / 3.0, TermFPrime3.Coefficient, 1.0e-8);
            Assert.AreEqual(3.0, TermFPrime3.Degree);

            // Test Term G
            PolynomialTerm TermGPrime = TermG.Antiderivative();
            Assert.AreEqual(double.NaN, TermGPrime.Coefficient);
            Assert.AreEqual(0.0, TermGPrime.Degree);

            PolynomialTerm TermGPrime3 = TermG.Antiderivative(3);
            Assert.AreEqual(double.NaN, TermGPrime3.Coefficient);
        }
    }
}
