using System;
using System.Collections.Generic;

using NUnit.Framework;

using FlexLib.ExpressionDom.Expressions;

namespace FlexLibTests.ExpressionDom.Expressions
{
    [TestFixture]
    public class ConstantExpressionTests
    {
        private IList<Tuple<ConstantExpression<int>, int>> TestExpressions;

        [SetUp]
        protected void SetUp()
        {
            TestExpressions = new List<Tuple<ConstantExpression<int>, int>>();

            TestExpressions.Add(new Tuple<ConstantExpression<int>, int>(new ConstantExpression<int>(1), 1));
            TestExpressions.Add(new Tuple<ConstantExpression<int>, int>(new ConstantExpression<int>(-2), -2));
            TestExpressions.Add(new Tuple<ConstantExpression<int>, int>(new ConstantExpression<int>(0), 0));
            TestExpressions.Add(new Tuple<ConstantExpression<int>, int>(new ConstantExpression<int>(-1548934581), -1548934581));
            TestExpressions.Add(new Tuple<ConstantExpression<int>, int>(new ConstantExpression<int>(int.MaxValue), int.MaxValue));
            TestExpressions.Add(new Tuple<ConstantExpression<int>, int>(new ConstantExpression<int>(int.MinValue), int.MinValue));
        }

        [Test]
        public void TestIsEvaluable()
        {
            foreach (Tuple<ConstantExpression<int>, int> pair in TestExpressions)
            {
                ConstantExpression<int> expr = pair.Item1;
                int value = pair.Item2;

                Assert.IsTrue(expr.IsEvaluable());
            }
        }
        [Test]
        public void TestEvaluate()
        {
            foreach (Tuple<ConstantExpression<int>, int> pair in TestExpressions)
            {
                ConstantExpression<int> expr = pair.Item1;
                int value = pair.Item2;

                Assert.AreEqual(expr.Evaluate(), value);
            }
        }
        [Test]
        public void TestValue()
        {
            foreach (Tuple<ConstantExpression<int>, int> pair in TestExpressions)
            {
                ConstantExpression<int> expr = pair.Item1;
                int value = pair.Item2;

                Assert.AreEqual(expr.Value, value);
            }
        }
    }
}