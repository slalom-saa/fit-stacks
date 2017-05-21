using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Slalom.Stacks.Services.Inventory;
using Slalom.Stacks.Services.Messaging;
using ExecutionContext = Slalom.Stacks.Services.Messaging.ExecutionContext;

namespace Slalom.Stacks.Tests
{
    public class TestDispatcher : RequestRouter, IRemoteRouter
    {
        private Dictionary<string, Action<object>> _endPoints = new Dictionary<string, Action<object>>();
        private Dictionary<string, Action<Request>> _namedEndPoints = new Dictionary<string, Action<Request>>();

        public void UseEndPoint<T>(Action<T> action)
        {
            _endPoints.Add(typeof(T).FullName, a =>
            {
                action((T)a);
            });
        }

        public TestDispatcher(IComponentContext components) : base(components)
        {
        }

        public override Task<MessageResult> Route(Request request, EndPointMetaData endPoint, ExecutionContext parentContext, TimeSpan? timeout = null)
        {
            if (_endPoints.ContainsKey(request.Message.MessageType))
            {
                var context = new ExecutionContext(request, endPoint, CancellationToken.None, parentContext);

                _endPoints[request.Message.MessageType](request.Message.Body);

                return Task.FromResult(new MessageResult(context));
            }

            if (!String.IsNullOrWhiteSpace(endPoint.Path) && _namedEndPoints.ContainsKey(endPoint.Path))
            {
                var context = new ExecutionContext(request, endPoint, CancellationToken.None, parentContext);

                _namedEndPoints[endPoint.Path](request);

                return Task.FromResult(new MessageResult(context));
            }

            return base.Route(request, endPoint, parentContext, timeout);
        }

        public void UseEndPoint(string path, Action<Request> action)
        {
            _namedEndPoints.Add(path, action);
        }

        public bool CanRoute(Request request)
        {
            return true;
        }

        public Task<MessageResult> Route(Request request, ExecutionContext parentContext, TimeSpan? timeout = null)
        {
            if (_endPoints.ContainsKey(request.Message.MessageType))
            {
                var context = new ExecutionContext(request, parentContext);

                _endPoints[request.Message.MessageType](request.Message.Body);

                return Task.FromResult(new MessageResult(context));
            }

            if (!String.IsNullOrWhiteSpace(request.Path) && _namedEndPoints.ContainsKey(request.Path))
            {
                var context = new ExecutionContext(request, parentContext);

                _namedEndPoints[request.Path](request);

                return Task.FromResult(new MessageResult(context));
            }

            return Task.FromResult(new MessageResult(parentContext));
        }
    }
}
