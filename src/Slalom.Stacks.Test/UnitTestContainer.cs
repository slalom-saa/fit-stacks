using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Messaging;

namespace Slalom.Stacks.Test
{
    public class Scenario
    {
        public InMemoryEntityContext EntityContext { get; set; } = new InMemoryEntityContext();

        public ClaimsPrincipal User { get; set; }

        public Scenario WithUser(string userName, params string[] roles)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, userName) };
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            this.User = new ClaimsPrincipal(new ClaimsIdentity(claims));
            return this;
        }

        public Scenario WithData(params IAggregateRoot[] items)
        {
            this.EntityContext.AddAsync(items).Wait();

            return this;
        }

        public Scenario()
        {
            ClaimsPrincipal.ClaimsPrincipalSelector = () => User;
        }

        public Scenario AsAdmin()
        {
            this.WithUser("admin@admin.com", "Administrator");

            return this;
        }
    }

    public class UnitTestContainer : ApplicationContainer, IHandleEvent
    {
        public readonly List<IEvent> RaisedEvents = new List<IEvent>();

        public UnitTestContainer()
            : base(typeof(UnitTestContainer))
        {
            this.Register(this);
        }

        public void UseScenario(Scenario scenario)
        {
            this.Register<IEntityContext>(scenario.EntityContext);
        }

        public Task HandleAsync(IEvent instance)
        {
            RaisedEvents.Add(instance);

            return Task.FromResult(0);
        }
    }
}