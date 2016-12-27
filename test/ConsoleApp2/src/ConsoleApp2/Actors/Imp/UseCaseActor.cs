using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Slalom.Stacks.Actors.Imp.Messages;
using Slalom.Stacks.Communication;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Actors
{
    public abstract class UseCaseActor<TCommand, TResult> : ReceiveActor where TCommand : ICommand
    {
        protected UseCaseActor()
        {
            this.ReceiveAsync<ExecuteUseCaseMessage>(this.HandleExecute);
        }

        private async Task HandleExecute(ExecuteUseCaseMessage message)
        {
            var errors = this.Validate((TCommand)message.Command).ToList();
            errors.AddRange(await this.ValidateAsync((TCommand)message.Command));
            if (errors.Any())
            {
                this.Sender.Tell(new UseCaseExecutionFailedMessage(message.Command, message.Caller, errors));
            }
            else
            {
                var target = await this.ExecuteAsync((TCommand)message.Command);
                this.Sender.Tell(new UseCaseExecutionSucceededMessage(message.Command, message.Caller, target));
            }
        }

        public virtual Task<TResult> ExecuteAsync(TCommand command)
        {
            return Task.FromResult(this.Execute(command));
        }

        public virtual TResult Execute(TCommand command)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<ValidationError> Validate(TCommand command)
        {
            yield break;
        }

        public virtual Task<IEnumerable<ValidationError>> ValidateAsync(TCommand command)
        {
            return Task.FromResult(Enumerable.Empty<ValidationError>());
        }
    }
}