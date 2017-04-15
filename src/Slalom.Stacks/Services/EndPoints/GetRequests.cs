using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Services.Logging;

namespace Slalom.Stacks.Services.EndPoints
{
    [EndPoint("_system/requests")]
    public class GetRequests : EndPoint<GetRequestsCommand, IEnumerable<RequestEntry>>
    {
        private readonly IRequestLog _source;

        public GetRequests(IRequestLog source)
        {
            _source = source;
        }

        public override async Task<IEnumerable<RequestEntry>> ReceiveAsync(GetRequestsCommand instance)
        {
            var result = await _source.GetEntries(instance.Start, instance.End);

            return result.Where(e => e.Path == null || !e.Path.StartsWith("_"));
        }
    }
}