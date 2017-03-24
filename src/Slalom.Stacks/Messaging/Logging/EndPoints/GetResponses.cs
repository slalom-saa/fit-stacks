using System.Collections.Generic;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging.Logging.EndPoints
{
    [EndPoint("_systems/messaging/responses")]
    public class GetResponses : EndPoint<GetResponsesCommand, IEnumerable<ResponseEntry>>
    {
        private readonly IResponseLog _source;

        public GetResponses(IResponseLog source)
        {
            _source = source;
        }

        public override Task<IEnumerable<ResponseEntry>> ReceiveAsync(GetResponsesCommand instance)
        {
            return _source.GetEntries(instance.Start, instance.End);
        }
    }
}