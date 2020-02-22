namespace FlexLib.Algebra
{
    /// <summary>
    /// Represents a mathematical field object. This field contains an additive and multiplicative identity called zero and one respectively that can be constructed at any time.
    /// </summary>
    /// <typeparam name="T">The type of field element under consideration.</typeparam>
    public interface IField<T> where T : BaseFieldElement<T>
    {
        /// <summary>
        /// Creates an instance of the additive identity <see cref="BaseFieldElement{T}"/> object of the current <see cref="IField{T}"/> object.
        /// </summary>
        /// <returns>The additive identity.</returns>
        T Zero();
        /// <summary>
        /// Creates an instance of the multiplicative identity <see cref="BaseFieldElement{T}"/> object of the current <see cref="IField{T}"/> object.
        /// </summary>
        /// <returns>The multiplicative identity.</returns>
        T One();
    }
}