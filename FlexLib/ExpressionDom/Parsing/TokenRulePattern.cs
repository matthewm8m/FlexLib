namespace FlexLib.ExpressionDom.Parsing
{
    /// <summary>
    /// Represents a token pattern that matches a token rule.
    /// </summary>
    public class TokenRulePattern : ITokenPattern
    {
        /// <summary>
        /// The rule that tokens must have in order to match this pattern.
        /// </summary>
        public TokenDefinition Rule;

        /// <summary>
        /// Determines whether the <see cref="TokenRulePattern"/> object matches a specified token.
        /// </summary>
        /// <param name="token">The token to check for a match.</param>
        /// <returns><c>true</c> if the <see cref="Rule"/> matches the token's <see cref="Token.Definition"/>; otherwise, <c>false</c>.</returns>
        public virtual bool IsMatch(Token token)
        {
            return Rule == token.Definition;
        }
    }

    /// <summary>
    /// Represents a token pattern that matches a token rule where the token has a specific type.
    /// </summary>
    /// <typeparam name="T">The type of token value that must match.</typeparam>
    public class TokenRulePattern<T> : TokenRulePattern
    {
        /// <summary>
        /// Used to specify where the data from matched tokens should be placed in a corresponding transformation.
        /// </summary>
        public int? Parameter;

        /// <summary>
        /// Determines whether the <see cref="TokenRulePattern{T}"/> object matches a specified token.
        /// </summary>
        /// <param name="token">The token to check for a match.</param>
        /// <returns><c>true</c> if the <see cref="TokenRulePattern.Rule"/> matches the token's <see cref="Token.Definition"/> and the token's <see cref="Token.Value"/> has type <c>T</c>; otherwise, <c>false</c>.</returns>
        public override bool IsMatch(Token token)
        {
            return base.IsMatch(token) && token.Value is T;
        }
        /// <summary>
        /// Determines whether the <see cref="TokenRulePattern{T}"/> object matches a specified token.
        /// </summary>
        /// <param name="token">The token to check for a match.</param>
        /// <param name="value">The value of the token if it matched.</param>
        /// <returns><c>true</c> if the <see cref="TokenRulePattern.Rule"/> matches the token's <see cref="Token.Definition"/> and the token's <see cref="Token.Value"/> has type <c>T</c>; otherwise, <c>false</c>.</returns>
        public bool IsMatch(Token token, out T value)
        {
            if (base.IsMatch(token) && token.Value is T tokenValue)
            {
                value = tokenValue;
                return true;
            }
            else
            {
                value = default(T);
                return false;
            }
        }
    }
}