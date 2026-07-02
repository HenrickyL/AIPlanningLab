namespace AIPlanningLab.Domain.Models;

/// <summary>
/// Conjunto de fatos verdadeiros.
/// </summary>
public interface IState
{
    /// <summary>
    /// Verifica se satisfaz condição.
    /// this ⊨ other
    /// </summary>
    bool Satisfies(IState condition);

    /// <summary>
    /// Combina estados.
    /// União de estados.
    /// (A ∪ B)
    /// Simbólico:
    /// A ∨ B
    /// </summary>
    IState Merge(IState other);  // Unior || Or

    /// <summary>
    /// Remove fatos presentes no outro estado.
    /// (A \ B)
    /// </summary>
    IState Remove(IState other); // Difference

    /// <summary>
    /// Mantém apenas fatos comuns.
    /// (A ∩ B)
    /// Simbólico:
    /// A ∧ B
    /// </summary>
    IState Restrict(IState other); // Intersection || And


    /// <summary>
    /// Verifica se representa conjunto vazio.
    /// </summary>
    bool IsEmpty();

    /// <summary>
    /// Cria cópia independente.
    /// </summary>
    //IState Clone();
}


/*
 Pensei em usar mais proximo de conjuntos

    bool IsEmpty();

    bool Contains(IState other);

    IState Intersect(IState other);

    IState Union(IState other);

    IState Difference(IState other);

Ou mais próximo de Lógica:
    bool IsEmpty();
    // method And,Or, exists 
 */