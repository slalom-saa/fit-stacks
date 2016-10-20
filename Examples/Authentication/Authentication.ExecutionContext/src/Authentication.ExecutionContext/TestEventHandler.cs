using System;
using System.Threading.Tasks;
using Slalom.LeanStack.Messaging;

namespace Authentication.ExecutionContext
{
    public class TestEventHandler : IHandleEvent<TestEvent>
    {
        public async Task Handle(TestEvent instance, IExecutionContext context)
        {
            await Console.Out.WriteLineAsync($"Entering event handler with user {context.User.Identity.Name}.");
        }
    }
}





