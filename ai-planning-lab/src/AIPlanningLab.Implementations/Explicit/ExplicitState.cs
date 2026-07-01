using AIPlanningLab.Domain.Collections;
using AIPlanningLab.Domain.Models;

namespace AIPlanningLab.Implementations.Explicit;

public class ExplicitState : IState
{
    private readonly IBitSet _facts;

    private readonly IPropositionRegistry _registry;

    public ExplicitState(
        IBitSet facts,
        IPropositionRegistry registry)
    {
        _facts = facts;
        _registry = registry;
    }

    public bool Satisfies(
        IState condition)
    {
        var s =
            (ExplicitState)condition;

        return
            s._facts.IsSubsetOf(_facts);
    }

    public IState Merge(
        IState other)
    {
        return new ExplicitState(
            _facts.Union(((ExplicitState)other)._facts),
            _registry
        );
    }

    public IState Remove(
        IState other)
    {
        return new ExplicitState(
            _facts.Difference(((ExplicitState)other)._facts),
            _registry
        );
    }

    public IState Restrict(
        IState other)
    {
        return new ExplicitState(
            _facts.Intersection(
                ((ExplicitState)other)._facts
            ),
            _registry
        );
    }

    public bool IsEmpty()
    {
        return _facts.IsEmpty();
    }
}
