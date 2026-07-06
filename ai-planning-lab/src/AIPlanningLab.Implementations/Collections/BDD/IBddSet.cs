using DecisionDiagrams;

namespace AIPlanningLab.Implementations.Collections.BDD;

/// <summary>
/// Representa uma função booleana armazenada como BDD.
/// </summary>
public interface IBddSet
{
    /// <summary>
    /// Fórmula representada.
    /// </summary>
    DD Formula { get; }
}
