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
        /// <returns>The number of tokens that match the pattern from the start of the enumerable. This is set to <c>0</c> if there is no match.</returns>
        int FindMatch(IEnumerable<Token> tokens);
    }
}