namespace FlexLib.ExpressionDom.Parsing
{
    /// <summary>
    /// Represents a function definition that can be used to convert a string into a particular data type.
    /// </summary>
    /// <param name="source">The source string to convert.</param>
    /// <param name="result">The output data that was converted.</param>
    /// <typeparam name="T">The type of output data.</typeparam>
    /// <returns><c>true</c> if the conversion was successful; otherwise, <c>false</c>.</returns>
    public delegate bool TokenConverter<T>(string source, out T result);
}