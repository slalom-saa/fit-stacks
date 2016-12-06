using System.Linq;
using System.Threading.Tasks;
using Slalom.FitStacks.ConsoleClient.Domain;
using Slalom.Stacks.Communication;
using Slalom.Stacks.Domain;
using Slalom.Stacks.EntityFramework;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Search;

namespace Slalom.FitStacks.ConsoleClient.Search
{
    public class ItemSearchIndex : SearchIndex<ItemSearchResult>, IHandleEvent<ItemAddedEvent>
    {
        public ItemSearchIndex(SearchContext context)
            : base(context)
        {
        }

        public IDomainFacade Domain { get; set; }

        public override async Task RebuildIndexAsync()
        {
            await this.ClearAsync();

            var index = 0;
            var size = 1000;

            var set = this.Domain.CreateQuery<Item>();

            var working = set.Take(size).ToList();
            while (working.Any())
            {
                await this.AddAsync(working.Select(e => new ItemSearchResult()).ToArray());
                working = set.Skip(++index * size).Take(size).ToList();
            }
        }

        public async Task Handle(ItemAddedEvent instance, ExecutionContext context)
        {
            await this.AddAsync(new ItemSearchResult
            {
                Text = instance.Item.Text
            });
        }
    }
}