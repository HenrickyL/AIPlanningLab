namespace AIPlanningLab.Implementations.Collections.Set;

/// <summary>
/// Conjunto booleano otimizado.
///
/// Representa conjuntos e operações
/// fundamentais usando bits.
/// </summary>
public interface IBitSet
{
    /// <summary>
    /// Adiciona posição.
    /// </summary>
    void Add(int index);

    /// <summary>
    /// Remove posição.
    /// </summary>
    void Remove(int index);

    /// <summary>
    /// Verifica presença.
    /// </summary>
    bool Contains(int index);

    /// <summary>
    /// União.
    /// </summary>
    IBitSet Union(IBitSet other);

    /// <summary>
    /// Diferença.
    /// </summary>
    IBitSet Difference(IBitSet other);

    /// <summary>
    /// Interseção.
    /// </summary>
    IBitSet Intersection(IBitSet other);

    /// <summary>
    /// Subconjunto.
    /// </summary>
    bool IsSubsetOf(IBitSet other);

    /// <summary>
    /// Conjunto vazio.
    /// </summary>
    bool IsEmpty();

    /// <summary>
    /// Cópia independente.
    /// </summary>
    IBitSet Clone();
}