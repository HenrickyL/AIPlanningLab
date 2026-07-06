using DecisionDiagrams;

namespace AIPlanningLab.Implementations.Collections.BDD;
/// <summary>
/// Encapsula o gerenciador da biblioteca.
/// </summary>
internal class BddManager : IBddManager
{
    private readonly DDManager<BDDNode> _manager;

    public BddManager()
    {
        _manager = new DDManager<BDDNode>();
    }

    public IBddSet True()
        => new BddSet(_manager.True());

    public IBddSet False()
        => new BddSet(_manager.False());

    public IBddSet CreateVariable()
    {
        VarBool<BDDNode> v = _manager.CreateBool();
        DD dd = _manager.Id(v);
        return new BddSet(dd);
    }

    public IBddSet And(
        IBddSet left,
        IBddSet right)
        => new BddSet(
            _manager.And(
                left.Formula,
                right.Formula));

    public IBddSet Or(
        IBddSet left,
        IBddSet right)
        => new BddSet(
            _manager.Or(
                left.Formula,
                right.Formula));

    public IBddSet Not(
        IBddSet value)
        => new BddSet(
            _manager.Not(
                value.Formula));

    public IBddSet Exists(
        IBddSet formula,
        IEnumerable<int> variables)
    {
        throw new NotImplementedException();
    }

    public bool IsEmpty(
        IBddSet formula)
        => formula.Formula.IsFalse();

    public bool Equals(
        IBddSet left,
        IBddSet right)
        => left.Formula.Equals(right.Formula);
}