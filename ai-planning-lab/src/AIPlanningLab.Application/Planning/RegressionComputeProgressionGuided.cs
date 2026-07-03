using AIPlanningLab.Application.Heuristics;
using AIPlanningLab.Application.Search;
using AIPlanningLab.Domain.Models;
using AIPlanningLab.Domain.Services;

namespace AIPlanningLab.Application.Planning;

public class RegressionComputeProgressionGuided : IPlanner
{
    private readonly IPlanningOperator _operator;
    private readonly ISearchAlgorithm _search;
    private readonly IPrecomputedHeuristic? _heuristic;

    private IPlanningProblem? _problem = null;

    public RegressionComputeProgressionGuided(
        IPlanningOperator planningOperator, 
        ISearchAlgorithm search,
        IPrecomputedHeuristic? heuristic = null)
    {
        _operator = planningOperator;
        _search = search;
        _heuristic = heuristic;

    }
    public SearchResult Solve(IPlanningProblem problem)
    {
        this._problem = problem;
        _heuristic?.Precompute(_problem);
        var root = SearchNode.CreateRoot(problem.InitialState, Expand, IsGoal, Heuristic);
        return _search.Search(root);
    }

    private int Heuristic(SearchNode node)
    {
        if (_heuristic == null || _problem == null)
            throw new Exception("Invalid");

        return _heuristic.Evaluate(node.State, _problem);
    }

    private bool IsGoal(SearchNode node)
    {
        if (_problem == null)
            throw new Exception("Invalid");

        return node.State.Satisfies(_problem.Goal);
    }

    private IEnumerable<SearchNode> Expand(SearchNode node)
    {
        if (_problem == null)
            throw new Exception("Invalid");
        foreach (var action in _problem.Domain.Actions)
        {
            if (!_operator.CanProgress(node.State, action))
                continue;

            var newState = _operator.Progress(node.State, action);
            yield return node.CreateChild(newState, action);
        }
    }
}
