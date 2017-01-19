using System.Linq;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Test.Examples.Search;

namespace Slalom.Stacks.Test.Examples.Actors.Items.Search
{
    public class SearchItems : UseCaseActor<SearchItemsCommand, IQueryable<ItemSearchResult>>
    {
        public override IQueryable<ItemSearchResult> Execute(SearchItemsCommand command)
        {
            return this.Search.Search<ItemSearchResult>(command.Text);
        }
    }
}