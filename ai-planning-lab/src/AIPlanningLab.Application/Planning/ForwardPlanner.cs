using AIPlanningLab.Application.Heuristics;
using AIPlanningLab.Application.Search;
using AIPlanningLab.Domain.Models;
using AIPlanningLab.Domain.Services;

namespace AIPlanningLab.Application.Planning;

/// <summary>
/// Planejamento por progressão (forward search).
/// </summary>
public sealed class ForwardPlanner : IPlanner
{
    private readonly IPlanningOperator _operator;
    private readonly ISearchAlgorithm _search;
    private readonly IHeuristic? _heuristic;

    private IPlanningProblem _problem;

    public ForwardPlanner(IPlanningOperator planningOperator, ISearchAlgorithm search, IHeuristic? heuristic = null)
    {
        _operator = planningOperator;
        _search = search;
        _heuristic = heuristic;
    }

    public SearchResult Solve(IPlanningProblem problem)
    {
        this._problem = problem;

        var root = SearchNode.CreateRoot(problem.InitialState, Expand, IsGoal, Heuristic);
        return _search.Search(root);
    }

    private int Heuristic(SearchNode node) {
        return _heuristic?.Evaluate(node.State, _problem) ?? 0;
    }

    private bool IsGoal(SearchNode node)
    {
        return node.State.Satisfies(_problem.Goal);
    }

    private IEnumerable<SearchNode> Expand(SearchNode node)
    {
        foreach (var action in _problem.Domain.Actions)
        {
            if (!_operator.CanProgress(node.State, action))
                continue;

            var newState = _operator.Progress(node.State, action);
            yield return node.CreateChild(newState, action);
        }
    }
}