using AIPlanningLab.Domain.Models;

namespace AIPlanningLab.Implementations.Explicit;

public class ExplicitAction : IAction
{
    public string Name { get; }

    public IState Preconditions { get; }

    public IState PositiveEffects { get; }

    public IState NegativeEffects { get; }

    public ExplicitAction(
        string name,
        ExplicitState pre,
        ExplicitState add,
        ExplicitState del)
    {
        Name = name;

        Preconditions = pre;

        PositiveEffects = add;

        NegativeEffects = del;
    }
}
