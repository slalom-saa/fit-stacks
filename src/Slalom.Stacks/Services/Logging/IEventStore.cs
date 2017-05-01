﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Slalom.Stacks.Services.Messaging;

namespace Slalom.Stacks.Services.Logging
{
    /// <summary>
    /// Stores and retrieves events.
    /// </summary>
    public interface IEventStore
    {
        /// <summary>
        /// Gets events that fall within the specified time frame.
        /// </summary>
        /// <param name="start">The start date.</param>
        /// <param name="end">The end date.</param>
        /// <returns>Returns events that fall within the specified time frame.</returns>
        Task<IEnumerable<EventMessage>> GetEvents(DateTimeOffset? start = null, DateTimeOffset? end = null);

        /// <summary>
        /// Appends the specified event instance to the store.
        /// </summary>
        /// <param name="instance">The event instance to add.</param>
        /// <returns>Returns a task for asynchronous programming.</returns>
        Task Append(EventMessage instance);
    }
}
