using System;

namespace FlexLib.ExpressionDom.Parsing
{
    /// <summary>
    /// Represents an exception that occurred while a lexer was converting text to tokens. 
    /// </summary>
    public class LexerSyntaxException : Exception
    {
        /// <summary>
        /// The source of the lexer error.
        /// </summary>
        public readonly TokenSource TokenSource;

        /// <summary>
        /// Creates a new instance of the <see cref="LexerSyntaxException"/> class.
        /// </summary>
        public LexerSyntaxException()
            : this((TokenSource)null) { }
        /// <summary>
        /// Creates a new instance of the <see cref="LexerSyntaxException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public LexerSyntaxException(string message)
            : this((TokenSource)null, message) { }
        /// <summary>
        /// Creates a new instance of the <see cref="LexerSyntaxException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        public LexerSyntaxException(string message, Exception inner)
            : this((TokenSource)null, message, inner) { }
        /// <summary>
        /// Creates a new instance of the <see cref="LexerSyntaxException"/> class with a specified source of erraneous text.
        /// </summary>
        /// <param name="source">The source of the erraneous lexer text.</param>
        public LexerSyntaxException(TokenSource source)
            : base()
        {
            TokenSource = source;
        }
        /// <summary>
        /// Creates a new instance of the <see cref="LexerSyntaxException"/> class with a specified source of erraneous text and an error message.
        /// </summary>
        /// <param name="source">The source of the erraneous lexer text.</param>
        /// <param name="message">The message that describes the error.</param>
        public LexerSyntaxException(TokenSource source, string message)
            : base(message)
        {
            TokenSource = source;
        }
        /// <summary>
        /// Creates a new instance of the <see cref="LexerSyntaxException"/> class with a specified source of erranous text, an error message, and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="source">The source of the erraneous lexer text.</param>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        public LexerSyntaxException(TokenSource source, string message, Exception inner)
            : base(message, inner)
        {
            TokenSource = source;
        }
    }
}