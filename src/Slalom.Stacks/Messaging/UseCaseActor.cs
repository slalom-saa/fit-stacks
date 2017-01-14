using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Search;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging
{
    public abstract class UseCaseActor<TCommand, TResult> : IHandle<TCommand> where TCommand : ICommand
    {
        public IDomainFacade Domain { get; set; }

        public ISearchFacade Search { get; set; }

        public virtual Task<TResult> ExecuteAsync(TCommand command)
        {
            return Task.FromResult(this.Execute(command));
        }

        public virtual TResult Execute(TCommand command)
        {
            throw new NotImplementedException($"The execution methods for the {this.GetType().Name} use case actor have not been implemented.");
        }

        public virtual IEnumerable<ValidationError> Validate(TCommand command)
        {
            yield break;
        }

        public virtual Task<IEnumerable<ValidationError>> ValidateAsync(TCommand command)
        {
            return Task.FromResult(this.Validate(command));
        }

        async Task<object> IHandle.HandleAsync(object instance)
        {
            var result = await this.ExecuteAsync((TCommand)instance);

            return result;
        }
    }
}
