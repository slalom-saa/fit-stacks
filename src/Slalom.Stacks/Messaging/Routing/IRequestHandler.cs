using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging.Routing
{
    public interface IRequestHandler
    {
        Task Handle(IMessage instance, MessageContext context);
    }
}