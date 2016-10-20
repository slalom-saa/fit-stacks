﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Slalom.FitStacks.Runtime;

namespace Slalom.FitStacks.Messaging
{
    /// <summary>
    /// Handles commands of the specified type.  The result type should either be an event or a document.
    /// </summary>
    /// <typeparam name="TCommand">The type of command.</typeparam>
    /// <typeparam name="TResult">The type of result.  Either an event or a message.</typeparam>
    /// <seealso cref="Slalom.FitStacks.Messaging.ICommandHandler{TCommand, TResult}" />
    /// <seealso href="http://bit.ly/2eajcKW">Enterprise Integration Patterns: Designing, Building, and Deploying Messaging Solutions</seealso>
    public abstract class CommandHandler<TCommand, TResult> : ICommandHandler<TCommand, TResult> where TCommand : Command<TResult>
    {
        /// <summary>
        /// Gets or sets the current <seealso cref="ExecutionContext"/>.
        /// </summary>
        /// <value>The current <seealso cref="ExecutionContext"/>.</value>
        public ExecutionContext Context { get; set; }

        /// <summary>
        /// Handles the specified command.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public abstract Task<TResult> Handle(TCommand command);
    }
}