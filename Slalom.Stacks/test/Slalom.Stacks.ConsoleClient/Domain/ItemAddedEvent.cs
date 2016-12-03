using Slalom.Stacks.Communication;

namespace Slalom.Stacks.ConsoleClient
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