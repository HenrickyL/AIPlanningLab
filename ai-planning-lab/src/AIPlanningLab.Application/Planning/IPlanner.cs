using AIPlanningLab.Application.Search;
using AIPlanningLab.Domain.Models;

namespace AIPlanningLab.Application.Planning;
/// <summary>
/// Resolve um problema de planejamento.
/// </summary>
public interface IPlanner
{
    SearchResult Solve(IPlanningProblem problem);
}
