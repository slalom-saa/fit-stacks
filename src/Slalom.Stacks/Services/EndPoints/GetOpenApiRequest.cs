using System;

namespace Slalom.Stacks.Services.EndPoints
{
    /// <summary>
    /// Request to get the OpenAPI definition.
    /// </summary>
    public class GetOpenApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetOpenApiRequest" /> class.
        /// </summary>
        /// <param name="host">The host name to display in the document.</param>
        /// <param name="all"><c>true</c> if all endpoints should be retrieved; otherwise, <c>false</c>.</param>
        public GetOpenApiRequest(string host, bool all)
        {
            this.Host = host;
            this.All = all;
        }

        /// <summary>
        /// Gets a value indicating whether all endpoints should be retrieved or just public ones.
        /// </summary>
        /// <value>
        ///   <c>true</c> if all endpoints should be retrieved; otherwise, <c>false</c>.
        /// </value>
        public bool All { get; }

        /// <summary>
        /// Gets the host name to display in the document.
        /// </summary>
        /// <value>
        /// The host name to display in the document.
        /// </value>
        public string Host { get; }
    }
}