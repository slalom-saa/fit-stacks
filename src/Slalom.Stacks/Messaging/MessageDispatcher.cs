using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Slalom.Stacks.Messaging.Pipeline;
using Slalom.Stacks.Runtime;
using System.Reflection;
using Newtonsoft.Json;
using Slalom.Stacks.Reflection;

namespace Slalom.Stacks.Messaging
{
    public class MessageDispatcher : IMessageDispatcher
    {
        private readonly IComponentContext _components;

        public MessageDispatcher(IComponentContext components)
        {
            _components = components;
        }

        public async Task<MessageResult> Send(ICommand instance, MessageContext context = null, TimeSpan? timeout = null)
        {
            var handler = _components.Resolve(typeof(IHandle<>).MakeGenericType(instance.GetType()));

            var executionContext = _components.Resolve<IExecutionContextResolver>().Resolve();
            context = new MessageContext(instance.Id, handler.GetType().Name, null, executionContext, context);

            if (handler is IUseMessageContext)
            {
                ((IUseMessageContext)handler).UseContext(context);
            }

            await (Task)typeof(IHandle<>).MakeGenericType(instance.GetType()).GetMethod("Handle").Invoke(handler, new object[] { instance });

            return new MessageResult(context);
        }


        public async Task Publish(IEvent instance, MessageContext context = null)
        {
            var handlers = _components.ResolveAll(typeof(IHandle<>).MakeGenericType(instance.GetType()));
            var executionContext = _components.Resolve<IExecutionContextResolver>().Resolve();
            foreach (var handler in handlers)
            {
                var inner = new MessageContext(instance.Id, handlers.GetType().Name, null, executionContext, context);
                if (handler is IUseMessageContext)
                {
                    ((IUseMessageContext)handler).UseContext(inner);
                }

                await (Task)typeof(IHandle<>).MakeGenericType(instance.GetType()).GetMethod("Handle").Invoke(handler, new object[] { instance });
            }
        }

        public async Task Publish(IEnumerable<IEvent> instance, MessageContext context = null)
        {
            foreach (var item in instance)
            {
                await this.Publish(item, context);
            }
        }

        public async Task<MessageResult> Send(string path, ICommand instance, MessageContext context = null, TimeSpan? timeout = null)
        {
            var handler = _components.ResolveAll(typeof(IHandle<>).MakeGenericType(instance.GetType()))
                                     .FirstOrDefault(e => e.GetType().GetAllAttributes<PathAttribute>().Any(x => x.Path == path));

            var executionContext = _components.Resolve<IExecutionContextResolver>().Resolve();
            context = new MessageContext(instance.Id, handler.GetType().Name, null, executionContext, context);

            if (handler is IUseMessageContext)
            {
                ((IUseMessageContext)handler).UseContext(context);
            }

            await (Task)typeof(IHandle<>).MakeGenericType(instance.GetType()).GetMethod("Handle").Invoke(handler, new object[] { instance });

            return new MessageResult(context);
        }

        public async Task<MessageResult> Send(string path, string command, MessageContext context = null, TimeSpan? timeout = null)
        {
            var handlerType = _components.Resolve<IDiscoverTypes>().Find(typeof(IHandle<>)).FirstOrDefault(e => e.GetAllAttributes<PathAttribute>().Any(x => x.Path == path));

            var message = (ICommand)JsonConvert.DeserializeObject(command, handlerType.GetRequestType());

            var handler = _components.Resolve(typeof(IHandle<>).MakeGenericType(message.GetType()));

            var executionContext = _components.Resolve<IExecutionContextResolver>().Resolve();
            context = new MessageContext(message.Id, handler.GetType().Name, null, executionContext, context);

            if (handler is IUseMessageContext)
            {
                ((IUseMessageContext)handler).UseContext(context);
            }

            await (Task)typeof(IHandle<>).MakeGenericType(message.GetType()).GetMethod("Handle").Invoke(handler, new object[] { message });

            return new MessageResult(context);
        }
    }
}