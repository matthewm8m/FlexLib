using System;
using System.Collections.Generic;

using NUnit.Framework;

using FlexLib.Algebra;
using FlexLib.ExpressionDom.Expressions;

namespace FlexLibTests.ExpressionDom.Expressions
{
    [TestFixture]
    public class ConstantExpressionTests
    {
        protected RealField RealField;

        protected IList<ExpressionWithExpectation<int>> TestIntExpressions;
        protected IList<ExpressionWithExpectation<RealFieldElement>> TestRealExpressions;

        [SetUp]
        protected void SetUp()
        {
            // Setup the integer test expressions.
            TestIntExpressions = new List<ExpressionWithExpectation<int>>();

            TestIntExpressions.Add(new ExpressionWithExpectation<int>(
                new ConstantExpression<int>(1), 1
            ));
            TestIntExpressions.Add(new ExpressionWithExpectation<int>(
                new ConstantExpression<int>(-2), -2
            ));
            TestIntExpressions.Add(new ExpressionWithExpectation<int>(
                new ConstantExpression<int>(0), 0
            ));
            TestIntExpressions.Add(new ExpressionWithExpectation<int>(
                new ConstantExpression<int>(-1548934581), -1548934581
            ));
            TestIntExpressions.Add(new ExpressionWithExpectation<int>(
                new ConstantExpression<int>(int.MinValue), int.MinValue
            ));
            TestIntExpressions.Add(new ExpressionWithExpectation<int>(
                new ConstantExpression<int>(int.MaxValue), int.MaxValue
            ));

            // Setup the real test expressions.
            RealField = new RealField(0.0001);
            TestRealExpressions = new List<ExpressionWithExpectation<RealFieldElement>>();

            TestRealExpressions.Add(new ExpressionWithExpectation<RealFieldElement>(
                new ConstantExpression<RealFieldElement>(1), 1
            ));
            TestRealExpressions.Add(new ExpressionWithExpectation<RealFieldElement>(
                new ConstantExpression<RealFieldElement>(-1), -1
            ));
            TestRealExpressions.Add(new ExpressionWithExpectation<RealFieldElement>(
                new ConstantExpression<RealFieldElement>(-0.21), -0.21
            ));
            TestRealExpressions.Add(new ExpressionWithExpectation<RealFieldElement>(
                new ConstantExpression<RealFieldElement>(double.MaxValue), double.MaxValue
            ));
            TestRealExpressions.Add(new ExpressionWithExpectation<RealFieldElement>(
                new ConstantExpression<RealFieldElement>(double.Epsilon), double.Epsilon
            ));
            TestRealExpressions.Add(new ExpressionWithExpectation<RealFieldElement>(
                new ConstantExpression<RealFieldElement>(double.NegativeInfinity), double.NegativeInfinity
            ));
        }

        [Test]
        public void TestIntIsEvaluable()
        {
            foreach (ExpressionWithExpectation<int> exprExpect in TestIntExpressions)
            {
                IExpression<int> expression = exprExpect.Expression;
                int expected = exprExpect.ExpectedValue;

                Assert.IsTrue(expression.IsEvaluable());
            }
        }
        [Test]
        public void TestRealIsEvaluable()
        {
            foreach (ExpressionWithExpectation<RealFieldElement> exprExpect in TestRealExpressions)
            {
                IExpression<RealFieldElement> expression = exprExpect.Expression;
                RealFieldElement expected = exprExpect.ExpectedValue;

                Assert.IsTrue(expression.IsEvaluable());
            }
        }

        [Test]
        public void TestIntEvaluate()
        {
            foreach (ExpressionWithExpectation<int> exprExpect in TestIntExpressions)
            {
                IExpression<int> expression = exprExpect.Expression;
                int expected = exprExpect.ExpectedValue;

                Assert.AreEqual(expected, expression.Evaluate());
            }
        }
        [Test]
        public void TestRealEvaluate()
        {
            foreach (ExpressionWithExpectation<RealFieldElement> exprExpect in TestRealExpressions)
            {
                IExpression<RealFieldElement> expression = exprExpect.Expression;
                RealFieldElement expected = exprExpect.ExpectedValue;

                Assert.IsTrue(RealField.ElementsEqual(expected, expression.Evaluate()));
            }
        }

        [Test]
        public void TestIntValue()
        {
            foreach (ExpressionWithExpectation<int> exprExpect in TestIntExpressions)
            {
                ConstantExpression<int> expression = exprExpect.Expression as ConstantExpression<int>;
                int expected = exprExpect.ExpectedValue;

                Assert.AreEqual(expected, expression.Value);
            }
        }
        [Test]
        public void TestRealValue()
        {
            foreach (ExpressionWithExpectation<RealFieldElement> exprExpect in TestRealExpressions)
            {
                ConstantExpression<RealFieldElement> expression = exprExpect.Expression as ConstantExpression<RealFieldElement>;
                RealFieldElement expected = exprExpect.ExpectedValue;

                Assert.IsTrue(RealField.ElementsEqual(expected, expression.Value));
            }
        }
    }
}