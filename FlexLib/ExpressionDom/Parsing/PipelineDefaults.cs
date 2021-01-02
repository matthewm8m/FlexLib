namespace FlexLib.ExpressionDom.Parsing
{
    /// <summary>
    /// A utility for storing preconstructed parsing structures.
    /// </summary>
    public static class PipelineDefaults
    {
        /// <summary>
        /// The default lexer for general arithmetic and algebra.
        /// </summary>
        public static readonly Lexer DefaultLexer;

        /// <summary>
        /// Sets up all of the default lexers, parsers, and pipelines.
        /// </summary>
        static PipelineDefaults()
        {
            DefaultLexer = CreateDefaultLexer();
        }

        /// <summary>
        /// Creates the default lexer.
        /// </summary>
        /// <returns>The default lexer.</returns>
        private static Lexer CreateDefaultLexer()
        {
            return null;
        }
    }
}