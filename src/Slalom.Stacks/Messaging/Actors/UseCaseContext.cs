using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Search;

namespace Slalom.Stacks.Messaging.Actors
{
    public class UseCaseContext
    {
        public IDomainFacade Domain { get; set; }

        public ISearchFacade Search { get; set; }

        public ExecutionContext Context { get; set; }
    }
}
