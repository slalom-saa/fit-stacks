using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Logging;
using Slalom.Stacks.Reflection;
using Slalom.Stacks.Search;
using Slalom.Stacks.Services.Logging;
using Slalom.Stacks.Services.Messaging;

namespace Slalom.Stacks.Tests
{
    public class TestStack : Stack
    {
        public readonly List<EventMessage> RaisedEvents = new List<EventMessage>();


#if !core
        public TestStack()
        {
            var method = new StackFrame(1).GetMethod();
            if (method != null)
            {
                var current = method.DeclaringType.Assembly;
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    if (assembly.FullName.StartsWith(current.FullName.Split('.')[0]))
                    {
                        foreach (var module in assembly.SafelyGetTypes().Where(e => e.GetAllAttributes<AutoLoadAttribute>().Any()))
                        {
                            if (module.GetConstructors().SingleOrDefault()?.GetParameters().Length == 0)
                            {
                                this.Use(builder =>
                                {
                                    builder.RegisterModule((Autofac.Module)Activator.CreateInstance(module));
                                });
                            }
                            if (module.GetConstructors().SingleOrDefault()?.GetParameters().SingleOrDefault()?.ParameterType == typeof(Stack))
                            {
                                this.Use(builder =>
                                {
                                    builder.RegisterModule((Autofac.Module)Activator.CreateInstance(module, this));
                                });
                            }
                        }
                        if (!this.Assemblies.Contains(assembly))
                        {
                            this.Assemblies.Add(assembly);
                        }
                    }
                }
            }

            var attribute = method.GetCustomAttributes<GivenAttribute>().FirstOrDefault();
            if (attribute != null)
            {
                var scenario = (Scenario)Activator.CreateInstance(attribute.Name);
                this.UseScenario(scenario);
            }
            //this.Use(builder => { builder.RegisterType<TestDispatcher>().AsSelf().AsImplementedInterfaces().SingleInstance(); });
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
            //this.Use(builder => { builder.RegisterType<TestDispatcher>().AsSelf().AsImplementedInterfaces().SingleInstance(); });
        }
#else

        public TestStack(params object[] markers) : base(markers)
        {
            //this.Use(builder => { builder.RegisterType<TestDispatcher>().AsSelf().AsImplementedInterfaces().SingleInstance(); });
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

        public MessageResult<T> Send<T>(object command)
        {
            var result = this.Container.Resolve<IMessageGateway>().Send(command).Result;

            return (MessageResult<T>)(this.LastResult = new MessageResult<T>(result));
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

        public void UseEndPoint<T>(Action<T, Request> action = null)
        {
            var dispatch = this.Container.Resolve<TestDispatcher>();

            if (dispatch == null)
            {
                throw new InvalidOperationException("The configured dispatcher is not a test dispatcher.  Please make sure that the test dispatcher is registered.");
            }

            dispatch.UseEndPoint(action ?? ((a, b) => { }));
        }

        public void UseEndPoint<T>(Func<T, Request, object> action = null)
        {
            var dispatch = this.Container.Resolve<TestDispatcher>();

            if (dispatch == null)
            {
                throw new InvalidOperationException("The configured dispatcher is not a test dispatcher.  Please make sure that the test dispatcher is registered.");
            }

            dispatch.UseEndPoint(action ?? ((a, b) => null));
        }
    }
}