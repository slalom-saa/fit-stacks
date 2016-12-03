using Slalom.Stacks.Communication;

namespace Slalom.Stacks.ConsoleClient
{
    public class AddItemCommand : Command<ItemAddedEvent>
    {
        public string Text { get; private set; }

        public AddItemCommand(string text)
        {
            this.Text = text;
        }
    }
}