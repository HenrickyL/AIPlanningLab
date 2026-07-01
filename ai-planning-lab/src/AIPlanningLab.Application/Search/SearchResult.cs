using AIPlanningLab.Domain.Models;

namespace AIPlanningLab.Application.Search;

/// <summary>
/// Resultado da busca.
/// </summary>
public sealed class SearchResult
{
    public bool Success { get; }

    public IReadOnlyList<IAction>? Plan { get; }

    public int ExpandedNodes { get; }

    public TimeSpan ExecutionTime { get; }
}
