using System.Collections.Generic;
namespace AIPlanningLab.Domain.Models;

/// <summary>
/// Define o universo do problema.
/// </summary>
public interface IDomain
{
    IReadOnlySet<IProposition> Predicates { get; }

    IReadOnlyList<IAction> Actions { get; }
}