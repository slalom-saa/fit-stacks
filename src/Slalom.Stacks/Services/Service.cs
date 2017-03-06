using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Messaging;

namespace Slalom.Stacks.Services
{
    public abstract class Service
    {
        public Request Request { get; set; }

        public ExecutionContext Context { get; set; }
    }
}
