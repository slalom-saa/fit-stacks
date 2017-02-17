using Slalom.Stacks.Messaging;
using Slalom.Stacks.Serialization;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Test.Examples.Actors.Items.Add
{
    [Request("Add Item")]
    public class AddItemCommand : Command
    {
        [NotNullOrWhiteSpace("An item must have a name to be added.")]
        public string Name { get; }

        public AddItemCommand(string name)
        {
            this.Name = name;
        }
    }
}