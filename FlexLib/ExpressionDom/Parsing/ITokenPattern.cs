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

        /// <summary>
        /// Collects the parameter values from a specified token stream.
        /// </summary>
        /// <param name="tokens">The token stream.</param>
        /// <param name="parameters">The number of parameters to expect.</param>
        /// <returns>A list of collected parameter values with length equal to <c>parameters</c>. If a parameter could not be found, it is assigned to <c>null</c>.</returns>
        IList<object> FindParameters(IEnumerable<Token> tokens, int parameters);
    }
}