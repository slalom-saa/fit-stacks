using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging.Logging
{
    public interface ICommandLogger
    {
        Task LogCompletion(MessageEnvelope instance, MessageExecutionResult result);

        Task LogStart(MessageEnvelope instance, IHandle handler);
    }
}