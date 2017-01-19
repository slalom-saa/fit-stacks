using System.Diagnostics;
using System.Threading.Tasks;
using Slalom.Stacks.Messaging;
using WebApplication1.Search.Products;

namespace WebApplication1.Search
{
    [Path("products/crawl")]
    public class CrawlProducts : UseCaseActor<CrawlProductsCommand, CrawlProductsEvent>
    {
        public override async Task<CrawlProductsEvent> ExecuteAsync(CrawlProductsCommand command)
        {
            var watch = Stopwatch.StartNew();

            await this.Search.RebuildIndexAsync<ProductSearchResult>();

            watch.Stop();

            return new CrawlProductsEvent(watch.Elapsed);
        }
    }
}