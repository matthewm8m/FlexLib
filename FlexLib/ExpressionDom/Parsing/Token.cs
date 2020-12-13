namespace FlexLib.ExpressionDom.Parsing
{
    /// <summary>
    /// Represents a <see cref="Token"/> created from a source by a <see cref="TokenDefinition"/>.
    /// </summary>
    public class Token
    {
        /// <summary>
        /// The source code that the token was extracted from.
        /// </summary>
        public readonly TokenSource Source;
        /// <summary>
        /// The token definition that was used to create the token.
        /// </summary>
        public readonly TokenDefinition Definition;
        /// <summary>
        /// The intrinsic value stored in the token by a parser.
        /// </summary>
        public readonly object Value;

        /// <summary>
        /// Creates a new <see cref="Token"/> object with the specified source, definition, and value.
        /// </summary>
        /// <param name="source">The source of the token.</param>
        /// <param name="definition">The token definition used to generate the token.</param>
        /// <param name="value">The internal value of the token.</param>
        public Token(
            TokenSource source,
            TokenDefinition definition,
            object value = null
        )
        {
            Source = source;
            Definition = definition;
            Value = value;
        }
    }
}