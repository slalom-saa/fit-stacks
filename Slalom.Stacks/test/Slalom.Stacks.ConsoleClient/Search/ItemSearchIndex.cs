using System.Linq;
using System.Threading.Tasks;
using Slalom.FitStacks.ConsoleClient.TestCommands;
using Slalom.Stacks.Communication;
using Slalom.Stacks.ConsoleClient;
using Slalom.Stacks.Data.EntityFramework;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Runtime;

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

    public Task Handle(ItemAddedEvent instance, ExecutionContext context)
    {
        return null;
    }
}