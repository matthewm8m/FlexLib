using System;

namespace FlexLib.ExpressionDom.Expressions
{
    /// <summary>
    /// Represents an expression node in an abstract syntax tree that is an operation on 1 operand.
    /// </summary>
    /// <typeparam name="T1">The type of operand 1.</typeparam>
    /// <typeparam name="TResult">The type of the resulting value.</typeparam>
    public class OperationExpression<T1, TResult> : IExpression<TResult>
    {
        /// <summary>
        /// The operation to perform on the evaluated operands.
        /// </summary>
        public readonly Func<T1, TResult> Operation;

        /// <summary>
        /// The operand in position 1.
        /// </summary>
        public readonly IExpression<T1> Operand1;

        /// <summary>
        /// Creates a new <see cref="OperationExpression{T1, TResult}"/> object with the specified operation and operands.
        /// </summary>
        /// <param name="operation">The operation to perform on the operands.</param>
        /// <param name="operand1">Operand 1.</param>
        public OperationExpression(
            Func<T1, TResult> operation,
            IExpression<T1> operand1
        )
        {
            Operation = operation;
            Operand1 = operand1;
        }

        /// <summary>
        /// Determines whether the expression can be validly evaluated.
        /// </summary>
        /// <returns><c>true</c> if all the operands are evaluable; otherwise, <c>false</c>.</returns>
        public bool IsEvaluable()
        {
            // The expression is only evaluable if all operands are also evaluable.
            return (
                Operand1.IsEvaluable()
            );
        }
        /// <summary>
        /// Evaluates the expression and returns its simplified value if possible.
        /// </summary>
        /// <returns>The value of the <see cref="OperationExpression{T1, TResult}"/> object after the operation is performed.</returns>
        public TResult Evaluate()
        {
            // Return the result of performing the operation on the evaluated operands if possible.
            // If impossible, return the default value for the result.
            if (IsEvaluable())
                return Operation(
                    Operand1.Evaluate()
                );
            else return default(TResult);
        }
    }
    /// <summary>
    /// Represents an expression node in an abstract syntax tree that is an operation on 2 operands.
    /// </summary>
    /// <typeparam name="T1">The type of operand 1.</typeparam>
    /// <typeparam name="T2">The type of operand 2.</typeparam>
    /// <typeparam name="TResult">The type of the resulting value.</typeparam>
    public class OperationExpression<T1, T2, TResult> : IExpression<TResult>
    {
        /// <summary>
        /// The operation to perform on the evaluated operands.
        /// </summary>
        public readonly Func<T1, T2, TResult> Operation;

        /// <summary>
        /// The operand in position 1.
        /// </summary>
        public readonly IExpression<T1> Operand1;
        /// <summary>
        /// The operand in position 2.
        /// </summary>
        public readonly IExpression<T2> Operand2;

        /// <summary>
        /// Creates a new <see cref="OperationExpression{T1, T2, TResult}"/> object with the specified operation and operands.
        /// </summary>
        /// <param name="operation">The operation to perform on the operands.</param>
        /// <param name="operand1">Operand 1.</param>
        /// <param name="operand2">Operand 2.</param>
        public OperationExpression(
            Func<T1, T2, TResult> operation,
            IExpression<T1> operand1,
            IExpression<T2> operand2
        )
        {
            Operation = operation;
            Operand1 = operand1;
            Operand2 = operand2;
        }

        /// <summary>
        /// Determines whether the expression can be validly evaluated.
        /// </summary>
        /// <returns><c>true</c> if all the operands are evaluable; otherwise, <c>false</c>.</returns>
        public bool IsEvaluable()
        {
            // The expression is only evaluable if all operands are also evaluable.
            return (
                Operand1.IsEvaluable() &&
                Operand2.IsEvaluable()
            );
        }
        /// <summary>
        /// Evaluates the expression and returns its simplified value if possible.
        /// </summary>
        /// <returns>The value of the <see cref="OperationExpression{T1, T2, TResult}"/> object after the operation is performed.</returns>
        public TResult Evaluate()
        {
            // Return the result of performing the operation on the evaluated operands if possible.
            // If impossible, return the default value for the result.
            if (IsEvaluable())
                return Operation(
                    Operand1.Evaluate(),
                    Operand2.Evaluate()
                );
            else return default(TResult);
        }
    }
    /// <summary>
    /// Represents an expression node in an abstract syntax tree that is an operation on 3 operands.
    /// </summary>
    /// <typeparam name="T1">The type of operand 1.</typeparam>
    /// <typeparam name="T2">The type of operand 2.</typeparam>
    /// <typeparam name="T3">The type of operand 3.</typeparam>
    /// <typeparam name="TResult">The type of the resulting value.</typeparam>
    public class OperationExpression<T1, T2, T3, TResult> : IExpression<TResult>
    {
        /// <summary>
        /// The operation to perform on the evaluated operands.
        /// </summary>
        public readonly Func<T1, T2, T3, TResult> Operation;

        /// <summary>
        /// The operand in position 1.
        /// </summary>
        public readonly IExpression<T1> Operand1;
        /// <summary>
        /// The operand in position 2.
        /// </summary>
        public readonly IExpression<T2> Operand2;
        /// <summary>
        /// The operand in position 3.
        /// </summary>
        public readonly IExpression<T3> Operand3;

        /// <summary>
        /// Creates a new <see cref="OperationExpression{T1, T2, T3, TResult}"/> object with the specified operation and operands.
        /// </summary>
        /// <param name="operation">The operation to perform on the operands.</param>
        /// <param name="operand1">Operand 1.</param>
        /// <param name="operand2">Operand 2.</param>
        /// <param name="operand3">Operand 3.</param>
        public OperationExpression(
            Func<T1, T2, T3, TResult> operation,
            IExpression<T1> operand1,
            IExpression<T2> operand2,
            IExpression<T3> operand3
        )
        {
            Operation = operation;
            Operand1 = operand1;
            Operand2 = operand2;
            Operand3 = operand3;
        }

        /// <summary>
        /// Determines whether the expression can be validly evaluated.
        /// </summary>
        /// <returns><c>true</c> if all the operands are evaluable; otherwise, <c>false</c>.</returns>
        public bool IsEvaluable()
        {
            // The expression is only evaluable if all operands are also evaluable.
            return (
                Operand1.IsEvaluable() &&
                Operand2.IsEvaluable() &&
                Operand3.IsEvaluable()
            );
        }
        /// <summary>
        /// Evaluates the expression and returns its simplified value if possible.
        /// </summary>
        /// <returns>The value of the <see cref="OperationExpression{T1, T2, T3, TResult}"/> object after the operation is performed.</returns>
        public TResult Evaluate()
        {
            // Return the result of performing the operation on the evaluated operands if possible.
            // If impossible, return the default value for the result.
            if (IsEvaluable())
                return Operation(
                    Operand1.Evaluate(),
                    Operand2.Evaluate(),
                    Operand3.Evaluate()
                );
            else return default(TResult);
        }
    }
    /// <summary>
    /// Represents an expression node in an abstract syntax tree that is an operation on 4 operands.
    /// </summary>
    /// <typeparam name="T1">The type of operand 1.</typeparam>
    /// <typeparam name="T2">The type of operand 2.</typeparam>
    /// <typeparam name="T3">The type of operand 3.</typeparam>
    /// <typeparam name="T4">The type of operand 4.</typeparam>
    /// <typeparam name="TResult">The type of the resulting value.</typeparam>
    public class OperationExpression<T1, T2, T3, T4, TResult> : IExpression<TResult>
    {
        /// <summary>
        /// The operation to perform on the evaluated operands.
        /// </summary>
        public readonly Func<T1, T2, T3, T4, TResult> Operation;

        /// <summary>
        /// The operand in position 1.
        /// </summary>
        public readonly IExpression<T1> Operand1;
        /// <summary>
        /// The operand in position 2.
        /// </summary>
        public readonly IExpression<T2> Operand2;
        /// <summary>
        /// The operand in position 3.
        /// </summary>
        public readonly IExpression<T3> Operand3;
        /// <summary>
        /// The operand in position 4.
        /// </summary>
        public readonly IExpression<T4> Operand4;

        /// <summary>
        /// Creates a new <see cref="OperationExpression{T1, T2, T3, T4, TResult}"/> object with the specified operation and operands.
        /// </summary>
        /// <param name="operation">The operation to perform on the operands.</param>
        /// <param name="operand1">Operand 1.</param>
        /// <param name="operand2">Operand 2.</param>
        /// <param name="operand3">Operand 3.</param>
        /// <param name="operand4">Operand 4.</param>
        public OperationExpression(
            Func<T1, T2, T3, T4, TResult> operation,
            IExpression<T1> operand1,
            IExpression<T2> operand2,
            IExpression<T3> operand3,
            IExpression<T4> operand4
        )
        {
            Operation = operation;
            Operand1 = operand1;
            Operand2 = operand2;
            Operand3 = operand3;
            Operand4 = operand4;
        }

        /// <summary>
        /// Determines whether the expression can be validly evaluated.
        /// </summary>
        /// <returns><c>true</c> if all the operands are evaluable; otherwise, <c>false</c>.</returns>
        public bool IsEvaluable()
        {
            // The expression is only evaluable if all operands are also evaluable.
            return (
                Operand1.IsEvaluable() &&
                Operand2.IsEvaluable() &&
                Operand3.IsEvaluable() &&
                Operand4.IsEvaluable()
            );
        }
        /// <summary>
        /// Evaluates the expression and returns its simplified value if possible.
        /// </summary>
        /// <returns>The value of the <see cref="OperationExpression{T1, T2, T3, T4, TResult}"/> object after the operation is performed.</returns>
        public TResult Evaluate()
        {
            // Return the result of performing the operation on the evaluated operands if possible.
            // If impossible, return the default value for the result.
            if (IsEvaluable())
                return Operation(
                    Operand1.Evaluate(),
                    Operand2.Evaluate(),
                    Operand3.Evaluate(),
                    Operand4.Evaluate()
                );
            else return default(TResult);
        }
    }
}