using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Services.Logging;

namespace Slalom.Stacks.Services.EndPoints
{
    [EndPoint("_system/responses")]
    public class GetResponses : EndPoint<GetResponsesCommand, IEnumerable<ResponseEntry>>
    {
        private readonly IResponseLog _source;

        public GetResponses(IResponseLog source)
        {
            _source = source;
        }

        public override async Task<IEnumerable<ResponseEntry>> ReceiveAsync(GetResponsesCommand instance)
        {
            var result = await _source.GetEntries(instance.Start, instance.End);

            return result.Where(e => e.ValidationErrors == null || !e.Path.StartsWith("_"));
        }
    }
}