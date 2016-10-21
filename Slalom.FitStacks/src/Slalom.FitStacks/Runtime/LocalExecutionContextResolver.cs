using System;

namespace Slalom.FitStacks.Runtime
{
    /// <summary>
    /// Resolves the message execution context containing information at the current time in processing. This information is otherwise lost
    /// when processing is multi-threaded or distributed.
    /// </summary>
    /// <seealso cref="Slalom.FitStacks.Runtime.IExecutionContextResolver" />
    public class LocalExecutionContextResolver : IExecutionContextResolver
    {
        private readonly LocalExecutionContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalExecutionContextResolver"/> class.
        /// </summary>
        public LocalExecutionContextResolver()
        {
            _context = new LocalExecutionContext();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalExecutionContextResolver"/> class.
        /// </summary>
        /// <param name="context">The context to return.</param>
        public LocalExecutionContextResolver(LocalExecutionContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Resolves the current execution context.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public ExecutionContext Resolve()
        {
            return _context;
        }
    }
}