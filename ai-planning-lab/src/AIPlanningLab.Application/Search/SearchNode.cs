using AIPlanningLab.Domain.Models;

namespace AIPlanningLab.Application.Search;

/// <summary>
/// Nó explorado pela busca.
/// </summary>
public class SearchNode
{
    public IState State { get; }

    public SearchNode? Parent { get; }

    public IAction? Action { get; }

    public int G { get; }

    public int H { get; }

    public int F => G + H;
}
