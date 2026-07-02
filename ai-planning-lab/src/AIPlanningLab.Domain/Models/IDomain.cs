using AIPlanningLab.Domain.Registry;
namespace AIPlanningLab.Domain.Models;

/// <summary>
/// Define o universo do problema.
/// </summary>
public interface IDomain
{
    IPropositionRegistry Propositions { get; }

    IReadOnlyList<IAction> Actions { get; }
}