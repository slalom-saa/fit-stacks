using System;

namespace Slalom.Stacks.Search
{
    /// <summary>
    /// Defines a Search Result that is crawled and indexed.
    /// </summary>
    public interface ISearchResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ISearchResult"/> has been crawled.
        /// </summary>
        /// <value><c>true</c> if crawled; otherwise, <c>false</c>.</value>
        bool Crawled { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        int Id { get; set; }
    }
}