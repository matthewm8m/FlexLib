namespace FlexLib.Parsing
{
    /// <summary>
    /// Represents the stage that the parsing process is in. The stages run sequentially as follows:
    /// 1. Link - where the context resource is linked to code references.
    /// 2. Tokenize - where the context is used to segment the user input.
    /// 3. Interpret/Compile - where the user input is interpreted and a result is created or the input is compiled into an assembly.
    /// </summary>
    public enum ParsingStage
    {
        /// <summary>
        /// The link stage.
        /// </summary>
        LINK,
        /// <summary>
        /// The tokenize stage.
        /// </summary>
        TOKENIZE,
        /// <summary>
        /// The interpret stage.
        /// </summary>
        INTERPRET,
        /// <summary>
        /// The compile stage.
        /// </summary>
        COMPILE
    }
}