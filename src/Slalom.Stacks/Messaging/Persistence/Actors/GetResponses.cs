using System.Collections.Generic;
using System.Threading.Tasks;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Registry;

namespace Slalom.Stacks.Messaging.Persistence.Actors
{
    [EndPoint("_systems/messaging/responses")]
    public class GetResponses : EndPoint<GetResponsesCommand, IEnumerable<ResponseEntry>>
    {
        private readonly IResponseStore _source;

        public GetResponses(IResponseStore source)
        {
            _source = source;
        }

        public override Task<IEnumerable<ResponseEntry>> ReceiveAsync(GetResponsesCommand instance)
        {
            return _source.GetEntries(instance.Start, instance.End);
        }
    }
}