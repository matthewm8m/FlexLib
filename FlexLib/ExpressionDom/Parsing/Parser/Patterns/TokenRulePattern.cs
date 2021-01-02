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
        public ITokenRule Rule;

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
                if (Rule == null || Rule == tokens.First().Rule)
                {
                    // We eat the first token if it matches.
                    return 1;
                }
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
        public virtual IList<object> FindParameters(IEnumerable<Token> tokens, int parameters)
        {
            // No parameter values in a default token rule pattern.
            object[] parameterList = new object[parameters];
            return parameterList;
        }

        /// <summary>
        /// Creates a string representation of the <see cref="TokenRulePattern"/> object.
        /// </summary>
        /// <returns>A string that represents the token rule pattern.</returns>
        public override string ToString()
        {
            return $"({Rule.Name})";
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
        /// Collects the parameter values from a specified token stream.
        /// </summary>
        /// <param name="tokens">The token stream.</param>
        /// <param name="parameters">The number of parameters to expect.</param>
        /// <returns>A list of collected parameter values with length equal to <c>parameters</c>. If a parameter could not be found, it is assigned to <c>null</c>.</returns>
        public override IList<object> FindParameters(IEnumerable<Token> tokens, int parameters)
        {
            // We may have a single parameter if the property is assigned a valid index.
            object[] parameterList = new object[parameters];
            if (Parameter.HasValue && FindMatch(tokens) > 0)
            {
                if (0 <= Parameter && Parameter < parameters)
                    parameterList[Parameter.Value] = tokens.FirstOrDefault().Value;
            }
            return parameterList;
        }
    }
}