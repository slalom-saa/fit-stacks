using Slalom.Stacks.Messaging;
using Slalom.Stacks.Test.Domain;

namespace Slalom.Stacks.Test.Commands.AddItem
{
    public class AddItemEvent : Event
    {
        public Item Item { get; private set; }

        public AddItemEvent(Item item)
        {
            this.Item = item;
        }
    }
}