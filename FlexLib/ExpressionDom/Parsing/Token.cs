namespace FlexLib.ExpressionDom.Parsing
{
    /// <summary>
    /// Represents a <see cref="Token"/> created from a source by a <see cref="TokenDefinition"/>.
    /// </summary>
    public class Token
    {
        /// <summary>
        /// The entire source code that the token was extracted from.
        /// </summary>
        public readonly string Source;
        /// <summary>
        /// The particular segment of source code that represents the token.
        /// </summary>
        public readonly string Snippet;
        /// <summary>
        /// The index location of the snippet source within the entire source.
        /// </summary>
        public readonly int Location;

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
        /// <param name="source">The entire source.</param>
        /// <param name="snippet">The part source that generated the token.</param>
        /// <param name="location">The location of the snippet within the source.</param>
        /// <param name="definition">The token definition used to generate the token.</param>
        /// <param name="value">The internal value of the token.</param>
        public Token(
            string source,
            string snippet,
            int location,
            TokenDefinition definition,
            object value = null
        )
        {
            Source = source;
            Snippet = snippet;
            Location = location;

            Definition = definition;
            Value = value;
        }
    }
}