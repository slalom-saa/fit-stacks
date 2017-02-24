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
        private LocalRegistry _registry;
        private IRequestContext _requestContext;

        public MessageDispatcher(IComponentContext components)
        {
            _components = components;
            _registry = components.Resolve<LocalRegistry>();
            _requestContext = components.Resolve<IRequestContext>();
        }

        public Task<MessageResult> Send(ICommand instance, MessageExecutionContext context = null, TimeSpan? timeout = null)
        {
            return this.Send(null, instance, context, timeout);
        }


        public async Task Publish(IEvent instance, MessageExecutionContext context = null)
        {
            //var handlers = _components.ResolveAll(typeof(IHandle<>).MakeGenericType(instance.GetType()));
            //var executionContext = _components.Resolve<IExecutionContext>().Resolve();
            //foreach (var handler in handlers)
            //{
            //    var inner = new MessageContext(instance.Id, handler.GetType().Name, null, executionContext, request);
            //    if (handler is IUseMessageContext)
            //    {
            //        ((IUseMessageContext)handler).UseContext(inner);
            //    }

            //    await (Task)typeof(IHandle<>).MakeGenericType(instance.GetType()).GetMethod("Handle").Invoke(handler, new object[] { instance });
            //}
        }

        public async Task Publish(IEnumerable<IEvent> instance, MessageExecutionContext context = null)
        {
            foreach (var item in instance)
            {
                await this.Publish(item, context);
            }
        }

        public async Task<MessageResult> Send(string path, ICommand instance, MessageExecutionContext parentContext = null, TimeSpan? timeout = null)
        {
            var entries = _registry.Find(instance).ToList();
            if (entries.Count() != 1)
            {
                throw new Exception("TBD");
            }

            var entry = entries.First();

            var handler = _components.Resolve(entry.Type);
            var request = _requestContext.Resolve(instance.CommandName, path, instance, parentContext?.Request);
            var executionContext = _components.Resolve<IExecutionContext>().Resolve();

            parentContext = new MessageExecutionContext(request, entry, executionContext, parentContext);

            if (handler is IUseMessageContext)
            {
                ((IUseMessageContext)handler).UseContext(parentContext);
            }

            await (Task)typeof(IHandle<>).MakeGenericType(instance.GetType()).GetMethod("Handle").Invoke(handler, new object[] { instance });

            return new MessageResult(parentContext);
        }

        public async Task<MessageResult> Send(string path, string command, MessageExecutionContext parentContext = null, TimeSpan? timeout = null)
        {
            //var handlerType = _components.Resolve<IDiscoverTypes>().Find(typeof(IHandle<>)).FirstOrDefault(e => e.GetAllAttributes<PathAttribute>().Any(x => x.Path == path));

            //var message = (ICommand)JsonConvert.DeserializeObject(command, handlerType.GetRequestType());

            //var handler = _components.Resolve(typeof(IHandle<>).MakeGenericType(message.GetType()));

            //var executionContext = _components.Resolve<IExecutionContext>().Resolve();
            //parentContext = new MessageContext(message.Id, handler.GetType().Name, null, executionContext, parentContext);

            //if (handler is IUseMessageContext)
            //{
            //    ((IUseMessageContext)handler).UseContext(parentContext);
            //}

            //await (Task)typeof(IHandle<>).MakeGenericType(message.GetType()).GetMethod("Handle").Invoke(handler, new object[] { message });

            //return new MessageResult(parentContext);

            return null;
        }
    }
}