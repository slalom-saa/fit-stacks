using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Events;
using Slalom.Stacks.Messaging.Pipeline;
using Slalom.Stacks.Search;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Services
{
    public abstract class Service : IService
    {
        /// <summary>
        /// Gets the configured <see cref="IDomainFacade"/> instance.
        /// </summary>
        /// <value>The configured <see cref="IDomainFacade"/> instance.</value>    
        public IDomainFacade Domain => this.Components.Resolve<IDomainFacade>();

        /// <summary>
        /// Gets the configured <see cref="ISearchFacade"/> instance.
        /// </summary>
        /// <value>The configured <see cref="ISearchFacade"/> instance.</value>
        public ISearchFacade Search => this.Components.Resolve<ISearchFacade>();

        /// <summary>
        /// Gets the configured <see cref="IComponentContext"/> instance.
        /// </summary>
        /// <value>The configured <see cref="IComponentContext"/> instance.</value>
        internal IComponentContext Components { get; set; }

        private ExecutionContext Context => ((IService)this).Context;

        public Request Request => ((IService)this).Context.Request;

        ExecutionContext IService.Context { get; set; }

        /// <summary>
        /// Adds the raised event that will fire on completion.
        /// </summary>
        /// <param name="instance">The instance to raise.</param>
        public void AddRaisedEvent(Event instance)
        {
            Argument.NotNull(instance, nameof(instance));

            this.Context.AddRaisedEvent(instance);
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="message">The command.</param>
        /// <returns>A task for asynchronous programming.</returns>
        protected Task<MessageResult> Send(string path, object message)
        {
            var messages = this.Components.Resolve<IMessageGateway>();

            return messages.Send(path, message, this.Context);
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The command.</param>
        /// <returns>A task for asynchronous programming.</returns>
        protected Task<MessageResult> Send(object message)
        {
            var messages = this.Components.Resolve<IMessageGateway>();

            return messages.Send(message, this.Context);
        }

        /// <summary>
        /// Completes the specified message.
        /// </summary>
        /// <returns>A task for asynchronous programming.</returns>
        internal virtual async Task Complete()
        {
            var steps = new List<IMessageExecutionStep>
            {
                this.Components.Resolve<HandleException>(),
                this.Components.Resolve<Complete>(),
                this.Components.Resolve<PublishEvents>(),
                this.Components.Resolve<LogCompletion>()
            };
            foreach (var step in steps)
            {
                await step.Execute(this.Request.Message, this.Context);
            }
        }

        /// <summary>
        /// Prepares the usecase for execution.
        /// </summary>
        /// <returns>A task for asynchronous programming.</returns>
        internal virtual async Task Prepare()
        {
            var steps = new List<IMessageExecutionStep>
            {
                this.Components.Resolve<LogStart>(),
                this.Components.Resolve<ValidateMessage>()
            };
            foreach (var step in steps)
            {
                await step.Execute(this.Request.Message, this.Context);
            }
        }
    }
}