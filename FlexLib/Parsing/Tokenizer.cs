using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using FlexLib.Reflection;

namespace FlexLib.Parsing
{
    /// <summary>
    /// Uses a specified tokenizer context to provide the rules for taking plain text and converting to tokens.
    /// </summary>
    public class Tokenizer
    {
        /// <summary>
        /// Specifies tokenizer rules and settings.
        /// </summary>
        private readonly TokenContext Context;
        /// <summary>
        /// The compiled regular expressions rule constructed from the context.
        /// </summary>
        private readonly Regex Rule;

        /// <summary>
        /// Constructs a tokenizer using a specific tokenizer context.
        /// </summary>
        /// <param name="context">The tokenizer context specifying rules and patterns.</param>
        public Tokenizer(TokenContext context)
        {
            // Set context.
            Context = context;

            // Compile the regular expression rule.
            int RuleIndex = 0;
            Rule = new Regex(
                $@"({
                    string.Join(@"|", Context.Resources.Select(tokenResource => $@"({
                        string.Join(@"|", tokenResource.Patterns.Select(tokenPattern => $@"(?<{++RuleIndex}>{tokenPattern.RegexPattern})"))
                    })"))
                }|(.))",
                RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.ExplicitCapture
            );
        }

        public IEnumerable<Token> Tokenize(string input)
        {
            // Find matches to the regular expression rule.
            MatchCollection matches = Rule.Matches(input);
            foreach (Match match in matches)
            {
                // For each match, find its corresponding token and emit it.
                bool tokenized = false;
                for (int i = 0; i < Context.Resources.Count; i++)
                {
                    if (!string.IsNullOrEmpty(match.Groups[i + 1].Value))
                    {
                        // Get the input value and the token resource.
                        string value = match.Groups[i + 1].Value;
                        TokenResource resource = Context.Resources[i];

                        // Only emit the token if the ignore flag is not set.
                        if (!resource.Ignore)
                        {
                            yield return new Token(resource.Name, value, resource.Ligature);
                        }

                        // A successful token has been matched; no need to raise an error.
                        tokenized = true;
                        break;
                    }
                }

                // Invalid syntax results in a syntax error.
                if (!tokenized)
                    throw new Exception($"Syntax error: Unexpected symbol '{match.Value}'.");
            }
        }
    }
}
