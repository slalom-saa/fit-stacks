using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Actors;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Test.Search;

namespace Slalom.Stacks.Test.Commands.SearchItems
{
    public class SearchItemsCommand : Command
    {
    }

    public class SearchItemsActor : UseCaseActor<SearchItemsCommand, IQueryable<ItemSearchResult>>
    {
        public override IQueryable<ItemSearchResult> Execute(SearchItemsCommand command, ExecutionContext context)
        {
            return this.Search.OpenQuery<ItemSearchResult>();
        }
    }
}
