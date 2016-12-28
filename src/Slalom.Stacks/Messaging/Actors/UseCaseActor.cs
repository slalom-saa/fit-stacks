using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging.Actors
{
    internal interface IUseCaseActor
    {
        Task<IEnumerable<ValidationError>> Validate(ICommand command, ExecutionContext context);
    }

    public abstract class UseCaseActor<TCommand, TResult> where TCommand : ICommand
    {
        private IUseCaseActor _useCaseActorImplementation;

        public IDomainFacade Domain { get; set; }

        public virtual Task<TResult> ExecuteAsync(TCommand command, ExecutionContext context)
        {
            return Task.FromResult(this.Execute(command, context));
        }

        public virtual TResult Execute(TCommand command, ExecutionContext context)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<ValidationError> Validate(TCommand command, ExecutionContext context)
        {
            yield break;
        }

        public virtual Task<IEnumerable<ValidationError>> ValidateAsync(TCommand command, ExecutionContext context)
        {
            return Task.FromResult(this.Validate(command, context));
        }

        public ExecutionContext Context { get; set; }
    }
}
