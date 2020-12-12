using System;

namespace FlexLib.Algebra
{
    /// <summary>
    /// Represents the mathematical real field. This field contains an additive and multiplicative identity called zero and one respectively that can be constructed at any time.
    /// </summary>
    public class RealField : BaseField<RealFieldElement>
    {
        /// <summary>
        /// The amount at which two <see cref="RealFieldElement"/> objects can differ without being considered different elements.
        /// </summary>
        public readonly double Tolerance;

        /// <summary>
        /// Creates a new <see cref="RealField"/> object with zero <see cref="Tolerance"/>.
        /// </summary>
        public RealField()
            : this(0.0) { }
        /// <summary>
        /// Creates a new <see cref="RealField"/> object with the specified <see cref="Tolerance"/>.
        /// </summary>
        /// <param name="tolerance">The <see cref="Tolerance"/> to compare elements with.</param>
        public RealField(double tolerance)
        {
            Tolerance = tolerance;
        }

        /// <summary>
        /// Creates an instance of the additive identity <see cref="RealFieldElement"/> object of the current <see cref="RealField"/> object.
        /// </summary>
        /// <returns>The additive identity.</returns>
        public override RealFieldElement Zero()
        {
            return new RealFieldElement(0.0);
        }
        /// <summary>
        /// Creates an instance of the multiplicative identity <see cref="RealFieldElement"/> object of the current <see cref="RealField"/> object.
        /// </summary>
        /// <returns>The multiplicative identity.</returns>
        public override RealFieldElement One()
        {
            return new RealFieldElement(1.0);
        }

        /// <summary>
        /// Computes the sum of two <see cref="RealFieldElement"/> objects.
        /// </summary>
        /// <param name="elementA">The first real element of the sum.</param>
        /// <param name="elementB">The second real element of the sum.</param>
        /// <returns>The sum of the real elements.</returns>
        public override RealFieldElement Add(RealFieldElement elementA, RealFieldElement elementB)
        {
            return new RealFieldElement(elementA.Value + elementB.Value);
        }
        /// <summary>
        /// Computes the product of two <see cref="RealFieldElement"/> objects.
        /// </summary>
        /// <param name="elementA">The first real element of the product.</param>
        /// <param name="elementB">The second real element of the product.</param>
        /// <returns>The product of the real elements.</returns>
        public override RealFieldElement Multiply(RealFieldElement elementA, RealFieldElement elementB)
        {
            return new RealFieldElement(elementA.Value * elementB.Value);
        }

        /// <summary>
        /// Computes the additive inverse of a <see cref="RealFieldElement"/> object.
        /// </summary>
        /// <param name="element">The element to find the multiplicative inverse of.</param>
        /// <returns>The additive inverse of the element.</returns>
        public override RealFieldElement Negative(RealFieldElement element)
        {
            return new RealFieldElement(-element.Value);
        }
        /// <summary>
        /// Computes the multiplicative inverse of a <see cref="RealFieldElement"/> object.
        /// </summary>
        /// <param name="element">The object to find the additive inverse of.</param>
        /// <returns>The additive inverse of the element.</returns>
        public override RealFieldElement Inverse(RealFieldElement element)
        {
            // Do not allow division by zero.
            if (element.Value == 0.0)
                throw new DivideByZeroException();
            return new RealFieldElement(1.0 / element.Value);
        }

        /// <summary>
        /// Creates a clone of a <see cref="RealFieldElement"/> object.
        /// </summary>
        /// <param name="element">The object to clone.</param>
        /// <returns>The clone of the element</returns>
        public override RealFieldElement Clone(RealFieldElement element)
        {
            return new RealFieldElement(element.Value);
        }

        /// <summary>
        /// Determines whether two instances of <see cref="RealFieldElement"/> are equal to each other.
        /// </summary>
        /// <param name="elementA">The first element.</param>
        /// <param name="elementB">The second element.</param>
        /// <returns></returns>
        public override bool ElementsEqual(RealFieldElement elementA, RealFieldElement elementB)
        {
            if (elementA.Value == elementB.Value)
                return true;
            if (Math.Abs(elementA.Value - elementB.Value) <= Tolerance)
                return true;
            return false;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current <see cref="RealField"/> object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is RealField field)
                return Tolerance == field.Tolerance;
            return false;
        }
        /// <summary>
        /// Creates a hash code for the current <see cref="RealField"/> object.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return new { Tolerance }.GetHashCode();
        }
    }
}