
namespace AIPlanningLab.Application.Search.Methods.Heuristic;

public sealed class AStarSearch : UniformCostSearch
{
    public AStarSearch(
        Action? startSearch = null,
        Func<bool>? checkLimit = null): base(startSearch,checkLimit)
    {}
    protected override int EvaluativeFunction(SearchNode node)
    {
        return base.EvaluativeFunction(node) + node.Heuristic();
    }
}
