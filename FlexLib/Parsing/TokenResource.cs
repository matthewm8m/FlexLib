using System.Collections.Generic;

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
            RegexPatterns = new List<string>(patterns);
        }
    }
}
