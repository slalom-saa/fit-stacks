using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Utilities.NewId;

namespace Slalom.Stacks.Messaging
{
    public class Message : IMessage
    {
        public string Id { get; } = NewId.NextId();
        public DateTimeOffset TimeStamp { get; } = DateTimeOffset.UtcNow;
    }
}
