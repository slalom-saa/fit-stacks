using System;
using System.Threading.Tasks;
using Slalom.Stacks.Runtime;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// The root IRequestHandler interface.
    /// </summary>
    /// <typeparam name="TMessage">The message type.</typeparam>
    public interface IHandle<in TMessage>
    {
        Task Handle(TMessage instance);
    }
}