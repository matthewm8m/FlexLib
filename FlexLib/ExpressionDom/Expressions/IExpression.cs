namespace FlexLib.ExpressionDom.Expressions
{
    /// <summary>
    /// Represents a expression node in an abstract syntax true that is evaluable to a specific type if certain conditions are met.
    /// </summary>
    /// <typeparam name="T">The type of the value returned when the expression is evaluated.</typeparam>
    public interface IExpression<T>
    {
        /// <summary>
        /// Determines whether the expression can be validly evaluated.
        /// </summary>
        /// <returns><c>true</c> if the expression can be evaluated to type <c>T</c>; otherwise, <c>false</c>.</returns>
        bool IsEvaluable();
        /// <summary>
        /// Evaluates the expression and returns its simplified value if possible.
        /// </summary>
        /// <returns>The evaluated value of the expression.</returns>
        T Evaluate();
    }
}