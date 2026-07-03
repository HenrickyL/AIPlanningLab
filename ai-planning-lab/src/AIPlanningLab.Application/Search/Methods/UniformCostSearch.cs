using AIPlanningLab.Domain.Models;
namespace AIPlanningLab.Application.Search.Methods;

public class UniformCostSearch : ISearchAlgorithm
{
    public SearchResult Search(SearchNode root)
    {
        var frontier = new PriorityQueue<SearchNode, int>();
        var bestKnownCost = new Dictionary<IState, int>();
        bestKnownCost[root.State] = root.PathCost;
        frontier.Enqueue(root, EvaluativeFunction(root));

        SearchNode node;
        int expanded = 0;

        while (frontier.Count > 0)
        {
            node = frontier.Dequeue();
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
        return node.PathCost; //+ node.Heuristic
    }
}
