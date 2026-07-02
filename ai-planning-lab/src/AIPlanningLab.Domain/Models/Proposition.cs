namespace AIPlanningLab.Domain.Models;

public class Proposition : IProposition, IEquatable<Proposition>
{
    public string Name { get; }
    public int Index { get; }

    internal Proposition(string name, int index)
    {
        Name = name;
        Index = index;
    }

    // Igualdade baseada em índice — O(1), sem comparação de strings.
    public bool Equals(Proposition? other) => other is not null && Index == other.Index;
    public override bool Equals(object? obj) => obj is Proposition p && Equals(p);
    public override int GetHashCode() => Index;
    public override string ToString() => Name;
}
