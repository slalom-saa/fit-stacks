using System;
using Slalom.Stacks.Messaging;

namespace Slalom.Stacks.TestStack.Examples.Actors.Items.Search
{
    [Request("Search Items")]
    public class SearchItemsCommand : Message
    {
        public SearchItemsCommand()
        {
        }

        public SearchItemsCommand(string text)
        {
            this.Text = text;
        }

        public string Text { get; }
    }
}