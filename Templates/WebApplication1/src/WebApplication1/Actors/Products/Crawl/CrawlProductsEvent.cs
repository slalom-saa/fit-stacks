using System;
using Slalom.Stacks.Messaging;

namespace WebApplication1.Search
{
    [Event(3000, Name = "Products Crawled")]
    public class CrawlProductsEvent : Event
    {
        public TimeSpan Elapsed { get; }

        public CrawlProductsEvent(TimeSpan elapsed)
        {
            this.Elapsed = elapsed;
        }
    }
}