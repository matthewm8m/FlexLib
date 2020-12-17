namespace FlexLib.ExpressionDom.Parsing
{
    /// <summary>
    /// Represents a pattern that can be used to match against a token.
    /// </summary>
    public interface ITokenPattern
    {
        /// <summary>
        /// Determines whether the <see cref="ITokenPattern"/> object matches a specified token.
        /// </summary>
        /// <param name="token">The token to check for a match.</param>
        /// <returns><c>true</c> if the pattern matches the token; otherwise, <c>false</c>.</returns>
        bool IsMatch(Token token);
    }
}