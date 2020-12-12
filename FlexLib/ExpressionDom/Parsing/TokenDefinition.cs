namespace FlexLib.ExpressionDom.Parsing
{
    /// <summary>
    /// Represents a function definition that can be used to parse a string into a particular data type.
    /// </summary>
    /// <param name="source">The source string to parse.</param>
    /// <param name="result">The output data that was parsed.</param>
    /// <returns><c>true</c> if the parsing was successful; otherwise, <c>false</c>.</returns>
    public delegate bool TokenParser(string source, out object result);

    /// <summary>
    /// Represents a definition to create tokens from source code based on a pattern, optional parser, and other parsing parameters.
    /// </summary>
    public class TokenDefinition
    {
        /// <summary>
        /// The user-friendly name of the token definition. Does not need to be specified.
        /// </summary>
        public string Name;
        /// <summary>
        /// The Regular Expression pattern to use for searching in the source.
        /// </summary>
        public string Pattern;
        /// <summary>
        /// Determines if the parser ignores the token or emits it. Set to <c>true</c> to stop the token from being emitted; otherwise, leave set to <c>false</c>.
        /// </summary>
        public bool Ignore;
        /// <summary>
        /// The parsing function to turn the source string into another value and pass it to the created token.
        /// </summary>
        public TokenParser Parser;
    }
}