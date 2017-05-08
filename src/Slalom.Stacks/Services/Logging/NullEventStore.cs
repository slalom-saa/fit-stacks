/* 
 * Copyright (c) Stacks Contributors
 * 
 * This file is subject to the terms and conditions defined in
 * the LICENSE file, which is part of this source code package.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Services.Messaging;

namespace Slalom.Stacks.Services.Logging
{
    internal class NullEventStore : IEventStore
    {
        public Task<IEnumerable<EventMessage>> GetEvents(DateTimeOffset? start, DateTimeOffset? end)
        {
            return Task.FromResult(new EventMessage[0].AsEnumerable());
        }

        public Task Append(EventMessage instance)
        {
            return Task.FromResult(0);
        }
    }
}