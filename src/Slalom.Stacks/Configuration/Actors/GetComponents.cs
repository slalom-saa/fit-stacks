using System;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Messaging;

namespace Slalom.Stacks.Configuration.Actors
{
    [EndPoint("_systems/components")]
    public class GetComponents : EndPoint<string>
    {
        public Task Receive(string message)
        {
            return Console.Out.WriteLineAsync(message);
        }
    }
}