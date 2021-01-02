using System.Collections.Generic;
using System.Linq;

namespace FlexLib.ExpressionDom.Parsing
{
    /// <summary>
    /// Represents the location of and text that generated a token within an original source text. This is used for debugging purposes to give detailed information on where parsing errors occur.
    /// </summary>
    public class TokenSource
    {
        /// <summary>
        /// The entire source code that the token was extracted from.
        /// </summary>
        public readonly string Source;
        /// <summary>
        /// The index start location of the snippet source within the entire source.
        /// </summary>
        public readonly int IndexStart;
        /// <summary>
        /// The index end location of the snippet source within the entire source.
        /// </summary>
        public readonly int IndexEnd;

        /// <summary>
        /// The particular segment of source code that represents the token.
        /// </summary>
        public string Snippet
        {
            get => Source.Substring(IndexStart, IndexEnd - IndexStart);
        }

        /// <summary>
        /// Creates a new <see cref="TokenSource"/> object with the specified source, snippet, and location.
        /// </summary>
        /// <param name="source">The entire source code.</param>
        /// <param name="indexStart">The start location of the snippet.</param>
        /// <param name="indexEnd">The end location of the snippet.</param>
        public TokenSource(string source, int indexStart, int indexEnd)
        {
            Source = source;
            IndexStart = indexStart;
            IndexEnd = indexEnd;
        }

        /// <summary>
        /// Joins multiple token sources together to form a new token source. Used when combining tokens to update debugging information.
        /// </summary>
        /// <param name="sources">The collection of tokens.</param>
        /// <returns>The combined token.</returns>
        public static TokenSource Join(IEnumerable<TokenSource> sources)
        {
            // Check if there are any sources and if not, return null.
            sources = sources?.Where(source => source != null);
            if (sources == null || !sources.Any())
                return null;

            // Find the start and end indexes of the union of sources.
            int indexStartNew = sources.Min(source => source.IndexStart);
            int indexEndNew = sources.Max(source => source.IndexEnd);

            // The new source is the same as any of the previous sources.
            string sourceNew = sources.First().Source;

            return new TokenSource(
                sourceNew,
                indexStartNew,
                indexEndNew
            );
        }
    }
}