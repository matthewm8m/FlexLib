using System;
using System.Collections.Generic;

using NUnit.Framework;

using FlexLib.ExpressionDom.Expressions;

namespace FlexLibTests.ExpressionDom.Expressions
{
    [TestFixture]
    public class OperationExpressionTests
    {
        protected readonly Func<bool, int, int, int> Ternary = (cond, x, y) => cond ? x : y;
        protected readonly Func<int, int, int> Add = (x, y) => x + y;
        protected readonly Func<int, int, int> Subtract = (x, y) => x - y;
        protected readonly Func<int, int, int> Multiply = (x, y) => x * y;
        protected readonly Func<int, int> Negate = (x) => -x;

        protected IList<ExpressionWithExpectation<int>> TestExpressions;

        [SetUp]
        protected void SetUp()
        {
            TestExpressions = new List<ExpressionWithExpectation<int>>();

            TestExpressions.Add(new ExpressionWithExpectation<int>(
                new OperationExpression<int, int, int>(
                    Add,
                    new ConstantExpression<int>(1),
                    new OperationExpression<int, int, int>(
                        Subtract,
                        new ConstantExpression<int>(3),
                        new ConstantExpression<int>(2)
                    )
                ), 2
            ));
            TestExpressions.Add(new ExpressionWithExpectation<int>(
                new OperationExpression<bool, int, int, int>(
                    Ternary,
                    new ConstantExpression<bool>(true),
                    new ConstantExpression<int>(3 * 3 + 1),
                    new ConstantExpression<int>(3 / 2)
                ), 10
            ));
            TestExpressions.Add(new ExpressionWithExpectation<int>(
                new OperationExpression<int, int, int>(
                    Multiply,
                    new OperationExpression<int, int>(
                        Negate,
                        new ConstantExpression<int>(2)
                    ),
                    new OperationExpression<int, int, int>(
                        Subtract,
                        new ConstantExpression<int>(5),
                        new ConstantExpression<int>(2)
                    )
                ), -6
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

                Assert.AreEqual(expression.Evaluate(), expected);
            }
        }
    }
}