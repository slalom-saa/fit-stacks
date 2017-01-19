using System;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Search;
using WebApplication1.Actors.Products.Add;
using WebApplication1.Actors.Products.Expire;
using WebApplication1.Actors.Products.Update;
using WebApplication1.Domain;

namespace WebApplication1.Search.Products
{
    public class ProductSearchIndex : SearchIndex<ProductSearchResult>, IHandleEvent<AddProductEvent>, IHandleEvent<UpdateProductEvent>,
                                        IHandleEvent<ExpireProductEvent>
    {
        public ProductSearchIndex(ISearchContext context)
            : base(context)
        {
        }

        public IDomainFacade Domain { get; set; }

        public Task HandleAsync(AddProductEvent instance)
        {
            return this.RebuildIndexAsync();
        }

        public Task HandleAsync(UpdateProductEvent instance)
        {
            return this.RebuildIndexAsync();
        }

        public Task HandleAsync(ExpireProductEvent instance)
        {
            return this.RebuildIndexAsync();
        }

        public override async Task RebuildIndexAsync()
        {
            await this.ClearAsync();

            var target = this.Domain.FindAsync<Product>().Result
                             .ToList()
                             .Select(instance => new ProductSearchResult
                             {
                                 Description = instance.Description,
                                 Name = instance.Name,
                                 ProductId = instance.Id,
                                 Expired = instance.Expired,
                                 Crawled = true
                             }).ToArray();

            await this.AddAsync(target);
        }
    }
}