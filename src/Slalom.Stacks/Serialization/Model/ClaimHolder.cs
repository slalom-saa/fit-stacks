using System.Security.Claims;

namespace Slalom.Stacks.Serialization.Model
{
    /// <summary>
    /// Type used to hold a <see cref="Claim"/>  for serialization.
    /// </summary>
    public class ClaimHolder
    {
        /// <summary>
        /// Gets or sets the claim type.
        /// </summary>
        /// <value>The claim type.</value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the claim value.
        /// </summary>
        /// <value>The claim value.</value>
        public string Value { get; set; }
    }
}