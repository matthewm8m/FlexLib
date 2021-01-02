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
        /// The particular segment of source code that represents the token.
        /// </summary>
        public readonly string Snippet;
        /// <summary>
        /// The index location of the snippet source within the entire source.
        /// </summary>
        public readonly int Location;

        /// <summary>
        /// Creates a new <see cref="TokenSource"/> object with the specified source, snippet, and location.
        /// </summary>
        /// <param name="source">The entire source code.</param>
        /// <param name="snippet">The snippet of source code.</param>
        /// <param name="location">The location of the snippet.</param>
        public TokenSource(string source, string snippet, int location)
        {
            Source = source;
            Snippet = snippet;
            Location = location;
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

            // Sort the sources by location and obtain the first source.
            var sourcesSorted = sources.OrderBy(source => source.Location);
            var sourceFirst = sourcesSorted.First();

            // Join the snippets together to get a new snippet.
            // Otherwise, the source and location should be the same as the first source.
            string snippetNew = string.Join("", sourcesSorted.Select(source => source.Snippet));
            string sourceNew = sourceFirst.Source;
            int locationNew = sourceFirst.Location;

            return new TokenSource(
                sourceNew,
                snippetNew,
                locationNew
            );
        }
    }
}