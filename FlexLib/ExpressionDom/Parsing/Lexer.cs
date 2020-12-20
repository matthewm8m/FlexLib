using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FlexLib.ExpressionDom.Parsing
{
    /// <summary>
    /// Represents a lexer which performs a conversion from a source text to a list of tokens.
    /// </summary>
    public class Lexer
    {
        private readonly IList<LexerRule> Rules;
        private readonly Regex Regex;

        /// <summary>
        /// Creates a new <see cref="Lexer"/> object with the specified lexer rules.
        /// </summary>
        /// <param name="rules">A collection of lexer rules to use.</param>
        public Lexer(IEnumerable<LexerRule> rules)
        {
            // We add an element to the lexer rules that represents any non-matching character sequences.
            // We can use this later to detect syntax errors.
            Rules = new List<LexerRule>(rules);
            Rules.Add(new LexerRule { Pattern = @"." });

            // We need to compile the regular expressions before we can lex.
            Regex = CompileRegex();
        }

        /// <summary>
        /// Creates the regular expression object necessary to perform lexing.
        /// </summary>
        /// <returns>The compiled <see cref="Regex"/> object.</returns>
        private Regex CompileRegex()
        {
            /*
                We join together the patterns from each of the lexer rules to form a pattern that matches all rules. We
                then separate them out using their IDs. Notice that if a lexer rule overlaps with another, the one
                specified first will take precedence.
            */

            // Each lexer rule gets its own ID that can be referred back to in the lexing process.
            // The rule with the last ID represents the syntax error token.
            int id = 0;

            // We use the multiline flag so the '^' and '$' patterns work more intuitively for code.
            // We must the explicit capture flag so that subgroups in the expression aren't captured.
            // We use the compiled flag so that lexing is as fast as possible.
            string regexPattern = string.Join("|", Rules.Select(rule => $"(?<{++id}>{rule.Pattern})"));
            RegexOptions regexOptions = RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled;

            return new Regex(regexPattern, regexOptions);
        }

        /// <summary>
        /// Performs lexing for a source text producing a list of tokens.
        /// </summary>
        /// <param name="source">The source text.</param>
        /// <param name="strict">Whether the lexer should raise an error when unknown syntax is encountered.</param>
        /// <returns>An <see cref="IEnumerable{Token}"/> of tokens contained in the source text.</returns>
        public IEnumerable<Token> Lex(string source, bool strict = true)
        {
            // We find all the regular expression matches in the source text and iterate through them in lexical order.
            MatchCollection matches = Regex.Matches(source);
            foreach (Match match in matches)
            {
                // We make sure to keep track of the ID of the lexer rule because it allows us to obtain which
                // regular expression group corresponds to the rule.
                int id = 0;
                foreach (LexerRule rule in Rules)
                {
                    // We check for success of each rule/group and take some action based on it.
                    Group matchGroup = match.Groups[++id];
                    if (matchGroup.Success)
                    {
                        // This is the location of the match with a reference to the source text.
                        TokenSource tokenSource = new TokenSource(source, matchGroup.Value, matchGroup.Index);

                        // If the group that matched was the last group (unknown token), we throw a syntax exception if
                        // strict mode was specified. We continue lexing regardless.
                        if (id == Rules.Count)
                        {
                            if (strict)
                                throw new LexerSyntaxException(tokenSource, "Unknown syntax encountered.");
                            continue;
                        }

                        // If we have a valid lexer rule, we yield the tokens that it decides to emit.
                        foreach (Token token in rule.Tokenize(tokenSource))
                            yield return token;

                        // We break here because we don't allow for overlapping patterns and this improves performance.
                        break;
                    }
                }
            }
        }
    }
}