using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.FitStacks.Reflection
{
    public interface IDiscoverTypes
    {
        IEnumerable<Type> Find<TType>();
    }
}
