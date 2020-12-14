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

        /// <summary>
        /// Converts the string representation of a real number into its <see cref="RealFieldElement"/> equivalent. A return value indicates whether the operation succeeded.
        /// </summary>
        /// <param name="source">The string representation of a real number.</param>
        /// <param name="result">The resulting value if the conversion succeeded or the default value otherwise.</param>
        /// <returns></returns>
        public static bool TryParse(string source, out RealFieldElement result)
        {
            // We parse a real field element exactly the same as we would a double.
            bool success = double.TryParse(source, out double value);
            result = success ? new RealFieldElement(value) : default(RealFieldElement);
            return success;
        }
    }
}