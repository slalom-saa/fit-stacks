using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Messaging;

namespace Slalom.Stacks.Services
{
    public abstract class SystemService<TRequest, TResponse> : Service, IHandle<TRequest>
    {
        async Task IHandle<TRequest>.Handle(TRequest instance)
        {
            this.Context.Response = await this.Execute(instance);
        }

        public abstract Task<TResponse> Execute(TRequest instance);
    }
}
