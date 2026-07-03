namespace AIPlanningLab.Domain.Models;

/// <summary>
/// Operador do domínio.
/// </summary>
public interface IAction
{
    string Name { get; }
    int Cost { get; }

    /// <summary>
    /// Condições necessárias.
    /// </summary>
    IState Preconditions { get; }

    /// <summary>
    /// Fatos adicionados.
    /// </summary>
    IState PositiveEffects { get; }

    /// <summary>
    /// Fatos removidos.
    /// </summary>
    IState NegativeEffects { get; }
}

/// TODO: Avalisar o uso de IState ou uma lista de IPredicates