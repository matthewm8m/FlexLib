using System.Collections.Generic;

namespace FlexLib.ExpressionDom.Parsing
{
    /// <summary>
    /// Represents a token pattern that matches an ordered group of sub-patterns. 
    /// </summary>
    public class TokenGroupPattern : ITokenPattern
    {
        /// <summary>
        /// Creates a new <see cref="TokenGroupPattern"/> object with the specified sub-patterns.
        /// </summary>
        /// <param name="patterns">The ordered sub-patterns.</param>
        public TokenGroupPattern(IEnumerable<ITokenPattern> patterns)
        {

        }

        /// <summary>
        /// Determines whether the <see cref="TokenGroupPattern"/> object matches a specified collection of tokens.
        /// </summary>
        /// <param name="tokens">The collections of tokens to check for a match. Matches are checked strictly from the beginning of the collection.</param>
        /// <returns>The number of tokens that match the pattern from the start of the enumerable. Set to the number of sub-patterns if a match was successful; otherwise, set to <c>0</c>.</returns>
        public int FindMatch(IEnumerable<Token> tokens)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// Collects the parameter values from a specified token stream.
        /// </summary>
        /// <param name="tokens">The token stream.</param>
        /// <param name="parameters">The number of parameters to expect.</param>
        /// <returns>A list of collected parameter values with length equal to <c>parameters</c>. If a parameter could not be found, it is assigned to <c>null</c>.</returns>
        public IList<object> FindParameters(IEnumerable<Token> tokens, int parameters)
        {
            throw new System.NotImplementedException();
        }
    }
}