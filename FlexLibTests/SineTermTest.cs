using System;
using FlexLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlexLibTests
{
    [TestClass]
    public class SineTermTest
    {
        SineTerm TermA, TermB, TermC, TermD, TermE, TermF, TermG, TermH;

        [TestInitialize]
        public void TestInitialize()
        {
            // Sine Terms
            TermA = new SineTerm(SineTerm.SinusoidalType.SINE, 1.0, 1.0, 0.0);
            TermB = new SineTerm(SineTerm.SinusoidalType.SINE, 3.0, 2.0, 4.0);
            TermC = new SineTerm(SineTerm.SinusoidalType.SINE, -2.0, 2.0, -1.0);
            TermD = new SineTerm(SineTerm.SinusoidalType.SINE, 1.0, 0.0, 0.0);

            // Cosine Terms
            TermE = new SineTerm(SineTerm.SinusoidalType.COSINE, 1.0, 1.0, 0.0);
            TermF = new SineTerm(SineTerm.SinusoidalType.COSINE, 2.0, 5.0, 3.0);
            TermG = new SineTerm(SineTerm.SinusoidalType.COSINE, -4.0, 1.0, -2.0);
            TermH = new SineTerm(SineTerm.SinusoidalType.COSINE, 1.0, 0.0, 0.0);
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
            Assert.AreEqual(0.0000000000, TermD.Evaluate(0.0), 1e-8);
            Assert.AreEqual(0.0000000000, TermD.Evaluate(Math.PI / 2.0), 1e-8);
            Assert.AreEqual(0.0000000000, TermD.Evaluate(Math.PI), 1e-8);
            Assert.AreEqual(0.0000000000, TermD.Evaluate(1.0), 1e-8);

            // Test Term E
            Assert.AreEqual(1.000000000000, TermE.Evaluate(0.0), 1e-8);
            Assert.AreEqual(0.000000000000, TermE.Evaluate(Math.PI / 2.0), 1e-8);
            Assert.AreEqual(-1.000000000000, TermE.Evaluate(Math.PI), 1e-8);
            Assert.AreEqual(0.540302305868, TermE.Evaluate(1.0), 1e-8);

            // Test Term F
            Assert.AreEqual(-1.9799849932, TermF.Evaluate(0.0), 1e-8);
            Assert.AreEqual(-0.28224001612, TermF.Evaluate(Math.PI / 2.0), 1e-8);
            Assert.AreEqual(1.9799849932, TermF.Evaluate(Math.PI), 1e-8);
            Assert.AreEqual(-0.291000067617, TermF.Evaluate(1.0), 1e-8);

            // Test Term G
            Assert.AreEqual(1.66458734619, TermG.Evaluate(0.0), 1e-8);
            Assert.AreEqual(-3.6371897073, TermG.Evaluate(Math.PI / 2.0), 1e-8);
            Assert.AreEqual(-1.66458734619, TermG.Evaluate(Math.PI), 1e-8);
            Assert.AreEqual(-2.16120922347, TermG.Evaluate(1.0), 1e-8);

            // Test Term H
            Assert.AreEqual(1.0000000000, TermH.Evaluate(0.0), 1e-8);
            Assert.AreEqual(1.0000000000, TermH.Evaluate(0.0), 1e-8);
            Assert.AreEqual(1.0000000000, TermH.Evaluate(0.0), 1e-8);
            Assert.AreEqual(1.0000000000, TermH.Evaluate(0.0), 1e-8);
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

            SineTerm TermAPrime3 = TermA.Derivative(3);
            Assert.AreEqual(SineTerm.SinusoidalType.COSINE, TermAPrime3.Type);
            Assert.AreEqual(-1.0, TermAPrime3.Amplitude);
            Assert.AreEqual(1.0, TermAPrime3.Frequency);
            Assert.AreEqual(0.0, TermAPrime3.Shift);

            // Test Term B
            SineTerm TermBPrime = TermB.Derivative();
            Assert.AreEqual(SineTerm.SinusoidalType.COSINE, TermBPrime.Type);
            Assert.AreEqual(6.0, TermBPrime.Amplitude);
            Assert.AreEqual(2.0, TermBPrime.Frequency);
            Assert.AreEqual(4.0, TermBPrime.Shift);

            SineTerm TermBPrime3 = TermB.Derivative(3);
            Assert.AreEqual(SineTerm.SinusoidalType.COSINE, TermBPrime3.Type);
            Assert.AreEqual(-24.0, TermBPrime3.Amplitude);
            Assert.AreEqual(2.0, TermBPrime3.Frequency);
            Assert.AreEqual(4.0, TermBPrime3.Shift);

            // Test Term C
            SineTerm TermCPrime = TermC.Derivative();
            Assert.AreEqual(SineTerm.SinusoidalType.COSINE, TermCPrime.Type);
            Assert.AreEqual(-4.0, TermCPrime.Amplitude);
            Assert.AreEqual(2.0, TermCPrime.Frequency);
            Assert.AreEqual(-1.0, TermCPrime.Shift);

            SineTerm TermCPrime3 = TermC.Derivative(3);
            Assert.AreEqual(SineTerm.SinusoidalType.COSINE, TermCPrime3.Type);
            Assert.AreEqual(16.0, TermCPrime3.Amplitude);
            Assert.AreEqual(2.0, TermCPrime3.Frequency);
            Assert.AreEqual(-1.0, TermCPrime3.Shift);

            // Test Term D
            SineTerm TermDPrime = TermD.Derivative();
            Assert.AreEqual(SineTerm.SinusoidalType.COSINE, TermDPrime.Type);
            Assert.AreEqual(0.0, TermDPrime.Amplitude);
            Assert.AreEqual(0.0, TermDPrime.Frequency);
            Assert.AreEqual(0.0, TermDPrime.Shift);

            SineTerm TermDPrime3 = TermD.Derivative(3);
            Assert.AreEqual(SineTerm.SinusoidalType.COSINE, TermDPrime3.Type);
            Assert.AreEqual(0.0, TermDPrime3.Amplitude);
            Assert.AreEqual(0.0, TermDPrime3.Frequency);
            Assert.AreEqual(0.0, TermDPrime3.Shift);

            // Test Term E
            SineTerm TermEPrime = TermE.Derivative();
            Assert.AreEqual(SineTerm.SinusoidalType.SINE, TermEPrime.Type);
            Assert.AreEqual(-1.0, TermEPrime.Amplitude);
            Assert.AreEqual(1.0, TermEPrime.Frequency);
            Assert.AreEqual(0.0, TermEPrime.Shift);

            SineTerm TermEPrime3 = TermE.Derivative(3);
            Assert.AreEqual(SineTerm.SinusoidalType.SINE, TermEPrime3.Type);
            Assert.AreEqual(1.0, TermEPrime3.Amplitude);
            Assert.AreEqual(1.0, TermEPrime3.Frequency);
            Assert.AreEqual(0.0, TermEPrime3.Shift);

            // Test Term F
            SineTerm TermFPrime = TermF.Derivative();
            Assert.AreEqual(SineTerm.SinusoidalType.SINE, TermFPrime.Type);
            Assert.AreEqual(-10.0, TermFPrime.Amplitude);
            Assert.AreEqual(5.0, TermFPrime.Frequency);
            Assert.AreEqual(3.0, TermFPrime.Shift);

            SineTerm TermFPrime3 = TermF.Derivative(3);
            Assert.AreEqual(SineTerm.SinusoidalType.SINE, TermFPrime3.Type);
            Assert.AreEqual(250.0, TermFPrime3.Amplitude);
            Assert.AreEqual(5.0, TermFPrime3.Frequency);
            Assert.AreEqual(3.0, TermFPrime3.Shift);

            // Test Term G
            SineTerm TermGPrime = TermG.Derivative();
            Assert.AreEqual(SineTerm.SinusoidalType.SINE, TermGPrime.Type);
            Assert.AreEqual(4.0, TermGPrime.Amplitude);
            Assert.AreEqual(1.0, TermGPrime.Frequency);
            Assert.AreEqual(-2.0, TermGPrime.Shift);

            SineTerm TermGPrime3 = TermG.Derivative(3);
            Assert.AreEqual(SineTerm.SinusoidalType.SINE, TermGPrime3.Type);
            Assert.AreEqual(-4.0, TermGPrime3.Amplitude);
            Assert.AreEqual(1.0, TermGPrime3.Frequency);
            Assert.AreEqual(-2.0, TermGPrime3.Shift);

            // Test Term H
            SineTerm TermHPrime = TermH.Derivative();
            Assert.AreEqual(SineTerm.SinusoidalType.SINE, TermHPrime.Type);
            Assert.AreEqual(0.0, TermHPrime.Amplitude);
            Assert.AreEqual(0.0, TermHPrime.Frequency);
            Assert.AreEqual(0.0, TermHPrime.Shift);

            SineTerm TermHPrime3 = TermH.Derivative(3);
            Assert.AreEqual(SineTerm.SinusoidalType.SINE, TermHPrime3.Type);
            Assert.AreEqual(0.0, TermHPrime3.Amplitude);
            Assert.AreEqual(0.0, TermHPrime3.Frequency);
            Assert.AreEqual(0.0, TermHPrime3.Shift);
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

            SineTerm TermAPrime3 = TermA.Antiderivative(3);
            Assert.AreEqual(SineTerm.SinusoidalType.COSINE, TermAPrime3.Type);
            Assert.AreEqual(1.0, TermAPrime3.Amplitude);
            Assert.AreEqual(1.0, TermAPrime3.Frequency);
            Assert.AreEqual(0.0, TermAPrime3.Shift);

            // Test Term B
            SineTerm TermBPrime = TermB.Antiderivative();
            Assert.AreEqual(SineTerm.SinusoidalType.COSINE, TermBPrime.Type);
            Assert.AreEqual(-1.5, TermBPrime.Amplitude);
            Assert.AreEqual(2.0, TermBPrime.Frequency);
            Assert.AreEqual(4.0, TermBPrime.Shift);

            SineTerm TermBPrime3 = TermB.Antiderivative(3);
            Assert.AreEqual(SineTerm.SinusoidalType.COSINE, TermBPrime3.Type);
            Assert.AreEqual(0.375, TermBPrime3.Amplitude);
            Assert.AreEqual(2.0, TermBPrime3.Frequency);
            Assert.AreEqual(4.0, TermBPrime3.Shift);

            // Test Term C
            SineTerm TermCPrime = TermC.Antiderivative();
            Assert.AreEqual(SineTerm.SinusoidalType.COSINE, TermCPrime.Type);
            Assert.AreEqual(1.0, TermCPrime.Amplitude);
            Assert.AreEqual(2.0, TermCPrime.Frequency);
            Assert.AreEqual(-1.0, TermCPrime.Shift);

            SineTerm TermCPrime3 = TermC.Antiderivative(3);
            Assert.AreEqual(SineTerm.SinusoidalType.COSINE, TermCPrime3.Type);
            Assert.AreEqual(-0.25, TermCPrime3.Amplitude);
            Assert.AreEqual(2.0, TermCPrime3.Frequency);
            Assert.AreEqual(-1.0, TermCPrime3.Shift);

            // Test Term D
            SineTerm TermDPrime = TermD.Antiderivative();
            Assert.AreEqual(SineTerm.SinusoidalType.COSINE, TermDPrime.Type);
            Assert.AreEqual(double.NegativeInfinity, TermDPrime.Amplitude);
            Assert.AreEqual(0.0, TermDPrime.Frequency);
            Assert.AreEqual(0.0, TermDPrime.Shift);

            SineTerm TermDPrime3 = TermD.Antiderivative(3);
            Assert.AreEqual(SineTerm.SinusoidalType.COSINE, TermDPrime3.Type);
            Assert.AreEqual(double.PositiveInfinity, TermDPrime3.Amplitude);
            Assert.AreEqual(0.0, TermDPrime3.Frequency);
            Assert.AreEqual(0.0, TermDPrime3.Shift);

            // Test Term E
            SineTerm TermEPrime = TermE.Antiderivative();
            Assert.AreEqual(SineTerm.SinusoidalType.SINE, TermEPrime.Type);
            Assert.AreEqual(1.0, TermEPrime.Amplitude);
            Assert.AreEqual(1.0, TermEPrime.Frequency);
            Assert.AreEqual(0.0, TermEPrime.Shift);

            SineTerm TermEPrime3 = TermE.Antiderivative(3);
            Assert.AreEqual(SineTerm.SinusoidalType.SINE, TermEPrime3.Type);
            Assert.AreEqual(-1.0, TermEPrime3.Amplitude);
            Assert.AreEqual(1.0, TermEPrime3.Frequency);
            Assert.AreEqual(0.0, TermEPrime3.Shift);

            // Test Term F
            SineTerm TermFPrime = TermF.Antiderivative();
            Assert.AreEqual(SineTerm.SinusoidalType.SINE, TermFPrime.Type);
            Assert.AreEqual(0.4, TermFPrime.Amplitude);
            Assert.AreEqual(5.0, TermFPrime.Frequency);
            Assert.AreEqual(3.0, TermFPrime.Shift);

            SineTerm TermFPrime3 = TermF.Antiderivative(3);
            Assert.AreEqual(SineTerm.SinusoidalType.SINE, TermFPrime3.Type);
            Assert.AreEqual(-0.016, TermFPrime3.Amplitude);
            Assert.AreEqual(5.0, TermFPrime3.Frequency);
            Assert.AreEqual(3.0, TermFPrime3.Shift);

            // Test Term G
            SineTerm TermGPrime = TermG.Antiderivative();
            Assert.AreEqual(SineTerm.SinusoidalType.SINE, TermGPrime.Type);
            Assert.AreEqual(-4.0, TermGPrime.Amplitude);
            Assert.AreEqual(1.0, TermGPrime.Frequency);
            Assert.AreEqual(-2.0, TermGPrime.Shift);

            SineTerm TermGPrime3 = TermG.Antiderivative(3);
            Assert.AreEqual(SineTerm.SinusoidalType.SINE, TermGPrime3.Type);
            Assert.AreEqual(4.0, TermGPrime3.Amplitude);
            Assert.AreEqual(1.0, TermGPrime3.Frequency);
            Assert.AreEqual(-2.0, TermGPrime3.Shift);

            // Test Term H
            SineTerm TermHPrime = TermH.Antiderivative();
            Assert.AreEqual(SineTerm.SinusoidalType.SINE, TermHPrime.Type);
            Assert.AreEqual(double.PositiveInfinity, TermHPrime.Amplitude);
            Assert.AreEqual(0.0, TermHPrime.Frequency);
            Assert.AreEqual(0.0, TermHPrime.Shift);

            SineTerm TermHPrime3 = TermH.Antiderivative(3);
            Assert.AreEqual(SineTerm.SinusoidalType.SINE, TermHPrime3.Type);
            Assert.AreEqual(double.NegativeInfinity, TermHPrime3.Amplitude);
            Assert.AreEqual(0.0, TermHPrime3.Frequency);
            Assert.AreEqual(0.0, TermHPrime3.Shift);
        }
    }
}
