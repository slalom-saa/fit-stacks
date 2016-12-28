using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Search;
using Slalom.Stacks.Test.Search;

namespace Slalom.Stacks.Test.Commands.SearchItems
{
    public class SearchItemsCommand : Command
    {
    }

    public class SearchItemsActor : UseCaseActor<SearchItemsCommand, IQueryable<ItemSearchResult>>
    {
        private readonly ISearchFacade _search;

        public SearchItemsActor(ISearchFacade search)
        {
            _search = search;
        }

        public override IQueryable<ItemSearchResult> Execute(SearchItemsCommand command, ExecutionContext context)
        {
            return _search.OpenQuery<ItemSearchResult>();
        }
    }
}
