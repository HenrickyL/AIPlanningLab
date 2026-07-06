using AIPlanningLab.Domain.Registry;

namespace AIPlanningLab.Implementations.Collections.BDD;

/// <summary>
/// Associa proposições aos índices utilizados pelo BDD.
///
/// Mantém o mesmo ordenamento do PropositionRegistry.
/// </summary>
internal sealed class BddVariableMap : IBddVariableMap
{
    private readonly IBddManager _manager;

    private readonly List<IBddSet> _variables = new();

    public int Count => _variables.Count;

    public BddVariableMap(IBddManager manager)
    {
        _manager = manager;
    }

    public void Initialize(IPropositionRegistry registry)
    {
        _variables.Clear();

        for (int i = 0; i < registry.Count; i++)
        {
            _variables.Add(
                _manager.CreateVariable());
        }
    }

    public IBddSet GetVariable(int propositionIndex)
    {
        return _variables[propositionIndex];
    }
}