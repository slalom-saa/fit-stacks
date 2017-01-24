using System;
using System.Linq;
using System.Threading.Tasks;
using ConsoleApplication7.Application.Search.Products;
using Slalom.Stacks.Messaging;

namespace ConsoleApplication7.Application.Actors.Products.Index
{
    public class IndexProducts : UseCaseActor<IndexProductsCommand, IndexProductsEvent>
    {
        public override async Task<IndexProductsEvent> ExecuteAsync(IndexProductsCommand command)
        {
            await this.Search.RebuildIndexAsync<ProductSearchResult>();

            return new IndexProductsEvent();
        }
    }
}