using System;
using FlexLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlexLibTests
{
    [TestClass]
    public class SineTermTest
    {
        SineTerm TermA, TermB, TermC, TermD, TermE, TermF;

        [TestInitialize]
        public void TestInitialize()
        {
            TermA = new SineTerm(SineTerm.SinusoidalType.SINE, 1.0, 1.0, 0.0);
            TermB = new SineTerm(SineTerm.SinusoidalType.SINE, 3.0, 2.0, 4.0);
            TermC = new SineTerm(SineTerm.SinusoidalType.SINE, -2.0, 2.0, -1.0);
            TermD = new SineTerm(SineTerm.SinusoidalType.COSINE, 1.0, 1.0, 0.0);
            TermE = new SineTerm(SineTerm.SinusoidalType.COSINE, 2.0, 5.0, 3.0);
            TermF = new SineTerm(SineTerm.SinusoidalType.COSINE, -4.0, 1.0, -2.0);
        }

        [TestMethod]
        public void TestEvaluation()
        {
            // Test Term A
            Assert.AreEqual(0.000000000000, TermA.Evaluate(0.0), 1e-8);
            Assert.AreEqual(1.000000000000, TermA.Evaluate(Math.PI / 2.0), 1e-8);
            Assert.AreEqual(0.000000000000, TermA.Evaluate(Math.PI), 1e-8);
            Assert.AreEqual(0.841470984808, TermA.Evaluate(1.0), 1e-8);

            // Test Term B
            Assert.AreEqual(-2.27040748592, TermB.Evaluate(0.0), 1e-8);
            Assert.AreEqual(2.27040748592, TermB.Evaluate(Math.PI / 2.0), 1e-8);
            Assert.AreEqual(-2.27040748592, TermB.Evaluate(Math.PI), 1e-8);
            Assert.AreEqual(-0.838246494597, TermB.Evaluate(1.0), 1e-8);

            // Test Term C
            Assert.AreEqual(1.68294196962, TermC.Evaluate(0.0), 1e-8);
            Assert.AreEqual(-1.68294196962, TermC.Evaluate(Math.PI / 2.0), 1e-8);
            Assert.AreEqual(1.68294196962, TermC.Evaluate(Math.PI), 1e-8);
            Assert.AreEqual(-1.68294196962, TermC.Evaluate(1.0), 1e-8);

            // Test Term D
            Assert.AreEqual(1.000000000000, TermD.Evaluate(0.0), 1e-8);
            Assert.AreEqual(0.000000000000, TermD.Evaluate(Math.PI / 2.0), 1e-8);
            Assert.AreEqual(-1.000000000000, TermD.Evaluate(Math.PI), 1e-8);
            Assert.AreEqual(0.540302305868, TermD.Evaluate(1.0), 1e-8);

            // Test Term E
            Assert.AreEqual(-1.9799849932, TermE.Evaluate(0.0), 1e-8);
            Assert.AreEqual(-0.28224001612, TermE.Evaluate(Math.PI / 2.0), 1e-8);
            Assert.AreEqual(1.9799849932, TermE.Evaluate(Math.PI), 1e-8);
            Assert.AreEqual(-0.291000067617, TermE.Evaluate(1.0), 1e-8);

            // Test Term F
            Assert.AreEqual(1.66458734619, TermF.Evaluate(0.0), 1e-8);
            Assert.AreEqual(-3.6371897073, TermF.Evaluate(Math.PI / 2.0), 1e-8);
            Assert.AreEqual(-1.66458734619, TermF.Evaluate(Math.PI), 1e-8);
            Assert.AreEqual(-2.16120922347, TermF.Evaluate(1.0), 1e-8);
        }

        [TestMethod]
        public void TestDerivative()
        {
            // Test Term A
            SineTerm TermAPrime = TermA.Derivative();
            Assert.AreEqual(SineTerm.SinusoidalType.COSINE, TermAPrime.Type);
            Assert.AreEqual(1.0, TermAPrime.Amplitude);
            Assert.AreEqual(1.0, TermAPrime.Frequency);
            Assert.AreEqual(0.0, TermAPrime.Shift);

            // Test Term B
            SineTerm TermBPrime = TermB.Derivative();
            Assert.AreEqual(SineTerm.SinusoidalType.COSINE, TermBPrime.Type);
            Assert.AreEqual(6.0, TermBPrime.Amplitude);
            Assert.AreEqual(2.0, TermBPrime.Frequency);
            Assert.AreEqual(4.0, TermBPrime.Shift);

            // Test Term C
            SineTerm TermCPrime = TermC.Derivative();
            Assert.AreEqual(SineTerm.SinusoidalType.COSINE, TermCPrime.Type);
            Assert.AreEqual(-4.0, TermCPrime.Amplitude);
            Assert.AreEqual(2.0, TermCPrime.Frequency);
            Assert.AreEqual(-1.0, TermCPrime.Shift);

            // Test Term D
            SineTerm TermDPrime = TermD.Derivative();
            Assert.AreEqual(SineTerm.SinusoidalType.SINE, TermDPrime.Type);
            Assert.AreEqual(-1.0, TermDPrime.Amplitude);
            Assert.AreEqual(1.0, TermDPrime.Frequency);
            Assert.AreEqual(0.0, TermDPrime.Shift);

            // Test Term E
            SineTerm TermEPrime = TermE.Derivative();
            Assert.AreEqual(SineTerm.SinusoidalType.SINE, TermEPrime.Type);
            Assert.AreEqual(-10.0, TermEPrime.Amplitude);
            Assert.AreEqual(5.0, TermEPrime.Frequency);
            Assert.AreEqual(3.0, TermEPrime.Shift);

            // Test Term F
            SineTerm TermFPrime = TermF.Derivative();
            Assert.AreEqual(SineTerm.SinusoidalType.SINE, TermFPrime.Type);
            Assert.AreEqual(4.0, TermFPrime.Amplitude);
            Assert.AreEqual(1.0, TermFPrime.Frequency);
            Assert.AreEqual(-2.0, TermFPrime.Shift);
        }

        [TestMethod]
        public void TestAntiderivative()
        {
            // Test Term A
            SineTerm TermAPrime = TermA.Antiderivative();
            Assert.AreEqual(SineTerm.SinusoidalType.COSINE, TermAPrime.Type);
            Assert.AreEqual(-1.0, TermAPrime.Amplitude);
            Assert.AreEqual(1.0, TermAPrime.Frequency);
            Assert.AreEqual(0.0, TermAPrime.Shift);

            // Test Term B
            SineTerm TermBPrime = TermB.Antiderivative();
            Assert.AreEqual(SineTerm.SinusoidalType.COSINE, TermBPrime.Type);
            Assert.AreEqual(-1.5, TermBPrime.Amplitude);
            Assert.AreEqual(2.0, TermBPrime.Frequency);
            Assert.AreEqual(4.0, TermBPrime.Shift);

            // Test Term C
            SineTerm TermCPrime = TermC.Antiderivative();
            Assert.AreEqual(SineTerm.SinusoidalType.COSINE, TermCPrime.Type);
            Assert.AreEqual(1.0, TermCPrime.Amplitude);
            Assert.AreEqual(2.0, TermCPrime.Frequency);
            Assert.AreEqual(-1.0, TermCPrime.Shift);

            // Test Term D
            SineTerm TermDPrime = TermD.Antiderivative();
            Assert.AreEqual(SineTerm.SinusoidalType.SINE, TermDPrime.Type);
            Assert.AreEqual(1.0, TermDPrime.Amplitude);
            Assert.AreEqual(1.0, TermDPrime.Frequency);
            Assert.AreEqual(0.0, TermDPrime.Shift);

            // Test Term E
            SineTerm TermEPrime = TermE.Antiderivative();
            Assert.AreEqual(SineTerm.SinusoidalType.SINE, TermEPrime.Type);
            Assert.AreEqual(0.4, TermEPrime.Amplitude);
            Assert.AreEqual(5.0, TermEPrime.Frequency);
            Assert.AreEqual(3.0, TermEPrime.Shift);

            // Test Term F
            SineTerm TermFPrime = TermF.Antiderivative();
            Assert.AreEqual(SineTerm.SinusoidalType.SINE, TermFPrime.Type);
            Assert.AreEqual(-4.0, TermFPrime.Amplitude);
            Assert.AreEqual(1.0, TermFPrime.Frequency);
            Assert.AreEqual(-2.0, TermFPrime.Shift);
        }
    }
}
