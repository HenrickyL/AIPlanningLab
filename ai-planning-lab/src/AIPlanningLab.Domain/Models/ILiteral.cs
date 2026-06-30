namespace AIPlanningLab.Domain.Models;

public interface ILiteral
{
    IProposition Predicate { get; }
    bool IsNegated { get; }
}
