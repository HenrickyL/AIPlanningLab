namespace AIPlanningLab.Application.Search.Methods.Heuristic; 
public sealed class GreedyBestFirstSearch : UniformCostSearch
{
    public GreedyBestFirstSearch(
        Action? startSearch = null,
        Func<bool>? checkLimit = null) : base(startSearch, checkLimit)
    { }
    protected override int EvaluativeFunction(SearchNode node)
    {
        return node.Heuristic();
    }
}
