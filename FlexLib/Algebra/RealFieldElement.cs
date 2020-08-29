using System;

using FlexLib.Reflection;

namespace FlexLib.Algebra
{
    /// <summary>
    /// Represents a real number in a mathematical field with the standard definitions of addition and mulitiplication.
    /// </summary>
    [Type("Real Number")]
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

        /// <summary>
        /// Parses a plain text number into a real element.
        /// </summary>
        /// <param name="text">The plain text number.</param>
        /// <returns>The parsed real element.</returns>
        [Parser]
        public static RealFieldElement ParseRealFieldElement(string text)
        {
            bool parseSuccess = double.TryParse(text, out double value);
            if (parseSuccess)
                return new RealFieldElement(value);
            else
                throw new ArgumentException($"'{text}' is invalid numeric string");
        }
    }
}