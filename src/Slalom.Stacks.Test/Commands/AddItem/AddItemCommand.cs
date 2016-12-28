using Slalom.Stacks.Messaging;
using Slalom.Stacks.Serialization;
using Slalom.Stacks.Test.Domain;

namespace Slalom.Stacks.Test.Commands.AddItem
{
    public class AddItemCommand : Command
    {
        public string Text { get; private set; }

        [Secure]
        public string SecureProperty { get; set; }

        [Ignore]
        public string IgnoredProperty { get; set; }

        public AddItemCommand(string text)
        {
            this.Text = text;
        }
    }
}