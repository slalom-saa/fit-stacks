using Slalom.Stacks.Messaging;

namespace Slalom.Stacks.Services
{
    public interface IService
    {
        ExecutionContext Context { get; set; }

        Request Request { get; }
    }
}