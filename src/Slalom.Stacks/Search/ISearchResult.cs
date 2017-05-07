/* 
 * Copyright (c) Stacks Contributors
 * 
 * This file is subject to the terms and conditions defined in
 * the LICENSE file, which is part of this source code package.
 */

namespace Slalom.Stacks.Search
{
    /// <summary>
    /// Defines a search result that is crawled and indexed.
    /// </summary>
    public interface ISearchResult
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        int Id { get; set; }
    }
}