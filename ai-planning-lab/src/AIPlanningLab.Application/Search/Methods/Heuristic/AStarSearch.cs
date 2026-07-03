
namespace AIPlanningLab.Application.Search.Methods.Heuristic;

public sealed class AStarSearch : UniformCostSearch
{
    protected override int EvaluativeFunction(SearchNode node)
    {
        return base.EvaluativeFunction(node) + node.Heuristic();
    }
}
