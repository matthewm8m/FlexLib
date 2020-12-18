using System.Collections.Generic;

namespace FlexLib.ExpressionDom.Parsing
{
    /// <summary>
    /// Represents a pattern that can be used to match against a token.
    /// </summary>
    public interface ITokenPattern
    {
        /// <summary>
        /// Determines whether the <see cref="ITokenPattern"/> object matches a specified collection of tokens.
        /// </summary>
        /// <param name="tokens">The collections of tokens to check for a match. Matches are checked strictly from the beginning of the collection.</param>
        /// <param name="count">The number of tokens that match the pattern from the start of the enumerable.</param>
        /// <returns><c>true</c> if the pattern matches the tokens; otherwise, <c>false</c>.</returns>
        bool IsMatch(IEnumerable<Token> tokens, out int count);
    }
}