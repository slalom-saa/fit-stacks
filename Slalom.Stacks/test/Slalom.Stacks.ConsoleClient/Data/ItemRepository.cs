using Slalom.FitStacks.ConsoleClient.Domain;
using Slalom.Stacks.Data.Mongo;

namespace Slalom.FitStacks.ConsoleClient.Data
{
    public class ItemRepository : MongoRepository<Item>
    {
        public ItemRepository(EntityContext context)
            : base(context)
        {
        }
    }
}