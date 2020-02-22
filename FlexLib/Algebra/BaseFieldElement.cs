namespace FlexLib.Algebra
{
    /// <summary>
    /// Represents an element in a mathematical field. Each element has additive and multiplicative inverses called the negative and inverse respectively. Two elements in a field may be added or multiplied together to form another element in the field.
    /// </summary>
    /// <typeparam name="T">The type of field element under consideration.</typeparam>
    public abstract class BaseFieldElement<T> where T : BaseFieldElement<T>
    {
        /// <summary>
        /// Computes the sum of the current and another <see cref="BaseFieldElement{T}"/> object.
        /// </summary>
        /// <param name="element">The other object.</param>
        /// <returns>The sum.</returns>
        public abstract T Add(T element);
        /// <summary>
        /// Computes the product of the current and another <see cref="BaseFieldElement{T}"/> object.
        /// </summary>
        /// <param name="element">The other object.</param>
        /// <returns>The product.</returns>
        public abstract T Multiply(T element);

        /// <summary>
        /// Computes the additive inverse of the current <see cref="BaseFieldElement{T}"/> object.
        /// </summary>
        /// <returns>The additive inverse.</returns>
        public abstract T Negative();
        /// <summary>
        /// Computes the multiplicative inverse of the current <see cref="BaseFieldElement{T}"/> object.
        /// </summary>
        /// <returns>The multiplicative inverse.</returns>
        public abstract T Inverse();

        /// <summary>
        /// Computes the sum of two <see cref="BaseFieldElement{T}"/> objects.
        /// </summary>
        /// <param name="elementA">The first object.</param>
        /// <param name="elementB">The second object.</param>
        /// <returns>The sum.</returns>
        public static T operator +(BaseFieldElement<T> elementA, T elementB)
        {
            return elementA.Add(elementB);
        }
        /// <summary>
        /// Creates a positive copy of a <see cref="BaseFieldElement{T}"/> object.
        /// </summary>
        /// <param name="element">The object.</param>
        /// <returns>The positive copy.</returns>
        public static T operator +(BaseFieldElement<T> element)
        {
            return element.Negative().Negative();
        }
        /// <summary>
        /// Computes the difference of two <see cref="BaseFieldElement{T}"/> objects.
        /// </summary>
        /// <param name="elementA">The first object.</param>
        /// <param name="elementB">The second object.</param>
        /// <returns>The difference.</returns>
        public static T operator -(BaseFieldElement<T> elementA, T elementB)
        {
            return elementA.Add(elementB.Negative());
        }
        /// <summary>
        /// Creates a negative copy of a <see cref="BaseFieldElement{T}"/> object.
        /// </summary>
        /// <param name="element">The object.</param>
        /// <returns>The negative copy.</returns>
        public static T operator -(BaseFieldElement<T> element)
        {
            return element.Negative();
        }
        /// <summary>
        /// Computes the product of two <see cref="BaseFieldElement{T}"/> objects.
        /// </summary>
        /// <param name="elementA">The first object.</param>
        /// <param name="elementB">The second object.</param>
        /// <returns>The product.</returns>
        public static T operator *(BaseFieldElement<T> elementA, T elementB)
        {
            return elementA.Multiply(elementB);
        }
        /// <summary>
        /// Computes the quotient of two <see cref="BaseFieldElement{T}"/> objects.
        /// </summary>
        /// <param name="elementA">The first object.</param>
        /// <param name="elementB">The second object.</param>
        /// <returns>The quotient.</returns>
        public static T operator /(BaseFieldElement<T> elementA, T elementB)
        {
            return elementA.Multiply(elementB.Inverse());
        }
    }
}