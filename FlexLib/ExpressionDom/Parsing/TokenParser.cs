namespace FlexLib.ExpressionDom.Parsing
{
    /// <summary>
    /// Represents a function definition that can be used to parse a string into a particular data type.
    /// </summary>
    /// <param name="source">The source string to parse.</param>
    /// <param name="result">The output data that was parsed.</param>
    /// <typeparam name="T">The type of output data.</typeparam>
    /// <returns><c>true</c> if the parsing was successful; otherwise, <c>false</c>.</returns>
    public delegate bool TokenParser<T>(string source, out T result);
}