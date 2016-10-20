using System;
using System.Linq;

namespace Slalom.FitStacks
{
    /// <summary>
    /// Defines a contract for an object that has an identity.
    /// </summary>
    public interface IHaveIdentity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        Guid Id { get; }
    }
}
