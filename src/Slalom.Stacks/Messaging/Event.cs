using System;
using System.Linq;
using System.Reflection;
using Slalom.Stacks.Utilities.NewId;

namespace Slalom.Stacks.Messaging
{
    public abstract class EventData
    {
    }

    /// <summary>
    /// An event that is raised when state changes within a particular domain.
    /// </summary>
    public class Event : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Event"/> class.
        /// </summary>
        public Event(EventData data) : base(data)
        {
        }
    }
}