using System.Collections.Generic;
using System.Threading.Tasks;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Registry;

namespace Slalom.Stacks.Messaging.Logging.EndPoints
{
    [EndPoint("_systems/messaging/requests")]
    public class GetRequests : EndPoint<GetRequestsCommand, IEnumerable<RequestEntry>>
    {
        private readonly IRequestLog _source;

        public GetRequests(IRequestLog source)
        {
            _source = source;
        }

        public override Task<IEnumerable<RequestEntry>> ReceiveAsync(GetRequestsCommand instance)
        {
            return _source.GetEntries(instance.Start, instance.End);
        }
    }
}