using AIPlanningLab.Domain.Models;

namespace AIPlanningLab.Application.Search.Methods;

/// <summary>
/// Busca em profundidade genérica.
/// </summary>
public sealed class DepthFirstSearch : ISearchAlgorithm
{
    public SearchResult Search(SearchNode root)
    {
        if (root.IsGoal())
        {
            return new SearchResult()
            {
                Success = true,
                Plan = root.ReconstructPlan(),
                ExpandedNodes = 0,
                Depth = root.Depth,
            };
        }

        var frontier = new Stack<SearchNode>();
        var visited = new HashSet<IState>();

        frontier.Push(root);
        visited.Add(root.State);

        SearchNode node;
        int expanded = 0;

        while (frontier.Count > 0)
        {
            node = frontier.Pop();
            expanded++;

            foreach (var child in node.Expand())
            {
                if (child.IsGoal())
                    return new SearchResult()
                    {
                        Success = true,
                        Plan = child.ReconstructPlan(),
                        ExpandedNodes = expanded,
                        Depth = child.Depth,
                    };

                if (visited.Add(child.State))
                    frontier.Push(child);
            }
        }

        return SearchResult.Failure(expanded);
    }
}
