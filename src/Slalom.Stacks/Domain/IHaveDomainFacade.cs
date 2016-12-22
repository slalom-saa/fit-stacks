namespace Slalom.Stacks.Domain
{
    /// <summary>
    /// Allows access to the domain facade by reference to this interface.
    /// </summary>
    public interface IHaveDomainFacade
    {
        /// <summary>
        /// Gets or sets the configured <see cref="IDomainFacade"/>.
        /// </summary>
        /// <value>The configured <see cref="IDomainFacade"/>.</value>
        IDomainFacade Domain { get; }
    }
}