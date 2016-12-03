using Slalom.FitStacks.ConsoleClient.Domain;
using Slalom.Stacks.Communication;

namespace Slalom.FitStacks.ConsoleClient.Commands.AddItem
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