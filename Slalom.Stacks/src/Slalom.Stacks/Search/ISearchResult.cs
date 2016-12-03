using System;

namespace Slalom.Stacks.Search
{
    /// <summary>
    /// Marker and constraint for a Search Result element.
    /// </summary>
    /// <seealso href="http://bit.ly/2cZxU7q">Microsoft .NET: Architecting Applications for the Enterprise, Second Edition: Chapter 11. Implementing CQRS</seealso>
    public interface ISearchResult
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        int Id { get; }
    }
}