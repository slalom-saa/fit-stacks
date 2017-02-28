using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Messaging;

namespace Slalom.Stacks.TestKit
{
    public class TestStack : Stack
    {
        public readonly List<Event> RaisedEvents = new List<Event>();

        public TestStack() : base(new StackFrame(1).GetMethod().DeclaringType)
        {
            var method = new StackFrame(1).GetMethod();
            var attribute = method.GetCustomAttributes<GivenAttribute>().FirstOrDefault();
            if (attribute != null)
            {
                var scenario = (Scenario)Activator.CreateInstance(attribute.Name);
                this.UseScenario(scenario);
            }
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
        }

        public Task HandleAsync(Event instance)
        {
            RaisedEvents.Add(instance);

            return Task.FromResult(0);
        }

        public MessageResult LastResult { get; set; }

        public MessageResult Send(ICommand command)
        {
            return this.LastResult = base.Container.Resolve<IMessageGateway>().Send(command).Result;
        }

        public void UseScenario(Scenario scenario)
        {
            this.Use(builder =>
            {
                builder.RegisterInstance(scenario.EntityContext).As<IEntityContext>();
            });
        }

        public void UseScenario(Type scenario)
        {
            this.Use(builder =>
            {
                var instance = Activator.CreateInstance(scenario) as Scenario;
                builder.RegisterInstance(instance.EntityContext).As<IEntityContext>();
            });
        }
    }
}