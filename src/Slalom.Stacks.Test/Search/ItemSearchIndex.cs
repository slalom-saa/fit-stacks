using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Communication;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Search;
using Slalom.Stacks.Test.Commands.AddItem;
using Slalom.Stacks.Test.Domain;

namespace Slalom.Stacks.Test.Search
{
    public class ItemSearchIndexer : SearchIndexer<ItemSearchResult>, IHandleEvent<AddItemEvent>
    {
        public ItemSearchIndexer(ISearchContext context)
            : base(context)
        {
        }

        public IDomainFacade Domain { get; set; }

        public override async Task RebuildIndexAsync()
        {
        }

        public async Task Handle(AddItemEvent instance, ExecutionContext context)
        {
            await this.AddAsync(new[] { new ItemSearchResult
            {
                Text = instance.Item.Text
            }});
        }
    }
}