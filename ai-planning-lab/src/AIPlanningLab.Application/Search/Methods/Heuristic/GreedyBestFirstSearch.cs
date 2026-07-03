namespace AIPlanningLab.Application.Search.Methods.Heuristic; 
public sealed class GreedyBestFirstSearch : UniformCostSearch
{
    protected override int EvaluativeFunction(SearchNode node)
    {
        return node.Heuristic();
    }
}
