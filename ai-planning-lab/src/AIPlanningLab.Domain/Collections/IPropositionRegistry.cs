namespace AIPlanningLab.Domain.Collections;

/// <summary>
/// Mapeia proposições para identificadores internos.
///
/// Permite desacoplar nomes da representação física.
/// </summary>
public interface IPropositionRegistry
{
    /// <summary>
    /// Registra uma proposição.
    /// </summary>
    int Register(string proposition);

    /// <summary>
    /// Obtém índice.
    /// </summary>
    int GetIndex(string proposition);

    /// <summary>
    /// Obtém nome.
    /// </summary>
    string GetName(int index);

    /// <summary>
    /// Verifica existência.
    /// </summary>
    bool Contains(string proposition);

    /// <summary>
    /// Total de proposições.
    /// </summary>
    int Count { get; }
}
