using System.Collections.Generic;
using System.Threading.Tasks;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Registry;

namespace Slalom.Stacks.Messaging.Persistence.Actors
{
    [EndPoint("_systems/messaging/requests")]
    public class GetRequests : Service<GetRequestsCommand, IEnumerable<RequestEntry>>
    {
        private readonly IRequestStore _source;

        public GetRequests(IRequestStore source)
        {
            _source = source;
        }

        public override Task<IEnumerable<RequestEntry>> ExecuteAsync(GetRequestsCommand instance)
        {
            return _source.GetEntries(instance.Start, instance.End);
        }
    }
}