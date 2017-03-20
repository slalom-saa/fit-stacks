//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Autofac;
//using Slalom.Stacks.Domain;
//using Slalom.Stacks.Messaging;
//using Slalom.Stacks.Messaging.Events;
//using Slalom.Stacks.Messaging.Pipeline;
//using Slalom.Stacks.Search;
//using Slalom.Stacks.Validation;

//namespace Slalom.Stacks.Services
//{
//    public abstract class Service : IService
//    {
        

       


       

//        /// <summary>
//        /// Adds the raised event that will fire on completion.
//        /// </summary>
//        /// <param name="instance">The instance to raise.</param>
//        public void AddRaisedEvent(Event instance)
//        {
//            Argument.NotNull(instance, nameof(instance));

//            this.Context.AddRaisedEvent(instance);
//        }

//        /// <summary>
//        /// Sends the specified message.
//        /// </summary>
//        /// <param name="path">The path.</param>
//        /// <param name="message">The command.</param>
//        /// <returns>A task for asynchronous programming.</returns>
//        protected Task<MessageResult> Send(string path, object message)
//        {
//            var messages = this.Components.Resolve<IMessageGateway>();

//            return messages.Send(path, message, this.Context);
//        }

//        /// <summary>
//        /// Sends the specified message.
//        /// </summary>
//        /// <param name="message">The command.</param>
//        /// <returns>A task for asynchronous programming.</returns>
//        protected Task<MessageResult> Send(object message)
//        {
//            var messages = this.Components.Resolve<IMessageGateway>();

//            return messages.Send(message, this.Context);
//        }

            
//    }
//}