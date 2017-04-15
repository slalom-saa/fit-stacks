using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Logging;
using Slalom.Stacks.Search;
using Slalom.Stacks.Services.Messaging;

namespace Slalom.Stacks.Tests
{
    public class TestStack : Stack
    {
        public readonly List<EventMessage> RaisedEvents = new List<EventMessage>();


#if !core
        public TestStack() : base(new StackFrame(1).GetMethod().DeclaringType)
        {
            var method = new StackFrame(1).GetMethod();
            var attribute = method.GetCustomAttributes<GivenAttribute>().FirstOrDefault();
            if (attribute != null)
            {
                var scenario = (Scenario)Activator.CreateInstance(attribute.Name);
                this.UseScenario(scenario);
            }
            this.Use(builder => { builder.RegisterType<TestDispatcher>().AsImplementedInterfaces().SingleInstance(); });
        }

        public TestStack(params object[] markers) : base(markers)
        {
            var method = new StackFrame(1).GetMethod();
            var attribute = method.GetCustomAttributes<GivenAttribute>().FirstOrDefault();
            if (attribute != null)
            {
                var scenario = (Scenario)Activator.CreateInstance(attribute.Name);
                this.UseScenario(scenario);
            }
            this.Use(builder => { builder.RegisterType<TestDispatcher>().AsImplementedInterfaces().SingleInstance(); });
        }
#else

        public TestStack(params object[] markers) : base(markers)
        {
            this.Use(builder => { builder.RegisterType<TestDispatcher>().AsImplementedInterfaces().SingleInstance(); });
        }

#endif

        public Task HandleAsync(EventMessage instance)
        {
            RaisedEvents.Add(instance);

            return Task.FromResult(0);
        }

        public MessageResult LastResult { get; set; }

        public MessageResult Send(object command)
        {
            return this.LastResult = this.Container.Resolve<IMessageGateway>().Send(command).Result;
        }

        public void UseScenario(Scenario scenario)
        {
            this.Use(builder => { builder.RegisterInstance(scenario.EntityContext).As<IEntityContext>(); });
        }

        public void UseScenario(Type scenario)
        {
            this.Use(builder =>
            {
                var instance = Activator.CreateInstance(scenario) as Scenario;
                builder.RegisterInstance(instance.EntityContext).As<IEntityContext>();
            });
        }

        /// <summary>
        /// Gets the configured <see cref="IDomainFacade" />.
        /// </summary>
        /// <value>The configured <see cref="IDomainFacade" />.</value>
        public IDomainFacade Domain => this.Container.Resolve<IDomainFacade>();

        /// <summary>
        /// Gets the configured <see cref="ILogger" />.
        /// </summary>
        /// <value>The configured <see cref="ILogger" />.</value>
        public ILogger Logger => this.Container.Resolve<ILogger>();

        /// <summary>
        /// Gets the configured <see cref="ISearchFacade" />.
        /// </summary>
        /// <value>The configured <see cref="ISearchFacade" />.</value>
        public ISearchFacade Search => this.Container.Resolve<ISearchFacade>();

        public void UseEndPoint<T>(Action<T> action = null)
        {
            var dispatch = this.Container.Resolve<ILocalMessageDispatcher>() as TestDispatcher;

            if (dispatch == null)
            {
                throw new InvalidOperationException("The configured dispatcher is not a test dispatcher.  Please make sure that the test dispatcher is registered.");
            }

            dispatch.UseEndPoint(action ?? (a => { }));
        }

        public void UseEndPoint(string path, Action<Request> action = null)
        {
            var dispatch = this.Container.Resolve<ILocalMessageDispatcher>() as TestDispatcher;

            if (dispatch == null)
            {
                throw new InvalidOperationException("The configured dispatcher is not a test dispatcher.  Please make sure that the test dispatcher is registered.");
            }

            dispatch.UseEndPoint(path, action ?? (a => { }));
        }
    }
}