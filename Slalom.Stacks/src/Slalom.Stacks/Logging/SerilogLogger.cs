using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Exceptions;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Logging
{
    /// <summary>
    /// A <see href="https://serilog.net/">Serilog</see> implementation of the <see cref="ILogger"/>.
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Logging.ILogger" />
    internal class SerilogLogger : ILogger
    {
        private readonly Logger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SerilogLogger" /> class.
        /// </summary>
        /// <param name="configuration">The configured <see cref="IConfiguration" /> instance.</param>
        /// <param name="context">The configured <see cref="IExecutionContextResolver" /> instance.</param>
        /// <param name="policies">The configured <see cref="IDestructuringPolicy">policies</see>.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="configuration" /> argument is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="context" /> argument is null.</exception>
        public SerilogLogger(IConfiguration configuration, IExecutionContextResolver context, IEnumerable<IDestructuringPolicy> policies)
        {
            Argument.NotNull(() => configuration);
            Argument.NotNull(() => context);

            var builder = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.WithExceptionDetails()
                .Destructure.With(policies.ToArray());

            this.AddApplicationInsightsSink(builder, configuration);

            _logger = builder.CreateLogger();
        }

        /// <summary>
        /// Write a log event with the debug level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="template">Message template describing the event.</param>
        /// <param name="properties">Objects positionally formatted into the message template.</param>
        public void Debug(Exception exception, string template, params object[] properties)
        {
            _logger.Debug(exception, template, properties);
        }

        /// <summary>
        /// Write a log event with the debug level.
        /// </summary>
        /// <param name="template">Message template describing the event.</param>
        /// <param name="properties">Objects positionally formatted into the message template.</param>
        public void Debug(string template, params object[] properties)
        {
            _logger.Debug(template, properties);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Write a log event with the error level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="template">Message template describing the event.</param>
        /// <param name="properties">Objects positionally formatted into the message template.</param>
        public void Error(Exception exception, string template, params object[] properties)
        {
            _logger.Error(exception, template, properties);
        }

        /// <summary>
        /// Write a log event with the error level.
        /// </summary>
        /// <param name="template">Message template describing the event.</param>
        /// <param name="properties">Objects positionally formatted into the message template.</param>
        public void Error(string template, params object[] properties)
        {
            _logger.Error(template, properties);
        }

        /// <summary>
        /// Write a log event with the fatal level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="template">Message template describing the event.</param>
        /// <param name="properties">Objects positionally formatted into the message template.</param>
        public void Fatal(Exception exception, string template, params object[] properties)
        {
            _logger.Fatal(exception, template, properties);
        }

        /// <summary>
        /// Write a log event with the fatal level.
        /// </summary>
        /// <param name="template">Message template describing the event.</param>
        /// <param name="properties">Objects positionally formatted into the message template.</param>
        public void Fatal(string template, params object[] properties)
        {
            _logger.Fatal(template, properties);
        }

        /// <summary>
        /// Write a log event with the information level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="template">Message template describing the event.</param>
        /// <param name="properties">Objects positionally formatted into the message template.</param>
        public void Information(Exception exception, string template, params object[] properties)
        {
            _logger.Information(exception, template, properties);
        }

        /// <summary>
        /// Write a log event with the information level.
        /// </summary>
        /// <param name="template">Message template describing the event.</param>
        /// <param name="properties">Objects positionally formatted into the message template.</param>
        public void Information(string template, params object[] properties)
        {
            _logger.Information(template, properties);
        }

        /// <summary>
        /// Write a log event with the verbose level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="template">Message template describing the event.</param>
        /// <param name="properties">Objects positionally formatted into the message template.</param>
        public void Verbose(Exception exception, string template, params object[] properties)
        {
            _logger.Verbose(exception, template, properties);
        }

        /// <summary>
        /// Write a log event with the verbose level.
        /// </summary>
        /// <param name="template">Message template describing the event.</param>
        /// <param name="properties">Objects positionally formatted into the message template.</param>
        public void Verbose(string template, params object[] properties)
        {
            _logger.Verbose(template, properties);
        }

        /// <summary>
        /// Write a log event with the warning level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="template">Message template describing the event.</param>
        /// <param name="properties">Objects positionally formatted into the message template.</param>
        public void Warning(Exception exception, string template, params object[] properties)
        {
            _logger.Warning(exception, template, properties);
        }

        /// <summary>
        /// Write a log event with the warning level.
        /// </summary>
        /// <param name="template">Message template describing the event.</param>
        /// <param name="properties">Objects positionally formatted into the message template.</param>
        public void Warning(string template, params object[] properties)
        {
            _logger.Warning(template, properties);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _logger?.Dispose();
            }
        }

        private void AddApplicationInsightsSink(LoggerConfiguration builder, IConfiguration configuration)
        {
            var value = configuration["ApplicationInsights:InstrumentationKey"];
            if (value != null)
            {
                builder.WriteTo.ApplicationInsightsEvents(value);
            }
        }
    }
}