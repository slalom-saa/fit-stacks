using System.Threading.Tasks;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Search;
using Slalom.Stacks.Test.Examples.Actors.Items.Add;

namespace Slalom.Stacks.Test.Examples.Search
{
    public class ItemSearchIndex : SearchIndex<ItemSearchResult>, IHandleEvent<AddItemEvent>
    {
        public ItemSearchIndex(ISearchContext context)
            : base(context)
        {
        }

        public IDomainFacade Domain { get; set; }

        public override async Task RebuildIndexAsync()
        {
        }

        public async Task HandleAsync(AddItemEvent instance)
        {
            await this.AddAsync(new[] { new ItemSearchResult
            {
                Text = instance.Item.Text
            }});
        }
    }
}