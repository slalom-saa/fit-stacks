using System;
using System.Threading.Tasks;
using Slalom.LeanStack.Messaging;

namespace Authentication.ExecutionContext
{
    public class TestCommandHandler : CommandHandler<TestCommand, TestEvent>
    {
        public override async Task<TestEvent> Handle(TestCommand command)
        {
            await Console.Out.WriteLineAsync($"Entering command handler with user {this.Context.User.Identity.Name}.");

            return new TestEvent();
        }
    }
}






