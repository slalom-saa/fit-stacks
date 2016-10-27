using System;
using System.Threading.Tasks;
using Slalom.Stacks.Communication;

namespace Authentication.SecurityRules.Communication
{
    public class TestCommandHandler : CommandHandler<TestCommand, TestEvent>
    {
        public override Task<TestEvent> Handle(TestCommand command)
        {
            throw new InvalidOperationException("The execution should not be this far.");
        }
    }
}