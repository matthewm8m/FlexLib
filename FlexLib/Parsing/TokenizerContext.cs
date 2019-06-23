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
        
        public static TokenizerContext FromXml(XDocument xml)
        {
            return new TokenizerContext(xml.Element("tokens").Elements("token")
                .Select(tokenElement =>
                    new TokenResource(
                        tokenElement.Attribute("name").Value,
                        tokenElement.Element("patterns").Elements("pattern")
                            .Select(patternElement => patternElement.Value)
                    )
                )
            );
        }
    }
}
