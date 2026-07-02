using AIPlanningLab.Domain.Models;

namespace AIPlanningLab.Implementations.Explicit;

public class ExplicitPlanningProblem : IPlanningProblem
{
    public IDomain Domain { get; }
    public IState InitialState { get; }
    public IState Goal { get; }
    public IState Constraints { get; }

    public ExplicitPlanningProblem(
        IDomain domain,
        IState initialState,
        IState goal,
        IState constraints)
    {
        Domain = domain;
        InitialState = initialState;
        Goal = goal;
        Constraints = constraints;
    }
}
