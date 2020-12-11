using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace FlexLib.Parsing
{
    public class TokenResource
    {
        public readonly string Name;
        public readonly string Ligature;
        public readonly List<string> RegexPatterns;

        public TokenResource(string name, IEnumerable<string> patterns)
            : this(name, null, patterns) { }
        public TokenResource(string name, string ligature, IEnumerable<string> patterns)
        {
            Name = name;
            Ligature = ligature;
            if (patterns == null)
            {
                RegexPatterns = new List<string>();
            } else
            {
                RegexPatterns = new List<string>(patterns);
            }
        }

        public static TokenResource FromXml(XElement xml)
        {
            return new TokenResource(
                        xml.Attribute("name")?.Value,
                        xml.Element("ligature")?.Value,
                        xml.Element("patterns")?.Elements("pattern")
                            ?.Select(patternElement => patternElement.Value));
        }
    }
}
