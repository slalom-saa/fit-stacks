using Slalom.Stacks.Messaging;

namespace ConsoleApplication7.Application.Actors.Products.Search
{
    [Request("Search Products")]
    public class SearchProductsCommand : Command
    {
        public SearchProductsCommand(string text)
        {
            this.Text = text;
        }

        public string Text { get; private set; }
    }
}