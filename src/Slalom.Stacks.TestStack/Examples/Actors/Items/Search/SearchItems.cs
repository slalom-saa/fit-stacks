using System.Linq;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.TestStack.Examples.Search;

namespace Slalom.Stacks.TestStack.Examples.Actors.Items.Search
{
    public class SearchItems : UseCaseActor<SearchItemsCommand, IQueryable<ItemSearchResult>>
    {
        public override IQueryable<ItemSearchResult> Execute(SearchItemsCommand message)
        {
            return this.Search.Search<ItemSearchResult>(message.Text);
        }
    }
}