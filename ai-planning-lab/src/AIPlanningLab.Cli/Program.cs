using AIPlanningLab.Application.Planning;
using AIPlanningLab.Application.Search;
using AIPlanningLab.Application.Search.Methods;
using AIPlanningLab.Application.Search.Methods.Heuristic;
using AIPlanningLab.Cli.Tests;
using AIPlanningLab.Domain.Models;
using AIPlanningLab.Domain.Services;
using AIPlanningLab.Implementations.Explicit;
using AIPlanningLab.Implementations.Explicit.Parser;
using AIPlanningLab.Infrastructure.Execution;
using AIPlanningLab.Infrastructure.Metrics;
using AIPlanningLab.Infrastructure.Parser;

namespace AIPlanningLab.CLI;

public class Program
{
    private static void Main(/*string[] args*/)
    {
        //string samplePath = args.Length > 0
        //    ? args[0]
        //    : Path.Combine(AppContext.BaseDirectory, "samples", "problems", "block-word", "BLOCK-WORD-1-GROUNDED.txt");

        string problemName = "block-word-3";
        //string problemName = "rovers-4";

        string samplePath = Path.Combine(AppContext.BaseDirectory, "samples", "problems", "block-word", "BLOCK-WORD-3-GROUNDED.txt");
        //string samplePath = Path.Combine(AppContext.BaseDirectory, "samples", "problems", "rovers", "rovers-04-GROUNDED.txt");


        if (!File.Exists(samplePath))
        {
            Console.Error.WriteLine($"Arquivo não encontrado: {samplePath}");
            Console.Error.WriteLine("Uso: dotnet run -- <caminho-para-o-arquivo-de-problema>");
            return;
        }

        IProblemParser parser = new ExplicitProblemParser();
        IPlanningProblem problem = parser.Parse(samplePath);
        Console.WriteLine($"Problema carregado: {problemName}");
        Console.WriteLine($"Proposições registradas: {problem.Domain.Propositions.Count}");
        Console.WriteLine($"Ações carregadas:        {problem.Domain.Actions.Count}");
        Console.WriteLine();

        Console.WriteLine("Ações:");
        foreach (var action in problem.Domain.Actions)
            Console.WriteLine($"  - {action.Name}");

        Console.WriteLine();
        Console.WriteLine($"Estado inicial: {problem.InitialState}");
        Console.WriteLine($"Meta:           {problem.Goal}");

        ITimeMetric timer = new StopwatchMetric();
        IExecutionLimiter excLimiter = new TimeLimiter(timer, 2 * 60);

        ITimeMetric timerForward = new StopwatchMetric();
        IExecutionLimiter excLimiterForward = new TimeLimiter(timerForward, 5 * 60);

        Action StarTime = () =>
        {
            timerForward.Start();
        };
        Func<bool> CheckLimit = () =>
        {
            bool response = excLimiterForward.ShouldStop();
            if (response) {
                Console.WriteLine(">TimeLimitExceded");
                timerForward.Restart();
            }
            return response;
        };
        IPlanningOperator planningOperator = new ExplicitPlanningOperator();
        ISearchAlgorithm BFS = new BreadthFirstSearch();
        ISearchAlgorithm DFS = new DepthFirstSearch();
        ISearchAlgorithm UCS = new UniformCostSearch(StarTime,CheckLimit);
        ISearchAlgorithm GBFS = new GreedyBestFirstSearch(StarTime, CheckLimit);
        ISearchAlgorithm AStar = new AStarSearch(StarTime, CheckLimit);


        List<(string, string, IPlanner)> options = new() { 
            ("BFS", "Forward", new ForwardPlanner(planningOperator, BFS)),
            ("BFS", "Backward", new BackwardPlanner(planningOperator, BFS)),

            ("DFS", "Forward", new ForwardPlanner(planningOperator, DFS)),
            ("DFS", "Backward", new BackwardPlanner(planningOperator, DFS)),

            ("UCS", "Forward", new ForwardPlanner(planningOperator, UCS)),
            ("UCS", "Backward", new BackwardPlanner(planningOperator, UCS)),
        };

        foreach (var (algName, plannerName, planner) in options)
        {
            ExecutePlanTest.Execute(problem, algName, planner, plannerName);
        }

        ExecuteHeuristicPlanTest.Execute(problem, AStar, "A*");
        Reset();
        ExecuteHeuristicPlanTest.Execute(problem, GBFS, "GBFS");
        Reset();
        ExecuteRegressHeuristProgressGuidedPlanTest.Execute(problem, AStar, "A*", timer, excLimiter);
        Reset();
        ExecuteRegressHeuristProgressGuidedPlanTest.Execute(problem, GBFS, "GBFS", timer, excLimiter);
        Reset();
    }

    private static void Reset() {
        GC.Collect();
        GC.WaitForPendingFinalizers();
    }
}
