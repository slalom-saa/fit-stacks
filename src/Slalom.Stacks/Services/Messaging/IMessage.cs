﻿/* 
 * Copyright (c) Stacks Contributors
 * 
 * This file is subject to the terms and conditions defined in
 * the LICENSE file, which is part of this source code package.
 */

using System;

namespace Slalom.Stacks.Services.Messaging
{
    /// <summary>
    /// An atomic packet of data that is transmitted through a message channel.
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// Gets the message body.
        /// </summary>
        /// <value>The body or content of the message.</value>
        object Body { get; }

        /// <summary>
        /// Gets the message identifier.
        /// </summary>
        /// <value>The Id of the message.</value>
        string Id { get; }

        /// <summary>
        /// Gets the message type.
        /// </summary>
        /// <value>The type of the message.</value>
        Type MessageType { get; }

        /// <summary>
        /// Gets the message name.
        /// </summary>
        /// <value>The name of the message.</value>
        string Name { get; }

        /// <summary>
        /// Gets the message timestamp.
        /// </summary>
        /// <value>When the message was created.</value>
        DateTimeOffset TimeStamp { get; }
    }
}