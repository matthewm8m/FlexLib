using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace FlexLib.Parsing
{
    public class TokenizerContext
    {
        public readonly List<TokenResource> TokenResources;

        public TokenizerContext()
            : this(Enumerable.Empty<TokenResource>()) { }
        public TokenizerContext(IEnumerable<TokenResource> tokenResources)
        {
            TokenResources = new List<TokenResource>(tokenResources);
        }

        public static TokenizerContext FromXml(XElement xml)
        {
            return new TokenizerContext(
                xml.Elements("token")
                   .Select(tokenElement => TokenResource.FromXml(tokenElement))
                );
        }
    }
}