using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Slalom.Stacks.Services.Inventory;
using Slalom.Stacks.Services.Messaging;
using Slalom.Stacks.Validation;
using ExecutionContext = Slalom.Stacks.Services.Messaging.ExecutionContext;

namespace Slalom.Stacks.Tests
{
    public class TestDispatcher : RequestRouter, IRemoteRouter
    {
        private Dictionary<Type, Func<object, Request, object>> _endPoints = new Dictionary<Type, Func<object, Request, object>>();

        public void UseEndPoint<T>(Action<T, Request> action)
        {
            _endPoints.Add(typeof(T), (a, b) =>
            {
                action((T)a, b);
                return null;
            });
        }

        public void UseEndPoint<T>(Func<T, Request, object> action)
        {
            _endPoints.Add(typeof(T), (a, b) => action((T)a, b));
        }

        public TestDispatcher(IComponentContext components) : base(components)
        {
        }

        public override Task<MessageResult> Route(Request request, EndPointMetaData endPoint, ExecutionContext parentContext, TimeSpan? timeout = null)
        {
            if (request.Message.MessageType != null && _endPoints.ContainsKey(request.Message.MessageType))
            {
                var context = new ExecutionContext(request, endPoint, CancellationToken.None, parentContext);
                var response = _endPoints[request.Message.MessageType](request.Message.Body, request);
                if (response is ValidationError)
                {
                    context.AddValidationErrors(new[] { response as ValidationError });
                }
                else
                {
                    context.Response = response;
                }
                return Task.FromResult(new MessageResult(context));
            }

            return base.Route(request, endPoint, parentContext, timeout);
        }

        public bool CanRoute(Request request)
        {
            return true;
        }

        public Task<MessageResult> Route(Request request, ExecutionContext parentContext, TimeSpan? timeout = null)
        {
            if (request.Message.MessageType != null && _endPoints.ContainsKey(request.Message.MessageType))
            {
                var context = new ExecutionContext(request, parentContext);
                var response = _endPoints[request.Message.MessageType](request.Message.Body, request);
                if (response is ValidationError)
                {
                    context.AddValidationErrors(new[] { response as ValidationError });
                }
                else
                {
                    context.Response = response;
                }
                return Task.FromResult(new MessageResult(context));
            }

            return Task.FromResult(new MessageResult(parentContext));
        }
    }
}
