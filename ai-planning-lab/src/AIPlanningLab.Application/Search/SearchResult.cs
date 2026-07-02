using AIPlanningLab.Domain.Models;

namespace AIPlanningLab.Application.Search;

/// <summary>
/// Resultado da busca.
/// </summary>
public record SearchResult
{
    public bool Success { get; init; }
    public IReadOnlyList<IAction>? Plan { get; init; }
    public int ExpandedNodes { get; init; }
    public int Depth { get; init; }

    public static SearchResult Failure(int expandedNodes) => new SearchResult()
    {
        Success = false,
        Plan = null,
        ExpandedNodes = expandedNodes,
        Depth = 0,
    };
} 

