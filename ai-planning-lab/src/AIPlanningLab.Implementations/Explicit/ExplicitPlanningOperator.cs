using AIPlanningLab.Domain.Models;
using AIPlanningLab.Domain.Services;

namespace AIPlanningLab.Implementations.Explicit;

public class ExplicitPlanningOperator : IPlanningOperator
{
    public bool CanProgress(IState state, IAction action)
        => state.Satisfies(action.Preconditions);

    public IState Progress(IState state, IAction action)
        => state.Remove(action.NegativeEffects).Merge(action.PositiveEffects);
    public bool CanRegress(IState target, IAction action)
    {
        bool relevant = !action.PositiveEffects.Restrict(target).IsEmpty();
        bool consistent = action.NegativeEffects.Restrict(target).IsEmpty();
        return relevant && consistent;
    }

    public IState Regress(IState target, IAction action)
        => target.Remove(action.PositiveEffects).Merge(action.Preconditions);

    public IState RelaxedProgress(IState state, IAction action)
        => state.Merge(action.PositiveEffects);

    public IState RelaxedRegress(IState state, IAction action)
        => state.Merge(action.Preconditions);

    public bool CanRelaxedRegress(IState target, IAction action)
    {
        bool relevant = !action.PositiveEffects.Restrict(target).IsEmpty();
        return relevant;
    }
}
