using AIPlanningLab.Application.Heuristics;
using AIPlanningLab.Application.Planning;
using AIPlanningLab.Application.Search;
using AIPlanningLab.Domain.Models;
using AIPlanningLab.Domain.Services;
using AIPlanningLab.Implementations.Explicit.Heuristics;
using AIPlanningLab.Infrastructure.Execution;
using AIPlanningLab.Infrastructure.Metrics;

namespace AIPlanningLab.Cli.Tests;

internal class ExecuteRegressHeuristProgressGuidedPlanTest
{
    public static void Execute(
        IPlanningProblem problem,
        ISearchAlgorithm search,
        string algName,
        ITimeMetric timer,
        IExecutionLimiter excLimiter,
        bool debug = false)
    {
        string plannerName = "Forward-PrecomputedHeuristic";

        string heiristicName = "BackwardDistanceHeuristic";
        Console.WriteLine($"----------------------");
        Console.WriteLine($"Executing {plannerName} plan by search {algName}...");
        Console.WriteLine($"Heuristic: {heiristicName}");
        string key = "Total.time";
        IPlanningOperator planningOperator = new PlanningOperator();
        IPrecomputedHeuristic heuristic = new BackwardDistanceHeuristic(planningOperator, timer, excLimiter);
        IPlanner planner = new RegressionComputeProgressionGuided(planningOperator, search, heuristic);
        timer.Start();
        SearchResult result = planner.Solve(problem);
        timer.Save(key);
        timer.Stop();


        int timePrecompute = -1;
        if (timer.GetMiliseconds("Precompute.Timeout") is int x)
            timePrecompute = x;
        else
            timePrecompute = timer.GetMiliseconds("Precompute.End") ?? -1;

        double timeTotal = timer.GetMiliseconds(key) ?? -1;
        Console.WriteLine($"TIME: [Precompute: {timePrecompute} ms, elapse: {timeTotal} ms]");
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
        if (result.Plan == null) return;
        Console.WriteLine($"Plano encontrado ({result.Plan} ações):");
        for (int i = 0; i < result.Plan.Count; i++)
            Console.WriteLine($"  {i + 1}. {result.Plan[i].Name}");
    }
}
