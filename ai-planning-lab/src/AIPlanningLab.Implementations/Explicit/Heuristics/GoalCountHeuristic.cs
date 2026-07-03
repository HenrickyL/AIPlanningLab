using AIPlanningLab.Application.Heuristics;
using AIPlanningLab.Domain.Models;

namespace AIPlanningLab.Implementations.Explicit.Heuristics;
/// <summary>
/// h(s) = número de proposições da meta que ainda não são verdadeiras em s.
/// </summary>
public sealed class GoalCountHeuristic : IHeuristic
{
    //public int Evaluate(IState state, IPlanningProblem problem)
    //{
    //    var explicitState = (ExplicitState)state;
    //    return explicitState.CountUnsatisfied(problem.Goal);
    //}

    public int Evaluate(IState state, IPlanningProblem problem)
    {
        var s = (ExplicitState)state;
        var goal = (ExplicitState)problem.Goal;
        return Count(s, goal);
    }

    private int Count(ExplicitState state, ExplicitState goal)
    {
        var missing = (ExplicitState)goal.Remove(state);
        int count = 0;
        for (int i = 0; i < state.Registry.Count; i++)
            if (missing.Facts.Contains(i))
                count++;
        return count;
    }
}
 