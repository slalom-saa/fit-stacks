using System;
using System.Threading.Tasks;

namespace Slalom.Stacks.Search
{
    /// <summary>
    /// Defines an interface for rebuilding a search index.
    /// </summary>
    public interface IRebuildSearchIndex
    {
        /// <summary>
        /// Rebuilds the search index.
        /// </summary>
        /// <returns>A task for asynchronous programming.</returns>
        Task RebuildIndexAsync();
    }
}