using System;
using System.Linq;
using ConsoleApplication7.Application.Search.Products;
using ConsoleApplication7.Domain;
using Slalom.Stacks.Messaging;

namespace ConsoleApplication7.Application.Actors.Products.Search
{
    public class SearchProducts : UseCaseActor<SearchProductsCommand, IQueryable<ProductSearchResult>>
    {
        public override IQueryable<ProductSearchResult> Execute(SearchProductsCommand command)
        {
            return this.Search.Search<ProductSearchResult>(command.Text)
                       .Where(e => e.State == ProductStatus.Active);
        }
    }
}