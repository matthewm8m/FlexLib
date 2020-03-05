using System;
using System.Collections;
using System.Collections.Generic;

using NUnit.Framework;

using FlexLib.Algebra;
using FlexLib.Linear;

namespace FlexLibTests.Linear
{
    [TestFixture]
    public class MatrixTests
    {
        private RealField TestRealField;
        private Dictionary<string, Matrix<RealFieldElement>> TestRealMatrices;

        [SetUp]
        protected void SetUp()
        {
            // Tolerance is 2.0^-8.
            TestRealField = new RealField(0.00390625);
            TestRealMatrices = new Dictionary<string, Matrix<RealFieldElement>>();

            TestRealMatrices.Add("A", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                {  2,  1,  0,  5 },
                {  3,  4,  0, -2 },
                {  6,  1,  0,  7 }
            }));
            TestRealMatrices.Add("B", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                {  2,  1,  5 },
                {  3,  4, -2 },
                {  6,  1,  7 }
            }));
            TestRealMatrices.Add("C", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                {  0,  0,  0,  0 },
                {  2,  1,  0,  5 },
                {  3,  4,  0, -2 },
                {  6,  1,  0,  7 }
            }));
            TestRealMatrices.Add("D", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                {  0,  0,  0 },
                {  2,  1,  5 },
                {  3,  4, -2 },
                {  6,  1,  7 }
            }));
            TestRealMatrices.Add("E", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                {  0,  2,  1,  5 },
                {  0,  3,  4, -2 },
                {  0,  6,  1,  7 }
            }));
            TestRealMatrices.Add("F", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                {  3,  4, -2 },
                {  2,  1,  5 },
                {  6,  1,  7 }
            }));
            TestRealMatrices.Add("G", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                {  1,  2,  5 },
                {  4,  3, -2 },
                {  1,  6,  7 }
            }));
            TestRealMatrices.Add("H", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                {  2,  3,  6 },
                {  1,  4,  1 },
                {  0,  0,  0 },
                {  5, -2,  7 }
            }));
            TestRealMatrices.Add("+A", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                {  2,  1,  0,  5 },
                {  3,  4,  0, -2 },
                {  6,  1,  0,  7 }
            }));
            TestRealMatrices.Add("-B", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                { -2, -1, -5 },
                { -3, -4,  2 },
                { -6, -1, -7 }
            }));
            TestRealMatrices.Add("+C", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                {  0,  0,  0,  0 },
                {  2,  1,  0,  5 },
                {  3,  4,  0, -2 },
                {  6,  1,  0,  7 }
            }));
            TestRealMatrices.Add("-D", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                { -0, -0, -0 },
                { -2, -1, -5 },
                { -3, -4,  2 },
                { -6, -1, -7 }
            }));
            TestRealMatrices.Add("+E", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                {  0,  2,  1,  5 },
                {  0,  3,  4, -2 },
                {  0,  6,  1,  7 }
            }));
            TestRealMatrices.Add("-F", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                { -3, -4,  2 },
                { -2, -1, -5 },
                { -6, -1, -7 }
            }));
            TestRealMatrices.Add("+G", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                {  1,  2,  5 },
                {  4,  3, -2 },
                {  1,  6,  7 }
            }));
            TestRealMatrices.Add("-H", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                { -2, -3, -6 },
                { -1, -4, -1 },
                { -0, -0, -0 },
                { -5,  2, -7 }
            }));
            TestRealMatrices.Add("A:Row[1]", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                {  3,  4,  0, -2 }
            }));
            TestRealMatrices.Add("C:Row[3]", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                {  6,  1,  0,  7 }
            }));
            TestRealMatrices.Add("E:Row[0]", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                {  0,  2,  1,  5 }
            }));
            TestRealMatrices.Add("G:Row[2]", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                {  1,  6,  7 }
            }));
            TestRealMatrices.Add("B:Column[2]", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                {  5 },
                { -2 },
                {  7 }
            }));
            TestRealMatrices.Add("D:Column[0]", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                {  0 },
                {  2 },
                {  3 },
                {  6 }
            }));
            TestRealMatrices.Add("F:Column[1]", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                {  4 },
                {  1 },
                {  1 }
            }));
            TestRealMatrices.Add("H:Column[0]", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                {  2 },
                {  1 },
                {  0 },
                {  5 }
            }));
            TestRealMatrices.Add("A+E", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                {  2,  3,  1, 10 },
                {  3,  7,  4, -4 },
                {  6,  7,  1, 14 }
            }));
            TestRealMatrices.Add("A-E", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                {  2, -1, -1,  0 },
                {  3,  1, -4,  0 },
                {  6, -5, -1,  0 }
            }));
            TestRealMatrices.Add("B+F", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                {  5,  5,  3 },
                {  5,  5,  3 },
                { 12,  2, 14 }
            }));
            TestRealMatrices.Add("B-F", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                { -1, -3,  7 },
                {  1,  3, -7 },
                {  0,  0,  0 }
            }));
            TestRealMatrices.Add("B+G", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                {  3,  3, 10 },
                {  7,  7, -4 },
                {  7,  7, 14 }
            }));
            TestRealMatrices.Add("B-G", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                {  1, -1,  0 },
                { -1,  1,  0 },
                {  5, -5,  0 }
            }));
            TestRealMatrices.Add("D+H", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                {  2,  3,  6 },
                {  3,  5,  6 },
                {  3,  4, -2 },
                { 11, -1, 14 }
            }));
            TestRealMatrices.Add("D-H", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                { -2, -3, -6 },
                {  1, -3,  4 },
                {  3,  4, -2 },
                {  1,  3,  0 }
            }));
            TestRealMatrices.Add("F+G", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                {  4,  6,  3 },
                {  6,  4,  3 },
                {  7,  7, 14 }
            }));
            TestRealMatrices.Add("F-G", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                {  2,  2, -7 },
                { -2, -2,  7 },
                {  5, -5,  0 }
            }));
            TestRealMatrices.Add("+2.0A", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                {  4,  2,  0, 10 },
                {  6,  8,  0, -4 },
                { 12,  2,  0, 14 }
            }));
            TestRealMatrices.Add("-1.5C", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                {  0.0,  0.0,  0.0,  0.0 },
                { -3.0, -1.5,  0.0, -7.5 },
                { -4.5, -6.0,  0.0,  3.0 },
                { -9.0, -1.5,  0.0,-10.5 }
            }));
            TestRealMatrices.Add("+0.0E", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                {  0,  0,  0,  0 },
                {  0,  0,  0,  0 },
                {  0,  0,  0,  0 }
            }));
            TestRealMatrices.Add("+2.2G", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                {  2.2,  4.4, 11.0 },
                {  8.8,  6.6, -4.4 },
                {  2.2, 13.2, 15.4 }
            }));
            TestRealMatrices.Add("B/+2.0", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                {  1.0,  0.5,  2.5 },
                {  1.5,  2.0, -1.0 },
                {  3.0,  0.5,  3.5 }
            }));
            TestRealMatrices.Add("D/-0.2", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                {   0.0,   0.0,   0.0 },
                { -10.0,  -5.0, -25.0 },
                { -15.0, -20.0,  10.0 },
                { -30.0,  -5.0, -35.0 }
            }));
            TestRealMatrices.Add("F/+1.0", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                {  3,  4, -2 },
                {  2,  1,  5 },
                {  6,  1,  7 }
            }));
            TestRealMatrices.Add("H/+0.8", Matrix<RealFieldElement>.FromArray(TestRealField, new RealFieldElement[,]
            {
                {  2.50,  3.75,  7.50 },
                {  1.25,  5.00,  1.25 },
                {  0.00,  0.00,  0.00 },
                {  6.25, -2.50,  8.75 }
            }));
            TestRealMatrices.Add("I", Matrix<RealFieldElement>.Identity(TestRealField, 4));
            TestRealMatrices.Add("0", Matrix<RealFieldElement>.Zeroes(TestRealField, 3, 4));
            TestRealMatrices.Add("1", Matrix<RealFieldElement>.Ones(TestRealField, 2, 7));
        }

        [TestCase("I", 0, 1, 0.0)]
        [TestCase("I", 2, 3, 0.0)]
        [TestCase("I", 1, 1, 1.0)]
        [TestCase("I", 3, 3, 1.0)]
        [TestCase("0", 1, 1, 0.0)]
        [TestCase("0", 2, 3, 0.0)]
        [TestCase("0", 0, 0, 0.0)]
        [TestCase("1", 1, 6, 1.0)]
        [TestCase("1", 0, 5, 1.0)]
        [TestCase("1", 1, 2, 1.0)]
        public void TestElement(string matrixKey, int i, int j, double element)
        {
            Matrix<RealFieldElement> matrix = TestRealMatrices[matrixKey];

            Assert.IsTrue(TestRealField.ElementsEqual(matrix[i, j], element));
        }

        [TestCase("A", "A:Row[1]", 1)]
        [TestCase("C", "C:Row[3]", 3)]
        [TestCase("E", "E:Row[0]", 0)]
        [TestCase("G", "G:Row[2]", 2)]
        public void TestRow(string matrixKey, string rowKey, int i)
        {
            Matrix<RealFieldElement> matrix = TestRealMatrices[matrixKey];
            Matrix<RealFieldElement> row = TestRealMatrices[rowKey];

            Assert.IsTrue(matrix.Row(i) == row);
            Assert.IsFalse(matrix.Row(i) != row);
        }

        [TestCase("B", "B:Column[2]", 2)]
        [TestCase("D", "D:Column[0]", 0)]
        [TestCase("F", "F:Column[1]", 1)]
        [TestCase("H", "H:Column[0]", 0)]
        public void TestColumn(string matrixKey, string columnKey, int j)
        {
            Matrix<RealFieldElement> matrix = TestRealMatrices[matrixKey];
            Matrix<RealFieldElement> column = TestRealMatrices[columnKey];

            Assert.IsTrue(matrix.Column(j) == column);
            Assert.IsFalse(matrix.Column(j) != column);
        }

        [TestCase("A", "+A", false)]
        [TestCase("B", "-B", true)]
        [TestCase("C", "+C", false)]
        [TestCase("D", "-D", true)]
        [TestCase("E", "+E", false)]
        [TestCase("F", "-F", true)]
        [TestCase("G", "+G", false)]
        [TestCase("H", "-H", true)]
        public void TestPositiveNegative(string matrixKey, string resultKey, bool negate)
        {
            Matrix<RealFieldElement> matrix = TestRealMatrices[matrixKey];
            Matrix<RealFieldElement> result = TestRealMatrices[resultKey];

            if (negate)
            {
                Assert.IsTrue(-matrix == result);
                Assert.IsFalse(-matrix != result);
            }
            else
            {
                Assert.IsTrue(+matrix == result);
                Assert.IsFalse(+matrix != result);
            }
        }

        [TestCase("A", "E", "A+E")]
        [TestCase("B", "F", "B+F")]
        [TestCase("B", "G", "B+G")]
        [TestCase("D", "H", "D+H")]
        [TestCase("F", "G", "F+G")]
        public void TestAdd(string matrixKeyA, string matrixKeyB, string matrixKeyC)
        {
            Matrix<RealFieldElement> matrixA = TestRealMatrices[matrixKeyA];
            Matrix<RealFieldElement> matrixB = TestRealMatrices[matrixKeyB];
            Matrix<RealFieldElement> matrixC = TestRealMatrices[matrixKeyC];

            Assert.IsTrue(matrixA + matrixB == matrixC);
            Assert.IsFalse(matrixA + matrixB != matrixC);
        }

        [TestCase("A", "E", "A-E")]
        [TestCase("B", "F", "B-F")]
        [TestCase("B", "G", "B-G")]
        [TestCase("D", "H", "D-H")]
        [TestCase("F", "G", "F-G")]
        public void TestSubtract(string matrixKeyA, string matrixKeyB, string matrixKeyC)
        {
            Matrix<RealFieldElement> matrixA = TestRealMatrices[matrixKeyA];
            Matrix<RealFieldElement> matrixB = TestRealMatrices[matrixKeyB];
            Matrix<RealFieldElement> matrixC = TestRealMatrices[matrixKeyC];

            Assert.IsTrue(matrixA - matrixB == matrixC);
            Assert.IsFalse(matrixA - matrixB != matrixC);
        }

        [TestCase("A", 2.0, "+2.0A")]
        [TestCase("C", -1.5, "-1.5C")]
        [TestCase("E", 0.0, "+0.0E")]
        [TestCase("G", 2.2, "+2.2G")]
        public void TestScale(string matrixKey, double scalar, string resultKey)
        {
            Matrix<RealFieldElement> matrix = TestRealMatrices[matrixKey];
            Matrix<RealFieldElement> result = TestRealMatrices[resultKey];

            Assert.IsTrue(scalar * matrix == result);
            Assert.IsTrue(matrix * scalar == result);
            Assert.IsFalse(scalar * matrix != result);
            Assert.IsFalse(matrix * scalar != result);
        }

        [TestCase("B", 2.0, "B/+2.0")]
        [TestCase("D", -0.2, "D/-0.2")]
        [TestCase("F", 1.0, "F/+1.0")]
        [TestCase("H", 0.8, "H/+0.8")]
        public void TestInverseScale(string matrixKey, double scalar, string resultKey)
        {
            Matrix<RealFieldElement> matrix = TestRealMatrices[matrixKey];
            Matrix<RealFieldElement> result = TestRealMatrices[resultKey];

            Assert.IsTrue(matrix / scalar == result);
            Assert.IsFalse(matrix / scalar != result);
        }

        [TestCase("A", false)]
        [TestCase("B", true)]
        [TestCase("C", true)]
        [TestCase("D", false)]
        [TestCase("E", false)]
        [TestCase("F", true)]
        [TestCase("G", true)]
        [TestCase("H", false)]
        public void TestSquare(string matrixKey, bool isSquare)
        {
            Matrix<RealFieldElement> matrix = TestRealMatrices[matrixKey];

            Assert.AreEqual(matrix.IsSquare(), isSquare);
        }

        [TestCase("B", 13.0)]
        [TestCase("C", 8.0)]
        [TestCase("F", 11.0)]
        [TestCase("G", 11.0)]
        public void TestTrace(string matrixKey, double trace)
        {
            Matrix<RealFieldElement> matrix = TestRealMatrices[matrixKey];

            Assert.IsTrue(TestRealField.ElementsEqual(matrix.Trace(), trace));
        }
    }
}