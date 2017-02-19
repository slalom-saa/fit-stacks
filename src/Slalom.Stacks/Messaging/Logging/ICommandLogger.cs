using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging.Logging
{
    public interface ICommandLogger
    {
        Task LogCompletion(MessageEnvelope instance, MessageExecutionResult result, IHandle handler);

        Task LogStart(MessageEnvelope instance, IHandle handler);
    }
}