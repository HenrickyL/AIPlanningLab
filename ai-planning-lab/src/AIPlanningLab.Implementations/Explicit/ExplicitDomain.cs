using AIPlanningLab.Domain.Collections;
using AIPlanningLab.Domain.Models;

namespace AIPlanningLab.Implementations.Explicit;

public class ExplicitDomain : IDomain
{
    public IPropositionRegistry Propositions { get; }

    public IReadOnlyList<IAction> Actions { get; }

    public ExplicitDomain(
        IEnumerable<ExplicitAction> actions,
        IPropositionRegistry registry)
    {
        Actions =actions.ToList();

        Propositions = registry;
    }
}
