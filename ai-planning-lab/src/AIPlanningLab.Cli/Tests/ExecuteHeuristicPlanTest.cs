using AIPlanningLab.Application.Heuristics;
using AIPlanningLab.Application.Planning;
using AIPlanningLab.Application.Search;
using AIPlanningLab.Domain.Models;
using AIPlanningLab.Domain.Services;
using AIPlanningLab.Implementations.Explicit;
using AIPlanningLab.Implementations.Explicit.Heuristics;
using System.Diagnostics;

namespace AIPlanningLab.Cli.Tests;

internal class ExecuteHeuristicPlanTest
{
    public static void Execute(
        IPlanningProblem problem,
        ISearchAlgorithm search,
        string algName,
        bool debug = false)
    {
        string plannerName = "Forward";
        string heuristicName = "GoalCountHeuristic";

        Console.WriteLine($"----------------------");
        Console.WriteLine($"Executing {plannerName} plan by search {algName}...");
        Console.WriteLine($"Heuristic: {heuristicName}");

        var stopwatch = new Stopwatch();
        stopwatch.Start();

        IPlanningOperator planningOperator = new ExplicitPlanningOperator();
        IHeuristic heuristic = new GoalCountHeuristic();
        IPlanner planner = new ForwardPlanner(planningOperator, search, heuristic);

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
