using AIPlanningLab.Domain.Models;
using AIPlanningLab.Domain.Registry;
using AIPlanningLab.Implementations.Collections;
using System.Text;

namespace AIPlanningLab.Implementations.Explicit;

/// <summary>
/// Representação explícita de um conjunto de fatos (estado, precondição,
/// efeito) usando um IBitSet.
///
/// O QUE É ARMAZENADO:
///   - _facts: o bitvector em si (a fonte de verdade sobre quais
///             proposições são verdadeiras)
///   - _registry: referência ao MESMO registry usado por todo o problema.
///                Não guarda cópia própria — apenas usa o registry para
///                traduzir índice → nome ao exibir (ToString/debug) e para
///                saber o tamanho (Count) ao criar novos IBitSet.
///
/// _registry é somente-leitura aqui: ExplicitState NUNCA registra novas
/// proposições. Registro só acontece uma vez, no parser, antes de qualquer
/// estado ser criado.
/// </summary>
public sealed class ExplicitState : IState
{
    private readonly IBitSet _facts;
    private readonly IPropositionRegistry _registry;

    public ExplicitState(IBitSet facts, IPropositionRegistry registry)
    {
        _facts = facts;
        _registry = registry;
    }

    public bool Satisfies(IState condition)
    {
        var c = Cast(condition);
        return c._facts.IsSubsetOf(_facts);
    }

    /// <summary>União: A ∪ B / A ∨ B</summary>
    public IState Merge(IState other)
        => new ExplicitState(_facts.Union(Cast(other)._facts), _registry);

    /// <summary>Diferença: A \ B</summary>
    public IState Remove(IState other)
        => new ExplicitState(_facts.Difference(Cast(other)._facts), _registry);

    /// <summary>Interseção: A ∩ B / A ∧ B</summary>
    public IState Restrict(IState other)
        => new ExplicitState(_facts.Intersection(Cast(other)._facts), _registry);

    public bool IsEmpty() => _facts.IsEmpty();

    // ---------------------------

    /// <summary>
    /// Retorna um NOVO estado com a proposição de <paramref name="index"/>
    /// marcada como verdadeira. Não muta o estado atual — clona o IBitSet
    /// internamente antes de escrever, preservando imutabilidade externa.
    /// </summary>
    public ExplicitState With(int index)
    {
        var copy = _facts.Clone();
        copy.Add(index);
        return new ExplicitState(copy, _registry);
    }


    /// <summary>Estado vazio (nenhuma proposição verdadeira).</summary>
    public static ExplicitState Empty(IPropositionRegistry registry)
        => new(BitSet.Empty(registry.Count), registry);

    /// <summary>
    /// Cria um estado a partir de VÁRIOS nomes de proposição de uma vez.
    /// Preferível a chamar With() em loop: aloca o bitvector uma única vez
    /// em vez de um novo array por proposição adicionada.
    /// </summary>
    public static ExplicitState FromPropositions(
        IPropositionRegistry registry,
        IEnumerable<string> propositionNames)
    {
        var bits = BitSet.Empty(registry.Count);
        foreach (var name in propositionNames)
            bits.Add(registry.GetIndex(name));

        return new ExplicitState(bits, registry);
    }

    /// <summary>Estado contendo uma única proposição.</summary>
    public static ExplicitState FromLiteral(IPropositionRegistry registry, string proposition)
        => FromPropositions(registry, [proposition]);


    public override string ToString()
    {
        var sb = new StringBuilder("{ ");
        bool first = true;
        for (int i = 0; i < _registry.Count; i++)
        {
            if (!_facts.Contains(i)) continue;
            if (!first) sb.Append(", ");
            sb.Append(_registry.GetName(i));
            first = false;
        }
        sb.Append(" }");
        return sb.ToString();
    }

    private static ExplicitState Cast(IState other)
        => other as ExplicitState
           ?? throw new ArgumentException(
               $"ExplicitState só opera com ExplicitState. Recebido: {other.GetType().Name}");

    public override bool Equals(object? obj)
    {
        if (obj is not ExplicitState other) return false;
        return _facts.IsSubsetOf(other._facts) && other._facts.IsSubsetOf(_facts);
    }

    public override int GetHashCode()
    {
        int hash = 17;
        for (int i = 0; i < _registry.Count; i++)
            if (_facts.Contains(i))
                hash = hash * 31 + i;
        return hash;
    }
}