using System;
using System.Collections.Generic;

using NUnit.Framework;

using FlexLib.ExpressionDom.Expressions;

namespace FlexLibTests.ExpressionDom.Expressions
{
    [TestFixture]
    public class ConstantExpressionTests
    {
        private IList<ExpressionWithExpectation<int>> TestExpressions;

        [SetUp]
        protected void SetUp()
        {
            TestExpressions = new List<ExpressionWithExpectation<int>>();

            TestExpressions.Add(new ExpressionWithExpectation<int>(
                new ConstantExpression<int>(1), 1
            ));
            TestExpressions.Add(new ExpressionWithExpectation<int>(
                new ConstantExpression<int>(-2), -2
            ));
            TestExpressions.Add(new ExpressionWithExpectation<int>(
                new ConstantExpression<int>(0), 0
            ));
            TestExpressions.Add(new ExpressionWithExpectation<int>(
                new ConstantExpression<int>(-1548934581), -1548934581
            ));
            TestExpressions.Add(new ExpressionWithExpectation<int>(
                new ConstantExpression<int>(int.MinValue), int.MinValue
            ));
            TestExpressions.Add(new ExpressionWithExpectation<int>(
                new ConstantExpression<int>(int.MaxValue), int.MaxValue
            ));
        }

        [Test]
        public void TestIsEvaluable()
        {
            foreach (ExpressionWithExpectation<int> exprExpect in TestExpressions)
            {
                IExpression<int> expression = exprExpect.Expression;
                int expected = exprExpect.ExpectedValue;

                Assert.IsTrue(expression.IsEvaluable());
            }
        }
        [Test]
        public void TestEvaluate()
        {
            foreach (ExpressionWithExpectation<int> exprExpect in TestExpressions)
            {
                IExpression<int> expression = exprExpect.Expression;
                int expected = exprExpect.ExpectedValue;

                Assert.AreEqual(expected, expression.Evaluate());
            }
        }
        [Test]
        public void TestValue()
        {
            foreach (ExpressionWithExpectation<int> exprExpect in TestExpressions)
            {
                ConstantExpression<int> expression = exprExpect.Expression as ConstantExpression<int>;
                int expected = exprExpect.ExpectedValue;

                Assert.AreEqual(expected, expression.Value);
            }
        }
    }
}