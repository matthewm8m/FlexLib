using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace FlexLib.Parsing
{
    /// <summary>
    /// Represents a regular expression and its corresponding parser.
    /// </summary>
    public struct TokenPattern
    {
        /// <summary>
        /// The regular expression to match text to tokens.
        /// </summary>
        public readonly string Pattern;
        /// <summary>
        /// The name of the parser to use to interpret data.
        /// </summary>
        public readonly string Parser;

        /// <summary>
        /// Construct a token patter with a pattern and a parser.
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="parser"></param>
        public TokenPattern(string pattern, string parser)
        {
            Pattern = pattern;
            Parser = parser;
        }
    }

    /// <summary>
    /// Represents a set of patterns and properties defining a token.
    /// </summary>
    public class TokenResource
    {
        /// <summary>
        /// The representative name of this type of token.
        /// </summary>
        public readonly string Name;
        /// <summary>
        /// Whether the token should be emitted by the tokenizer.
        /// </summary>
        public readonly bool Ignore;
        /// <summary>
        /// A symbol to represent any instance of this type of token.
        /// </summary>
        public readonly string Ligature;
        /// <summary>
        /// The type of data that the token will represent.
        /// </summary>
        public readonly string Type;
        /// <summary>
        /// The list of regular expression patterns that are used to match this type of token.
        /// </summary>
        public readonly List<TokenPattern> TokenPatterns;

        /// <summary>
        /// Constructs a new token resource with specified names, patterns, and properties.
        /// </summary>
        /// <param name="name">The name of the type of token.</param>
        /// <param name="patterns">A collection of the regular expression patterns to match.</param>
        /// <param name="ignore">Whether to ignore rather than emit a token from the tokenizer.</param>
        /// <param name="ligature">The symbol to represent the type of token.</param>
        /// <param name="type">The name of the type for the token data.</param>
        public TokenResource(string name, IEnumerable<TokenPattern> patterns, bool ignore = false, string ligature = null, string type = null)
        {
            // Set properties of the resource.
            Name = name;
            Ignore = ignore;
            Ligature = ligature;
            Type = type;

            // Check if patterns have been specified or use a default value.
            if (patterns == null)
                TokenPatterns = new List<TokenPattern>();
            else
                TokenPatterns = new List<TokenPattern>(patterns);
        }

        /// <summary>
        /// Constructs a new token resource from an XML `token` element.
        /// </summary>
        /// <param name="xml">The XML element containing token resource information.</param>
        /// <returns>The newly constructed token resource.</returns>
        public static TokenResource FromXml(XElement xml)
        {
            // Get all of the information from the formatted XML element.
            string name =
                xml.Element("name")?.Value;
            string ligature =
                xml.Element("ligature")?.Value;
            string type =
                xml.Element("type")?.Value;
            IEnumerable<TokenPattern> patterns =
                xml.Element("patterns")
                   .Elements("pattern")
                   .Select(patternElement =>
                        new TokenPattern(
                            patternElement.Element("regex")?.Value,
                            patternElement.Element("parser")?.Value
                        ));
            bool ignoreParseSuccess = bool.TryParse(
                xml.Element("ignore")?.Value, out bool ignore
            );

            // Raise an exception if any parsing was unsuccessful.
            if (!ignoreParseSuccess)
                throw new FormatException($"Token '{name}' has malformatted 'ignore' tag.");

            // Construct and return the token resource.
            return new TokenResource(
                name,
                patterns,
                ignore,
                ligature,
                type
            );
        }
    }
}
