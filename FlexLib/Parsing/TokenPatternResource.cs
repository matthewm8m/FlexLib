using System;
using System.Xml;
using System.Xml.Linq;

namespace FlexLib.Parsing
{
    /// <summary>
    /// Represents a text pattern to be tokenized and its corresponding parsing function.
    /// </summary>
    public class TokenPatternResource
    {
        /// <summary>
        /// The regular expression pattern used to find matches in text for the token. 
        /// </summary>
        public readonly string RegexPattern;
        /// <summary>
        /// The function used to convert matches in text to an object.
        /// </summary>
        public readonly Func<string, object> Parser;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenPatternResource"/> class with the specified matching and parsing details.
        /// </summary>
        /// <param name="regexPattern">The regular expression matching pattern.</param>
        /// <param name="parser">The parsing function for matches.</param>
        public TokenPatternResource(string regexPattern, Func<string, object> parser = null)
        {
            // Set properties of the token pattern.
            RegexPattern = regexPattern;
            Parser = parser;
        }
    }
}