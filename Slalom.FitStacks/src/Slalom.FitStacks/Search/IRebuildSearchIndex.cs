using System;
using System.Threading.Tasks;

namespace Slalom.FitStacks.Search
{
    /// <summary>
    /// Rebuilds a search index.
    /// </summary>
    public interface IRebuildSearchIndex
    {
        /// <summary>
        /// Rebuilds the search index.
        /// </summary>
        /// <returns>Task.</returns>
        Task RebuildIndexAsync();
    }
}