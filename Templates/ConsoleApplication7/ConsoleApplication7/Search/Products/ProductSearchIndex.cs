using System;
using System.Linq;
using System.Threading.Tasks;
using ConsoleApplication7.Application.Actors.Products.Add;
using ConsoleApplication7.Domain;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Search;

namespace ConsoleApplication7.Application.Search.Products
{
    public class ProductSearchIndex : SearchIndex<ProductSearchResult>, IHandleEvent<AddProductEvent>
    {
        public ProductSearchIndex(ISearchContext context)
            : base(context)
        {
        }

        public override async Task RebuildIndexAsync()
        {
            await this.ClearAsync();

            var source = await this.Domain.FindAsync<Product>();
            var search = source.Select(e => new ProductSearchResult
            {
                Crawled = true,
                ProductId = e.Id,
                Description = e.Description,
                State = e.State,
                Name = e.Name
            });

            await this.AddAsync(search.ToArray());
        }

        public Task HandleAsync(AddProductEvent instance)
        {
            return this.AddAsync(new ProductSearchResult { Name = instance.Product.Name });
        }
    }
}
