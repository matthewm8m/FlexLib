using System.Collections.Generic;

namespace FlexLib.ExpressionDom.Parsing
{
    /// <summary>
    /// Represents a parser which performs a conversion from a token stream to a reduced token stream based on a set of specified <see cref="ParserRule"/> in order of precedence in <see cref="ParserLevel"/>.
    /// </summary>
    public class Parser
    {
        private readonly IList<ParserLevel> Levels;

        /// <summary>
        /// Creates a new <see cref="Parser"/> object with the specified parser rules organized in levels.
        /// </summary>
        /// <param name="levels">The ordered parser levels including all parser rules.</param>
        public Parser(IEnumerable<ParserLevel> levels)
        {

        }

        /// <summary>
        /// Performs parsing to reduce a token stream.
        /// </summary>
        /// <param name="tokens">The input token stream.</param>
        /// <returns>The output reduced token stream.</returns>
        public IEnumerable<Token> Parse(IEnumerable<Token> tokens)
        {
            yield break;
        }
        /// <summary>
        /// Performs parsing to reduce a token stream into a single token. If the parsing cannot reduce the token stream to a single token, a <see cref="ParserIncompleteException"/> is thrown.
        /// </summary>
        /// <param name="tokens">The input token stream.</param>
        /// <returns>The output single token.</returns>
        public Token ParseSingular(IEnumerable<Token> tokens)
        {
            return null;
        }
    }
}