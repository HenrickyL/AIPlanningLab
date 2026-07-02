using AIPlanningLab.Domain.Models;

namespace AIPlanningLab.Application.Search.Methods;
/// <summary>
/// Busca em largura genérica.
/// </summary>
public sealed class BreadthFirstSearch : ISearchAlgorithm
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

        var frontier = new Queue<SearchNode>();
        var visited = new HashSet<IState>();

        frontier.Enqueue(root);
        visited.Add(root.State);

        int expanded = 0;

        while (frontier.Count > 0)
        {
            var node = frontier.Dequeue();
            expanded++;

            foreach (var child in node.Expand())
            {
                if (child.IsGoal())
                    return new SearchResult()
                    {
                        Success= true,
                        Plan = child.ReconstructPlan(),
                        ExpandedNodes = expanded,
                        Depth = child.Depth,
                    };

                if (visited.Add(child.State))
                    frontier.Enqueue(child);
            }
        }

        return SearchResult.Failure(expanded);
    }
}
