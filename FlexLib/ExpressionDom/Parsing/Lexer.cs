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
        private readonly IList<LexerRule> TokenDefinitions;
        private readonly Regex Regex;

        /// <summary>
        /// Creates a new <see cref="Lexer"/> object with the specified token definitions.
        /// </summary>
        /// <param name="definitions">A collection of token definitions to use.</param>
        public Lexer(IEnumerable<LexerRule> definitions)
        {
            // We add an element to the token definitions that represents any non-matching character sequences.
            // We can use this later to detect syntax errors.
            TokenDefinitions = new List<LexerRule>(definitions);
            TokenDefinitions.Add(new LexerRule { Pattern = @"." });

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
                We join together the patterns from each of the token definitions to form a pattern that matches all
                definitions. We then separate them out using their IDs. Notice that if a token definition overlaps with
                another, the one specified first will take precedence.
            */

            // Each token definition gets its own ID that can be referred back to in the lexing process.
            // The definition with the last ID represents the syntax error token.
            int id = 0;

            // We use the multiline flag so the '^' and '$' patterns work more intuitively for code.
            // We must the explicit capture flag so that subgroups in the expression aren't captured.
            // We use the compiled flag so that lexing is as fast as possible.
            string regexPattern = string.Join("|", TokenDefinitions.Select(def => $"(?<{++id}>{def.Pattern})"));
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
                // We make sure to keep track of the ID of the token definition because it allows us to obtain which
                // regular expression group corresponds to the definition.
                int id = 0;
                foreach (LexerRule def in TokenDefinitions)
                {
                    // We check for success of each definition/group and take some action based on it.
                    Group matchGroup = match.Groups[++id];
                    if (matchGroup.Success)
                    {
                        // This is the location of the match with a reference to the source text.
                        TokenSource tokenSource = new TokenSource(source, matchGroup.Value, matchGroup.Index);

                        // If the group that matched was the last group (unknown token), we throw a syntax exception if
                        // strict mode was specified. We continue lexing regardless.
                        if (id == TokenDefinitions.Count)
                        {
                            if (strict)
                                throw new LexerSyntaxException(tokenSource, "Unknown syntax encountered.");
                            continue;
                        }

                        // If we have a valid token definition, we yield the tokens that it decides to emit.
                        foreach (Token token in def.Tokenize(tokenSource))
                            yield return token;

                        // We break here because we don't allow for overlapping patterns and this improves performance.
                        break;
                    }
                }
            }
        }
    }
}