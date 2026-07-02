using AIPlanningLab.Domain.Models;
using AIPlanningLab.Domain.Registry;

namespace AIPlanningLab.Implementations.Explicit;

public class ExplicitDomain : IDomain
{
    public IPropositionRegistry Propositions { get; }
    public IReadOnlyList<IAction> Actions { get; }

    public ExplicitDomain(IPropositionRegistry propositions, IReadOnlyList<IAction> actions)
    {
        Propositions = propositions;
        Actions = actions;
    }
}
