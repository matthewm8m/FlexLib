using System;

namespace FlexLib.ExpressionDom.Parsing
{
    /// <summary>
    /// Represents an exception that prevented the parser from completing its operation.
    /// </summary>
    public class ParserIncompleteException : Exception
    {
        /// <summary>
        /// The source of the parser error.
        /// </summary>
        public readonly TokenSource TokenSource;

        /// <summary>
        /// Creates a new instance of the <see cref="ParserIncompleteException"/> class.
        /// </summary>
        public ParserIncompleteException()
            : this((TokenSource)null) { }
        /// <summary>
        /// Creates a new instance of the <see cref="ParserIncompleteException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ParserIncompleteException(string message)
            : this((TokenSource)null, message) { }
        /// <summary>
        /// Creates a new instance of the <see cref="ParserIncompleteException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        public ParserIncompleteException(string message, Exception inner)
            : this((TokenSource)null, message, inner) { }
        /// <summary>
        /// Creates a new instance of the <see cref="ParserIncompleteException"/> class with a specified source of erraneous text.
        /// </summary>
        /// <param name="source">The source of the erraneous parser text.</param>
        public ParserIncompleteException(TokenSource source)
            : base()
        {
            TokenSource = source;
        }
        /// <summary>
        /// Creates a new instance of the <see cref="ParserIncompleteException"/> class with a specified source of erraneous text and an error message.
        /// </summary>
        /// <param name="source">The source of the erraneous parser text.</param>
        /// <param name="message">The message that describes the error.</param>
        public ParserIncompleteException(TokenSource source, string message)
            : base(message)
        {
            TokenSource = source;
        }
        /// <summary>
        /// Creates a new instance of the <see cref="ParserIncompleteException"/> class with a specified source of erranous text, an error message, and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="source">The source of the erraneous parser text.</param>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        public ParserIncompleteException(TokenSource source, string message, Exception inner)
            : base(message, inner)
        {
            TokenSource = source;
        }
    }
}