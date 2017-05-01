using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Services.Logging;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Services.EndPoints
{
    /// <summary>
    /// Gets requests that have happened in the service context.
    /// </summary>
    [EndPoint("_system/requests")]
    public class GetRequests : EndPoint<GetRequestsRequest, IEnumerable<RequestEntry>>
    {
        private readonly IRequestLog _source;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetRequests"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        public GetRequests(IRequestLog source)
        {
            Argument.NotNull(source, nameof(source));

            _source = source;
        }

        /// <summary>
        /// Receives the call to the endpoint.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>Returns the response to the request.</returns>
        public override async Task<IEnumerable<RequestEntry>> ReceiveAsync(GetRequestsRequest instance)
        {
            var result = await _source.GetEntries(instance.Start, instance.End);

            return result.Where(e => e.Path == null || !e.Path.StartsWith("_"));
        }
    }
}