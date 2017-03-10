using System.Collections.Generic;
using System.Threading.Tasks;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Registry;

namespace Slalom.Stacks.Messaging.Logging.EndPoints
{
    [EndPoint("_systems/messaging/responses")]
    public class GetResponses : SystemEndPoint<GetResponsesCommand, IEnumerable<ResponseEntry>>
    {
        private readonly IResponseLog _source;

        public GetResponses(IResponseLog source)
        {
            _source = source;
        }

        public override Task<IEnumerable<ResponseEntry>> Execute(GetResponsesCommand instance)
        {
            return _source.GetEntries(instance.Start, instance.End);
        }
    }
}