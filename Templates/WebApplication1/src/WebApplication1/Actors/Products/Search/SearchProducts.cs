using System;
using System.Linq;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Runtime;
using WebApplication1.Controllers;
using WebApplication1.Search.Products;

namespace WebApplication1.Actors.Products.Search
{
    [Path("products/search")]
    public class SearchProducts : UseCaseActor<SearchProductsCommand, IQueryable<ProductSearchResult>>
    {
        public override IQueryable<ProductSearchResult> Execute(SearchProductsCommand command)
        {
            return this.Search.Search<ProductSearchResult>();
        }
    }

    [Path("products/search/active")]
    public class SearchActiveProducts : UseCaseActor<SearchProductsCommand, IQueryable<ProductSearchResult>>
    {
        public override IQueryable<ProductSearchResult> Execute(SearchProductsCommand command)
        {
            return this.Search.Search<ProductSearchResult>()
                      .Where(e => e.Expired == false);
        }
    }
}