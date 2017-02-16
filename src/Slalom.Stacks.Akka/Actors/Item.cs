using Slalom.Stacks.Domain;

namespace Slalom.Stacks.Messaging
{
    public class Item : AggregateRoot
    {
        public string Name { get; }

        public Item(string name)
        {
            this.Name = name;
        }
    }
}