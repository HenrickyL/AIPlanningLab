using AIPlanningLab.Application.Planning;
using AIPlanningLab.Application.Search;
using AIPlanningLab.Application.Search.Methods;
using AIPlanningLab.Domain.Models;
using AIPlanningLab.Domain.Services;
using System.Diagnostics;

namespace AIPlanningLab.Cli.Tests;

internal class BFSPlanForwardTest
{
    public static void Execute(IPlanningProblem problem) {
        Console.WriteLine("Executing BFS plan...");
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        IPlanningOperator planningOperator = new PlanningOperator();
        ISearchAlgorithm search = new BreadthFirstSearch();
        IPlanner planner = new ForwardPlanner(planningOperator, search);
        SearchResult result = planner.Solve(problem);

        stopwatch.Stop();
        Console.WriteLine($"Time: {stopwatch.ElapsedMilliseconds} ms");
        PrintResult(result);
    }

    private static void PrintResult(SearchResult result)
    {
        Console.WriteLine($"Nós expandidos: {result.ExpandedNodes}");
        Console.WriteLine($"Profundidade:   {result.Depth}");
        Console.WriteLine();

        if (!result.Success)
        {
            Console.WriteLine("Nenhum plano encontrado.");
            return;
        }

        Console.WriteLine($"Plano encontrado ({result.Plan.Count} ações):");
        for (int i = 0; i < result.Plan.Count; i++)
            Console.WriteLine($"  {i + 1}. {result.Plan[i].Name}");
    }
}
