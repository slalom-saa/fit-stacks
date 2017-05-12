/* 
 * Copyright (c) Stacks Contributors
 * 
 * This file is subject to the terms and conditions defined in
 * the LICENSE file, which is part of this source code package.
 */

using System.Collections.Generic;
using System.Threading.Tasks;
using Slalom.Stacks.Services.Logging;
using Slalom.Stacks.Services.Messaging;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Services.EndPoints
{
    /// <summary>
    /// Gets the events that have occurred within the service context.
    /// </summary>
    [EndPoint("_system/events", Public = false)]
    public class GetEvents : EndPoint<GetEventsRequest, IEnumerable<EventEntry>>
    {
        private readonly IEventStore _events;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetEvents" /> class.
        /// </summary>
        /// <param name="events">The configured event store.</param>
        public GetEvents(IEventStore events)
        {
            Argument.NotNull(events, nameof(events));

            _events = events;
        }

        /// <summary>
        /// Receives the call to the endpoint.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>Returns the response to the request.</returns>
        public override Task<IEnumerable<EventEntry>> ReceiveAsync(GetEventsRequest instance)
        {
            return _events.GetEvents(instance.Start, instance.End);
        }
    }
}