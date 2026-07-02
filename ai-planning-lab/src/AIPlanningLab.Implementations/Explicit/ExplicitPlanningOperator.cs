using AIPlanningLab.Domain.Models;
using AIPlanningLab.Domain.Services;

namespace AIPlanningLab.Implementations.Explicit;

public class ExplicitPlanningOperator : IPlanningOperator
{
    public bool CanApply(IState state, IAction action)
    {
        return
            state.Satisfies(action.Preconditions);
    }

    public IState Progress(
        IState state,
        IAction action)
    {
        if (!CanApply(state, action))
        {
            return state;
        }

        return state
            .Remove(action.NegativeEffects)
            .Merge(action.PositiveEffects);
    }

    public IState Regress(IState goal, IAction action)
    {
        return goal
            .Remove(action.PositiveEffects)
            .Merge(action.Preconditions);
    }

    public IState RelaxedProgress(IState state, IAction action)
    {
        throw new NotImplementedException();
    }

    public IState RelaxedRegress(IState state, IAction action)
    {
        throw new NotImplementedException();
    }
}
