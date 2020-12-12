using FlexLib.ExpressionDom.Expressions;

namespace FlexLibTests.ExpressionDom.Expressions
{
    public struct ExpressionWithExpectation<T>
    {
        public IExpression<T> Expression;
        public T ExpectedValue;

        public ExpressionWithExpectation(IExpression<T> expression, T expectedValue)
        {
            Expression = expression;
            ExpectedValue = expectedValue;
        }
    }
}