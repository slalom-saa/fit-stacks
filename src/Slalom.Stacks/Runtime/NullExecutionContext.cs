using System.Security.Principal;

namespace Slalom.Stacks.Runtime
{
    /// <summary>
    /// Represents an execution request.  Intended to implement the null object pattern.
    /// </summary>
    /// <seealso cref="ExecutionContext" />
    /// <seealso href="http://bit.ly/29e2gRR">Wikipedia: Null Object pattern</seealso>
    public class NullExecutionContext : ExecutionContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullExecutionContext"/> class.
        /// </summary>
        public NullExecutionContext()
            : base("", "", "", 0)
        {
        }
    }
}