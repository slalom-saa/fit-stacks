using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Slalom.Stacks.Messaging.Logging;

namespace Slalom.Stacks.ConsoleClient.Aspects
{
    public class ActionStore : IActionStore
    {
        public Task Append(ActionEntry entry)
        {
            Console.WriteLine(JsonConvert.SerializeObject(entry, Formatting.Indented));

            return Task.FromResult(0);
        }
    }
}