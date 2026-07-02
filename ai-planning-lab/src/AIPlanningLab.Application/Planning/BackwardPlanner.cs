using AIPlanningLab.Application.Search;
using AIPlanningLab.Domain.Models;
using AIPlanningLab.Domain.Services;

namespace AIPlanningLab.Application.Planning;

/// <summary>
/// Planejamento por regressão.
/// </summary>
public class BackwardPlanner : IPlanner
{
    private readonly IPlanningOperator _operator;
    private readonly ISearchAlgorithm _search;

    private IPlanningProblem _problem;

    public BackwardPlanner(IPlanningOperator planningOperator, ISearchAlgorithm search)
    {
        _operator = planningOperator;
        _search = search;
    }

    public SearchResult Solve(IPlanningProblem problem)
    {
        this._problem = problem;

        var root = SearchNode.CreateRoot(problem.Goal, Expand, IsGoal);
        return _search.Search(root);
    }

    private bool IsGoal(SearchNode node)
    {
        return _problem.InitialState.Satisfies(node.State);
    }

    private IEnumerable<SearchNode> Expand(SearchNode node)
    {
        foreach (var action in _problem.Domain.Actions)
        {
            if (!_operator.CanRegress(node.State, action))
                continue;

            var newState = _operator.Regress(node.State, action);
            yield return node.CreateChild(newState, action);
        }
    }
}