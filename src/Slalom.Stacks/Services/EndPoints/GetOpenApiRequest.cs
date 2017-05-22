using System;

namespace Slalom.Stacks.Services.EndPoints
{
    /// <summary>
    /// Request to get the OpenAPI definition.
    /// </summary>
    public class GetOpenApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetOpenApiRequest"/> class.
        /// </summary>
        /// <param name="host">The host name to display in the document.</param>
        public GetOpenApiRequest(string host)
        {
            this.Host = host;
        }

        /// <summary>
        /// Gets the host name to display in the document.
        /// </summary>
        /// <value>
        /// The host name to display in the document.
        /// </value>
        public string Host { get; }
    }
}