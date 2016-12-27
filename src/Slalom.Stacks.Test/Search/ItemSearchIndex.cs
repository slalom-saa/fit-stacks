using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Search;
using Slalom.Stacks.Test.Domain;

namespace Slalom.Stacks.Test.Search
{
    public class ItemSearchIndex : SearchIndexer<ItemSearchResult>, IHandleEvent<ItemAddedEvent>
    {
        public ItemSearchIndex(ISearchContext context)
            : base(context)
        {
        }

        public IDomainFacade Domain { get; set; }

        public override async Task RebuildIndexAsync()
        {
            await this.ClearAsync();

            var index = 0;
            var size = 1000;

            var set = (await this.Domain.FindAsync<Item>(e => true)).AsQueryable();

            var working = set.Take(size).ToList();
            while (working.Any())
            {
                await this.AddAsync(working.Select(e => new ItemSearchResult()).ToArray());
                working = set.Skip(++index * size).Take(size).ToList();
            }
        }

        public async Task Handle(ItemAddedEvent instance, ExecutionContext context)
        {
            await this.AddAsync(new[] { new ItemSearchResult
            {
                Text = instance.Item.Text
            }});
        }
    }
}