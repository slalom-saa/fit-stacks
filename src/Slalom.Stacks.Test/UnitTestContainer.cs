using System;
using System.Reflection;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Messaging;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Autofac;

namespace Slalom.Stacks.Test
{
    public class Scenario
    {
        public Scenario()
        {
            ClaimsPrincipal.ClaimsPrincipalSelector = () => User;
        }

        public InMemoryEntityContext EntityContext { get; set; } = new InMemoryEntityContext();

        public ClaimsPrincipal User { get; set; }

        public Scenario WithUser(string userName, params string[] roles)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, userName) };
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            User = new ClaimsPrincipal(new ClaimsIdentity(claims));
            return this;
        }

        public Scenario WithData(params IAggregateRoot[] items)
        {
            EntityContext.AddAsync(items).Wait();

            return this;
        }

        public Scenario AsAdmin()
        {
            WithUser("admin@admin.com", "Administrator");

            return this;
        }
    }
    public class GivenAttribute : Attribute
    {
        public Type Name { get; }

        public GivenAttribute(Type name)
        {
            Name = name;
        }
    }

    public class UnitTestContainer : Stack, IHandleEvent
    {
        public readonly List<IEvent> RaisedEvents = new List<IEvent>();


        public UnitTestContainer(object instance = null, [CallerMemberName] string callerName = "")
            : base(typeof(UnitTestContainer))
        {
            this.Container.Update(builder =>
            {
                builder.RegisterInstance(this).As<IHandleEvent>();
            });

            if (instance != null && callerName != null)
            {
                var method = instance.GetType().GetTypeInfo().GetMethod(callerName);
                var attribute = method.GetCustomAttributes<GivenAttribute>().FirstOrDefault();
                if (attribute != null)
                {
                    var scenario = (Scenario)Activator.CreateInstance(attribute.Name);
                    this.UseScenario(scenario);
                }
            }
        }

        public Task HandleAsync(IEvent instance)
        {
            RaisedEvents.Add(instance);

            return Task.FromResult(0);
        }

        public CommandResult Send(ICommand command)
        {
            return SendAsync(command).Result;
        }

        public void UseScenario(Scenario scenario)
        {
            this.Container.Update(builder =>
            {
                builder.RegisterInstance(scenario.EntityContext).As<IEntityContext>();
            });
        }

        public void UseScenario(Type scenario)
        {
            this.Container.Update(builder =>
            {
                var instance = Activator.CreateInstance(scenario) as Scenario;
                builder.RegisterInstance(instance.EntityContext).As<IEntityContext>();
            });
        }
    }
}