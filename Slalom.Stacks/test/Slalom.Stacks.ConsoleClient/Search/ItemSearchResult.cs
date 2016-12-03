using System;
using Slalom.Stacks.Search;

public class ItemSearchResult : ISearchResult
{
    public Guid Id { get; set; } = Guid.NewGuid();
}