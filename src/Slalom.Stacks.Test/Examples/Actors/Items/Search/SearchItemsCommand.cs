using System;
using Slalom.Stacks.Messaging;

namespace Slalom.Stacks.Test.Examples.Actors.Items.Search
{
    [Request("Search Items")]
    public class SearchItemsCommand : Command
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