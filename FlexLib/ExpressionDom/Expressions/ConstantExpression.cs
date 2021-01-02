namespace FlexLib.ExpressionDom.Expressions
{
    /// <summary>
    /// Represents an expression node in an abstract syntax tree with a constant value of a specific type.
    /// </summary>
    /// <typeparam name="T">The type of the constant value.</typeparam>
    public class ConstantExpression<T> : IExpression<T>
    {
        /// <summary>
        /// The value of the <see cref="ConstantExpression{T}"/> object.
        /// </summary>
        public readonly T Value;

        /// <summary>
        /// Creates a new <see cref="ConstantExpression{T}"/> object with the specified value.
        /// </summary>
        /// <param name="value">The constant value.</param>
        public ConstantExpression(T value)
        {
            Value = value;
        }

        /// <summary>
        /// Determines whether the expression can be validly evaluated.
        /// </summary>
        /// <returns><c>true</c> always for <see cref="ConstantExpression{T}"/> objects.</returns>
        public bool IsEvaluable()
        {
            // A constant expression is always evaluable.
            return true;
        }
        /// <summary>
        /// Evaluates the expression and returns its simplified value if possible.
        /// </summary>
        /// <returns>The constant value of the <see cref="ConstantExpression{T}"/> object assigned at construction.</returns>
        public T Evaluate()
        {
            // Simply return the assigned constant value.
            return Value;
        }

        /// <summary>
        /// Creates a string representation of the <see cref="ConstantExpression{T}"/> object.
        /// </summary>
        /// <returns>A string that represents the value of the expression.</returns>
        public override string ToString()
        {
            return Value.ToString();
        }

        /// <summary>
        /// Converts a value of generic type to its corresponding <see cref="ConstantExpression{T}"/> instance.
        /// </summary>
        /// <param name="value">The constant value.</param>
        public static implicit operator ConstantExpression<T>(T value) => new ConstantExpression<T>(value);
        /// <summary>
        /// Converts a constant expression to its corresponding unboxed value.
        /// </summary>
        /// <param name="expr">The constant expression.</param>
        public static explicit operator T(ConstantExpression<T> expr) => expr.Value;
    }
}