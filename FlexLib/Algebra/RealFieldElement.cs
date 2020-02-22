using System;

namespace FlexLib.Algebra
{
    /// <summary>
    /// Represents a real number in a mathematical field with the standard definitions of addition and mulitiplication. Any <see cref="RealFieldElement"/> has all the properties of a <see cref="BaseFieldElement{T}"/> object.
    /// </summary>
    public class RealFieldElement : BaseFieldElement<RealFieldElement>
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
        /// Computes the sum of the current and another <see cref="RealFieldElement"/> object.
        /// </summary>
        /// <param name="element">The other object.</param>
        /// <returns>The sum.</returns>
        public override RealFieldElement Add(RealFieldElement element)
        {
            return new RealFieldElement(Value + element.Value);
        }
        /// <summary>
        /// Computes the product of the current and another <see cref="RealFieldElement"/> object.
        /// </summary>
        /// <param name="element">The other object.</param>
        /// <returns>The product.</returns>
        public override RealFieldElement Multiply(RealFieldElement element)
        {
            return new RealFieldElement(Value * element.Value);
        }

        /// <summary>
        /// Computes the additive inverse of the current <see cref="RealFieldElement"/> object.
        /// </summary>
        /// <returns>The additive inverse.</returns>
        public override RealFieldElement Negative()
        {
            return new RealFieldElement(-Value);
        }
        /// <summary>
        /// Computes the multiplicative inverse of the current <see cref="RealFieldElement"/> object.
        /// </summary>
        /// <returns>The multiplicative inverse.</returns>
        public override RealFieldElement Inverse()
        {
            // Do not allow division by zero.
            if (Value == 0.0)
                throw new DivideByZeroException();
            return new RealFieldElement(1.0 / Value);
        }
    }
}