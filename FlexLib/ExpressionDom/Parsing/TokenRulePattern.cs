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
        /// <param name="count">The number of tokens that match the pattern from the start of the enumerable. Set to <c>1</c> if a match was successful; otherwise, set to <c>0</c>.</param>
        /// <returns><c>true</c> if the <see cref="Rule"/> matches the first token's <see cref="Token.Definition"/>; otherwise, <c>false</c>.</returns>
        public virtual bool IsMatch(IEnumerable<Token> tokens, out int count)
        {
            // In order to match the given enumerable, there must be at least 1 token and it must match the specified rule.
            if (tokens != null && tokens.Any())
            {
                if (Rule == null || Rule == tokens.First().Definition)
                {
                    // We eat the first token if it matches.
                    count = 1;
                    return true;
                }
            }

            // We eat no tokens if there is no match.
            count = 0;
            return false;
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
        /// <param name="count">The number of tokens that match the pattern from the start of the enumerable. Set to <c>1</c> if a match was successful; otherwise, set to <c>0</c>.</param>
        /// <returns><c>true</c> if the <see cref="TokenRulePattern.Rule"/> matches the first token's <see cref="Token.Definition"/> and the first token's <see cref="Token.Value"/> has type <c>T</c>; otherwise, <c>false</c>.</returns>
        public override bool IsMatch(IEnumerable<Token> tokens, out int count)
        {
            // We check if the base match conditions are satisfied first. If they are, we additionally require the token value type to match. 
            bool match = base.IsMatch(tokens, out _);
            if (match)
            {
                if (tokens.First().Value is T)
                {
                    // We eat the first token if it matches.
                    count = 1;
                    return true;
                }
            }

            // We eat no tokens if there is no match.
            count = 0;
            return false;
        }
        /// <summary>
        /// Determines whether the <see cref="TokenRulePattern{T}"/> object matches a specified collection of tokens.
        /// </summary>
        /// <param name="tokens">The collections of tokens to check for a match. Matches are checked strictly from the beginning of the collection.</param>
        /// <param name="count">The number of tokens that match the pattern from the start of the enumerable. Set to <c>1</c> if a match was successful; otherwise, set to <c>0</c>.</param>
        /// <param name="value">The value of the token if it matched.</param>
        /// <returns><c>true</c> if the <see cref="TokenRulePattern.Rule"/> matches the first token's <see cref="Token.Definition"/> and the first token's <see cref="Token.Value"/> has type <c>T</c>; otherwise, <c>false</c>.</returns>
        public bool IsMatch(IEnumerable<Token> tokens, out int count, out T value)
        {
            // We check if the base match conditions are satisfied first. If they are, we additionally require the token value type to match. 
            bool match = base.IsMatch(tokens, out _);
            if (match)
            {
                if (tokens.First().Value is T tokenValue)
                {
                    // We eat the first token if it matches.
                    count = 1;
                    value = tokenValue;
                    return true;
                }
            }

            // We eat no tokens if there is no match.
            count = 0;
            value = default(T);
            return false;
        }
    }
}