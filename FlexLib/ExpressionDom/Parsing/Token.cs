namespace FlexLib.ExpressionDom.Parsing
{
    /// <summary>
    /// Represents a <see cref="Token"/> created from a source by a <see cref="ITokenRule"/>.
    /// </summary>
    public class Token
    {
        /// <summary>
        /// The source code that the token was extracted from.
        /// </summary>
        public readonly TokenSource Source;
        /// <summary>
        /// The rule that was used to create the token.
        /// </summary>
        public readonly ITokenRule Rule;
        /// <summary>
        /// The intrinsic value stored in the token by a parser.
        /// </summary>
        public readonly object Value;

        /// <summary>
        /// Creates a new <see cref="Token"/> object with the specified source, rule, and value.
        /// </summary>
        /// <param name="source">The source of the token.</param>
        /// <param name="rule">The rule used to generate the token.</param>
        /// <param name="value">The internal value of the token.</param>
        public Token(
            ITokenRule rule,
            TokenSource source = null,
            object value = null
        )
        {
            Source = source;
            Rule = rule;
            Value = value;
        }

        /// <summary>
        /// Creates a string representation of the <see cref="Token"/> object.
        /// </summary>
        /// <returns>A string that represents the rule, source, and/or value of the token.</returns>
        public override string ToString()
        {
            if (Rule == null)
            {
                if (Value == null)
                    return $"[{Source?.Snippet}]";
                else
                    return $"[{Value}]";
            }
            else
            {
                if (Value == null)
                    return $"[{Rule.Name}:{Source?.Snippet}]";
                else
                    return $"[{Rule.Name}={Value}]";
            }
        }
    }
}