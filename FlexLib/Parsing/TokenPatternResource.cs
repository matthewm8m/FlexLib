using System;
using System.Text.RegularExpressions;

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

            // Check if the regular expression pattern contains groups.
            // If so, throw an exception because this will screw with the tokenizer.
            Regex regex = new Regex(
                RegexPattern,
                RegexOptions.Compiled | RegexOptions.ExplicitCapture
            );
            if (regex.GetGroupNumbers().Length > 1)
                throw new FormatException($"Regular expressions used for token patterns must not contain groups. Violating pattern was '{RegexPattern}'.");
        }
    }
}