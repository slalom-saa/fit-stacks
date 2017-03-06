using System.Collections.Generic;
using System.Threading.Tasks;
using Slalom.Stacks.Services;

namespace Slalom.Stacks.Messaging.Persistence.Actors
{
    [EndPoint("_systems/responses")]
    public class GetResponses : UseCase<GetResponsesCommand, IEnumerable<ResponseEntry>>
    {
        private readonly IResponseStore _source;

        public GetResponses(IResponseStore source)
        {
            _source = source;
        }

        public override Task<IEnumerable<ResponseEntry>> ExecuteAsync(GetResponsesCommand command)
        {
            return _source.GetEntries(command.Start, command.End);
        }
    }
}