using System.Linq;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.TestStack.Examples.Search;

namespace Slalom.Stacks.TestStack.Examples.Actors.Items.Search
{
    public class SearchItems : Actor<SearchItemsCommand, IQueryable<ItemSearchResult>>
    {
        public override IQueryable<ItemSearchResult> Execute(SearchItemsCommand command)
        {
            return this.Search.Search<ItemSearchResult>(command.Text);
        }
    }
}