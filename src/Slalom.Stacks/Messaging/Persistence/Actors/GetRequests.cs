using System.Collections.Generic;
using System.Threading.Tasks;
using Slalom.Stacks.Services;

namespace Slalom.Stacks.Messaging.Persistence.Actors
{
    [EndPoint("_systems/requests")]
    public class GetRequests : UseCase<GetRequestsCommand, IEnumerable<RequestEntry>>
    {
        private readonly IRequestStore _source;

        public GetRequests(IRequestStore source)
        {
            _source = source;
        }

        public override Task<IEnumerable<RequestEntry>> ExecuteAsync(GetRequestsCommand command)
        {
            return _source.GetEntries(command.Start, command.End);
        }
    }
}