using AIPlanningLab.Domain.Models;
using AIPlanningLab.Implementations.Collections.BDD;

namespace AIPlanningLab.Implementations.Symbolic;

/// <summary>
/// Estado representado simbolicamente por um BDD.
/// </summary>
public class SymbolicState : IState
{
    private readonly IBddManager _manager;

    public IBddSet Formula { get; }

    public SymbolicState(
        IBddManager manager,
        IBddSet formula)
    {
        _manager = manager;
        Formula = formula;
    }

    /// <inheritdoc/>
    public bool Satisfies(IState condition)
    {
        var other = (SymbolicState)condition;

        var intersection =
            _manager.And(
                Formula,
                other.Formula);

        return _manager.Equals(
            intersection,
            other.Formula);
    }

    /// <inheritdoc/>
    public IState Merge(IState other)
    {
        var state = (SymbolicState)other;

        return new SymbolicState(
            _manager,
            _manager.Or(
                Formula,
                state.Formula));
    }

    /// <inheritdoc/>
    public IState Restrict(IState other)
    {
        var state = (SymbolicState)other;

        return new SymbolicState(
            _manager,
            _manager.And(
                Formula,
                state.Formula));
    }

    /// <inheritdoc/>
    public IState Remove(IState other)
    {
        var state = (SymbolicState)other;

        return new SymbolicState(
            _manager,
            _manager.And(
                Formula,
                _manager.Not(
                    state.Formula)));
    }

    /// <inheritdoc/>
    public bool IsEmpty()
        => _manager.IsEmpty(Formula);

    public override bool Equals(object? obj)
    {
        if (obj is not SymbolicState other)
            return false;

        return _manager.Equals(
            Formula,
            other.Formula);
    }

    public override int GetHashCode()
        => Formula.GetHashCode();
}