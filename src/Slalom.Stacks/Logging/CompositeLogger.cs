using System;
using System.Collections.Generic;
using System.Linq;
using Slalom.Stacks.Configuration;

namespace Slalom.Stacks.Logging
{
    /// <summary>
    /// A composite <see cref="ILogger"/> implemenation that uses the component context to use all registered loggers.
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Logging.ILogger" />
    public class CompositeLogger : ILogger
    {
        private readonly IComponentContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeLogger"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public CompositeLogger(IComponentContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Write a log event with the debug level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="template">Message template describing the event.</param>
        /// <param name="properties">Objects positionally formatted into the message template.</param>
        public void Debug(Exception exception, string template, params object[] properties)
        {
            foreach (var logger in this.GetLoggers())
            {
                logger.Debug(exception, template, properties);
            }
        }

        /// <summary>
        /// Write a log event with the debug level.
        /// </summary>
        /// <param name="template">Message template describing the event.</param>
        /// <param name="properties">Objects positionally formatted into the message template.</param>
        public void Debug(string template, params object[] properties)
        {
            foreach (var logger in this.GetLoggers())
            {
                logger.Debug(template, properties);
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Write a log event with the error level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="template">Message template describing the event.</param>
        /// <param name="properties">Objects positionally formatted into the message template.</param>
        public void Error(Exception exception, string template, params object[] properties)
        {
            foreach (var logger in this.GetLoggers())
            {
                logger.Error(exception, template, properties);
            }
        }

        /// <summary>
        /// Write a log event with the error level.
        /// </summary>
        /// <param name="template">Message template describing the event.</param>
        /// <param name="properties">Objects positionally formatted into the message template.</param>
        public void Error(string template, params object[] properties)
        {
            foreach (var logger in this.GetLoggers())
            {
                logger.Error(template, properties);
            }
        }

        /// <summary>
        /// Write a log event with the fatal level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="template">Message template describing the event.</param>
        /// <param name="properties">Objects positionally formatted into the message template.</param>
        public void Fatal(Exception exception, string template, params object[] properties)
        {
            foreach (var logger in this.GetLoggers())
            {
                logger.Fatal(exception, template, properties);
            }
        }

        /// <summary>
        /// Write a log event with the fatal level.
        /// </summary>
        /// <param name="template">Message template describing the event.</param>
        /// <param name="properties">Objects positionally formatted into the message template.</param>
        public void Fatal(string template, params object[] properties)
        {
            foreach (var logger in this.GetLoggers())
            {
                logger.Fatal(template, properties);
            }
        }

        /// <summary>
        /// Write a log event with the information level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="template">Message template describing the event.</param>
        /// <param name="properties">Objects positionally formatted into the message template.</param>
        public void Information(Exception exception, string template, params object[] properties)
        {
            foreach (var logger in this.GetLoggers())
            {
                logger.Information(exception, template, properties);
            }
        }

        /// <summary>
        /// Write a log event with the information level.
        /// </summary>
        /// <param name="template">Message template describing the event.</param>
        /// <param name="properties">Objects positionally formatted into the message template.</param>
        public void Information(string template, params object[] properties)
        {
            foreach (var logger in this.GetLoggers())
            {
                logger.Information(template, properties);
            }
        }

        /// <summary>
        /// Write a log event with the verbose level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="template">Message template describing the event.</param>
        /// <param name="properties">Objects positionally formatted into the message template.</param>
        public void Verbose(Exception exception, string template, params object[] properties)
        {
            foreach (var logger in this.GetLoggers())
            {
                logger.Verbose(exception, template, properties);
            }
        }

        /// <summary>
        /// Write a log event with the verbose level.
        /// </summary>
        /// <param name="template">Message template describing the event.</param>
        /// <param name="properties">Objects positionally formatted into the message template.</param>
        public void Verbose(string template, params object[] properties)
        {
            foreach (var logger in this.GetLoggers())
            {
                logger.Verbose(template, properties);
            }
        }

        /// <summary>
        /// Write a log event with the warning level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="template">Message template describing the event.</param>
        /// <param name="properties">Objects positionally formatted into the message template.</param>
        public void Warning(Exception exception, string template, params object[] properties)
        {
            foreach (var logger in this.GetLoggers())
            {
                logger.Warning(exception, template, properties);
            }
        }

        /// <summary>
        /// Write a log event with the warning level.
        /// </summary>
        /// <param name="template">Message template describing the event.</param>
        /// <param name="properties">Objects positionally formatted into the message template.</param>
        public void Warning(string template, params object[] properties)
        {
            foreach (var logger in this.GetLoggers())
            {
                logger.Warning(template, properties);
            }
        }

        private IEnumerable<ILogger> GetLoggers()
        {
            return _context.ResolveAll<ILogger>().ToList().Where(e => e != this);
        }
    }
}