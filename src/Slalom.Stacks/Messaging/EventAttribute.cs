﻿using System;
using System.Linq;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// Defines information about an EventName.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class)]
    public class EventAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the event name.
        /// </summary>
        /// <value>The event name.</value>
        public string Name { get; set; }
    }
}