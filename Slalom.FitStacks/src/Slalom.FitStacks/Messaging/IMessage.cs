﻿using System;
using System.Linq;

namespace Slalom.FitStacks.Messaging
{
    /// <summary>
    /// An atomic packet of data that is transmitted through a message channel.
    /// </summary>
    /// <seealso cref="Slalom.FitStacks.IHaveIdentity" />
    public interface IMessage : IHaveIdentity
    {
        /// <summary>
        /// Gets the message time stamp.
        /// </summary>
        /// <value>The message time stamp.</value>
        DateTimeOffset TimeStamp { get; }
    }
}
