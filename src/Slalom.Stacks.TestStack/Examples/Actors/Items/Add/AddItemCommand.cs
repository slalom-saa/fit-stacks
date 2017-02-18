using Slalom.Stacks.Messaging;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.TestStack.Examples.Actors.Items.Add
{
    [Request("Add Item")]
    public class AddItemCommand : Message
    {
        [NotNullOrWhiteSpace("An item must have a name to be added.")]
        public string Name { get; }

        public AddItemCommand(string name)
        {
            this.Name = name;
        }
    }
}