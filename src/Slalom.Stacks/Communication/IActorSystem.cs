using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.Stacks.Communication
{
    public interface IActorSystem
    {
        Task<object> Ask(object message, TimeSpan? timeout = null);

        void Tell(object message);
    }

    public class NullActorSystem : IActorSystem
    {
        public Task<object> Ask(object message, TimeSpan? timeout = null)
        {
            return Task.FromResult((object)null);
        }

        public void Tell(object message)
        {
        }
    }
}
