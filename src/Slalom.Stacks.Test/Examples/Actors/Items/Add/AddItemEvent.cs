using Slalom.Stacks.Messaging;
using Slalom.Stacks.Test.Examples.Domain;

namespace Slalom.Stacks.Test.Examples.Actors.Items.Add
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