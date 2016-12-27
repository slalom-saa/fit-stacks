using System;
using System.Collections.Concurrent;
using System.Linq;
using Akka.Actor;
using Slalom.Stacks.Messaging.Validation;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Actors
{
    public class CommandValidationActor : ReceiveActor
    {
        private readonly IComponentContext _context;
        private readonly ConcurrentDictionary<Type, ICommandValidator> _instances = new ConcurrentDictionary<Type, ICommandValidator>();

        public CommandValidationActor(IComponentContext context)
        {
            _context = context;
            this.Receive<ValidateExternalRulesMessage>(e => this.HandleCommandReceived(e));
        }

        public void HandleCommandReceived(ValidateExternalRulesMessage message)
        {
            var validator = _instances.GetOrAdd(message.CommandType,
                key => (ICommandValidator)_context.Resolve(typeof(CommandValidator<>).MakeGenericType(message.CommandType)));

            var results = validator.Validate(message.Command, new LocalExecutionContext()).Result;

            this.Sender.Tell(new ExternalRulesValidatedMessage(message.Command, message.Caller, results));
        }
    }
}