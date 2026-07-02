using AIPlanningLab.Application.Search.Methods;
using AIPlanningLab.Cli.Tests;
using AIPlanningLab.Domain.Models;
using AIPlanningLab.Implementations.Explicit.Parser;
using AIPlanningLab.Infrastructure.Parser;

namespace AIPlanningLab.CLI;

public class Program
{
    private static void Main(/*string[] args*/)
    {

        //string samplePath = args.Length > 0
        //    ? args[0]
        //    : Path.Combine(AppContext.BaseDirectory, "samples", "problems", "block-word", "BLOCK-WORD-1-GROUNDED.txt");

        Console.WriteLine(AppContext.BaseDirectory);
        //string samplePath = Path.Combine(AppContext.BaseDirectory, "samples", "problems", "block-word", "BLOCK-WORD-1-GROUNDED.txt");
        string samplePath = Path.Combine(AppContext.BaseDirectory, "samples", "problems", "rovers", "rovers-02-GROUNDED.txt");


        if (!File.Exists(samplePath))
        {
            Console.Error.WriteLine($"Arquivo não encontrado: {samplePath}");
            Console.Error.WriteLine("Uso: dotnet run -- <caminho-para-o-arquivo-de-problema>");
            return;
        }

        IProblemParser parser = new ExplicitProblemParser();
        IPlanningProblem problem = parser.Parse(samplePath);

        Console.WriteLine($"Proposições registradas: {problem.Domain.Propositions.Count}");
        Console.WriteLine($"Ações carregadas:        {problem.Domain.Actions.Count}");
        Console.WriteLine();

        Console.WriteLine("Ações:");
        foreach (var action in problem.Domain.Actions)
            Console.WriteLine($"  - {action.Name}");

        Console.WriteLine();
        Console.WriteLine($"Estado inicial: {problem.InitialState}");
        Console.WriteLine($"Meta:           {problem.Goal}");

        ExecutePlanTest.Execute(problem, new BreadthFirstSearch(), "BFS");
        ExecutePlanTest.Execute(problem, new DepthFirstSearch(), "DFS");
    }
}
