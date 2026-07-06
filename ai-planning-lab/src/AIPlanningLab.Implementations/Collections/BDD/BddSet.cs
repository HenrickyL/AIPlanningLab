using DecisionDiagrams;

namespace AIPlanningLab.Implementations.Collections.BDD;

public sealed class BddSet : IBddSet
{
    public DD Formula { get; }

    public BddSet(DD formula)
    {
        Formula = formula;
    }
}