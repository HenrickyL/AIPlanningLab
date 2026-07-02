namespace AIPlanningLab.Domain.Registry;

/// <summary>
/// Mantém o mapeamento entre proposições e índices.
///
/// Exemplo:
/// on_a_b   → 0
/// clear_a  → 1
/// handempty→ 2
///
/// Permite representar estados usando vetores de bits.
/// </summary>
public sealed class PropositionRegistry : IPropositionRegistry
{
    private readonly Dictionary<string, int> _index = new();

    private readonly List<string> _propositions = new();

    /// <summary>
    /// Quantidade total de proposições registradas.
    /// </summary>
    public int Count => _propositions.Count;

    /// <summary>
    /// Registra uma proposição.
    /// Ignora duplicatas.
    /// </summary>
    public int Register(string proposition)
    {
        if (_index.TryGetValue(proposition, out var existing))
            return existing;

        int id = _propositions.Count;

        _index[proposition] = id;
        _propositions.Add(proposition);

        return id;
    }

    /// <summary>
    /// Obtém índice de uma proposição.
    /// </summary>
    public int GetIndex(string proposition)
    {
        return _index[proposition];
    }

    /// <summary>
    /// Obtém proposição pelo índice.
    /// </summary>
    public string GetName(int index)
    {
        return _propositions[index];
    }

    /// <summary>
    /// Verifica existência.
    /// </summary>
    public bool Contains(string proposition)
    {
        return _index.ContainsKey(proposition);
    }

    public IReadOnlyList<string> GetAll()
    {
        return _propositions;
    }
}
