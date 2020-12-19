using System.Collections.Generic;
using System.Linq;

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
        public LexerRule Rule;

        /// <summary>
        /// Determines whether the <see cref="TokenRulePattern"/> object matches a specified collection of tokens.
        /// </summary>
        /// <param name="tokens">The collections of tokens to check for a match. Matches are checked strictly from the beginning of the collection.</param>
        /// <returns>The number of tokens that match the pattern from the start of the enumerable. Set to <c>1</c> if a match was successful; otherwise, set to <c>0</c>.</returns>
        public virtual int FindMatch(IEnumerable<Token> tokens)
        {
            // In order to match the given enumerable, there must be at least 1 token and it must match the specified rule.
            if (tokens != null && tokens.Any())
            {
                if (Rule == null || Rule == tokens.First().Definition)
                {
                    // We eat the first token if it matches.
                    return 1;
                }
            }

            // We eat no tokens if there is no match.
            return 0;
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
        /// Determines whether the <see cref="TokenRulePattern{T}"/> object matches a specified collection of tokens.
        /// </summary>
        /// <param name="tokens">The collections of tokens to check for a match. Matches are checked strictly from the beginning of the collection.</param>
        /// <returns>The number of tokens that match the pattern from the start of the enumerable. Set to <c>1</c> if a match was successful; otherwise, set to <c>0</c>.</returns>
        public override int FindMatch(IEnumerable<Token> tokens)
        {
            // We check if the base match conditions are satisfied first. If they are, we additionally require the token value type to match. 
            bool match = base.FindMatch(tokens) > 0;
            if (match)
            {
                if (tokens.First().Value is T)
                {
                    // We eat the first token if it matches.
                    return 1;
                }
            }

            // We eat no tokens if there is no match.
            return 0;
        }
        /// <summary>
        /// Determines whether the <see cref="TokenRulePattern{T}"/> object matches a specified collection of tokens.
        /// </summary>
        /// <param name="tokens">The collections of tokens to check for a match. Matches are checked strictly from the beginning of the collection.</param>
        /// <param name="value">The value of the token if it matched.</param>
        /// <returns>The number of tokens that match the pattern from the start of the enumerable. Set to <c>1</c> if a match was successful; otherwise, set to <c>0</c>.</returns>
        public int FindMatch(IEnumerable<Token> tokens, out T value)
        {
            // We check if the base match conditions are satisfied first. If they are, we additionally require the token value type to match. 
            bool match = base.FindMatch(tokens) > 0;
            if (match)
            {
                if (tokens.First().Value is T tokenValue)
                {
                    // We eat the first token if it matches.
                    value = tokenValue;
                    return 1;
                }
            }

            // We eat no tokens if there is no match.
            value = default(T);
            return 0;
        }
    }
}