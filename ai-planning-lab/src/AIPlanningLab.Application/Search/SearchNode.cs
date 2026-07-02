using AIPlanningLab.Domain.Models;

namespace AIPlanningLab.Application.Search;

/// <summary>
/// Nó explorado pela busca.
/// </summary>
public sealed class SearchNode
{
    private readonly Func<SearchNode, IEnumerable<SearchNode>> _expand;
    private readonly Func<SearchNode, bool> _isGoal;

    public IState State { get; }
    public SearchNode? Parent { get; }
    public IAction? Action { get; }
    public int Depth { get; }

    private SearchNode(
        IState state,
        SearchNode? parent,
        IAction? action,
        int depth,
        Func<SearchNode, IEnumerable<SearchNode>> expand,
        Func<SearchNode, bool> isGoal)
    {
        State = state;
        Parent = parent;
        Action = action;
        Depth = depth;
        _expand = expand;
        _isGoal = isGoal;
    }

    /// <summary>Cria o nó raiz. Chamado uma vez pelo Planner.</summary>
    public static SearchNode CreateRoot(
        IState initialState,
        Func<SearchNode, IEnumerable<SearchNode>> expand,
        Func<SearchNode, bool> isGoal)
        => new(initialState, parent: null, action: null, depth: 0, expand, isGoal);

    /// <summary>
    /// Cria um nó filho reutilizando as mesmas closures de expansão/objetivo
    /// da raiz — usado internamente pela closure _expand do Planner.
    /// </summary>
    public SearchNode CreateChild(IState newState, IAction actionApplied)
        => new(newState, parent: this, action: actionApplied, depth: Depth + 1, _expand, _isGoal);

    /// <summary>Delega para a closure de expansão injetada pelo Planner.</summary>
    public IEnumerable<SearchNode> Expand() => _expand(this);

    /// <summary>Delega para a closure de teste de objetivo injetada pelo Planner.</summary>
    public bool IsGoal() => _isGoal(this);

    /// <summary>
    /// Reconstrói o plano subindo a cadeia de Parent até a raiz.
    /// </summary>
    public IReadOnlyList<IAction> ReconstructPlan()
    {
        var actions = new List<IAction>();
        var current = this;
        while (current?.Action is not null)
        {
            actions.Add(current.Action);
            current = current.Parent;
        }
        actions.Reverse();
        return actions;
    }
}
