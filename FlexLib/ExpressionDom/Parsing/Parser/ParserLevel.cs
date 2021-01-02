using System.Collections.Generic;

namespace FlexLib.ExpressionDom.Parsing
{
    /// <summary>
    /// Represents a level of precedence in a the order of operations of a parser.
    /// </summary>
    public class ParserLevel
    {
        /// <summary>
        /// The parser rules of the level.
        /// </summary>
        public IList<ParserRule> Rules;
        /// <summary>
        /// The associativity of the level. Determines which direction the parser handles tokens.
        /// </summary>
        public ParserAssociativity Associativity;

        /// <summary>
        /// Creates a new instance of a <see cref="ParserLevel"/> with the specified rules.
        /// </summary>
        /// <param name="rules">The parser rules of the level.</param>
        public ParserLevel(IList<ParserRule> rules)
        {
            Rules = rules;
        }
        /// <summary>
        /// Creates a new instance of a <see cref="ParserLevel"/> with no rules.
        /// </summary>
        public ParserLevel()
        {
            Rules = new List<ParserRule>();
        }
    }
}