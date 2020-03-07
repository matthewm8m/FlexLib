using System;

namespace FlexLib.Algebra
{
    /// <summary>
    /// Represents a real number in a mathematical field with the standard definitions of addition and mulitiplication.
    /// </summary>
    public struct RealFieldElement
    {
        /// <summary>
        /// The value of the real element as a floating point number representation.
        /// </summary>
        public readonly double Value;

        /// <summary>
        /// Constructs a new real element based on a floating point number.
        /// </summary>
        /// <param name="value">The floating point number.</param>
        public RealFieldElement(double value)
        {
            Value = value;
        }

        /// <summary>
        /// Implicitly converts a <see cref="double"/> object to a <see cref="RealFieldElement"/> object.
        /// </summary>
        /// <param name="value">The value of the real element.</param>
        public static implicit operator RealFieldElement(double value)
        {
            return new RealFieldElement(value);
        }
    }
}