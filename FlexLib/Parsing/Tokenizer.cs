using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace FlexLib.Parsing
{
    public class Tokenizer
    {
        private readonly TokenizerContext Context;
        private readonly Regex Rule;

        public Tokenizer(TokenizerContext context)
            : this(context, true) { }
        public Tokenizer(TokenizerContext context, bool ignoreWhitespace)
        {
            string whitespaceDelimiter = ignoreWhitespace ? @"\s*" : @"";

            Context = context;
            Rule = new Regex($@"{whitespaceDelimiter}(?:{
                    string.Join(@"|", Context.Select(tokenResource => $@"({
                        string.Join(@"|", tokenResource.RegexPatterns.Select(tokenPattern => $@"(?:{tokenPattern})"))
                        })"))
                    }|(.)){whitespaceDelimiter}", RegexOptions.Compiled);
        }

        public IEnumerable<Token> Tokenize(string input)
        {
            MatchCollection matches = Rule.Matches(input);

            foreach (Match match in matches)
            {
                bool tokenized = false;
                for (int i = 0; i < Context.Count; i++)
                {
                    if (!string.IsNullOrEmpty(match.Groups[i + 1].Value))
                    {
                        yield return new Token(Context[i].Name, input, match.Index);
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
