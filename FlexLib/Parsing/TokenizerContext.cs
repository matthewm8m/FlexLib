using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace FlexLib.Parsing
{
    public class TokenizerContext : List<TokenResource>
    {
        public TokenizerContext()
            : base() { }
        public TokenizerContext(IEnumerable<TokenResource> tokenResources)
            : base(tokenResources) { }
        
        public static TokenizerContext FromXml(XElement xml)
        {
            return new TokenizerContext(xml.Elements("token")
                .Select(tokenElement => TokenResource.FromXml(tokenElement)));
        }
    }
}
