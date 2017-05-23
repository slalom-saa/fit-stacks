namespace Slalom.Stacks.Services.OpenApi
{
    /// <summary>
    /// Allows the definition of a security scheme that can be used by the operations. Supported schemes are basic authentication, an API key (either as a header or as a query parameter) and OAuth2's common flows (implicit, password, application and access code).
    /// </summary>
    public class SecurityScheme
    {
        /// <summary>
        /// Gets or sets the type of the security scheme. Valid values are "basic", "apiKey" or "oauth2".
        /// </summary>
        /// <value>
        /// The type of the security scheme. Valid values are "basic", "apiKey" or "oauth2".
        /// </value>
        public string Type { get; set; }
    }
}