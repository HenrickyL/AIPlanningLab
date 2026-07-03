using AIPlanningLab.Domain.Models;

namespace AIPlanningLab.Application.Heuristics;
/// <summary>
/// Heurísticas que precisam de uma fase de pré-processamento ANTES da busca
/// </summary>
public interface IPrecomputedHeuristic : IHeuristic
{
    /// <summary>
    /// Executa a fase de pré-processamento. Chamado exatamente uma vez,
    /// antes de qualquer chamada a Evaluate().
    /// </summary>
    void Precompute(IPlanningProblem problem);
}
