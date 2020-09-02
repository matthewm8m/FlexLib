namespace FlexLib.Parsing
{
    /// <summary>
    /// Represents the origin details of a exception that occurred while the library was parsing user input.
    /// </summary>
    public struct ParsingExceptionInfo
    {
        /// <summary>
        /// Used to access the data of exceptions that occurred during parsing.
        /// </summary>
        public const string NAME = nameof(ParsingExceptionInfo);

        /// <summary>
        /// The stage of parsing when the exception occurred.
        /// </summary>
        public readonly ParsingStage Stage;
        /// <summary>
        /// The name of the file containing the text that caused the exception.
        /// </summary>
        public readonly string File;
        /// <summary>
        /// The line number in the file that is the origin of the exception.
        /// </summary>
        public readonly int? Line;
        /// <summary>
        /// The position on the line in the file that is the origin of the exception.
        /// </summary>
        public readonly int? Position;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParsingExceptionInfo"/> class with the specified exception origin details.
        /// </summary>
        /// <param name="stage"></param>
        /// <param name="file"></param>
        /// <param name="line"></param>
        /// <param name="position"></param>
        public ParsingExceptionInfo(
            ParsingStage stage,
            string file = null,
            int? line = null,
            int? position = null)
        {
            Stage = stage;
            File = file;
            Line = line;
            Position = position;
        }
    }
}