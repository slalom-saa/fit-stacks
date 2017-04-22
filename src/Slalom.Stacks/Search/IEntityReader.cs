using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.Stacks.Search
{
    public interface IEntityReader<TEntity>
    {
        IQueryable<TEntity> Read();
    }
}
