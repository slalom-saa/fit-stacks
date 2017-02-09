using Slalom.Stacks.Domain;

namespace Slalom.Stacks.Test.Examples.Domain
{
    public class Item : AggregateRoot
    {
        protected Item()
        {
        }

        public static Item Create(string text)
        {
            return new Item
            {
                Text = text
            };
        }

        public string Text { get; private set; }
    }
}