using ConsoleApplication7.Domain;
using Slalom.Stacks.Search;

namespace ConsoleApplication7.Application.Search.Products
{
    public class ProductSearchResult : ISearchResult
    {
        public bool Crawled { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string ProductId { get; set; }

        public string Description { get; set; }

        public ProductStatus State { get; set; } = ProductStatus.Draft;
    }
}