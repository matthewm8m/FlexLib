using System;
using System.Collections.Generic;

using NUnit.Framework;

using FlexLib.Algebra;
using FlexLib.ExpressionDom.Expressions;

namespace FlexLibTests.ExpressionDom.Expressions
{
    [TestFixture]
    public class OperationExpressionTests
    {
        protected Func<bool, int, int, int> IntTernary;
        protected Func<int, int, int> IntAdd;
        protected Func<int, int, int> IntSubtract;
        protected Func<int, int, int> IntMultiply;
        protected Func<int, int> IntNegate;

        protected RealField RealField;
        protected Func<RealFieldElement, RealFieldElement, RealFieldElement> RealAdd;
        protected Func<RealFieldElement, RealFieldElement, RealFieldElement> RealSubtract;
        protected Func<RealFieldElement, RealFieldElement, RealFieldElement> RealMultiply;
        protected Func<RealFieldElement, RealFieldElement, RealFieldElement> RealDivide;

        protected IList<ExpressionWithExpectation<int>> TestIntExpressions;
        protected IList<ExpressionWithExpectation<RealFieldElement>> TestRealExpressions;

        [SetUp]
        protected void SetUp()
        {
            // Setup the integer operations.
            IntTernary = (cond, x, y) => cond ? x : y;
            IntAdd = (x, y) => x + y;
            IntSubtract = (x, y) => x - y;
            IntMultiply = (x, y) => x * y;
            IntNegate = (x) => -x;

            // Setup the real operations.
            RealField = new RealField(0.0001);
            RealAdd = RealField.Add;
            RealSubtract = (x, y) => RealField.Add(x, RealField.Negative(y));
            RealMultiply = RealField.Multiply;
            RealDivide = (x, y) => RealField.Multiply(x, RealField.Inverse(y));

            // Setup the integer test expressions.
            TestIntExpressions = new List<ExpressionWithExpectation<int>>();

            TestIntExpressions.Add(new ExpressionWithExpectation<int>(
                new OperationExpression<int, int, int>(
                    IntAdd,
                    new ConstantExpression<int>(1),
                    new OperationExpression<int, int, int>(
                        IntSubtract,
                        new ConstantExpression<int>(3),
                        new ConstantExpression<int>(2)
                    )
                ), 2
            ));
            TestIntExpressions.Add(new ExpressionWithExpectation<int>(
                new OperationExpression<bool, int, int, int>(
                    IntTernary,
                    new ConstantExpression<bool>(true),
                    new ConstantExpression<int>(3 * 3 + 1),
                    new ConstantExpression<int>(3 / 2)
                ), 10
            ));
            TestIntExpressions.Add(new ExpressionWithExpectation<int>(
                new OperationExpression<int, int, int>(
                    IntMultiply,
                    new OperationExpression<int, int>(
                        IntNegate,
                        new ConstantExpression<int>(2)
                    ),
                    new OperationExpression<int, int, int>(
                        IntSubtract,
                        new ConstantExpression<int>(5),
                        new ConstantExpression<int>(2)
                    )
                ), -6
            ));

            // Setup the real test expressions.
            TestRealExpressions = new List<ExpressionWithExpectation<RealFieldElement>>();

            TestRealExpressions.Add(new ExpressionWithExpectation<RealFieldElement>(
                new OperationExpression<RealFieldElement, RealFieldElement, RealFieldElement>(
                    RealMultiply,
                    new OperationExpression<RealFieldElement, RealFieldElement, RealFieldElement>(
                        RealSubtract,
                        new ConstantExpression<RealFieldElement>(10),
                        new ConstantExpression<RealFieldElement>(5)
                    ),
                    new ConstantExpression<RealFieldElement>(3)
                ), 15
            ));
            TestRealExpressions.Add(new ExpressionWithExpectation<RealFieldElement>(
                new OperationExpression<RealFieldElement, RealFieldElement, RealFieldElement>(
                    RealDivide,
                    new ConstantExpression<RealFieldElement>(3),
                    new OperationExpression<RealFieldElement, RealFieldElement, RealFieldElement>(
                        RealAdd,
                        new ConstantExpression<RealFieldElement>(-1),
                        new ConstantExpression<RealFieldElement>(3)
                    )
                ), 1.5
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
    }
}