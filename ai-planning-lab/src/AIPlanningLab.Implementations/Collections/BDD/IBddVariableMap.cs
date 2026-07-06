using AIPlanningLab.Domain.Registry;

namespace AIPlanningLab.Implementations.Collections.BDD;

/// <summary>
/// Associa cada proposição do domínio à variável correspondente do BDD.
/// </summary>
public interface IBddVariableMap
{
    /// <summary>
    /// Inicializa o mapeamento das proposições.
    /// Deve ser chamado apenas uma vez.
    /// </summary>
    void Initialize(IPropositionRegistry registry);

    /// <summary>
    /// Obtém a variável correspondente à proposição.
    /// </summary>
    IBddSet GetVariable(int propositionIndex);

    /// <summary>
    /// Quantidade de variáveis.
    /// </summary>
    int Count { get; }
}
