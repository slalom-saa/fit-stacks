using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Reflection;
using Slalom.Stacks.Runtime;

namespace Slalom.Stacks.Messaging
{
    public class MessageRouter : IMessageRouter, IEventStream
    {
        private readonly IComponentContext _components;
        private readonly ActorController _controller;
        private readonly IExecutionContextResolver _context;
        private readonly List<HandlerMapping> _mappings = new List<HandlerMapping>();

        public MessageRouter(IComponentContext components, Assembly[] assemblies)
        {
            _components = components;
            _controller = components.Resolve<ActorController>();
            _context = components.Resolve<IExecutionContextResolver>();
            var actors = assemblies.SafelyGetTypes(typeof(IHandle));

            foreach (var actor in actors)
            {
                var path = actor.GetTypeInfo().GetCustomAttributes<PathAttribute>().FirstOrDefault()?.Path;
                _mappings.Add(new HandlerMapping(path, actor));
            }
        }

        public IEnumerable<Type> GetHandlers(string path)
        {
            return _mappings.Where(e => e.Path == path)
                            .Select(e => e.Type);
        }

        public IEnumerable<Type> GetHandlers(IMessage message)
        {
            return _mappings.Where(e => e.Type.GetRequestType() == message.Type)
                            .Select(e => e.Type);
        }

        public IEnumerable<Type> GetHandlers(string path, IMessage message)
        {
            return _mappings.Where(e => e.Type.GetRequestType() == message.Type && e.Path == path)
                            .Select(e => e.Type);
        }

        public IEnumerable<Type> GetHandlers(string path, string message)
        {
            return _mappings.Where(e => e.Path == path)
                            .Select(e => e.Type);
        }

        public void Publish<TEvent>(TEvent instance, ExecutionContext context) where TEvent : Event
        {
            var list = new List<Task<MessageExecutionResult>>();
            var handlers = this.GetHandlers(instance);
            foreach (var item in handlers)
            {
                list.Add(_controller.Execute(new MessageEnvelope(instance, context), (IHandle)_components.Resolve(item)));
            }
        }

        public Task<MessageExecutionResult> Send(IMessage message, ExecutionContext context)
        {
            var list = new List<Task<MessageExecutionResult>>();
            var handlers = this.GetHandlers(message);
            foreach (var item in handlers)
            {
                list.Add(_controller.Execute(new MessageEnvelope(message, context), (IHandle)_components.Resolve(item)));
            }
            return list.First();
        }

        public Task<MessageExecutionResult> Send(IMessage command, TimeSpan? timeout = null)
        {
            var handlers = this.GetHandlers(command);
            if (handlers.Count() > 1)
            {
                throw new InvalidOperationException();
            }
            if (!handlers.Any())
            {
                throw new InvalidOperationException();
            }
            return _controller.Execute(new MessageEnvelope(command, _context.Resolve()), (IHandle)_components.Resolve(handlers.First()));
        }

        public Task<MessageExecutionResult> Send(string path, IMessage command, TimeSpan? timeout = null)
        {
            var handlers = this.GetHandlers(path, command);
            if (handlers.Count() > 1)
            {
                throw new InvalidOperationException();
            }
            if (!handlers.Any())
            {
                throw new InvalidOperationException();
            }
            var context = _context.Resolve();
            context.Path = path;
            return _controller.Execute(new MessageEnvelope(command, context), (IHandle)_components.Resolve(handlers.First()));
        }

        public Task<MessageExecutionResult> Send(string path, string command, TimeSpan? timeout = null)
        {
            var handlers = this.GetHandlers(path);
            if (handlers.Count() > 1)
            {
                throw new InvalidOperationException();
            }
            if (!handlers.Any())
            {
                throw new InvalidOperationException();
            }
            var target = handlers.First();
            var context = _context.Resolve();
            context.Path = path;

            var instance = new MessageEnvelope((IMessage)JsonConvert.DeserializeObject(command, target.GetRequestType()), context);

            return _controller.Execute(instance, (IHandle)_components.Resolve(handlers.First()));
        }

        public Task<MessageExecutionResult> Send(string path, TimeSpan? timeout)
        {
            var handlers = this.GetHandlers(path);
            if (handlers.Count() > 1)
            {
                throw new InvalidOperationException();
            }
            if (!handlers.Any())
            {
                throw new InvalidOperationException();
            }
            var target = handlers.First();
            var context = _context.Resolve();
            context.Path = path;

            var instance = new MessageEnvelope((IMessage)JsonConvert.DeserializeObject("{}", target.GetRequestType()), context);

            return _controller.Execute(instance, (IHandle)_components.Resolve(handlers.First()));
        }
    }
}