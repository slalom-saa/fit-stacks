using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.Stacks.Search
{
    /// <summary>
    /// Reads data elements from the domain so that the
    /// </summary>
    /// <typeparam name="TEntity">The type of the t entity.</typeparam>
    public interface IEntityReader<TEntity>
    {
        /// <summary>
        /// Reads elements of the specified .
        /// </summary>
        /// <returns>IQueryable&lt;TEntity&gt;.</returns>
        IQueryable<TEntity> Read();
    }
}
