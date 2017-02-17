using System.Threading.Tasks;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Search;
using Slalom.Stacks.TestStack.Examples.Actors.Items.Add;

namespace Slalom.Stacks.TestStack.Examples.Search
{
    public class ItemSearchIndex : SearchIndex<ItemSearchResult>, IHandleEvent<AddItemEvent>
    {
        public ItemSearchIndex(ISearchContext context)
            : base(context)
        {
        }

        public override Task RebuildIndexAsync()
        {
            return Task.FromResult(0);
        }

        public async Task HandleAsync(AddItemEvent instance)
        {
            await this.AddAsync(new[] { new ItemSearchResult
            {
                Text = instance.Item.Name
            }});
        }
    }
}