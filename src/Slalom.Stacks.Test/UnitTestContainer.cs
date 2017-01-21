using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Runtime;

namespace Slalom.Stacks.Test
{
    public class UnitTestContainer : ApplicationContainer, IHandleEvent
    {
        public UnitTestContainer() : base(typeof(UnitTestContainer))
        {
            base.Register(this);
        }

        public readonly List<IEvent> RaisedEvents = new List<IEvent>(); 

        public Task HandleAsync(IEvent instance)
        {
            RaisedEvents.Add(instance);

            return Task.FromResult(0);
        }

        public void Do()
        {
            Console.WriteLine(RaisedEvents);
        }
    }
}
