using AIPlanningLab.Application.Planning;
using AIPlanningLab.Application.Search;
using AIPlanningLab.Domain.Models;
using AIPlanningLab.Domain.Services;
using System.Diagnostics;

namespace AIPlanningLab.Cli.Tests;

internal class ExecutePlanTest
{
    public static void Execute(
        IPlanningProblem problem, 
        string algName,
        IPlanner planner,
        string plannerName,
        bool debug = false) {
        Console.WriteLine($"----------------------");
        Console.WriteLine($"Executing {plannerName} plan by search {algName}...");
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        
        //ISearchAlgorithm search = new DepthFirstSearch();
        //IPlanner planner = new ForwardPlanner(planningOperator, search);
        SearchResult result = planner.Solve(problem);

        stopwatch.Stop();
        Console.WriteLine($"Time: {stopwatch.ElapsedMilliseconds} ms");
        Console.WriteLine($"Success: {result.Success}, expanded Nodes: {result.ExpandedNodes}, solutionSteps: {result.Depth}");
        if (debug) PrintResult(result);
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
