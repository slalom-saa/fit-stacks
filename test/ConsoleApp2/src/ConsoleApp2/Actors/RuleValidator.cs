using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Slalom.Stacks.Messaging.Validation;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Actors
{
    public class RuleValidator : ReceiveActor
    {
        private readonly IComponentContext _context;
        private readonly ConcurrentDictionary<Type, ICommandValidator> _instances = new ConcurrentDictionary<Type, ICommandValidator>();

        public RuleValidator(IComponentContext context)
        {
            _context = context;

            this.ReceiveAsync<ExecuteUseCase>(this.HandleCommandReceived);
        }

        public async Task HandleCommandReceived(ExecuteUseCase message)
        {
            var validator = _instances.GetOrAdd(message.CommandType,
                key => (ICommandValidator)_context.Resolve(typeof(CommandValidator<>).MakeGenericType(message.CommandType)));

            var results = await validator.Validate(message.Command, message.Context); ;

            message.Result.AddValidationErrors(results);

            this.Sender.Tell(message);
        }
    }
}