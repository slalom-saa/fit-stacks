using Slalom.Stacks.Domain;
using Slalom.Stacks.Search;

namespace Slalom.Stacks.Caching
{
    internal static class Identity
    {
        public static object GetIdentity(object instance)
        {
            var entity = instance as IAggregateRoot;
            if (entity != null)
            {
                return entity.Id;
            }
            var result = instance as ISearchResult;
            if (result != null)
            {
                return result.Id;
            }
            return instance.GetHashCode();
        }
    }
}