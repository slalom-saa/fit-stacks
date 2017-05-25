/*
 * Copyright 2017 Stacks Contributors
 * 
 * This file is subject to the terms and conditions defined in
 * file 'LICENSE.txt', which is part of this source code package.
 */

using System;
using Newtonsoft.Json;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Services.Messaging;

namespace Slalom.Stacks.Services.Logging
{
    /// <summary>
    /// A serializable record of an event that has been raised.
    /// </summary>
    public class EventEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventEntry" /> class.
        /// </summary>
        public EventEntry()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventEntry" /> class.
        /// </summary>
        /// <param name="instance">The event instance.</param>
        /// <param name="environment">The current environment.</param>
        public EventEntry(EventMessage instance, Application environment)
        {
            this.RequestId = instance.RequestId;
            this.ApplicationName = environment.Title;
            try
            {
                this.Body = JsonConvert.SerializeObject(instance.Body);
            }
            catch
            {
                this.Body = "{ \"error\" : \"Serialization failed.\" }";
            }
            this.Id = instance.Id;
            this.MessageType = instance.MessageType.FullName;
            this.Name = instance.Name;
            this.EnvironmentName = environment.Environment;
        }

        /// <summary>
        /// Gets the name of the application.
        /// </summary>
        /// <value>The name of the application.</value>
        public string ApplicationName { get; set; }

        /// <summary>
        /// Gets the event body.
        /// </summary>
        /// <value>The body or content of the event.</value>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the name of the environment.
        /// </summary>
        /// <value>
        /// The name of the environment.
        /// </value>
        public string EnvironmentName { get; set; }

        /// <summary>
        /// Gets the event identifier.
        /// </summary>
        /// <value>The Id of the event.</value>
        public string Id { get; set; }

        /// <summary>
        /// Gets the message type.
        /// </summary>
        /// <value>The type of the message.</value>
        public string MessageType { get; set; }

        /// <summary>
        /// Gets the event name.
        /// </summary>
        /// <value>The name of the message.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets the request message identifier.
        /// </summary>
        /// <value>The request message identifier.</value>
        public string RequestId { get; set; }

        /// <summary>
        /// Gets the event timestamp.
        /// </summary>
        /// <value>When the event was created.</value>
        public DateTimeOffset TimeStamp { get; set; } = DateTimeOffset.Now;
    }
}