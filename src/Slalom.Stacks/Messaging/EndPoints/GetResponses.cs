using System.Collections.Generic;
using System.Threading.Tasks;
using Slalom.Stacks.Messaging.Logging;

namespace Slalom.Stacks.Messaging.EndPoints
{
    [EndPoint("_system/responses")]
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