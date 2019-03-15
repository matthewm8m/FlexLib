using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathemediaConsole
{
    public struct TokenRule
    {
        public readonly string Pattern;
        public readonly Func<string, object> Parse;

        public TokenRule(string pattern, Func<string, object> parse)
        {
            Pattern = pattern;
            Parse = parse;
        }
    }

    public struct Token
    {

    }

    public class Tokenizer
    {

    }
}
