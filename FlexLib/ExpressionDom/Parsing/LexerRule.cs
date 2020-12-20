using System;
using System.Collections.Generic;

namespace FlexLib.ExpressionDom.Parsing
{
    /// <summary>
    /// Represents a lexer rule to create tokens from source code based on a pattern, optional parser, and other parsing parameters.
    /// </summary>
    public class LexerRule : TokenRule<TokenSource>
    {
        /// <summary>
        /// The regular expression pattern to use for searching in the source.
        /// </summary>
        public string Pattern;
        /// <summary>
        /// Determines if the parser ignores the token or emits it. Set to <c>true</c> to stop the token from being emitted; otherwise, leave set to <c>false</c>.
        /// </summary>
        public bool Ignore;

        /// <summary>
        /// Parses the snippet of source code in order to generate a sequence of tokens to replace it. Subclasses should override this method to yield sequences of tokens or typed tokens.
        /// </summary>
        /// <param name="source">The source of the token.</param>
        /// <returns>An <see cref="IEnumerable{Token}"/> of tokens generated by the rule.</returns>
        public override IEnumerable<Token> Tokenize(TokenSource source)
        {
            // When using the untyped lexer rule, there is no token data.
            // We only emit the token if the ignore flag is not raised.
            if (!Ignore)
                yield return new Token(source, this);
        }

        /// <summary>
        /// Creates a string representation of the <see cref="LexerRule"/> object.
        /// </summary>
        /// <returns>A string that represents the name, pattern, and emittance of the rule.</returns>
        public override string ToString()
        {
            // Should create a string of the form:
            // "Integer = /\d+/"
            // "Whitespace = ?/\s+/"
            return $"{Name} = {(Ignore ? "?" : "")}/{Pattern}/";
        }
    }

    /// <summary>
    /// Represents a lexer rule to create tokens of a particular type from source code based on a pattern, optional parser, and other parsing parameters.
    /// </summary>
    /// <typeparam name="T">The type of data to store in generated tokens.</typeparam>
    public class LexerRule<T> : LexerRule
    {
        /// <summary>
        /// The parsing function to turn the source string into another value and pass it to the created token.
        /// </summary>
        public TokenParser<T> Parser;

        /// <summary>
        /// Parses the snippet of source code in order to generate a sequence of typed tokens to replace it. Subclasses should override this method to yield sequences of tokens or typed tokens.
        /// </summary>
        /// <param name="source">The source of the token.</param>
        /// <returns>An <see cref="IEnumerable{Token}"/> of tokens generated by the rule.</returns>
        public override IEnumerable<Token> Tokenize(TokenSource source)
        {
            // If the ignore flag is raised, we don't emit any tokens.
            if (Ignore)
                yield break;

            // If a parser is not defined, we yield a token with the default data value.
            if (Parser == null)
                yield return new Token(source, this, default(T));

            // Otherwise, we attempt to parse the snippet into a data value.
            // If we succeed, we yield a token with that data.
            // Else, we raise an exception to signify that our regular expression and parser are not compatible.
            else
            {
                if (Parser(source.Snippet, out T value))
                    yield return new Token(source, this, value);
                else
                    throw new FormatException($"Regular expression and parser are incompatible in lexer rule: {this}");
            }
        }
    }
}