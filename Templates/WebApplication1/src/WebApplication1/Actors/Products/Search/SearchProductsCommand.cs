using System.Linq;
using Slalom.Stacks.Messaging;

namespace WebApplication1.Actors.Products.Search
{
    public class SearchProductsCommand : Command
    {
        public string Search { get; }

        public SearchProductsCommand(string search)
        {
            this.Search = search;
        }
    }
}