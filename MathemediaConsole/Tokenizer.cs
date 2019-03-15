using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
        private readonly List<TokenRule> TokenRules;
        private readonly Regex Rule;

        public Tokenizer(List<TokenRule> tokenRules)
        {
            TokenRules = tokenRules;
            Rule = new Regex($@"\s*(?:{
                    string.Join(@"|", tokenRules.Select(r => $@"({r.Pattern})"))
                }|(.))\s*", RegexOptions.Compiled);
        }

        public IEnumerable<Token> Tokenize(string text, int start = 0)
        {
            MatchCollection matches = Rule.Matches(text, start);

            foreach(Match match in matches)
            {
                bool tokenized = false;
                for (int i = 0; i < TokenRules.Count; i++)
                {
                    if (!string.IsNullOrEmpty(match.Groups[i + 1].Value))
                    {
                        yield return new Token(match.Value, match.Index, TokenRules[i].Parse(match.Value));
                        tokenized = true;
                        break;
                    }
                }

                if (!tokenized)
                    throw new Exception($"Syntax error: Unexpected symbol '{match.Value}'");
            }
        }
    }
}
