namespace Slalom.Stacks.Search
{
    /// <summary>
    /// Allows access to the search facade by reference to this interface.
    /// </summary>
    public interface IHaveSearchFacade
    {
        /// <summary>
        /// Gets or sets the configured <see cref="ISearchFacade"/>.
        /// </summary>
        /// <value>The configured <see cref="ISearchFacade"/>.</value>
        ISearchFacade Search { get; }
    }
}