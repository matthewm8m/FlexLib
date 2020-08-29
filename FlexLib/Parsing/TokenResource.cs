using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace FlexLib.Parsing
{
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
        /// The list of regular expression patterns that are used to match this type of token.
        /// </summary>
        public readonly List<string> RegexPatterns;

        /// <summary>
        /// Constructs a new token resource with specified names, patterns, and properties.
        /// </summary>
        /// <param name="name">The name of the type of token.</param>
        /// <param name="patterns">A collection of the regular expression patterns to match.</param>
        /// <param name="ignore">Whether to ignore rather than emit a token from the tokenizer.</param>
        /// <param name="ligature">The symbol to represent the type of token.</param>
        public TokenResource(string name, IEnumerable<string> patterns, bool ignore = false, string ligature = null)
        {
            // Set properties of the resource.
            Name = name;
            Ignore = ignore;
            Ligature = ligature;

            // Check if patterns have been specified or use a default value.
            if (patterns == null)
                RegexPatterns = new List<string>();
            else
                RegexPatterns = new List<string>(patterns);
        }

        /// <summary>
        /// Constructs a new token resource from an XML `token` element.
        /// </summary>
        /// <param name="xml">The XML element containing token resource information.</param>
        /// <returns>The newly constructed token resource.</returns>
        public static TokenResource FromXml(XElement xml)
        {
            // Load the XML based on specific format.
            return new TokenResource(
                xml.Element("name")?.Value,                             // Name of token type
                xml.Element("patterns")?                                // Regular expression patterns
                   .Elements("pattern")?
                   .Select(patternElement => patternElement.Value),
                Convert.ToBoolean(xml.Element("ignore")?.Value),        // Ignore or emit
                xml.Element("ligature")?.Value                          // Ligature of token type
            );
        }
    }
}
