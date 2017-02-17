using Slalom.Stacks.Messaging;
using Slalom.Stacks.TestStack.Examples.Domain;

namespace Slalom.Stacks.TestStack.Examples.Actors.Items.Add
{
    [Event(2000, Name = "Item Added")]
    public class AddItemEvent : Event
    {
        public Item Item { get; private set; }

        public AddItemEvent(Item item)
        {
            this.Item = item;
        }
    }
}