namespace FlexLib.Algebra
{
    /// <summary>
    /// Represents a mathematical field object. This field contains an additive and multiplicative identity called zero and one respectively that can be constructed at any time.
    /// </summary>
    /// <typeparam name="T">The type of field element under consideration.</typeparam>
    public abstract class BaseField<T> where T : BaseFieldElement<T>
    {
        /// <summary>
        /// Creates an instance of the additive identity <see cref="BaseFieldElement{T}"/> object of the current <see cref="BaseField{T}"/> object.
        /// </summary>
        /// <returns>The additive identity.</returns>
        public abstract T Zero();
        /// <summary>
        /// Creates an instance of the multiplicative identity <see cref="BaseFieldElement{T}"/> object of the current <see cref="BaseField{T}"/> object.
        /// </summary>
        /// <returns>The multiplicative identity.</returns>
        public abstract T One();
        /// <summary>
        /// Determines whether two instances of <see cref="BaseFieldElement{T}"/> are equal to each other.
        /// </summary>
        /// <param name="elementA">The first element.</param>
        /// <param name="elementB">The second element.</param>
        /// <returns></returns>
        public abstract bool ElementsEqual(T elementA, T elementB);

        /// <summary>
        /// Determines whether the specified object is equal to the current <see cref="BaseField{T}"/> object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is BaseField<T>)
                return true;
            return false;
        }
        /// <summary>
        /// Creates a hash code for the current <see cref="BaseField{T}"/> object.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Determines whether two <see cref="BaseField{T}"/> objects are equal. 
        /// </summary>
        /// <param name="fieldA">The first field.</param>
        /// <param name="fieldB">The second field.</param>
        /// <returns><c>true</c> if the fields are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(BaseField<T> fieldA, BaseField<T> fieldB)
        {
            return fieldA.Equals(fieldB);
        }
        /// <summary>
        /// Determines whether <see cref="BaseField{T}"/> objects are inequal.
        /// </summary>
        /// <param name="fieldA">The first field.</param>
        /// <param name="fieldB">The second field.</param>
        /// <returns><c>true</c> if the fields are not equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(BaseField<T> fieldA, BaseField<T> fieldB)
        {
            return !fieldA.Equals(fieldB);
        }
    }
}