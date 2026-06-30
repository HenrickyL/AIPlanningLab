namespace AIPlanningLab.Domain.Models;

/// <summary>
/// Instância de planejamento.
/// </summary>
public interface IPlanningProblem
{
    IDomain Domain { get; }

    IState InitialState { get; }

    IState Goal { get; }

    IState Constraints { get; }
}