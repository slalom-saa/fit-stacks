using Slalom.Stacks.Messaging;

namespace ConsoleApplication7.Application.Actors.Products.Index
{
    [Event(2000, Name = "Products Indexed")]
    public class IndexProductsEvent : Event
    {
    }
}