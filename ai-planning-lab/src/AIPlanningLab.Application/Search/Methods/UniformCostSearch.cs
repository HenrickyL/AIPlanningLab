using AIPlanningLab.Domain.Models;
namespace AIPlanningLab.Application.Search.Methods;

public class UniformCostSearch : ISearchAlgorithm
{
    private readonly Func<bool>? _checkLimit;
    private readonly Action? _startSearch;

    public UniformCostSearch(
        Action? startSearch = null,
        Func<bool>? checkLimit = null
    ){
        _startSearch = startSearch;
        _checkLimit = checkLimit;
    }
    public SearchResult Search(SearchNode root)
    {
        _startSearch?.Invoke();
        var frontier = new PriorityQueue<SearchNode, int>();
        var bestKnownCost = new Dictionary<IState, int>();
        bestKnownCost[root.State] = root.PathCost;
        frontier.Enqueue(root, EvaluativeFunction(root));

        SearchNode node;
        int expanded = 0;
        //int lastDepth = root.Depth;

        while (frontier.Count > 0)
        {
            node = frontier.Dequeue();
            if (_checkLimit?.Invoke() == true) {
                break;
            }
            //lasy deletion
            if (node.PathCost > bestKnownCost.GetValueOrDefault(node.State, int.MaxValue))
                continue;

            if (node.IsGoal())
            {
                return new SearchResult()
                {
                    Success = true,
                    Plan = node.ReconstructPlan(),
                    ExpandedNodes = expanded,
                    Depth = node.Depth,
                    Cost = node.PathCost
                };
            }
            //if(lastDepth < node.Depth)
            //{
            //    lastDepth = node.Depth;
            //    Console.WriteLine($"Depth:{lastDepth}");
            //}
            expanded++;
            foreach (var child in node.Expand()) {
                if (!bestKnownCost.TryGetValue(child.State, out var known) || child.PathCost < known) {
                    bestKnownCost[child.State] = child.PathCost;
                    frontier.Enqueue(child, EvaluativeFunction(child));
                }
            }
        }

        return SearchResult.Failure(expanded);
    }

    protected virtual int EvaluativeFunction(SearchNode node)
    { 
        return node.PathCost;
    }
}
