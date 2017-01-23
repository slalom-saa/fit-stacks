using Slalom.Stacks.Messaging;
using Slalom.Stacks.Serialization;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Test.Examples.Actors.Items.Add
{
    public class AddItemCommand : Command
    {
        [NotNullOrWhitespace("An item must have text to be added.")]
        public string Text { get; }

        public AddItemCommand(string text)
        {
            this.Text = text;
        }
    }
}