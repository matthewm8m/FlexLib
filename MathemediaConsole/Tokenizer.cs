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
        public readonly string Text;
        public readonly int Position;
        public readonly object Value;

        public Token(string text, int pos, object val)
        {
            Text = text;
            Position = pos;
            Value = val;
        }
    }

    public class Tokenizer
    {

    }
}
