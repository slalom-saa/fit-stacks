using System;
using Slalom.Stacks.Search;

namespace Slalom.Stacks.TestStack.Examples.Search
{
    public class ItemSearchResult : ISearchResult
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public bool Crawled { get; set; }
    }
}