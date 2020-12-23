namespace FlexLib.ExpressionDom.Parsing
{
    /// <summary>
    /// The direction of the parser for transforming tokens.
    /// </summary>
    public enum ParserAssociativity
    {
        /// <summary>
        /// Work through tokens from left to right.
        /// </summary>
        LeftToRight = 0,
        /// <summary>
        /// Work through tokens from right to left.
        /// </summary>
        RightToLeft = 1,
    }
}