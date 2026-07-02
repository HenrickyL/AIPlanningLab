using AIPlanningLab.Domain.Models;

namespace AIPlanningLab.Domain.Services;

public sealed class PlanningOperator : IPlanningOperator
{
    public bool CanApply(IState state, IAction action)
        => state.Satisfies(action.Preconditions);

    public IState Progress(IState state, IAction action)
        => state.Remove(action.NegativeEffects).Merge(action.PositiveEffects);

    public IState Regress(IState target, IAction action)
        => target.Remove(action.PositiveEffects).Merge(action.Preconditions);

    public IState RelaxedProgress(IState state, IAction action)
        => state.Merge(action.PositiveEffects);

    public IState RelaxedRegress(IState state, IAction action)
        => state.Merge(action.Preconditions);
}
