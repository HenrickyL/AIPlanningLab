using AIPlanningLab.Domain.Models;

namespace AIPlanningLab.Implementations.Explicit;

public class ExplicitAction : IAction
{
    public string Name { get; }
    public IState Preconditions { get; }
    public IState PositiveEffects { get; }
    public IState NegativeEffects { get; }
    public int Cost { get; }

    public ExplicitAction(
        string name,
        IState preconditions,
        IState positiveEffects,
        IState negativeEffects,
        int cost = 1)
    {
        Name = name;
        Preconditions = preconditions;
        PositiveEffects = positiveEffects;
        NegativeEffects = negativeEffects;
        Cost = cost;
    }

    public override string ToString() => Name;
}
