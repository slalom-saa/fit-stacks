using Slalom.Stacks.Data.Mongo;

public class ItemRepository : MongoRepository<Item>
{
    public ItemRepository(EntityContext context)
        : base(context)
    {
    }
}