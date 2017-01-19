using Slalom.Stacks.Search;

namespace WebApplication1.Search.Products
{
    public class ProductSearchResult : ISearchResult
    {
        public int Id { get; set; }

        public string ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Expired { get; set; }

        public bool Crawled { get; set; }
    }
}