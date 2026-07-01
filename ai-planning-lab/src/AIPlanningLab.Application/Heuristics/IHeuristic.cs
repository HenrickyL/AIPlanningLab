using AIPlanningLab.Domain.Models;

namespace AIPlanningLab.Application.Heuristics;

/// <summary>
/// Estima distância até objetivo.
/// </summary>
public interface IHeuristic
{
    int Evaluate(IState state, IPlanningProblem problem);
}
