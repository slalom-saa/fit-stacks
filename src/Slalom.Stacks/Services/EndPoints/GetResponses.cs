using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Services.Logging;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Services.EndPoints
{
    /// <summary>
    /// Gets responses that have been provided in a service context.
    /// </summary>
    [EndPoint("_system/responses")]
    public class GetResponses : EndPoint<GetResponsesRequest, IEnumerable<ResponseEntry>>
    {
        private readonly IResponseLog _source;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetResponses"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        public GetResponses(IResponseLog source)
        {
            Argument.NotNull(source, nameof(source));

            _source = source;
        }

        /// <summary>
        /// Receives the call to the endpoint.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>Returns the response to the request.</returns>
        public override async Task<IEnumerable<ResponseEntry>> ReceiveAsync(GetResponsesRequest instance)
        {
            var result = await _source.GetEntries(instance.Start, instance.End);

            return result.Where(e => e.ValidationErrors == null || !e.Path.StartsWith("_"));
        }
    }
}