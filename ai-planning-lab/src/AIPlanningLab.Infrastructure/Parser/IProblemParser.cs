using AIPlanningLab.Domain.Models;

namespace AIPlanningLab.Infrastructure.Parser;

/// <summary>
/// Converte um arquivo em um problema de planejamento.
/// </summary>
public interface IProblemParser
{
    IPlanningProblem Parse(string path);
}