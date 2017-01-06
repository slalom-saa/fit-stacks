using Slalom.Stacks.Messaging;
using Slalom.Stacks.Serialization;

namespace Slalom.Stacks.Test.Examples.Actors.Items.Add
{
    public class AddItemCommand : Command
    {
        public string Text { get; private set; }

        public AddItemCommand(string text)
        {
            this.Text = text;
        }
    }
}