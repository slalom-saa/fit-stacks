using Slalom.Stacks.Communication;
using Slalom.Stacks.Messaging;

namespace Slalom.Stacks.Test.Domain
{
    public class ItemAddedEvent : Event
    {
        public Item Item { get; private set; }

        public ItemAddedEvent(Item item)
        {
            this.Item = item;
        }
    }
}