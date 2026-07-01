using AIPlanningLab.Domain.Models;

namespace AIPlanningLab.Implementations.Explicit;

public class ExplicitProblem : IPlanningProblem
{
    public IDomain Domain { get; }

    public IState InitialState { get; }

    public IState Goal { get; }

    public IState Constraints { get; }

    public ExplicitProblem(
        ExplicitDomain domain,
        ExplicitState initial,
        ExplicitState goal,
        ExplicitState constraints)
    {
        Domain = domain;
        InitialState = initial;
        Goal = goal;
        Constraints = constraints;
    }
}
