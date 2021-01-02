using System.Collections.Generic;
using System.Linq;

namespace FlexLib.ExpressionDom.Parsing
{
    /// <summary>
    /// Represents a token pattern that matches an ordered group of sub-patterns. 
    /// </summary>
    public class TokenGroupPattern : ITokenPattern
    {
        private readonly IList<ITokenPattern> Patterns;

        /// <summary>
        /// Creates a new <see cref="TokenGroupPattern"/> object with the specified sub-patterns.
        /// </summary>
        /// <param name="patterns">The ordered sub-patterns.</param>
        public TokenGroupPattern(IEnumerable<ITokenPattern> patterns)
        {
            Patterns = new List<ITokenPattern>(patterns);
        }

        /// <summary>
        /// Determines whether the <see cref="TokenGroupPattern"/> object matches a specified collection of tokens.
        /// </summary>
        /// <param name="tokens">The collections of tokens to check for a match. Matches are checked strictly from the beginning of the collection.</param>
        /// <returns>The number of tokens that match the pattern from the start of the enumerable. Set to the number of sub-patterns if a match was successful; otherwise, set to <c>0</c>.</returns>
        public int FindMatch(IEnumerable<Token> tokens)
        {
            // In order to match the given enumerable, the tokens must not be null.
            if (tokens != null)
            {
                int matched = 0;
                foreach (ITokenPattern pattern in Patterns)
                {
                    // Check each pattern for matching (0 indicates no match).
                    // We skip ahead the number of tokens matched so patterns match in order correctly.
                    int matchedPattern = pattern.FindMatch(tokens);
                    if (matchedPattern > 0)
                        matched += matchedPattern;
                    else
                        return 0;
                    tokens = tokens.Skip(matchedPattern);
                }

                // Return the number of total tokens matched.
                return matched;
            }

            // We eat no tokens if there is no match.
            return 0;
        }
        /// <summary>
        /// Collects the parameter values from a specified token stream.
        /// </summary>
        /// <param name="tokens">The token stream.</param>
        /// <param name="parameters">The number of parameters to expect.</param>
        /// <returns>A list of collected parameter values with length equal to <c>parameters</c>. If a parameter could not be found, it is assigned to <c>null</c>.</returns>
        public IList<object> FindParameters(IEnumerable<Token> tokens, int parameters)
        {
            // We collect parameters from all of the sub-patterns in order.
            object[] parameterList = new object[parameters];
            foreach (ITokenPattern pattern in Patterns)
            {
                // We need the number of tokens that match in order to obtain valid parameters.
                int matchedTokens = pattern.FindMatch(tokens);

                // We only set a parameter if the pattern yields a non-null value.
                IList<object> parameterListPattern = pattern.FindParameters(tokens.Take(matchedTokens), parameters);
                for (int k = 0; k < parameters; k++)
                    parameterList[k] = parameterListPattern[k] ?? parameterList[k];

                // We need to skip ahead based on the number of matched tokens.
                tokens = tokens.Skip(matchedTokens);
            }
            return parameterList;
        }
    }
}