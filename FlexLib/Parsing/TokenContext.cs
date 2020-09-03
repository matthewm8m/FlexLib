using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace FlexLib.Parsing
{
    /// <summary>
    /// Represents a set of token resources and how to interpret them.
    /// </summary>
    public class TokenContext
    {
        /// <summary>
        /// A collection of token resources to be used by the tokenizer.
        /// </summary>
        public readonly IList<TokenResource> Resources;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenContext"/> class with no resources.
        /// </summary>
        public TokenContext()
            : this(Enumerable.Empty<TokenResource>()) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenContext"/> class with the specified resources.
        /// </summary>
        /// <param name="tokenResources">A collection of token resources used to define the properties of tokens.</param>
        public TokenContext(IEnumerable<TokenResource> tokenResources)
        {
            Resources = new List<TokenResource>(tokenResources);
        }

        /// <summary>
        /// Constructs a new token context from an XML `tokens` element.
        /// </summary>
        /// <param name="xml">The XML element containing token resources.</param>
        /// <returns>The newly constructed token context.</returns>
        public static TokenContext FromXml(XElement xml)
        {
            // Throw an exception if no argument passed in.
            if (xml == null)
                throw new ArgumentNullException(nameof(xml));

            // Throw an exception if we are given the wrong type of element.
            string xmlName = xml.Name.LocalName.ToLower();
            if (!xmlName.Equals("tokens"))
                throw ParsingExceptionInfo.AttachLinkExceptionInfo(
                    new ArgumentException($"Expected element of type 'tokens'; got type '{xmlName}'."),
                    xml
                );

            // Create a new token context and return it.
            TokenContext tokenContext = new TokenContext(
                xml.Elements("token")
                   .Select(xmlToken => TokenResource.FromXml(xmlToken))
                );
            return tokenContext;
        }
    }
}