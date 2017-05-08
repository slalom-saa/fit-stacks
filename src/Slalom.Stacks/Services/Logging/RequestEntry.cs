﻿/* 
 * Copyright (c) Stacks Contributors
 * 
 * This file is subject to the terms and conditions defined in
 * the LICENSE file, which is part of this source code package.
 */

using System;
using Newtonsoft.Json;
using Slalom.Stacks.Serialization;
using Slalom.Stacks.Services.Messaging;
using Slalom.Stacks.Utilities.NewId;
using Environment = Slalom.Stacks.Runtime.Environment;

namespace Slalom.Stacks.Services.Logging
{
    /// <summary>
    /// Represents a request log entry - something that tracks the request at the application level.
    /// </summary>
    public class RequestEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestEntry" /> class.
        /// </summary>
        public RequestEntry()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestEntry" /> class.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="environment">The environment.</param>
        public RequestEntry(Request request, Environment environment)
        {
            try
            {
                if (request.Message.Body != null)
                {
                    this.Body = JsonConvert.SerializeObject(request.Message.Body, new JsonSerializerSettings
                    {
                        ContractResolver = new BaseContractResolver()
                    });
                }
            }
            catch
            {
                this.Body = "{ \"Error\" : \"Serialization failed.\" }";
            }
            if (request.Message is IMessage)
            {
                var message = request.Message;
                this.RequestType = message.MessageType;
                this.RequestId = message.Id;
                this.TimeStamp = message.TimeStamp;
            }
            else
            {
                this.RequestType = request.Message?.MessageType;
            }
            this.SessionId = request.SessionId;
            this.UserName = request.User?.Identity?.Name;
            this.Path = request.Path;
            this.SourceAddress = request.SourceAddress;
            this.CorrelationId = request.CorrelationId;
            this.Parent = request.Parent?.Message?.Id;
            this.MachineName = environment.MachineName;
            this.ApplicationName = environment.ApplicationName;
            this.EnvironmentName = environment.EnvironmentName;
            this.ThreadId = environment.ThreadId;
        }

        /// <summary>
        /// Gets or sets the name of the application that received the request.
        /// </summary>
        /// <value>The name of the application that received the request.</value>
        public string ApplicationName { get; set; }

        /// <summary>
        /// Gets or sets the request payload.
        /// </summary>
        /// <value>The request payload.</value>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the correlation identifier.
        /// </summary>
        /// <value>The correlation identifier.</value>
        public string CorrelationId { get; set; }

        /// <summary>
        /// Gets or sets the name of the environment that received the request (Local, DEV, QA, PROD, etc.)
        /// </summary>
        /// <value>The name of the environment that received the request.</value>
        public string EnvironmentName { get; set; }

        /// <summary>
        /// Gets or sets the instance identifier.
        /// </summary>
        /// <value>The instance identifier.</value>
        public string Id { get; set; } = NewId.NextId();

        /// <summary>
        /// Gets or sets the name of the machine that received the request.
        /// </summary>
        /// <value>The name of the machine that received the request.</value>
        public string MachineName { get; set; }

        /// <summary>
        /// Gets or sets the parent request identifier.
        /// </summary>
        /// <value>The parent request identifier.</value>
        public string Parent { get; set; }

        /// <summary>
        /// Gets or sets the request path or URL.
        /// </summary>
        /// <value>The request path or URL.</value>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the request identifier.
        /// </summary>
        /// <value>The request identifier.</value>
        public string RequestId { get; set; }

        /// <summary>
        /// Gets or sets the name of the request.
        /// </summary>
        /// <value>The name of the request.</value>
        public string RequestType { get; set; }

        /// <summary>
        /// Gets or sets the session identifier.
        /// </summary>
        /// <value>The session identifier.</value>
        public string SessionId { get; set; }

        /// <summary>
        /// Gets or sets the user host address.
        /// </summary>
        /// <value>The user host address.</value>
        public string SourceAddress { get; set; }

        /// <summary>
        /// Gets or sets the ID of the thread that received the request.
        /// </summary>
        /// <value>The ID of the thread that received the request.</value>
        public int ThreadId { get; set; }

        /// <summary>
        /// Gets or sets the message time stamp.
        /// </summary>
        /// <value>The the message stamp.</value>
        public DateTimeOffset? TimeStamp { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }
    }
}