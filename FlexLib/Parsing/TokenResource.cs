using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using FlexLib.Reflection;

namespace FlexLib.Parsing
{
    /// <summary>
    /// Represents a set of patterns and properties defining a token.
    /// </summary>
    public class TokenResource
    {
        /// <summary>
        /// The common name of this type of token.
        /// </summary>
        public readonly string Name;
        /// <summary>
        /// The type of data that represents this type of token.
        /// Will be set to <c>null</c> if no special data should be associated.
        /// </summary>
        public readonly Type Type;
        /// <summary>
        /// Whether the token should be emitted by the tokenizer.
        /// If <c>true</c>, token will not be emitted. If <c>false</c>, token will be emitted.
        /// </summary>
        public readonly bool Ignore;
        /// <summary>
        /// A symbol to represent any instance of this type of token.
        /// Will be set to <c>null</c> if no special symbol is specified.
        /// </summary>
        public readonly string Ligature;
        /// <summary>
        /// The regular expression patterns and corresponding parsers for this type of token.
        /// Each of the parser functions in the <see cref="TokenPatternResource"/> class should be guaranteed to return an object of type <see cref="Type"/>.
        /// </summary>
        public readonly IList<TokenPatternResource> Patterns;

        /// <summary>
        /// Constructs a new token resource with specified names, patterns, and properties.
        /// </summary>
        /// <param name="name">The common name of the type of token.</param>
        /// <param name="patterns">A collection of the regular expression patterns to match.</param>
        /// <param name="type">The name of the type for the token data.</param>
        /// <param name="ignore">Whether to ignore, rather than emit, a token from the tokenizer.</param>
        /// <param name="ligature">The symbol to represent the type of token.</param>
        public TokenResource(
            string name,
            IEnumerable<TokenPatternResource> patterns = null,
            Type type = null,
            bool ignore = false,
            string ligature = null)
        {
            // Set properties of the resource.
            Name = name;
            Type = type;
            Ignore = ignore;
            Ligature = ligature;

            // Check if patterns have been specified or use a default value.
            if (patterns == null)
                Patterns = new List<TokenPatternResource>();
            else
                Patterns = new List<TokenPatternResource>(patterns);
        }

        /// <summary>
        /// Constructs a new token resource from an XML `token` element.
        /// </summary>
        /// <param name="xml">The XML element containing token resource information.</param>
        /// <returns>The newly constructed token resource.</returns>
        public static TokenResource FromXml(XElement xml)
        {
            // Throw an exception if no argument passed in.
            if (xml == null)
                throw new ArgumentNullException(nameof(xml));

            // Throw an exception if we are given the wrong type of element.
            string xmlName = xml.Name.LocalName.ToLower();
            if (!xmlName.Equals("token"))
                throw ParsingExceptionInfo.AttachLinkExceptionInfo(
                    new ArgumentException($"Expected element of type 'token'; got type '{xmlName}'."),
                    xml
                );

            // Read the token type.
            // Then, use the attribute helper to find the corresponding type if it exists.
            // Note that we do not need to find a valid type because the resource can define new psuedotypes.
            string typeName = xml.Element("type")?.Value;
            Type type = AttributeHelper.FindType(typeName);

            // Read whether the ignore flag has been set in the XML element.
            bool ignore = xml.Element("ignore") != null;

            // Read the token ligature.
            string ligature = xml.Element("ligature")?.Value;

            // For each of the pattern elements, create an associated token pattern.
            IEnumerable<XElement> xmlPatterns = xml.Element("patterns")?.Elements("pattern");
            List<TokenPatternResource> patternResources = new List<TokenPatternResource>();
            if (xmlPatterns != null)
            {
                foreach (XElement xmlPattern in xmlPatterns)
                {
                    // Read the parser for the pattern.
                    string parserName = xml.Element("parser")?.Value;
                    Func<string, object> parser = AttributeHelper.FindParser(type, parserName);

                    // If the user specified a parser but we cannot find it, then an error has occured.
                    if (parserName != null && parser == null)
                        throw ParsingExceptionInfo.AttachLinkExceptionInfo(
                            new KeyNotFoundException($"Could not find parser named '{parserName}' for type '{typeName}'."),
                            xmlPattern
                        );

                    // Read the regular expression for the pattern.
                    string regex = xml.Element("regex")?.Value;

                    // Create a new pattern resource and add it to a list of patterns for our token.
                    TokenPatternResource patternResource = new TokenPatternResource(regex, parser);
                    patternResources.Add(patternResource);
                }
            }

            // Create a new token resource and return it.
            TokenResource tokenResource = new TokenResource(
                typeName,
                patternResources,
                type,
                ignore,
                ligature
            );
            return tokenResource;
        }
    }
}
