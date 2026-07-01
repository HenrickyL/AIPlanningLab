using AIPlanningLab.Domain.Collections;

namespace AIPlanningLab.Implementations.Collections;

/// <summary>
/// Implementação baseada em vetor de ulong.
/// </summary>
public sealed class BitSet : IBitSet
{
    private readonly ulong[] _bits;

    public BitSet(int capacity)
    {
        _bits =
            new ulong[
                (capacity + 63) / 64
            ];
    }

    private BitSet(ulong[] bits)
    {
        _bits = bits;
    }

    public void Add(int index)
    {
        _bits[index >> 6]
            |= 1UL << (index & 63);
    }

    public void Remove(int index)
    {
        _bits[index >> 6]
            &= ~(1UL << (index & 63));
    }

    public bool Contains(int index)
    {
        return
            (_bits[index >> 6]
             &
             (1UL << (index & 63)))
             != 0;
    }

    public IBitSet Union(IBitSet other)
    {
        return Apply(
            (a, b) => a | b,
            other
        );
    }

    public IBitSet Difference(IBitSet other)
    {
        return Apply(
            (a, b) => a & ~b,
            other
        );
    }

    public IBitSet Intersection(IBitSet other)
    {
        return Apply(
            (a, b) => a & b,
            other
        );
    }

    public bool IsSubsetOf(
        IBitSet other)
    {
        var s =
            (BitSet)other;

        for (int i = 0; i < _bits.Length; i++)
        {
            if ((_bits[i] & s._bits[i])
                != _bits[i])
                return false;
        }

        return true;
    }

    public bool IsEmpty()
    {
        return
            _bits.All(x => x == 0);
    }

    private BitSet Apply(
        Func<ulong, ulong, ulong> op,
        IBitSet other)
    {
        var s =
            (BitSet)other;

        ulong[] result =
            new ulong[
                _bits.Length
            ];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] =
                op(
                    _bits[i],
                    s._bits[i]
                );
        }

        return new BitSet(result);
    }
}