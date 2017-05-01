using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.Stacks.Search
{
    /// <summary>
    /// Reads data elements from the domain so that they can be indexed.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity to read.</typeparam>
    public interface IEntityReader<TEntity>
    {
        /// <summary>
        /// Reads elements so that they can be indexed.
        /// </summary>
        /// <returns>Returns a query that can be used to layer additional queries.</returns>
        IQueryable<TEntity> Read();
    }
}
