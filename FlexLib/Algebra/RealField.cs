namespace FlexLib.Algebra
{
    /// <summary>
    /// Represents the mathematical real field. This field contains an additive and multiplicative identity called zero and one respectively that can be constructed at any time.
    /// </summary>
    public class RealField : IField<RealFieldElement>
    {
        /// <summary>
        /// Creates an instance of the additive identity <see cref="RealFieldElement"/> object of the current <see cref="RealField"/> object.
        /// </summary>
        /// <returns>The additive identity.</returns>
        public RealFieldElement Zero()
        {
            return new RealFieldElement(0.0);
        }
        /// <summary>
        /// Creates an instance of the multiplicative identity <see cref="RealFieldElement"/> object of the current <see cref="RealField"/> object.
        /// </summary>
        /// <returns>The multiplicative identity.</returns>
        public RealFieldElement One()
        {
            return new RealFieldElement(1.0);
        }
    }
}